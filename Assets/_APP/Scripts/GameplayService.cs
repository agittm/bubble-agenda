using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

public class GameplayService : IInitializable, IStartable, ITickable, IDisposable
{
    private readonly GameplayView view;
    private readonly GameplaySettings settings;
    private readonly StereotypeDatabase stereotypeDatabase;

    private BubbleItemUI[] spawnedBubbles;
    private int[] sideCount = new int[3];
    private int currentLevelIndex = 0;
    private GameLevelData currentLevel => settings.LevelDatas[currentLevelIndex];

    public GameplayService(GameplayView view, GameplaySettings settings, StereotypeDatabase stereotypeDatabase)
    {
        this.view = view;
        this.settings = settings;
        this.stereotypeDatabase = stereotypeDatabase;

        spawnedBubbles = new BubbleItemUI[settings.GridSize.x * settings.GridSize.y];
    }

    void IInitializable.Initialize()
    {
        GameEvents.OnGameOver += HandleOnGameOver;
        GameEvents.OnFinishLevel += HandleOnFinishLevel;

        view.sliderMorality.onValueChanged.AddListener(HandleOnSliderMoralityValueChanged);
        view.gameOverUI.Init(HandleOnRestartGame);
        view.levelCompleteUI.Init(HandleOnNextLevel);
    }

    void IStartable.Start()
    {
        currentLevelIndex = 0;
        StartGame();
    }

    void ITickable.Tick()
    {

    }

    void IDisposable.Dispose()
    {
        GameEvents.OnGameOver -= HandleOnGameOver;
        GameEvents.OnFinishLevel -= HandleOnFinishLevel;

        view.sliderMorality.onValueChanged.RemoveListener(HandleOnSliderMoralityValueChanged);
    }

    private void StartGame()
    {
        view.gameOverUI.gameObject.SetActive(false);
        view.levelCompleteUI.gameObject.SetActive(false);
        view.balancingBarUI.gameObject.SetActive(currentLevel.CurrentSideCount > 1);

        view.sliderMorality.minValue = 0;
        view.sliderMorality.maxValue = settings.MaxMoral;
        view.sliderMorality.value = settings.InitialMoral;

        SpawnBubbles(settings.GridSize.x * settings.GridSize.y);
    }

    private void SpawnBubbles(int count)
    {
        ClearBubbles();

        for (int i = 0; i < count; i++)
        {
            BubbleItemUI ui = SpawnBubble();
            ui.SetIndex(i);
            spawnedBubbles[i] = ui;
        }
    }

    private BubbleItemUI SpawnBubble(OpinionType opinionType)
    {
        // Randomize Side
        Side side = (Side)UnityEngine.Random.Range(0, currentLevel.CurrentSideCount);
        //while (sideCount[(int)side] >= currentMinMaxSide.y)
        //{
        //    side = (Side)UnityEngine.Random.Range(0, 3);
        //}

        sideCount[(int)side]++;

        // Randomize Stereotype
        Stereotype stereotype = stereotypeDatabase.GetRandom(currentLevel.CurrentStereotypeCount);

        // Randomize Opinion
        float timelimit = 0;
        switch (opinionType)
        {
            case OpinionType.Branding:
                timelimit = settings.DefaultBrandingTime;
                break;
            case OpinionType.Hoax:
                timelimit = settings.DefaultHoaxTime;
                break;
            case OpinionType.Notes:
            case OpinionType.HateSpeech:
                timelimit = settings.DefaultSmileyTime;
                break;
        }
        timelimit *= stereotype.TimeMultiplier;
        Opinion opinion = new Opinion(opinionType, timelimit);

        // Build
        BubbleData data = new BubbleData(side, opinion, stereotype);
        BubbleItemUI ui = UnityEngine.Object.Instantiate(view.prefabBubble, view.parentBubble);
        ui.Init(HandleOnClickBubble, HandleOnBubbleTimeout);
        ui.SetData(data);

        GameEvents.OnSpawnBubble?.Invoke(ui);
        return ui;
    }

    private BubbleItemUI SpawnBubble()
    {
        return SpawnBubble((OpinionType)UnityEngine.Random.Range(0, currentLevel.OpinionTypeCount));
    }

    private void ClearBubbles()
    {
        for (int i = 0; i < spawnedBubbles.Length; i++)
        {
            if (spawnedBubbles[i] != null)
            {
                UnityEngine.Object.Destroy(spawnedBubbles[i].gameObject);
            }
        }

        spawnedBubbles = new BubbleItemUI[settings.GridSize.x * settings.GridSize.y];
    }

    private void ClickBubble(BubbleItemUI ui)
    {
        if (ui.Data.Side == Side.Left)
        {
            if (ui.Data.Opinion.Type == OpinionType.Branding)
            {
                view.balancingBarUI.AddLeftValue(-settings.CostBalance);
                view.sliderMorality.value--;
            }
            else if (ui.Data.Opinion.Type == OpinionType.Hoax)
            {
                view.balancingBarUI.AddLeftValue(settings.CostBalance);
                view.sliderMorality.value++;
            }
        }
        else if (ui.Data.Side == Side.Right)
        {
            if (ui.Data.Opinion.Type == OpinionType.Branding)
            {
                view.balancingBarUI.AddRightValue(-settings.CostBalance);
                view.sliderMorality.value--;
            }
            else if (ui.Data.Opinion.Type == OpinionType.Hoax)
            {
                view.balancingBarUI.AddRightValue(settings.CostBalance);
                view.sliderMorality.value++;
            }
        }
        else if (ui.Data.Side == Side.Neutral)
        {
            if (ui.Data.Opinion.Type == OpinionType.Branding)
            {
                view.sliderMorality.value--;
            }
            else if (ui.Data.Opinion.Type == OpinionType.Hoax)
            {
                view.sliderMorality.value++;
            }
        }

        if (view.sliderMorality.value == 0)
        {
            GameEvents.OnGameOver?.Invoke(GameOverReason.Immoral);
        }
        else if (view.balancingBarUI.LeftValue == 0 || view.balancingBarUI.RightValue == 0)
        {
            GameEvents.OnGameOver?.Invoke(GameOverReason.Imbalance);
        }
        else if (view.sliderMorality.value == view.sliderMorality.maxValue &&
            view.balancingBarUI.LeftValue==view.balancingBarUI.RightValue)
        {
            GameEvents.OnFinishLevel?.Invoke();
        }
        else
        {
            GameEvents.OnSelectBubble?.Invoke(ui);
            RespawnBubble(ui);
        }
    }

    private void SpreadBubble(BubbleItemUI ui)
    {
        float chance = 0f;

        for (int i = 0; i < spawnedBubbles.Length; i++)
        {
            int index = ui.Index;
            int power = GetPower(ui);

            if (i == index - 1 || i == index + 1 || i == index + settings.GridSize.y || i == index - settings.GridSize.y)
            {
                chance += 0.25f;

                if (ui.Data.Stereotype == spawnedBubbles[i].Data.Stereotype &&
                    ui.Data.Opinion.Type == spawnedBubbles[i].Data.Opinion.Type)
                    continue;

                if (chance >= UnityEngine.Random.value && power >= spawnedBubbles[i].Data.Stereotype.Power)
                {
                    spawnedBubbles[i].SetData(ui.Data);
                    sideCount[(int)ui.Data.Side]++;
                    break;
                }
            }
        }

        ui.ResetData();
    }

    private void RemoveBubble(BubbleItemUI ui)
    {
        sideCount[(int)ui.Data.Side]--;

        for (int i = 0; i < spawnedBubbles.Length; i++)
        {
            if (spawnedBubbles[i] == ui)
            {
                spawnedBubbles[i] = null;
            }
        }

        UnityEngine.Object.Destroy(ui.gameObject);
    }

    private void RespawnBubble(BubbleItemUI ui)
    {
        int index = ui.Index;
        RemoveBubble(ui);

        OpinionType opinionType = (OpinionType)UnityEngine.Random.Range(0, currentLevel.OpinionTypeCount);
        while (opinionType == ui.Data.Opinion.Type)
        {
            opinionType = (OpinionType)UnityEngine.Random.Range(0, currentLevel.OpinionTypeCount);
        }
        BubbleItemUI newUI = SpawnBubble(opinionType);
        newUI.SetIndex(index);
        spawnedBubbles[index] = newUI;
    }

    private int GetPower(BubbleItemUI ui)
    {
        int basePower = ui.Data.Stereotype.Power;
        int result = basePower;
        int index = ui.Index;

        if (index - 1 >= 0 && spawnedBubbles[index - 1].Data.Stereotype == ui.Data.Stereotype)
        {
            result += basePower;
        }
        if (index + 1 < spawnedBubbles.Length && spawnedBubbles[index + 1].Data.Stereotype == ui.Data.Stereotype)
        {
            result += basePower;
        }
        if (index - settings.GridSize.y >= 0 && spawnedBubbles[index - settings.GridSize.y].Data.Stereotype == ui.Data.Stereotype)
        {
            result += basePower;
        }
        if (index + settings.GridSize.y < spawnedBubbles.Length && spawnedBubbles[index + settings.GridSize.y].Data.Stereotype == ui.Data.Stereotype)
        {
            result += basePower;
        }

        return result;
    }

    private void HandleOnClickBubble(BubbleItemUI ui)
    {
        ClickBubble(ui);
    }

    private void HandleOnBubbleTimeout(BubbleItemUI ui)
    {
        if (ui.Data.Opinion.Type == OpinionType.Hoax)
        {
            SpreadBubble(ui);
        }
        else
        {
            RespawnBubble(ui);
        }
    }

    private void HandleOnSliderMoralityValueChanged(float value)
    {
        view.bgMorality.SetInteger("Moral", Mathf.RoundToInt(value));
    }

    private void HandleOnGameOver(GameOverReason reason)
    {
        view.gameOverUI.gameObject.SetActive(true);
    }

    private void HandleOnFinishLevel()
    {
        view.levelCompleteUI.gameObject.SetActive(true);
    }

    private void HandleOnRestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void HandleOnNextLevel()
    {
        currentLevelIndex = Mathf.Clamp(currentLevelIndex + 1, 0, settings.LevelDatas.Length - 1);
        StartGame();
    }
}

public enum Side
{
    Neutral,
    Left,
    Right,
}

public enum OpinionType
{
    Branding,
    Hoax,
    Notes,
    HateSpeech,
}

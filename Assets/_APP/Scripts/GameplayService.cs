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

    private int currentColoredOpinionCount = 0;
    private Vector2Int currentMinMaxColoredOpinion = new Vector2Int(2, 3);

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

        view.gameOverUI.Init(HandleOnRestartGame);
    }

    void IStartable.Start()
    {
        view.gameOverUI.gameObject.SetActive(false);

        view.sliderMorality.minValue = 0;
        view.sliderMorality.maxValue = settings.MaxMoral;
        view.sliderMorality.value = settings.InitialMoral;

        SpawnBubbles(settings.GridSize.x * settings.GridSize.y);
    }

    void ITickable.Tick()
    {

    }

    void IDisposable.Dispose()
    {
        GameEvents.OnGameOver -= HandleOnGameOver;
    }

    private void HandleOnGameOver(GameOverReason reason)
    {
        view.gameOverUI.gameObject.SetActive(true);
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

    private BubbleItemUI SpawnBubble()
    {
        Side side = Side.Neutral;
        bool isNeutral = UnityEngine.Random.value >= 0.5f;
        if (currentColoredOpinionCount < currentMinMaxColoredOpinion.x || (!isNeutral && currentColoredOpinionCount < currentMinMaxColoredOpinion.y))
        {
            side = UnityEngine.Random.value >= 0.5f ? Side.Left : Side.Right;
            currentColoredOpinionCount++;
        }

        OpinionType opinion = UnityEngine.Random.value >= 0.5f ? OpinionType.Branding : OpinionType.Hoax;
        Stereotype stereotype = stereotypeDatabase.GetRandom();
        BubbleData data = new BubbleData(side, opinion, stereotype);

        BubbleItemUI ui = UnityEngine.Object.Instantiate(view.prefabBubble, view.parentBubble);
        ui.Init(HandleOnClickBubble, HandleOnBubbleTimeout);
        ui.SetData(data);

        GameEvents.OnSpawnBubble?.Invoke(ui);
        return ui;
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
            if (ui.Data.OpinionType == OpinionType.Branding)
            {
                view.balancingBarUI.AddLeftValue(-settings.CostBalance);
                view.sliderMorality.value--;
            }
            else if (ui.Data.OpinionType == OpinionType.Hoax)
            {
                view.balancingBarUI.AddLeftValue(settings.CostBalance);
                view.sliderMorality.value++;
            }
        }
        else if (ui.Data.Side == Side.Right)
        {
            if (ui.Data.OpinionType == OpinionType.Branding)
            {
                view.balancingBarUI.AddRightValue(-settings.CostBalance);
                view.sliderMorality.value--;
            }
            else if (ui.Data.OpinionType == OpinionType.Hoax)
            {
                view.balancingBarUI.AddRightValue(settings.CostBalance);
                view.sliderMorality.value++;
            }
        }
        else if (ui.Data.Side == Side.Neutral)
        {
            if (ui.Data.OpinionType == OpinionType.Branding)
            {
                view.sliderMorality.value--;
            }
            else if (ui.Data.OpinionType == OpinionType.Hoax)
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
        else
        {
            GameEvents.OnSelectBubble?.Invoke(ui);
            RespawnBubble(ui);
        }
    }

    private void SpreadBubble(BubbleItemUI ui)
    {
        for (int i = 0; i < spawnedBubbles.Length; i++)
        {
            int index = ui.Index;
            int power = GetPower(ui);

            if (i == index - 1 || i == index + 1 || i == index + settings.GridSize.y || i == index - settings.GridSize.y)
            {
                if (ui.Data.Stereotype == spawnedBubbles[i].Data.Stereotype)
                    continue;

                if (power >= spawnedBubbles[i].Data.Stereotype.Power)
                {
                    spawnedBubbles[i].SetData(ui.Data);
                    currentColoredOpinionCount++;
                }
            }
        }
    }

    private void RemoveBubble(BubbleItemUI ui)
    {
        if (ui.Data.Side != Side.Neutral)
        {
            currentColoredOpinionCount--;
        }

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

        BubbleItemUI newUI = SpawnBubble();
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
        if (ui.Data.Side != Side.Neutral)
        {
            SpreadBubble(ui);
        }

        RespawnBubble(ui);
    }

    private void HandleOnRestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BubbleItemUI : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected Image bubble;
    [SerializeField] protected Image icon;
    [SerializeField] protected Button button;
    [SerializeField] protected GameObject panelHoaxTime;
    [SerializeField] protected Image bubbleTimeImage;

    [Header("Settings")]
    [SerializeField] protected Sprite[] iconByTypes;
    [SerializeField] protected Color[] colorByTypes;

    private BubbleData data;
    public BubbleData Data => data;

    private int index;
    public int Index => index;

    private bool isRunning;
    private float bubbleTime;
    private float bubbleDuration;

    private System.Action<BubbleItemUI> OnClick;
    private System.Action<BubbleItemUI> OnTimeOut;

    private void Awake()
    {
        button.onClick.AddListener(HandleOnClick);
        GameEvents.OnCleanFromHoax += HandleOnCleanFromHoax;
        GameEvents.OnGameOver += HandleOnGameOver;
        GameEvents.OnFinishLevel += HandleOnFinishLevel;
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(HandleOnClick);
        GameEvents.OnCleanFromHoax -= HandleOnCleanFromHoax;
        GameEvents.OnGameOver -= HandleOnGameOver;
        GameEvents.OnFinishLevel -= HandleOnFinishLevel;
    }

    private void Update()
    {
        if (data == null)
            return;

        if (!isRunning)
            return;

        bubbleTime -= Time.deltaTime;
        bubbleTimeImage.fillAmount = bubbleTime / bubbleDuration;

        if (bubbleTime <= 0)
        {
            isRunning = false;
            animator.SetTrigger("Timeout");
            OnTimeOut?.Invoke(this);
        }
    }

    public void Init(System.Action<BubbleItemUI> OnClick, System.Action<BubbleItemUI> OnTimeOut)
    {
        this.OnClick = OnClick;
        this.OnTimeOut = OnTimeOut;
    }

    public void SetData(BubbleData data)
    {
        if (this.data != null)
        {
            animator.SetTrigger("Timeout");
        }

        this.data = data;
        ResetData();
    }

    public void SetIndex(int index)
    {
        this.index = index;
        transform.SetSiblingIndex(index);
    }

    public void ResetData()
    {
        bubbleTime = data.Opinion.Timelimit;
        bubbleDuration = data.Opinion.Timelimit;
        isRunning = bubbleTime > 0;
        panelHoaxTime.SetActive(isRunning);

        SetupUI();
    }

    private void SetupUI()
    {
        bubble.sprite = data.Stereotype.BubbleIcon;
        icon.color = colorByTypes[(int)data.Side];
        icon.sprite = iconByTypes[(int)data.Opinion.Type];
    }

    private IEnumerator ClickProcess(float delay)
    {
        yield return new WaitForSeconds(delay);
        OnClick?.Invoke(this);
    }

    private void HandleOnClick()
    {
        animator.SetTrigger("Tap");
        StartCoroutine(ClickProcess(0.5f));
    }

    private void HandleOnCleanFromHoax()
    {
        animator.SetTrigger("Tap");
    }

    private void HandleOnGameOver(GameOverReason reason)
    {
        isRunning = false;
    }

    private void HandleOnFinishLevel()
    {
        isRunning = false;
    }
}
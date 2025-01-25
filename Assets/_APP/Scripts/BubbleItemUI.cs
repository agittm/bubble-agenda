using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BubbleItemUI : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected Image bubble;
    [SerializeField] protected Image icon;
    [SerializeField] protected Button button;
    [SerializeField] protected Slider sliderTime;

    [Header("Settings")]
    [SerializeField] protected Sprite[] iconByTypes;
    [SerializeField] protected Color[] colorByTypes;

    private BubbleData data;
    public BubbleData Data => data;

    private int index;
    public int Index => index;

    private bool isRunning;

    private System.Action<BubbleItemUI> OnClick;
    private System.Action<BubbleItemUI> OnTimeOut;

    private void Awake()
    {
        button.onClick.AddListener(HandleOnClick);
        GameEvents.OnGameOver += HandleOnGameOver;
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(HandleOnClick);
        GameEvents.OnGameOver -= HandleOnGameOver;
    }

    private void Update()
    {
        if (data == null)
            return;

        if (!isRunning)
            return;

        sliderTime.value -= Time.deltaTime;

        if (sliderTime.value == 0)
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
        this.data = data;

        sliderTime.minValue = 0;
        sliderTime.maxValue = this.data.Stereotype.Timelimit;
        sliderTime.value = this.data.Stereotype.Timelimit;
        isRunning = true;

        SetupUI();
    }

    public void SetIndex(int index)
    {
        this.index = index;
        transform.SetSiblingIndex(index);
    }

    private void SetupUI()
    {
        bubble.sprite = data.Stereotype.BubbleIcon;
        icon.color = colorByTypes[(int)data.Side];
        icon.sprite = iconByTypes[(int)data.OpinionType];
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

    private void HandleOnGameOver(GameOverReason reason)
    {
        isRunning = false;
    }
}
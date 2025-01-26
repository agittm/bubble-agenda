using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Button buttonNextLevel;
    [SerializeField] private int minLevelForChangeImage;
    [SerializeField] private Sprite iconBasic;
    [SerializeField] private Sprite iconAdvanced;

    private int currentLevel;
    private System.Action OnNextLevel;

    private void Awake()
    {
        buttonNextLevel.onClick.AddListener(HandleOnClickNextLevel);
    }

    private void Start()
    {
        currentLevel = 0;
    }

    private void OnEnable()
    {
        if (currentLevel < minLevelForChangeImage)
            icon.sprite = iconBasic;
        else
            icon.sprite = iconAdvanced;
    }

    private void OnDestroy()
    {
        buttonNextLevel.onClick.RemoveListener(HandleOnClickNextLevel);
    }

    public void Init(System.Action OnRestart)
    {
        this.OnNextLevel = OnRestart;
    }

    private void HandleOnClickNextLevel()
    {
        currentLevel++;
        OnNextLevel?.Invoke();
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text textReason;
    [SerializeField] private Button buttonRestart;
    [SerializeField] private Button buttonMainmenu;

    [Header("Settings")]
    [SerializeField] protected Sprite[] iconByTypes;

    private System.Action OnRestart;
    private System.Action OnMainmenu;

    private void Awake()
    {
        buttonRestart.onClick.AddListener(HandleOnClickRestart);
        buttonMainmenu.onClick.AddListener(HandleOnClickMainmenu);
        GameEvents.OnGameOver += HandleOnGameOver;
    }

    private void OnDestroy()
    {
        buttonRestart.onClick.RemoveListener(HandleOnClickRestart);
        buttonMainmenu.onClick.RemoveListener(HandleOnClickMainmenu);
        GameEvents.OnGameOver -= HandleOnGameOver;
    }

    public void Init(System.Action OnRestart, System.Action OnMainmenu)
    {
        this.OnRestart = OnRestart;
        this.OnMainmenu = OnMainmenu;
    }

    private void HandleOnClickRestart()
    {
        OnRestart?.Invoke();
    }

    private void HandleOnClickMainmenu()
    {
        OnMainmenu?.Invoke();
    }

    private void HandleOnGameOver(GameOverReason reason)
    {
        switch (reason)
        {
            case GameOverReason.Immoral:
                textReason.text = "You're running out of moral point";
                break;
            case GameOverReason.Imbalance:
                textReason.text = "You let one of side in dominating";
                break;
        }
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Button buttonResume;
    [SerializeField] private Button buttonMainmenu;

    private System.Action OnResume;
    private System.Action OnMainmenu;

    private void Awake()
    {
        buttonResume.onClick.AddListener(HandleOnClickRestart);
        buttonMainmenu.onClick.AddListener(HandleOnClickMainmenu);
    }

    private void OnDestroy()
    {
        buttonResume.onClick.RemoveListener(HandleOnClickRestart);
        buttonMainmenu.onClick.RemoveListener(HandleOnClickMainmenu);
    }

    public void Init(System.Action OnRestart, System.Action OnMainmenu)
    {
        this.OnResume = OnRestart;
        this.OnMainmenu = OnMainmenu;
    }

    private void HandleOnClickRestart()
    {
        OnResume?.Invoke();
    }

    private void HandleOnClickMainmenu()
    {
        OnMainmenu?.Invoke();
    }
}

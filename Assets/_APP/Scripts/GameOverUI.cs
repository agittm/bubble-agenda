using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button buttonRestart;

    private System.Action OnRestart;

    private void Awake()
    {
        buttonRestart.onClick.AddListener(HandleOnClickRestart);
    }

    private void OnDestroy()
    {
        buttonRestart.onClick.RemoveListener(HandleOnClickRestart);
    }

    public void Init(System.Action OnRestart)
    {
        this.OnRestart = OnRestart;
    }

    private void HandleOnClickRestart()
    {
        OnRestart?.Invoke();
    }
}

using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteUI : MonoBehaviour
{
    [SerializeField] private Button buttonNextLevel;

    private System.Action OnNextLevel;

    private void Awake()
    {
        buttonNextLevel.onClick.AddListener(HandleOnClickNextLevel);
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
        OnNextLevel?.Invoke();
    }
}

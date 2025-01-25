using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity.Example;

public class GameplayView : MonoBehaviour
{
    public Animator bgMorality;
    public Slider sliderMorality;
    public GridLayoutGroup parentBubble;
    public BubbleItemUI prefabBubble;
    public BalancingBarUI balancingBarUI;
    public TMP_Text textLevel;
    public Button buttonPause;
    public PauseUI pauseUI;
    public GameOverUI gameOverUI;
    public LevelCompleteUI levelCompleteUI;
    public VNManager vnManager;
}

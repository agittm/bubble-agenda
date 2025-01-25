using UnityEngine;

public class BalancingBarUI : MonoBehaviour
{
    [SerializeField] private RectTransform left;
    [SerializeField] private RectTransform right;

    private int leftValue = 50;
    public int LeftValue => leftValue;

    private int rightValue = 50;
    public int RightValue => rightValue;

    private void Start()
    {
        UpdateUI();
    }

    public void AddLeftValue(int value)
    {
        SetLeftValue(leftValue + value);
    }

    public void SetLeftValue(int value)
    {
        if (!gameObject.activeInHierarchy)
            return;

        leftValue = value;
        rightValue = 100 - value;
        UpdateUI();
    }

    public void AddRightValue(int value)
    {
        SetRightValue(rightValue + value);
    }

    public void SetRightValue(int value)
    {
        if (!gameObject.activeInHierarchy)
            return;

        rightValue = value;
        leftValue = 100 - value;
        UpdateUI();
    }

    private void UpdateUI()
    {
        left.anchorMax = new Vector2(leftValue / 100f, 1f);
        right.anchorMin = new Vector2(left.anchorMax.x, 0f);
    }
}

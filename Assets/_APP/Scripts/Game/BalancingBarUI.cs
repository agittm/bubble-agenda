using UnityEngine;

public class BalancingBarUI : MonoBehaviour
{
    [SerializeField] private RectTransform left;
    [SerializeField] private RectTransform right;

    private float leftValue = 50;
    public float LeftValue => leftValue;

    private float rightValue = 50;
    public float RightValue => rightValue;

    private void Start()
    {
        UpdateUI();
    }

    public void AddLeftValue(float value)
    {
        SetLeftValue(leftValue + value);
    }

    public void SetLeftValue(float value)
    {
        if (!gameObject.activeInHierarchy)
            return;

        leftValue = value;
        rightValue = 100 - value;
        UpdateUI();
    }

    public void AddRightValue(float value)
    {
        SetRightValue(rightValue + value);
    }

    public void SetRightValue(float value)
    {
        if (!gameObject.activeInHierarchy)
            return;

        rightValue = value;
        leftValue = 100 - value;
        UpdateUI();
    }

    private void UpdateUI()
    {
        LeanTween.value(left.anchorMax.x, leftValue / 100f, 0.25f).setOnUpdate((float value) =>
        {
            left.anchorMax = new Vector2(value, 1f);
        });
        LeanTween.value(right.anchorMin.x, 1f - rightValue / 100f, 0.25f).setOnUpdate((float value) =>
        {
            right.anchorMin = new Vector2(value, 0f);
        });
    }
}

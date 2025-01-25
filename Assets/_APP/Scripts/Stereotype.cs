using UnityEngine;

[CreateAssetMenu]
public class Stereotype : ScriptableObject
{
    [SerializeField] protected string stereotypeName;
    public string StereotypeName => stereotypeName;

    [SerializeField] protected Sprite bubbleIcon;
    public Sprite BubbleIcon => bubbleIcon;

    [SerializeField] protected float minTimelimit;
    [SerializeField] protected float maxTimelimit;
    public float Timelimit => Random.Range(minTimelimit, maxTimelimit);

    [SerializeField] protected int power;
    public int Power => power;
}
using UnityEngine;

[CreateAssetMenu]
public class Stereotype : ScriptableObject
{
    [SerializeField] protected string stereotypeName;
    public string StereotypeName => stereotypeName;

    [SerializeField] protected Sprite bubbleIcon;
    public Sprite BubbleIcon => bubbleIcon;

    [SerializeField] protected int power;
    public int Power => power;

    [SerializeField] protected float timeMultiplier;
    public float TimeMultiplier => timeMultiplier;
}
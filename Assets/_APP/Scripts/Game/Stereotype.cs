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

    [Header("Audios")]
    [SerializeField] protected AudioClip appearClip;
    public AudioClip AppearClip => appearClip;

    [SerializeField] protected AudioClip clickedClip;
    public AudioClip ClickedClip => clickedClip;

    [SerializeField] protected AudioClip timeoutClip;
    public AudioClip TimeoutClip => timeoutClip;
}
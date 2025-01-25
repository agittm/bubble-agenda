using UnityEngine;

[CreateAssetMenu]
public class GameplaySettings : ScriptableObject
{
    [SerializeField] private float defaultHoaxTime;
    public float DefaultHoaxTime => defaultHoaxTime;

    [SerializeField] private float defaultBrandingTime;
    public float DefaultBrandingTime => defaultBrandingTime;

    [SerializeField] private float defaultSmileyTime;
    public float DefaultSmileyTime => defaultSmileyTime;

    [SerializeField] private float costBalance = 10;
    public float CostBalance => costBalance;

    [SerializeField] private int costMorality = 1;
    public int CostMorality => costMorality;

    [SerializeField] private int initialMoral = 5;
    public int InitialMoral => initialMoral;

    [SerializeField] private int maxMoral = 10;
    public int MaxMoral => maxMoral;

    [SerializeField] private Vector2 grid2CountToSize;
    public Vector2 Grid2CountToSize => grid2CountToSize;

    [SerializeField] private Vector2 grid3CountToSize;
    public Vector2 Grid3CountToSize => grid3CountToSize;

    [SerializeField] private GameLevelData[] levelDatas;
    public GameLevelData[] LevelDatas => levelDatas;
}

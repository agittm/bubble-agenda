using UnityEngine;

[CreateAssetMenu]
public class GameplaySettings : ScriptableObject
{
    [SerializeField] private Vector2Int gridSize;
    public Vector2Int GridSize => gridSize;

    [SerializeField] private float defaultHoaxTime;
    public float DefaultHoaxTime => defaultHoaxTime;

    [SerializeField] private float defaultBrandingTime;
    public float DefaultBrandingTime => defaultBrandingTime;

    [SerializeField] private float defaultSmileyTime;
    public float DefaultSmileyTime => defaultSmileyTime;

    [SerializeField] private int costBalance = 10;
    public int CostBalance => costBalance;

    [SerializeField] private int costMorality = 1;
    public int CostMorality => costMorality;

    [SerializeField] private int initialMoral = 5;
    public int InitialMoral => initialMoral;

    [SerializeField] private int maxMoral = 10;
    public int MaxMoral => maxMoral;

    [SerializeField] private GameLevelData[] levelDatas;
    public GameLevelData[] LevelDatas => levelDatas;
}

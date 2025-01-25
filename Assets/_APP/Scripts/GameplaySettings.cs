using UnityEngine;

[CreateAssetMenu]
public class GameplaySettings : ScriptableObject
{
    [SerializeField] private Vector2Int gridSize;
    public Vector2Int GridSize => gridSize;

    [SerializeField] private int costBalance = 10;
    public int CostBalance => costBalance;

    [SerializeField] private int costMorality = 1;
    public int CostMorality => costMorality;

    [SerializeField] private int initialMoral = 5;
    public int InitialMoral => initialMoral;

    [SerializeField] private int maxMoral = 10;
    public int MaxMoral => maxMoral;
}

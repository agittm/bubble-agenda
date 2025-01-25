using UnityEngine;

[System.Serializable]
public class GameLevelData
{
    [SerializeField] private Vector2Int gridSize;
    public Vector2Int GridSize => gridSize;

    [SerializeField] private int opinionTypeCount = 2;
    public int OpinionTypeCount => opinionTypeCount;

    [SerializeField] private int currentStereotypeCount = 1;
    public int CurrentStereotypeCount => currentStereotypeCount;

    [SerializeField] private int currentSideCount = 1;
    public int CurrentSideCount => currentSideCount;

    [SerializeField] private Vector2Int currentMinMaxSide = new Vector2Int(4, 4);
    public Vector2Int CurrentMinMaxSide => currentMinMaxSide;
}

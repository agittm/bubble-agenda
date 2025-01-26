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

    [SerializeField] private int initialMoral = 5;
    public int InitialMoral => initialMoral;

    [SerializeField] private int maxMoral = 10;
    public int MaxMoral => maxMoral;

    [SerializeField] private Vector2 grid2CountToSize;
    public Vector2 Grid2CountToSize => grid2CountToSize;

    [SerializeField] private Vector2 grid3CountToSize;
    public Vector2 Grid3CountToSize => grid3CountToSize;

    [Header("Audio")]
    [SerializeField] private AudioClip levelCompleteClip;
    public AudioClip LevelCompleteClip => levelCompleteClip;

    [SerializeField] private AudioClip gameOverClip;
    public AudioClip GameOverClip => gameOverClip;

    [SerializeField] private GameLevelData[] levelDatas;
    public GameLevelData[] LevelDatas => levelDatas;

    [SerializeField] private DialogData[] dialogDatas;

    public string[] GetDialogContents(string code)
    {
        for (int i = 0; i < dialogDatas.Length; i++)
        {
            if (dialogDatas[i].Code == code)
                return dialogDatas[i].Contents;
        }

        return new string[0];
    }
}

[System.Serializable]
public class DialogData
{
    [SerializeField] private string code;
    public string Code => code;

    [SerializeField, TextArea] string[] contents;
    public string[] Contents => contents;
}
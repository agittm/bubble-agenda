using System;

public static class GameEvents
{
    public static Action<BubbleItemUI> OnSpawnBubble;
    public static Action<BubbleItemUI> OnSelectBubble;
    public static Action OnCleanFromHoax;
    public static Action<Side> OnFullOfHoax;
    public static Action OnFinishLevel;
    public static Action<GameOverReason> OnGameOver;
}

public enum GameOverReason
{
    Immoral,
    Imbalance,
}
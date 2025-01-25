using Hellmade.Sound;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameplayLifetimeScope : LifetimeScope
{
    [SerializeField] private AudioClip bgm;
    [SerializeField] private GameplayView gameplayView;
    [SerializeField] private GameplaySettings gameplaySettings;
    [SerializeField] private StereotypeDatabase stereotypeDatabase;

    protected override void Configure(IContainerBuilder builder)
    {
        EazySoundManager.PlayMusic(bgm, 0.5f, true, false, 1f, 1f);

        builder.RegisterEntryPoint<GameplayService>(Lifetime.Scoped)
            .WithParameter(gameplayView)
            .WithParameter(gameplaySettings)
            .WithParameter(stereotypeDatabase)
            .AsSelf();
    }
}

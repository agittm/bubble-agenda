using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameplayLifetimeScope : LifetimeScope
{
    [SerializeField] private GameplayView gameplayView;
    [SerializeField] private GameplaySettings gameplaySettings;
    [SerializeField] private StereotypeDatabase stereotypeDatabase;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<GameplayService>(Lifetime.Scoped)
            .WithParameter(gameplayView)
            .WithParameter(gameplaySettings)
            .WithParameter(stereotypeDatabase)
            .AsSelf();
    }
}

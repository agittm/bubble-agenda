using Hellmade.Sound;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class MainmenuLifetimeScope : LifetimeScope
{
    [SerializeField] private AudioClip bgm;

    protected override void Configure(IContainerBuilder builder)
    {
        EazySoundManager.PlayMusic(bgm, 1f, true, false, 1f, 1f);
    }
}

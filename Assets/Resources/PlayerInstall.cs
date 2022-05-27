using Gameplay.Character;
using Gameplay.Character.Abilities;
using Gameplay.Character.AnimationControllers;
using Gameplay.Character.Leveling;
using Gameplay.Character.MovementControllers;
using Gameplay.Character.Weapons;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "PlayerInstall", menuName = "Installers/PlayerInstall")]
public class PlayerInstall : ScriptableObjectInstaller<PlayerInstall>
{
    [SerializeField] private CharacterConfig _characterConfig;
    [SerializeField] private GameObject _heroPrefab;

    // ReSharper disable Unity.PerformanceAnalysis
    public override void InstallBindings()
    {
        Container.Bind<CharacterConfig>()
            .FromInstance(_characterConfig)
            .AsSingle();

        GameObject character = Container
            .InstantiatePrefab(_heroPrefab);

        Container.Bind<HeroData>()
            .FromComponentOn(_heroPrefab)
            .AsSingle();

        Container.Bind<CharacterAnimationController>()
            .FromComponentOn(_heroPrefab)
            .AsSingle();

        Container.Bind<MovementController>()
            .FromComponentOn(_heroPrefab)
            .AsSingle();
    }
}

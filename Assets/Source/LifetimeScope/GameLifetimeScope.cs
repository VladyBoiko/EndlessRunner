using Level;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Injections
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private LevelConfig _levelConfig;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private GameObject[] _platformPrefabs;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_levelConfig);
            builder.RegisterInstance(_playerTransform);
            builder.RegisterInstance(_platformPrefabs);

            builder.Register<LevelGenerator>(Lifetime.Singleton).As<ITickable>();
        }
    }
}
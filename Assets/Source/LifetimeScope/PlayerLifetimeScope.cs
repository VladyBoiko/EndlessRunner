using Animation;
using Input;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace Injections
{
    public class PlayerLifetimeScope : LifetimeScope
    {
        [Header("Dependencies")]
        [SerializeField] private InputActionAsset _inputActionAsset;
        [SerializeField] private InputConfig _inputConfig;
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private Rigidbody2D _playerRigidbody;
        [SerializeField] private Collider2D _playerCollider; 
        [SerializeField] private Animator _playerAnimator;
        [SerializeField] private AnimationConfig _animationConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_inputActionAsset);
            builder.RegisterInstance(_inputConfig);
            builder.RegisterInstance(_playerConfig);
            builder.RegisterInstance(_playerRigidbody);
            builder.RegisterInstance(_playerCollider);
            builder.RegisterInstance(_playerAnimator);
            builder.RegisterInstance(_animationConfig);

            builder.Register<InputController>(Lifetime.Singleton);
            builder.Register<PlayerStateModel>(Lifetime.Singleton);
            builder.Register<PlayerControllerDI>(Lifetime.Singleton).As<IStartable, IFixedTickable, ITickable>().AsSelf();
            builder.Register<JumpActionDI>(Lifetime.Singleton).As<IStartable>();
            builder.Register<SlideActionDI>(Lifetime.Singleton).As<IStartable, ITickable>();
            builder.Register<PlayerAnimationControllerDI>(Lifetime.Singleton).As<IStartable, ITickable>();
            
            builder.RegisterComponentInHierarchy<WallChecker>();
            builder.RegisterComponentInHierarchy<AnimationEventTransmitter>();
        }
    }
}
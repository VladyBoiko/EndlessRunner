using System;
using Input;
using UnityEngine;
using VContainer.Unity;

namespace Player
{
    public class SlideActionDI : IStartable, ITickable, IDisposable
    {
        private readonly PlayerControllerDI _playerController;
        private readonly InputController _inputController;
        private readonly PlayerStateModel _state;
        private readonly float _slideCooldown;
        
        private float _currentSlideCooldown;
        
        public SlideActionDI(PlayerControllerDI playerController,
            InputController inputController,
            PlayerConfig playerConfig,
            PlayerStateModel state
        )
        {
            _playerController = playerController;
            _inputController = inputController;
            _state = state;
            
            _slideCooldown = playerConfig.SlideCooldown;
        }

        public void Start()
        {
            _inputController.OnSlideInput += SlideInputHandler;
        }
        
        public void Tick()
        {
            _currentSlideCooldown -= Time.deltaTime;
        }
        
        public void Dispose()
        {
            _inputController.OnSlideInput -= SlideInputHandler;
        }
        

        private void SlideInputHandler()
        {
            if(_currentSlideCooldown > 0) return;
            if(!_playerController.IsGrounded) return;
            if(_state.IsHanging) return;
            if(_state.IsOnWall) return;
            
            _playerController.StateMachine.SetState((byte)PlayerControllerState.Slide);
            _currentSlideCooldown = _slideCooldown;
        }
    }
}
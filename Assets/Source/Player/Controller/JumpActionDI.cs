using System;
using Input;
using Unity.VisualScripting;
using UnityEngine;
using VContainer.Unity;

namespace Player
{
    public class JumpActionDI : IStartable, IDisposable
    {
        private readonly PlayerControllerDI _playerController;
        private readonly InputController _inputController;
        private readonly PlayerConfig _playerConfig;
        private readonly PlayerStateModel _state;
        
        public JumpActionDI(PlayerControllerDI playerController, 
            PlayerConfig playerConfig, 
            InputController inputController, 
            PlayerStateModel state)
        {
            _playerController = playerController;
            _inputController = inputController;
            _playerConfig = playerConfig;
            _state = state;
        }

        public void Start()
        {
            _inputController.OnJumpInput += JumpInputHandler;
        }
        
        public void Dispose()
        {
            _inputController.OnJumpInput -= JumpInputHandler;
        }
        
        private void JumpInputHandler()
        {
            if(_state.JumpCount >= _playerConfig.MaxJumpCount)
            {
                Debug.Log("JumpInputHandler: Jump count exceeded");
                return;
            }
            
            if(_state.JumpCount == 0 && !_playerController.IsGrounded) return;
            
            // if (_playerController.StateMachine.CurrentState.StateId == (byte)PlayerControllerState.Slide)
            if (_playerController.IsCellingDetected || _state.IsHanging || _state.IsClimbing)
                return;
            
            _playerController.StateMachine.SetState((byte)PlayerControllerState.Jump);
        }
    }
}
using System.Collections.Generic;
using GlobalSource;
using Input;
using UnityEngine;

namespace Player
{
    public class PlayerMovementState : StateBase
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly PlayerConfig _playerConfig;
        // private readonly InputController _inputController;
        // private readonly PlayerStateModel _state;
        
        // private float _localDirection;
        private float _currentSpeed;
        private Vector2 _currentPosition;
        
        public PlayerMovementState(StateMachine stateMachine,
            byte stateId,
            Rigidbody2D rigidbody,
            PlayerConfig playerConfig/*,
            InputController inputController,
            PlayerStateModel state*/)
            : base(stateMachine, stateId)
        {
            _rigidbody = rigidbody;
            _playerConfig = playerConfig;
            // _inputController = inputController;
            // _state = state;
            
            conditions = new List<IStateCondition>
            {
                new BaseCondition((byte)PlayerControllerState.Fall, IsFalling),
                new BaseCondition((byte)PlayerControllerState.Idle, IsIdle),
            };
            
            _currentSpeed = _playerConfig.MoveSpeed;
            _currentPosition = _rigidbody.position;
            
            // _inputController.OnMovementInput += MovementInputHandler;
        }
        
        protected override void OnUpdate(float deltaTime)
        {
            float distance = (_rigidbody.position - _currentPosition).magnitude;
            _currentSpeed = Mathf.Min(_currentSpeed + (_playerConfig.SpeedGrowth * distance), _playerConfig.MaxSpeed);
            // Debug.Log(_currentSpeed);
            _rigidbody.linearVelocity = new Vector2(_currentSpeed, _rigidbody.linearVelocity.y);
            
            // _rigidbody.linearVelocity = new Vector2(_localDirection * _playerConfig.MoveSpeed, 
            //     _rigidbody.linearVelocity.y);

            // if (_localDirection != 0)
            // {
            //     FlipCharacter(_localDirection);
            // }
        }
        
        public override void Dispose()
        {
            // _inputController.OnMovementInput -= MovementInputHandler;
        }
        
        // private void MovementInputHandler(float input)
        // {
        //     _localDirection = input;
        // }
        
        // private void FlipCharacter(float input)
        // {
        //     Vector3 scale = _rigidbody.transform.localScale;
        //     scale.x = Mathf.Sign(input) * Mathf.Abs(scale.x);
        //     _rigidbody.transform.localScale = scale;
        // }
        
        private bool IsIdle()
        {
            return false;
            // return Mathf.Approximately(_localDirection, 0f);
        }
        
        private bool IsFalling()
        {
            return _rigidbody.linearVelocity.y < 0;
        }
    }
}
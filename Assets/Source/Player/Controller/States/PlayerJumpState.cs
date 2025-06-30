using System;
using System.Collections.Generic;
using GlobalSource;
using UnityEngine;

namespace Player
{
    public class PlayerJumpState : StateBase
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly PlayerConfig _playerConfig;
        private readonly PlayerStateModel _state;
        private readonly LedgeClimbDetector _ledgeClimbDetector;
        
        private float _velocity;
        private int _direction;
        
        public PlayerJumpState(StateMachine stateMachine, 
            byte stateId, 
            Rigidbody2D rigidbody,
            PlayerConfig playerConfig,
            PlayerStateModel state,
            LedgeClimbDetector ledgeClimbDetector)
            : base(stateMachine, stateId)
        {
            _rigidbody = rigidbody;
            _playerConfig = playerConfig;
            _state = state;
            _ledgeClimbDetector = ledgeClimbDetector;
            
            conditions = new List<IStateCondition>
            {
                new BaseCondition((byte)PlayerControllerState.Fall, JumpComplete),
                new BaseCondition((byte)PlayerControllerState.LedgeHang, CanClimbLedge),
            };
        }

        public override void Enter()
        {
            _state.AddJumpCount();
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocityX, _playerConfig.JumpForce);
            _velocity = _rigidbody.linearVelocityY;
            // _direction = _state.FacingDirection;
        }
        
        protected override void OnUpdate(float deltaTime)
        {
            _velocity = _rigidbody.linearVelocityY;
            // Debug.Log(_velocity);
        }
        
        public override void Dispose()
        {
        }
        
        private bool JumpComplete()
        {
            return _velocity <= 0f;
        }

        private bool CanClimbLedge()
        {
            return _ledgeClimbDetector.CanClimbLedge(1, _state.IsOnWall);
        }
    }
}
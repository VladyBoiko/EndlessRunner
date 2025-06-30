using System;
using System.Collections.Generic;
using GlobalSource;
using UnityEngine;

namespace Player
{
    public class PlayerSlideState : StateBase
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly PlayerConfig _playerConfig;
        private readonly PlayerStateModel _state;
        private readonly Func<bool> _isCellingDetected;
        private readonly Func<bool> _isGroundDetected;
        
        private float _slideDuration;

        public PlayerSlideState(StateMachine stateMachine, 
            byte stateId,
            Rigidbody2D rigidbody,
            PlayerConfig playerConfig,
            PlayerStateModel state,
            Func<bool> isCellingDetected,
            Func<bool> isGroundDetected)
            : base(stateMachine, stateId)
        {
            _rigidbody = rigidbody;
            _playerConfig = playerConfig;
            _state = state;
            _isCellingDetected = isCellingDetected;
            _isGroundDetected = isGroundDetected;
            
            conditions = new List<IStateCondition>
            {
                new BaseCondition((byte)PlayerControllerState.Idle, SlideComplete),
                new BaseCondition((byte)PlayerControllerState.Fall, IsOnWall),
            };
        }

        public override void Enter()
        {
            _rigidbody.linearVelocityX = _playerConfig.SlideSpeed/* * _state.FacingDirection*/;
            
            _slideDuration = _playerConfig.SlideDuration;
        }

        protected override void OnUpdate(float deltaTime)
        {
            _slideDuration -= deltaTime;
        }
        
        public override void Dispose()
        {
        }

        private bool IsOnWall()
        {
            return _state.IsOnWall;
        }
        
        private bool SlideComplete()
        {
            return _slideDuration <= 0 && !_isCellingDetected() && _isGroundDetected();
        }
    }
}
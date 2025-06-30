using System;
using System.Collections.Generic;
using GlobalSource;
using UnityEngine;

namespace Player
{
    public class PlayerFallingState : StateBase
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly PlayerStateModel _state;
        private readonly LedgeClimbDetector _ledgeClimbDetector;

        private int _direction;

        public PlayerFallingState(StateMachine stateMachine,
            byte stateId,
            Rigidbody2D rigidbody,
            PlayerStateModel state,
            LedgeClimbDetector ledgeClimbDetector)
            : base(stateMachine, stateId)
        {
            _rigidbody = rigidbody;
            _state = state;
            _ledgeClimbDetector = ledgeClimbDetector;

            conditions = new List<IStateCondition>
            {
                new BaseCondition((byte)PlayerControllerState.Idle, IsIdle),
                new BaseCondition((byte)PlayerControllerState.LedgeHang, CanClimbLedge)
            };
        }

        public override void Dispose()
        {
        }

        private bool IsIdle()
        {
            return Mathf.Approximately(_rigidbody.linearVelocityY, 0f);
        }

        private bool CanClimbLedge()
        {
            return _ledgeClimbDetector.CanClimbLedge(1, _state.IsOnWall);
        }
    }
}
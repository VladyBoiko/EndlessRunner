using System.Collections.Generic;
using GlobalSource;
using Input;
using UnityEngine;

namespace Player
{
    public class PlayerIdleState : StateBase
    {
        private readonly Rigidbody2D _rigidbody;
        // private readonly PlayerConfig _playerConfig;
        private readonly PlayerStateModel _state;
        
        // private readonly InputController _inputController;
        // private float _localDirection;
        
        public PlayerIdleState(StateMachine stateMachine,
            byte stateId, 
            Rigidbody2D rigidbody, 
            /*PlayerConfig playerConfig,*/ 
            // InputController inputController,
            PlayerStateModel state)
            : base(stateMachine, stateId)
        {
            _rigidbody = rigidbody;
            // _playerConfig = playerConfig;
            // _inputController = inputController;
            _state = state;
            
            conditions = new List<IStateCondition>
            {
                new BaseCondition((byte)PlayerControllerState.Movement, IsMoving),
            };
            
            // _inputController.OnMovementInput += MovementInputHandler;
        }

        public override void Enter()
        {
            _state.SetJumpCount(0);
            _rigidbody.linearVelocity = Vector2.zero;
        }
        
        // protected override void OnUpdate(float deltaTime)
        // {
        //     Debug.Log(_rigidbody.linearVelocity);
        //     _ = _rigidbody.linearVelocity = new Vector2(_localDirection * _playerConfig.MoveSpeed, _rigidbody.linearVelocity.y);
        // }
        
        public override void Dispose()
        {
            // _inputController.OnMovementInput -= MovementInputHandler;
        }
        
        // private void MovementInputHandler(float input)
        // {
        //     _localDirection = input;
        // }
        
        private bool IsMoving()
        {
            // return !Mathf.Approximately(_localDirection, 0f);
            return !_state.IsOnWall;
        }
    }
}
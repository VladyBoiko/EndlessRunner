using System.Collections.Generic;
using GlobalSource;
using Input;
using Player;
using UnityEngine;

public class PlayerLedgeHangState : StateBase
{
    private readonly Rigidbody2D _rigidbody;
    private readonly PlayerStateModel _state;
    private readonly InputController _input;

    private bool _waitingForInput;
    private float _hangTime;

    private const float HangCooldown = 0.5f;
    
    public PlayerLedgeHangState(
        StateMachine stateMachine,
        byte stateId,
        Rigidbody2D rigidbody,
        PlayerStateModel state,
        InputController input)
        : base(stateMachine, stateId)
    {
        _rigidbody = rigidbody;
        _state = state;
        _input = input;

        conditions = new List<IStateCondition>
        {
            new BaseCondition((byte)PlayerControllerState.LedgeClimb, IsFinishedHanging),
            new BaseCondition((byte)PlayerControllerState.Fall, IsHangingTimeExceeded)
        };
    }

    public override void Enter()
    {
        _rigidbody.linearVelocity = Vector2.zero;
        _rigidbody.gravityScale = 0f;
        _rigidbody.bodyType = RigidbodyType2D.Kinematic;

        _waitingForInput = true;
        _hangTime = 0f;
        _state.SetIsHanging(true);
        
        _input.OnJumpInput += JumpInputHandler;
    }

    protected override void OnUpdate(float deltaTime)
    {
        _hangTime += deltaTime;
    }

    private void JumpInputHandler()
    {
        if (!_waitingForInput) return;

        _waitingForInput = false;
    }

    public override void Exit(byte nextStateId)
    {
        _state.SetHangCooldownTimer(HangCooldown);
        
        if (nextStateId == (byte)PlayerControllerState.Fall)
        {
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
            _rigidbody.gravityScale = 1f;
        }

        _state.SetIsHanging(false);
    }

    public override void Dispose()
    {
        _input.OnJumpInput -= JumpInputHandler;
    }
    
    private bool IsFinishedHanging()
    {
        return !_waitingForInput;
    }
    
    private bool IsHangingTimeExceeded()
    {
        return _hangTime >= 3f;
    }
}
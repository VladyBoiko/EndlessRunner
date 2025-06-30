using System.Collections.Generic;
using GlobalSource;
using Player;
using UnityEngine;

public class PlayerLedgeClimbState : StateBase
{
    private readonly Rigidbody2D _rigidbody;
    private readonly Collider2D _collider;
    private readonly PlayerStateModel _state;

    private bool _isClimbingFinished;
    
    public PlayerLedgeClimbState(
        StateMachine stateMachine,
        byte stateId,
        Rigidbody2D rigidbody,
        Collider2D collider,
        PlayerStateModel state,
        AnimationEventTransmitter transmitter)
        : base(stateMachine, stateId)
    {
        _rigidbody = rigidbody;
        _collider = collider;
        _state = state;

        conditions = new List<IStateCondition>
        {
            new BaseCondition((byte)PlayerControllerState.Idle, IsFinishedClimbing),
        };

        transmitter.OnLedgeClimbFinished += LedgeClimbFinishedHandler;
    }

    public override void Enter()
    {
        _state.SetIsClimbing(true);
        _isClimbingFinished = false;
        Debug.Log("Entered LedgeClimbState: waiting for input...");
    }

    private void LedgeClimbFinishedHandler()
    {
        _isClimbingFinished = true;
        
        Debug.Log("LedgeClimb: animation finished — back to normal physics.");
    }

    public override void Exit(byte nextStateId)
    {
        Vector2 offset = _collider.bounds.size;

        Vector2 targetPosition = _rigidbody.position + offset;
        _rigidbody.position = targetPosition;
        
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        _rigidbody.gravityScale = 1f;
        
        _state.SetIsClimbing(false);
    }

    public override void Dispose()
    {
    }
    
    private bool IsFinishedClimbing()
    {
        return _isClimbingFinished;
    }
}
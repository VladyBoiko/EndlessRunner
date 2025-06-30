using System;
using Animation;
using Player;
using UnityEngine;
using VContainer.Unity;

public class PlayerAnimationControllerDI : IStartable, ITickable, IDisposable
{
    private readonly Animator _animator;
    private readonly PlayerStateModel _state;
    private readonly PlayerControllerDI _controller;

    private readonly float _crossFadeTime;

    private readonly int _idleState;
    private readonly int _movementState;
    private readonly int _jumpState;
    private readonly int _doubleJumpState;
    private readonly int _fallState;
    private readonly int _slideState;
    private readonly int _ledgeClimbState;
    private readonly int _ledgeHangState;

    private PlayerControllerState _currentState;
    private bool _wasOnWall;
    private int _lastPlayedAnimHash;

    public PlayerAnimationControllerDI(
        Animator animator,
        PlayerStateModel state,
        PlayerControllerDI controller,
        AnimationConfig animationConfig)
    {
        _animator = animator;
        _state = state;
        _controller = controller;

        _crossFadeTime = animationConfig.CrossFadeTime;

        _idleState = Animator.StringToHash(animationConfig.IdleState);
        _movementState = Animator.StringToHash(animationConfig.MovementState);
        _jumpState = Animator.StringToHash(animationConfig.JumpState);
        _doubleJumpState = Animator.StringToHash(animationConfig.DoubleJumpState);
        _fallState = Animator.StringToHash(animationConfig.FallState);
        _slideState = Animator.StringToHash(animationConfig.SlideState);
        _ledgeClimbState = Animator.StringToHash(animationConfig.LedgeClimbState);
        _ledgeHangState = Animator.StringToHash(animationConfig.LedgeHangState);
    }

    public void Start()
    {
        _controller.OnStateChanged += StateChangedHandler;
    }

    public void Tick()
    {
        if (_currentState == PlayerControllerState.Movement)
        {
            if (_wasOnWall == _state.IsOnWall) return;
            CrossFadeIfNeeded(_state.IsOnWall ? _idleState : _movementState);
            _wasOnWall = _state.IsOnWall;
        }
    }

    private void StateChangedHandler(PlayerControllerState state)
    {
        _currentState = state;

        switch (state)
        {
            case PlayerControllerState.Idle:
                InstantCrossFade(_idleState);
                break;
            case PlayerControllerState.Movement:
                CrossFadeIfNeeded(_state.IsOnWall ? _idleState : _movementState);
                _wasOnWall = _state.IsOnWall;
                break;
            case PlayerControllerState.Fall:
                CrossFadeIfNeeded(_fallState);
                break;
            case PlayerControllerState.Jump:
                if (_state.JumpCount == 1)
                    CrossFadeIfNeeded(_jumpState);
                else
                    CrossFadeIfNeeded(_doubleJumpState);
                break;
            case PlayerControllerState.Slide:
                CrossFadeIfNeeded(_slideState);
                break;
            case PlayerControllerState.LedgeClimb:
                CrossFadeIfNeeded(_ledgeClimbState);
                break;
            case PlayerControllerState.LedgeHang:
                CrossFadeIfNeeded(_ledgeHangState);
                break;
        }
    }

    private void CrossFadeIfNeeded(int newAnimHash)
    {
        if (_lastPlayedAnimHash == newAnimHash) return;

        _animator.CrossFadeInFixedTime(newAnimHash, _crossFadeTime);
        _lastPlayedAnimHash = newAnimHash;
    }

    private void InstantCrossFade(int newAnimHash)
    {
        if (_lastPlayedAnimHash == newAnimHash) return;
        
        _animator.CrossFade(newAnimHash, 0f);
        _lastPlayedAnimHash = newAnimHash;
    }
    
    public void Dispose()
    {
        _controller.OnStateChanged -= StateChangedHandler;
    }
}

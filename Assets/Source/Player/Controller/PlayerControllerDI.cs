using System;
using GlobalSource;
using Input;
using UnityEngine;
using VContainer.Unity;

namespace Player
{
    public class PlayerControllerDI : IStartable, IFixedTickable, ITickable, IDisposable
    {
        public event Action<PlayerControllerState> OnStateChanged;
        
        private readonly PlayerConfig _playerConfig;
        private readonly Rigidbody2D _rigidbody;
        private readonly Collider2D _collider;
        private readonly PlayerStateModel _state;
        private readonly InputController _inputController;
        private readonly AnimationEventTransmitter _animationEventTransmitter;
        
        private StateMachine _stateMachine;
        
        private bool _isGrounded;
        private bool _isCellingDetected;
        public bool IsGrounded => _isGrounded;
        public bool IsCellingDetected => _isCellingDetected;
        
        public StateMachine StateMachine => _stateMachine;

        public PlayerControllerDI(PlayerConfig playerConfig, 
            Rigidbody2D rigidbody,
            Collider2D collider,
            InputController inputController, 
            PlayerStateModel state,
            AnimationEventTransmitter animationEventTransmitter)
        {
            _playerConfig = playerConfig;
            _rigidbody = rigidbody;
            _collider = collider;
            _state = state;
            _inputController = inputController;
            _animationEventTransmitter = animationEventTransmitter;
        }

        public void Start()
        {
            LedgeClimbDetector ledgeClimbDetector = new (_collider, 
                _playerConfig, 
                () => _isCellingDetected,
                _state);
            
            _stateMachine = new StateMachine();
            _stateMachine.AddState((byte)PlayerControllerState.Idle ,
                new PlayerIdleState(
                    _stateMachine,
                    (byte)PlayerControllerState.Idle,
                    _rigidbody, 
                    // _playerConfig, 
                    // _inputController,
                    _state));
            
            _stateMachine.AddState((byte)PlayerControllerState.Movement,
                new PlayerMovementState(
                    _stateMachine,
                    (byte)PlayerControllerState.Movement,
                    _rigidbody, 
                    _playerConfig/*, 
                    _inputController,
                    _state*/));
            
            _stateMachine.AddState((byte)PlayerControllerState.Fall,
                new PlayerFallingState(
                    _stateMachine,
                    (byte)PlayerControllerState.Fall,
                    _rigidbody,
                    _state,
                    ledgeClimbDetector));
            
            _stateMachine.AddState((byte)PlayerControllerState.Jump,
                new PlayerJumpState(
                    _stateMachine,
                    (byte)PlayerControllerState.Jump,
                    _rigidbody,
                    _playerConfig, 
                    _state,
                    ledgeClimbDetector));
            
            _stateMachine.AddState((byte)PlayerControllerState.Slide,
                new PlayerSlideState(
                    _stateMachine,
                    (byte)PlayerControllerState.Slide,
                    _rigidbody,
                    _playerConfig,
                    _state,
                    () => _isCellingDetected,
                    ()=> _isGrounded));
            
            _stateMachine.AddState((byte)PlayerControllerState.LedgeClimb,
                new PlayerLedgeClimbState(
                    _stateMachine,
                    (byte)PlayerControllerState.LedgeClimb,
                    _rigidbody,
                    _collider,
                    _state,
                    _animationEventTransmitter));
            
            _stateMachine.AddState((byte)PlayerControllerState.LedgeHang,
                new PlayerLedgeHangState(
                    _stateMachine,
                    (byte)PlayerControllerState.LedgeHang,
                    _rigidbody,
                    _state,
                    _inputController));
            
            _stateMachine.InitState((byte)PlayerControllerState.Idle);
            
            _stateMachine.OnStateChange += StateChangeHandler;
        }

        public void Tick()
        {
            if (_state.HangCooldownTimer > 0f)
            {
                float newCooldown = _state.HangCooldownTimer - Time.deltaTime;
                _state.SetHangCooldownTimer(newCooldown);
            }
        }
        
        public void FixedTick()
        {
            _isGrounded = Checker(Vector2.down, 
                _playerConfig.GroundCheckDistance, 
                _playerConfig.GroundLayer
            );
            
            _isCellingDetected = Checker(Vector2.up, 
                _playerConfig.CellingCheckDistance, 
                _playerConfig.CellingLayer
            );
            
            Debug.Log(_stateMachine.CurrentState);
            _stateMachine.Update(Time.fixedDeltaTime);
        }
        
        private void StateChangeHandler(byte stateId)
        {
            OnStateChanged?.Invoke((PlayerControllerState)stateId);
        }
        
        public void Dispose()
        {
            _stateMachine.OnStateChange -= StateChangeHandler;
            _stateMachine.Dispose();
        }

        private bool Checker(Vector2 direction, float checkDistance, LayerMask layerMask)
        {
            Vector2 offset = _collider.offset + Vector2.Scale(_collider.bounds.extents, direction.normalized);
            Vector2 origin = _rigidbody.position + offset;

            Debug.DrawRay(origin, direction * checkDistance, Color.green);

            return Physics2D.Raycast(origin, direction, checkDistance, layerMask);
        }

    }
}
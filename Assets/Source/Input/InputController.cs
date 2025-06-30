using System;
using Player;
using UnityEngine.InputSystem;

namespace Input
{
    public class InputController : IDisposable
    {
        // public event Action<float> OnMovementInput;
        public event Action OnJumpInput;
        public event Action OnSlideInput; 

        private readonly InputActionAsset _inputActionAsset;
        private readonly InputConfig _inputConfig;
        private readonly PlayerStateModel _state;
        
        private InputActionMap _defaultInputActionMap;
        
        // private InputAction _movementAction;
        private InputAction _jumpAction;
        private InputAction _slideAction;

        public InputController(InputActionAsset inputActionAsset, InputConfig config, PlayerStateModel state)
        {
            _inputActionAsset = inputActionAsset;
            _inputConfig = config;
            _state = state;
            
            _inputActionAsset.Enable();

            InitializeActions();
            SubscribeToEvents();
        }
        
        private void InitializeActions()
        {
            _defaultInputActionMap = _inputActionAsset.FindActionMap(_inputConfig.DefaultInputActionMapName);
            if (_defaultInputActionMap == null)
                throw new InvalidOperationException($"Action map '{_inputConfig.DefaultInputActionMapName}' not found.");

            // _movementAction = _defaultInputActionMap.FindAction(_inputConfig.MovementActionName);
            // if (_movementAction == null)
            //     throw new InvalidOperationException($"Movement action '{_inputConfig.MovementActionName}' not found.");

            _jumpAction = _defaultInputActionMap.FindAction(_inputConfig.JumpActionName);
            if (_jumpAction == null)
                throw new InvalidOperationException($"Jump action '{_inputConfig.JumpActionName}' not found.");
            
            _slideAction = _defaultInputActionMap.FindAction(_inputConfig.SlideActionName);
            if(_slideAction == null)
                throw new InvalidOperationException($"Slide action '{_inputConfig.SlideActionName}' not found.");
        }

        private void SubscribeToEvents()
        {
            // _movementAction.performed += MovementActionPerformedHandler;
            // _movementAction.canceled += MovementActionCanceledHandler;
            _jumpAction.performed += JumpActionPerformedHandler;
            _slideAction.performed += SlideActionPerformedHandler;
        }

        private void UnsubscribeFromEvents()
        {
            // _movementAction.performed -= MovementActionPerformedHandler;
            // _movementAction.canceled -= MovementActionCanceledHandler;
            _jumpAction.performed -= JumpActionPerformedHandler;
            _slideAction.performed -= SlideActionPerformedHandler;
        }
        
        // private void MovementActionPerformedHandler(InputAction.CallbackContext context)
        // {
        //     var inputValue = context.ReadValue<float>();
        //     _state.SetFacingDirection(inputValue);
        //     OnMovementInput?.Invoke(inputValue);
        // }
        // private void MovementActionCanceledHandler(InputAction.CallbackContext context)
        // {
        //     _state.SetFacingDirection(0f);
        //     OnMovementInput?.Invoke(0f);
        // }

        private void JumpActionPerformedHandler(InputAction.CallbackContext context)
        {
            OnJumpInput?.Invoke();
        }
        
        private void SlideActionPerformedHandler(InputAction.CallbackContext context)
        {
            OnSlideInput?.Invoke();
        }
        
        public void Dispose()
        {
            UnsubscribeFromEvents();
        }
    }
}
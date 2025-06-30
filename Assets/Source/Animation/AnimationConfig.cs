using UnityEngine;

namespace Animation
{
    [CreateAssetMenu(fileName = "AnimationConfig", menuName = "Data/Animation/AnimationConfig", order = 0)]
    public class AnimationConfig : ScriptableObject
    {
        [Header("Animation States")]
        [SerializeField] private string _idleState = "Idle";
        [SerializeField] private string _movementState = "Movement";
        [SerializeField] private string _jumpState = "Jump";
        [SerializeField] private string _doubleJumpState = "DoubleJump";
        [SerializeField] private string _fallState = "Fall";
        [SerializeField] private string _slideState = "Slide"; 
        [SerializeField] private string _ledgeClimbState = "LedgeClimb";
        [SerializeField] private string _ledgeHangState = "LedgeHang";
        
        [Header("Animation Parameters")]
        [SerializeField] private float _crossFadeTime = 0.125f;
        
        // Properties to access the animation states and parameters
        public string IdleState => _idleState;
        public string MovementState => _movementState;
        public string JumpState => _jumpState;
        public string DoubleJumpState => _doubleJumpState;
        public string FallState => _fallState;
        public string SlideState => _slideState;
        public string LedgeClimbState => _ledgeClimbState;
        public string LedgeHangState => _ledgeHangState;
        
        // CrossFade time for transitioning between animations
        public float CrossFadeTime => _crossFadeTime;
    }
}
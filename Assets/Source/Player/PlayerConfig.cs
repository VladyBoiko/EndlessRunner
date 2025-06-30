using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Data/Player/PlayerConfig", order = 0)]
    public class PlayerConfig : ScriptableObject
    {
        [Header("Player Settings")]
        [Header("Movement")]
        [SerializeField] private float _moveSpeed = 10f;
        [SerializeField] private float _maxSpeed = 100f;
        [SerializeField] private float _speedGrowth = 0.0005f;
        [Header("Jump Settings")]
        [SerializeField] private float _jumpForce = 8f;
        [SerializeField] private float _maxJumpCount = 2f;
        [Header("Slide Settings")]
        [SerializeField] private float _slideSpeed = 5f;
        [SerializeField] private float _slideDuration = 1f;
        [SerializeField] private float _slideCooldown = 2f;
        
        [Header("Ground Check Settings")]
        [SerializeField] private float _groundCheckDistance = 0.25f;
        [SerializeField] private LayerMask _groundLayer;
        [Header("Celling Check Settings")]
        [SerializeField] private float _cellingCheckDistance = 0.25f;
        [SerializeField] private LayerMask _cellingLayer;
        
        public float MoveSpeed => _moveSpeed;
        public float MaxSpeed => _maxSpeed;
        public float SpeedGrowth => _speedGrowth;
        public float JumpForce => _jumpForce;
        public float MaxJumpCount => _maxJumpCount;
        public float SlideSpeed => _slideSpeed;
        public float SlideDuration => _slideDuration;
        public float SlideCooldown => _slideCooldown;
        
        public float GroundCheckDistance => _groundCheckDistance;
        public LayerMask GroundLayer => _groundLayer;
        public float CellingCheckDistance => _cellingCheckDistance;
        public LayerMask CellingLayer => _cellingLayer;
    }
}
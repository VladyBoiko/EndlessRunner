using System;
using UnityEngine;

namespace Player
{
    public class LedgeClimbDetector
    {
        private readonly Collider2D _collider;
        private readonly PlayerConfig _config;
        private readonly Func<bool> _isCeilingDetected;
        private readonly PlayerStateModel _state;

        public LedgeClimbDetector(Collider2D collider, 
            PlayerConfig config, 
            Func<bool> isCeilingDetected,
            PlayerStateModel playerStateModel)
        {
            _collider = collider;
            _config = config;
            _isCeilingDetected = isCeilingDetected;
            _state = playerStateModel;
        }

        public bool CanClimbLedge(int facingDirection, bool isOnWall)
        {
            if (_state.HangCooldownTimer > 0f) return false;
            if (!isOnWall) return false;
            if (_isCeilingDetected()) return false;

            Vector2 forward = new Vector2(facingDirection, 0f);
            
            Vector2 headPosition = new Vector2(
                facingDirection > 0 ? _collider.bounds.max.x : _collider.bounds.min.x,
                _collider.bounds.max.y
            );
            
            Debug.DrawRay(headPosition, forward * 0.3f, Color.green);
            var hit1 = Physics2D.Raycast(headPosition, forward, 0.3f, _config.GroundLayer);
            
            Vector2 origin = new Vector2(headPosition.x, headPosition.y + 0.15f);
            Debug.DrawRay(origin, forward * 0.3f, Color.blue);
            var hit2 = Physics2D.Raycast(origin, forward, 0.3f, _config.GroundLayer);

            return hit1.collider != null && hit2.collider == null;
        }
    }
}
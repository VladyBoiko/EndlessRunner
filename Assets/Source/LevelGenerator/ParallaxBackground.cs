using UnityEngine;

namespace Level
{
    public class ParallaxBackground : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private float _speed;
    
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private float _length;
        private float _xPosition;

        private void Start()
        { 
            _length = _spriteRenderer.bounds.size.x;
            _xPosition = transform.position.x;
        }

        private void FixedUpdate()
        {
            float distanceMoved = _camera.transform.position.x * (1 - _speed);
            float distanceToMove = _camera.transform.position.x * _speed;
        
            transform.position = new Vector3(_xPosition + distanceToMove, transform.position.y);

            if (distanceMoved > _xPosition + _length)
            {
                _xPosition += _length;
            }
        }
    }
}

using UnityEngine;

namespace Input
{
    [CreateAssetMenu(fileName = "InputConfig", menuName = "Data/Input/InputConfig", order = 0)]
    public class InputConfig : ScriptableObject
    {
        [Header("Input Settings")]
        [SerializeField] private string _defaultInputActionMapName = "Default";
        [SerializeField] private string _movementActionName = "Movement";
        [SerializeField] private string _jumpActionName = "Jump";
        [SerializeField] private string _slideActionName = "Slide";
        
        public string DefaultInputActionMapName => _defaultInputActionMapName;
        public string MovementActionName => _movementActionName;
        public string JumpActionName => _jumpActionName;
        public string SlideActionName => _slideActionName;
    }
}
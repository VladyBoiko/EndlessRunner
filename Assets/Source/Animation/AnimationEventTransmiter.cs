using System;
using UnityEngine;

public class AnimationEventTransmitter : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    public Animator Animator => _animator;
    
    public event Action OnLedgeClimbFinished;

    public void OnLedgeClimbAnimationFinished()
    {
        OnLedgeClimbFinished?.Invoke();
    }
}

using System;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public bool isContinued;
    [SerializeField] protected Animator _animator;
    [SerializeField] protected Enemy _enemy;
    [SerializeField] protected MachineEnemy _brainSkeleton;
    [SerializeField] protected State _stateChanged;

    public virtual void Entry() { }
    public virtual void Do() { }
    public virtual void End() { }
}

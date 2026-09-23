using System;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public bool inState;
    [SerializeField] protected Animator _animator;
    [SerializeField] protected Enemy _enemy;

    public abstract void Entry();
    public abstract void Do();
    public abstract void End();
}

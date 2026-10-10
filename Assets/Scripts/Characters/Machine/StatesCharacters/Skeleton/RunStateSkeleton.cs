using UnityEngine;

public class RunStateSkeleton : State
{
    [SerializeField]private float _distanceToPlayer;
    [SerializeField]private float _detectionRadius;
    private Transform _transform;
    public override void Entry()
    {
        isContinued = true;
        _animator.SetBool("inRunning", isContinued);
    }

    public override void Do()
    {
        _enemy.Target();
        _detectionRadius = _enemy.detectionRadius;
        _distanceToPlayer = _enemy.distanceToPlayer;

        if (isContinued == true && _distanceToPlayer < _detectionRadius && _distanceToPlayer > 4)
        {
            _enemy.MoveEnemy();
            _animator.SetBool("inRunning", isContinued);
        }
        else
        {
            isContinued = false;
            End();
        }
    }

    public override void End()
    {
        if (!isContinued)
        {
            _animator.SetBool("inRunning", isContinued);
            _brainSkeleton.StateChange(_stateChanged);
        }
    }

    void Update()
    {
    }
}

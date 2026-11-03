using UnityEngine;

public class IdleStateSkeleton : State
{
    [SerializeField] private float _distanceToPlayer;
    [SerializeField] private float _detectionRadius;

    public override void Entry()
    {
        isContinued = true;
        Do();
    }

    public override void Do()
    {
        _enemy.MoveEnemy();
        _distanceToPlayer = _enemy.distanceToPlayer;
        _detectionRadius = _enemy.detectionRadius;

        if (isContinued == true && _distanceToPlayer > _detectionRadius)
        {
            _animator.SetBool("inIdle", isContinued);
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
            _animator.SetBool("inIdle", isContinued);
            _brainCharacter.StateChange(_stateChanged);
        }
    }
}

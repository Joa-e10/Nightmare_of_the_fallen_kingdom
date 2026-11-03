using UnityEngine;

public class WalkStateEyeRed : State
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
        _enemy.Target();
        _detectionRadius = _enemy.detectionRadius;
        _distanceToPlayer = _enemy.distanceToPlayer;

        if (isContinued == true && _distanceToPlayer < _detectionRadius && _distanceToPlayer > 4)
        {
            _enemy.MoveEnemy();
            _animator.SetBool("IsWalking", isContinued);
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
            _animator.SetBool("IsWalking", isContinued);
            _brainCharacter.StateChange(_stateChanged);
        }
    }
}

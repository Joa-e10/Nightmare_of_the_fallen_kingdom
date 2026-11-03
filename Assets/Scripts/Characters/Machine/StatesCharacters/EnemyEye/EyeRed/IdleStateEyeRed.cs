using UnityEngine;
public class IdleStateEyeRed : State
{
    [SerializeField] private float _distanceToPlayer;
    [SerializeField] private float _detectionRadius;

    public override void Entry()
    {
        isContinued = true;
        During();
    }

    public override void During()
    {
        _enemy.MoveEnemy();
        _distanceToPlayer = _enemy.distanceToPlayer;
        _detectionRadius = _enemy.detectionRadius;

        if (isContinued == true && _distanceToPlayer > _detectionRadius || _enemy._isCooldown)
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

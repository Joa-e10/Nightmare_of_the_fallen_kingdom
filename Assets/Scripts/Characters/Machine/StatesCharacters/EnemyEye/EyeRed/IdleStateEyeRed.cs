using UnityEngine;
public class IdleStateEyeRed : State
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

        if (isContinued == true && _distanceToPlayer > _detectionRadius || _enemy._isCooldown)
        {
            _animator.SetBool("InIdle", isContinued);
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
            _animator.SetBool("InIdle", isContinued);
            _brainCharacter.StateChange(_stateChanged);
        }
    }
}

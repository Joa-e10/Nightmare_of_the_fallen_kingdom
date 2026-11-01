using UnityEngine;
public class IdleStateEye : State
{
    [SerializeField] private float _distanceToPlayer;
    [SerializeField] private float _detectionRadius;
    public bool _inMove;

    public override void Entry()
    {
        isContinued = true;
        _animator.SetBool("InIdle", isContinued);
    }

    public override void Do()
    {
        _enemy.MoveEnemy();
        _distanceToPlayer = _enemy.distanceToPlayer;
        _detectionRadius = _enemy.detectionRadius;

        _inMove = _enemy._inMove;
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

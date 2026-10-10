using UnityEngine;

public class GuardStateSkeleton : State
{
    private bool _isAttacking;
    private bool _isCooldown;
    private RaycastHit _hit;
    [SerializeField] private float _distanceToPlayer;
    [SerializeField] private float _detectionRadius;
    public override void Entry()
    {
        isContinued = true;
        _animator.SetBool("onGuard", isContinued);
        Debug.Log($"De esta pasada tenemos a isAttaking como {_isAttacking} y tenemos a isCooldown como {_isCooldown}");
    }

    public override void Do()
    {
    
        _enemy.AttackEnemy();
        _isCooldown = _enemy._isCooldown;

        if (isContinued && _isCooldown)
        {
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
            _animator.SetBool("onGuard", isContinued);
            _brainSkeleton.StateChange(_stateChanged);
        }
    }
}

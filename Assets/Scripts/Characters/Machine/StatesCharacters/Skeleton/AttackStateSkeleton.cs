using System.Collections;
using UnityEngine;

public class AttackStateSkeleton : State
{
    private Transform _transform;
    private bool _isAttacking;
    private bool _isCooldown;
    public override void Entry()
    {
        isContinued = true;
        _animator.SetBool("isAttacking", isContinued);
        Debug.Log($"De esta pasada tenemos a isAttaking como {_isAttacking} y tenemos a isCooldown como {_isCooldown}");
    }
    public override void Do()
    {
       
        _enemy.AttackEnemy();
        _isAttacking = _enemy._isAttacking;
        _isCooldown = _enemy._isCooldown;

        if (isContinued == true && _isAttacking && !_isCooldown)
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
            _animator.SetBool("isAttacking", isContinued);
            _brainSkeleton.StateChange(_stateChanged);
        }
    }
}

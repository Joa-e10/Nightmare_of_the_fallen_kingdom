using UnityEngine;
public class AttackStateEyeRed : State
{
    private bool _isAttacking;
    private bool _isCooldown;
    public override void Entry()
    {
        isContinued = true;
        During();
    }
    public override void During()
    {

        _enemy.AttackEnemy();
        _isAttacking = _enemy._isAttacking;
        _isCooldown = _enemy._isCooldown;

        if (isContinued == true && _isAttacking && !_isCooldown)
        {
            _animator.SetBool("isAttacking", isContinued);
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
            _brainCharacter.StateChange(_stateChanged);
        }
    }
}

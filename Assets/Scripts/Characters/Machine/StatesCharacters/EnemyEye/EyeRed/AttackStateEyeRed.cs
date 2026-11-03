using UnityEngine;
public class AttackStateEyeRed : State
{
    private bool _isAttacking;
    private bool _isCooldown;
    public override void Entry()
    {
        isContinued = true;
        Do();
    }
    public override void Do()
    {

        _enemy.AttackEnemy();
        _isAttacking = _enemy._isAttacking;
        _isCooldown = _enemy._isCooldown;

        if (isContinued == true && _isAttacking && !_isCooldown)
        {
            _animator.SetBool("IsAttacking", isContinued);
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
            _animator.SetBool("IsAttacking", isContinued);
            _brainCharacter.StateChange(_stateChanged);
        }
    }
}

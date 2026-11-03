using UnityEngine;

public class GuardStateSkeleton : State
{
    private bool _isCooldown;
    [SerializeField] private float _distanceToPlayer;
    [SerializeField] private float _detectionRadius;
    public override void Entry()
    {
        isContinued = true;
        During();
    }

    public override void During()
    {
    
        _enemy.AttackEnemy();
        _isCooldown = _enemy._isCooldown;

        if (isContinued && _isCooldown)
        {
            _animator.SetBool("onGuard", isContinued);
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
            _brainCharacter.StateChange(_stateChanged);
        }
    }
}

using UnityEngine;

public class DieStateEye : State
{
    private bool _isDie;
    public override void Entry()
    {
        isContinued = true;
        During();
    }
    public override void During()
    {
        _isDie = _enemy._isDie;
        if (isContinued == true && _isDie)
        {
            _animator.SetBool("isDie", isContinued);
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
            _animator.SetBool("isDie", isContinued);
            _brainCharacter.StateChange(_stateChanged);
        }
    }
}

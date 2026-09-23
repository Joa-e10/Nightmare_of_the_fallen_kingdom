using UnityEngine;

public class RunState : State
{

    public override void Entry()
    {
        inState = true;
    }

    public override void Do()
    {
        if (inState == true)
        {
            _enemy.MoveEnemy();
        }
        else 
        {

        }
    }

    public override void End()
    {
        inState = false;
    }

    void Start()
    {
        
    }

    void Update()
    {
        Do();
    }
}

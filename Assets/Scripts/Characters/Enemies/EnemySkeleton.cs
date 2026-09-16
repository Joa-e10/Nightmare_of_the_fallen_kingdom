using UnityEngine;

public class EnemySkeleton : Enemy
{
    public Animator _animatorController;

    private void OnEnable()
    {
        _animatorController.GetComponent<Animator>();
    }
    public override void AttackEnemy()
    {
    }

    public override void MoveEnemy()
    {
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

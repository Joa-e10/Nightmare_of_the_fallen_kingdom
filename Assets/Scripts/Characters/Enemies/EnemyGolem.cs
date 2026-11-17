using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGolem : Enemy
{
    public int limitAttack;
    private int _attackCounter = 0;
    [SerializeField]private Animator _animator;
    public override void OnNetworkSpawn()
    {
        _agent = GetComponent<NavMeshAgent>();
        _playerScript = GetComponent<Player>();
        _agent.speed = _speed;
    }
    private IEnumerator CooldownBase()
    {
        yield return new WaitForSeconds(2f);

        _isCooldown = false;

        Debug.Log($"Pasaron 2 segs.");
    }

    public override void AttackEnemy()
    {
        if (_isAttacking || _isCooldown) return;

        if (!_agent.isStopped) return;

        if (!Physics.Raycast(_rangeCheck.position, transform.forward, out _hit, rangeDistance, hitLayer)) return;

        if (_attackCounter <= limitAttack)
        {
            PrimaryAttack();
            _isCooldown = true;
            _attackCounter++;
        }
        else 
        {
            SecondaryAttack();
            _isCooldown = true;
            _attackCounter = 0;
        }

            StartCoroutine(CooldownBase());
        Debug.DrawLine(_rangeCheck.position, _hit.point, Color.red);
    }

    private void PrimaryAttack() 
    {
        _isAttacking = true;
        _animator.SetTrigger("isPrimaryAttack");
        Debug.Log("Golem: Primario");
    }

    private void SecondaryAttack()
    {
        _isAttacking = true;
        _animator.SetTrigger("isSecondAttack");
        Debug.Log("Golem: Secundario");
    }

    public override void MoveEnemy()
    {
        Target();

        if (_newTarget != null)
        {
            if (distanceToPlayer < 4 || distanceToPlayer > detectionRadius)
            {
                _agent.isStopped = true;
                _inMove = false;
                Vector3 direction = (_player.position - transform.position);
                direction.y = 0;

                Quaternion targetRotation = Quaternion.LookRotation(direction);

                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360f * Time.deltaTime);
            }
            else
            {
                _agent.isStopped = false;
                _inMove = true;
                _agent.SetDestination(_newTarget.position);
            }
        }
    }

    public void OnUpdateAnimations()
    {
        _animator.SetBool("isMove", _inMove);
        _animator.SetBool("isDamaged", _isDamaged);
        _animator.SetBool("isDie", _isDie);
    }

    void Update()
    {
        MoveEnemy();
        OnUpdateAnimations();
        AttackEnemy();
    }
}

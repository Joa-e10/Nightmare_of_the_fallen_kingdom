
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
public class EnemySkeletonKing : Enemy
{
    private bool _secondPhase = true;
    [SerializeField] List<Transform> _transformSpawnerList = new List<Transform>();
    [SerializeField] GameObject _enemyPrefab;
    private int _counterSkeletons = 0;
    public int _limitSkeletons = 0;
    private bool _stopGenerate;
    public static event Action OnKingDie;

    void OnEnable()
    {
        EnemySkeleton.OnChildrenDie += RestChildren;
    }

    void OnDisable()
    {
        EnemySkeleton.OnChildrenDie -= RestChildren;
    }
    public override void OnNetworkSpawn()
    {
        _agent = GetComponent<NavMeshAgent>();
        _playerScript = GetComponent<Player>();
        _agent.speed = _speed;
        _agent.isStopped = true;
    }

    public override void OnNetworkPreDespawn()
    {
        OnKingDie?.Invoke();
    }
    public override void AttackEnemy()
    {
        if (_isAttacking || _isCooldown) return;

        if (!_agent.isStopped) return;

        if (!Physics.Raycast(_rangeCheck.position, transform.forward, out _hit, rangeDistance, hitLayer)) return;
        Debug.Log("Estamos atacando");

        if (_counterSkeletons >= _limitSkeletons)
        {
            _stopGenerate = true;
        }
        else
        {
            _stopGenerate = false;
        }

        if (_secondPhase && !_stopGenerate)
        {
            SecondaryAttack();
            _isCooldown = true;
        }
        else
        {
            PrimaryAttack();
            _isCooldown = true;
        }

        StartCoroutine(CooldownBase());
        Debug.DrawLine(_rangeCheck.position, _hit.point, Color.red);
    }

    private IEnumerator CooldownBase()
    {
        yield return new WaitForSeconds(3f);

        _isCooldown = false;
        _isAttacking = false;
        Debug.Log($"Pasaron 2 segs.");
    }

    private void PrimaryAttack()
    {
        _isAttacking = true;
        //_animator.SetTrigger("isPrimaryAttack");
        Debug.Log("KING: Primario");
    }

    private void SecondaryAttack()
    {
        _isAttacking = true;
        int randomNum = Random.Range(0, _transformSpawnerList.Count);
        GameObject enemySpawned = Instantiate(_enemyPrefab, _transformSpawnerList[randomNum].position, Quaternion.identity);
        EnemySkeleton enemyComponent = enemySpawned.GetComponent<EnemySkeleton>();
        enemyComponent.SetIsChildren();

        enemySpawned.GetComponent<NetworkObject>().Spawn();
        _counterSkeletons++;
        //_animator.SetTrigger("isSecondAttack");
        Debug.Log("KING: Secundario");
    }
    public override void MoveEnemy()
    {
        Target();

        if (_newTarget != null)
        {
            _inMove = false;

            Vector3 direction = (_player.position - transform.position);
            direction.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360f * Time.deltaTime);
        }
    }

    private void RestChildren() 
    {
        _counterSkeletons--;
    }

    private void Update()
    {
        MoveEnemy();
        AttackEnemy();

        if (_currentHealth <= 0) 
        {
            Die();
        }
    }
}

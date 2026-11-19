using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;

public class EnemySkeletonKing : Enemy
{
    private bool _secondPhase = true;
    [SerializeField] List<Transform> _transformSpawnerList = new List<Transform>();
    [SerializeField] GameObject _enemyPrefab;
    public override void OnNetworkSpawn()
    {
        _agent = GetComponent<NavMeshAgent>();
        _playerScript = GetComponent<Player>();
        _agent.speed = _speed;
        _agent.isStopped = true;
    }
    public override void AttackEnemy()
    {
        if (_isAttacking || _isCooldown) return;

        if (!_agent.isStopped) return;

        if (!Physics.Raycast(_rangeCheck.position, transform.forward, out _hit, rangeDistance, hitLayer)) return;
        Debug.Log("Estamos atacando");
        if (_secondPhase)
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
        yield return new WaitForSeconds(2f);

        _isCooldown = false;

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
        enemySpawned.GetComponent<NetworkObject>().Spawn();
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

    private void Update()
    {
        MoveEnemy();
        AttackEnemy();
    }
}

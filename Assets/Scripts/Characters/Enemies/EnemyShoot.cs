using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyShoot : Enemy
{
    [SerializeField]private GameObject _bulletEnemyPrefab;
    private bool _isShooting;

    public override void OnNetworkSpawn()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.Find("Player").GetComponent<Transform>();//ESTO TENEMOS QUE SOLUCIONAR
        _agent.speed = _speed;//Cambiamos la velocidad del agente.
    }

    void Update()
    {
            //MoveEnemy();
    }

    public override void MoveEnemy()
    {

        Target();

        if (distanceToPlayer < detectionRadius)//La distancia del player es menor a radio?
        {
            _inMove = true;
            _agent.SetDestination(_player.position); // Lo dirigimos hasta la posicion del PLAYER

            if (distanceToPlayer <= 4)
            {
                _agent.isStopped = true;

                Vector3 direction = (_player.position - transform.position);
                direction.y = 0;

                Quaternion targetRotation = Quaternion.LookRotation(direction);

                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360f * Time.deltaTime);
            }
            else
            {
                _agent.isStopped = false;
            }
        }
        else
        {
            _inMove = false;
            _agent.isStopped = _inMove;
        }


    }
    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(2f);

        _isCooldown = false;

        Debug.Log($"Pasaron 2 segs.");
    }
    public override void AttackEnemy()
    {
        if (_isAttacking || _isCooldown) return;
        if (Physics.Raycast(_rangeCheck.position, transform.forward, out _hit, rangeDistance, hitLayer))
        {
            _isAttacking = true;
            _isCooldown = true;
        }

            StartCoroutine(Cooldown());
            Debug.DrawLine(_rangeCheck.position, _hit.point, Color.red);
    }

    public void ProjectileLaunch() 
    {
        Vector3 direction = (_player.position - transform.position).normalized;
        GameObject generatedBullet = Instantiate(_bulletEnemyPrefab, transform.position, Quaternion.identity);
        BulletEnemy bulletComponent = generatedBullet.GetComponent<BulletEnemy>();
        bulletComponent.setDirectionBullet(direction);
    }
}

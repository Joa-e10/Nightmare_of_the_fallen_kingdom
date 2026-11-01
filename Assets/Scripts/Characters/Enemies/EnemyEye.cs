using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class EnemyEye : Enemy
{
    public override void OnNetworkSpawn()
    {
        //_rangeCheck = GameObject.Find("RangeCheck").GetComponent<Transform>(); //Tomamos el Transform del objeto RANGECHECK.
        _agent = GetComponent<NavMeshAgent>();
        //_player = GameObject.Find("Player").GetComponent<Transform>();
        _playerScript = GetComponent<Player>();//Tomamos el Transform del objeto PLAYER.
        _agent.speed = _speed;//Cambiamos la velocidad del agente.
    }

    public override void MoveEnemy()
    {
        Target();
        // distanceToPlayer = Vector3.Distance(transform.position, _player.position); // Distancia del player con respecto al enemy.

        if (_newTarget != null)
        {
            if (distanceToPlayer < 4 || distanceToPlayer > detectionRadius)
            {
                _agent.isStopped = true;
                _isStoppedEnemy = _agent.isStopped;
            }
            else
            {
                _agent.isStopped = false;
                _isStoppedEnemy = _agent.isStopped;
                _agent.SetDestination(_newTarget.position);
            }
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
        if (!Physics.Raycast(_rangeCheck.position, transform.forward, out _hit, rangeDistance, hitLayer)) return;
        _isAttacking = true;
        _isCooldown = true;
       // count++;
        Debug.Log($"Cantidad de veces en ataque: ");
        // Debug.Log($"De la pasada nro:{count} Tenemos a isAttaking como {_isAttacking} y tenemos a isCooldown como {_isCooldown}");

        StartCoroutine(Cooldown());
        Debug.DrawLine(_rangeCheck.position, _hit.point, Color.red); //Dibuja en la escena el rayo de deteccion.
    }
    void Update()
    {
        if (_isAttacking == false)
        {
            _agent.isStopped = false;
            MoveEnemy();
        }
        else
        {

            _agent.isStopped = true;
        }

        AttackEnemy();
       // Debug.Log("Tenemos en contacto al: " + _hit.transform);
        //Debug.Log("Delay: " + _delay);
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, detectionRadius);
    }*/
}

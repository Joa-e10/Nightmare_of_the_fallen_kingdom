using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class EnemySkeleton : Enemy
{
    public int  count = 0;
    public override void OnNetworkSpawn()
    {
        _agent = GetComponent<NavMeshAgent>();
        _playerScript = GetComponent<Player>();//Tomamos el Transform del objeto PLAYER.
        _agent.speed = _speed;//Cambiamos la velocidad del agente.

    }

    public override void MoveEnemy()
    {
         Target();

        if (_newTarget != null) {
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

    public void SetAttacking()
    {
        _isAttacking = false;
    }
    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(2f);

        _isCooldown = false;

        Debug.Log($"Pasaron 2 segs.");
    }
    public override void AttackEnemy()
    {
        if (_isAttacking || _isCooldown)return;
        if (!Physics.Raycast(_rangeCheck.position,transform.forward,out _hit,rangeDistance, hitLayer))return;
        _isAttacking = true;
        _isCooldown = true;
        count++;
        Debug.Log($"Cantidad de veces en ataque: "+count);
       // Debug.Log($"De la pasada nro:{count} Tenemos a isAttaking como {_isAttacking} y tenemos a isCooldown como {_isCooldown}");

        StartCoroutine(Cooldown());
        Debug.DrawLine(_rangeCheck.position, _hit.point, Color.red); //Dibuja en la escena el rayo de deteccion.
    }
    

}

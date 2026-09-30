using UnityEngine;
using UnityEngine.AI;
public class EnemyEye : Enemy
{
    public override void OnNetworkSpawn()
    {
        _rangeCheck = GameObject.Find("RangeCheck").GetComponent<Transform>(); //Tomamos el Transform del objeto RANGECHECK.
        _agent = GetComponent<NavMeshAgent>();
        //_player = GameObject.Find("Player").GetComponent<Transform>();
        _playerScript = GetComponent<Player>();//Tomamos el Transform del objeto PLAYER.
        _agent.speed = _speed;//Cambiamos la velocidad del agente.
    }

    public override void MoveEnemy()
    {
         Target();

        if (_newTarget != null) {

            _agent.SetDestination(_newTarget.position);
            Debug.Log("Tu destino es: "+_newTarget);
            if (distanceToPlayer <= 2 || distanceToPlayer > detectionRadius)
            {
                _agent.isStopped = true;
            }
            else
            {
                _agent.isStopped = false;
            }
        }
    }
    public override void AttackEnemy()
    {
        if (Physics.Raycast(_rangeCheck.position, transform.forward, out _hit, rangeDistance, hitLayer)) //Creamos una deteccion por rayo y consultamos si colisiono con un objeto "Player".
        {
            if (_delay <= 0)
            {
                Debug.Log("Estoy atacando con todo!!");
                _isAttacking = true;
                _weaponMeele.SetActive(_isAttacking);
                _delay = 2f;
            }
            else
            {
                Debug.Log("Estoy descansando el ataque perrito malvado");
                _isAttacking = false;
                _weaponMeele.SetActive(_isAttacking);
                _delay -= Time.deltaTime;
            }
        }
        else
        {
            _isAttacking = false;
            _weaponMeele.SetActive(_isAttacking);
            _delay = 0;
        }
        Debug.DrawLine(_rangeCheck.position, _hit.point, Color.red); //Dibuja en la escena el rayo de deteccion.
    }
    void Update()
    {
        /*if (_isAttacking == false)
        {
            _agent.isStopped = false;
            //MoveEnemy();
        }
        else
        {

            _agent.isStopped = true;
        }

        AttackEnemy();*/
       // Debug.Log("Tenemos en contacto al: " + _hit.transform);
        //Debug.Log("Delay: " + _delay);
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, detectionRadius);
    }*/
}

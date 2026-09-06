using Unity.Services.Multiplayer;
using UnityEngine;
using UnityEngine.AI;

public class EnemyShoot : Enemy
{
    [SerializeField]private GameObject _bulletEnemyPrefab;
    private bool _isShooting;

    public override void OnNetworkSpawn()
    {
        _rangeCheck = GameObject.Find("RangeCheck").GetComponent<Transform>();
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.Find("Player").GetComponent<Transform>();//ESTO TENEMOS QUE SOLUCIONAR
        _agent.speed = _speed;//Cambiamos la velocidad del agente.
    }

    void Update()
    {
        if (_isShooting == false)
        {
            _agent.isStopped = false;
            MoveEnemy();
            Debug.Log("El ENEMY NO esta atacando y SI se mueve");


        }
        else
        {

            _agent.isStopped = true;
            Debug.Log("El ENEMY esta atacando y NO se mueve");
        }

        AttackEnemy();
    }

    public override void MoveEnemy()
    {

        Target();
        distanceToPlayer = Vector3.Distance(transform.position, _player.position); // Distancia del player con respecto al enemy.

        if (distanceToPlayer < detectionRadius)//La distancia del player es menor a radio?
        {
            _inMove = true;
            _agent.SetDestination(_player.position); // Lo dirigimos hasta la posicion del PLAYER

            if (distanceToPlayer <= 4)
            {
                _agent.isStopped = true;
            }
            else
            {
                _agent.isStopped = false;
            }
        }
        else
        {
            _inMove = false;
        }


    }

    public override void AttackEnemy()
    {
        if (Physics.Raycast(_rangeCheck.position, transform.forward, out _hit, rangeDistance, hitLayer)) //Creamos una deteccion por rayo y consultamos si colisiono con un objeto "Player".
        {
            if (_delay <= 0)
            {
                Debug.Log("Estoy disparando con todo!!");
                _isShooting = true;

                //Vector3 direction = (_player.position - transform.position);
                Vector3 direction = (_player.position - transform.position).normalized;
                GameObject generatedBullet = Instantiate(_bulletEnemyPrefab, transform.position, Quaternion.identity);
                BulletEnemy bulletComponent = generatedBullet.GetComponent<BulletEnemy>();
                bulletComponent.setDirectionBullet(direction);

                _delay = 2f;
            }
            else
            {
                Debug.Log("Estoy descansando el ataque perrito malvado");
                _isShooting = false;
                _delay -= Time.deltaTime;
            }
            Debug.Log("Tenemos en contacto al: " + _hit.transform);
        }
        else
        {
            _isShooting = false;
            _delay = 0;
        }
        Debug.DrawLine(_rangeCheck.position, _hit.point, Color.red); //Dibuja en la escena el rayo de deteccion.
    }
}

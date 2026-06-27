using Unity.Services.Multiplayer;
using UnityEngine;
using UnityEngine.AI;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField]private GameObject _bulletEnemyPrefab;
    private float _delayS = 0;
    private NavMeshAgent _agentS;
    private Transform _player;
    private Transform _rangeCheckS;
    private bool _isShooting;
    protected float distanceToPlayer;
    public float rangeDistanceS = 5f;
    protected float detectionRadius = 30;
    protected bool _inMove;
    private RaycastHit _hit;
    public LayerMask hitLayer;
    private int _speed = 5;
    void Start()
    {
        _rangeCheckS = GameObject.Find("RangeCheckS").GetComponent<Transform>();
        _agentS = GetComponent<NavMeshAgent>();
        _player = GameObject.Find("Player").GetComponent<Transform>();
        _agentS.speed = _speed;//Cambiamos la velocidad del agente.
    }
    public void MoveEnemyS() //Movimiento del enemigo.
    {
        distanceToPlayer = Vector3.Distance(transform.position, _player.position); // Distancia del player con respecto al enemy.

        if (distanceToPlayer < detectionRadius)//La distancia del player es menor a radio?
        {
            _inMove = true;
            _agentS.SetDestination(_player.position); // Lo dirigimos hasta la posicion del PLAYER

            if (distanceToPlayer <= 4)
            {
                _agentS.isStopped = true;
            }
            else
            {
                _agentS.isStopped = false;
            }
        }
        else
        {
            _inMove = false;
        }


    }

    public void ShootEnemy() //Ataque del enemigo.
    {
        if (Physics.Raycast(_rangeCheckS.position, transform.forward, out _hit, rangeDistanceS, hitLayer)) //Creamos una deteccion por rayo y consultamos si colisiono con un objeto "Player".
        {
            if (_delayS <= 0)
            {
                Debug.Log("Estoy disparando con todo!!");
                _isShooting = true;

                //Vector3 direction = (_player.position - transform.position);
                Vector3 direction = (_player.position - transform.position).normalized;
                GameObject generatedBullet = Instantiate(_bulletEnemyPrefab,transform.position, Quaternion.identity);
                BulletEnemy bulletComponent = generatedBullet.GetComponent<BulletEnemy>();
                bulletComponent.setDirectionBullet(direction);

                _delayS = 2f;
            }
            else
            {
                Debug.Log("Estoy descansando el ataque perrito malvado");
                _isShooting = false;
                _delayS -= Time.deltaTime;
            }

        }
        else
        {
            _isShooting = false;
            _delayS = 0;
        }
        Debug.DrawLine(_rangeCheckS.position, _hit.point, Color.red); //Dibuja en la escena el rayo de deteccion.
    }

    void Update()
    {
        if (_isShooting == false)
        {
            _agentS.isStopped = false;
            MoveEnemyS();
            Debug.Log("El ENEMY NO esta atacando y SI se mueve");


        }
        else
        {

            _agentS.isStopped = true;
            Debug.Log("El ENEMY esta atacando y NO se mueve");
        }

        ShootEnemy();
    }
}

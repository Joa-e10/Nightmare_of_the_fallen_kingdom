using UnityEngine;
using UnityEngine.AI;

public class enemy : characters
{
    private int damage;

    private NavMeshAgent _agent;
    protected float distanceToPlayer;
    protected float detectionRadius = 30;
    private Transform _player;
    private Transform _rangeCheck;
    private bool _isAttacking;

    public float rangeDistance = 20f;
    private RaycastHit _hit;
    public LayerMask hitLayer;

    void Start()
    {
        _rangeCheck = GameObject.Find("RangeCheck").GetComponent<Transform>(); //Tomamos el Transform del objeto RANGECHECK.
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.Find("Player").GetComponent<Transform>(); //Tomamos el Transform del objeto PLAYER.
        _agent.speed = _speed;//Cambiamos la velocidad del agente.
    }

    public void MoveEnemy() //Movimiento del enemigo.
    {
        distanceToPlayer = Vector3.Distance(transform.position, _player.position); // Distancia del player con respecto al enemy.

        if (distanceToPlayer < detectionRadius)//La distancia del player es menor a radio?
        {
            _inMove = true;
            _agent.SetDestination(_player.position); // Lo dirigimos hasta la posicion del PLAYER
        }
        else 
        {
            _inMove = false;
        }
            

    }

    public void AttackEnemy() //Ataque del enemigo.
    {

        if (Physics.Raycast(_rangeCheck.position, transform.forward, out _hit, rangeDistance, hitLayer)) //Creamos una deteccion por rayo y consultamos si colisiono con un objeto "Player".
        {
            Debug.Log("Colisiono con un Jugador!" + _hit.collider.gameObject.name);
            _isAttacking = true;
        }
        else 
        {
            _isAttacking = false;
        }

            Debug.DrawLine(_rangeCheck.position, _hit.point, Color.red); //Dibuja en la escena el rayo de deteccion.

    }
    void Update()
    {
        MoveEnemy();
        if (_inMove == true) // Si _inMove es verdadero
        {
            //Debug.Log("El ENEMY se esta moviendo");
        }
        else 
        {
            //Debug.Log("El ENEMY se detuvo");
        }
        AttackEnemy();
        if (_isAttacking == true) // Si _isAttacking es verdadero
        {
            Debug.Log("El ENEMY esta atacando");
        }
        else
        {
            Debug.Log("El ENEMY NO esta atacando");
        }
    }
    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);

        Debug.Log($"Enemigo {gameObject.name} recibió {amount} de daño.");
    }
}

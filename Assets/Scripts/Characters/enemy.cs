using UnityEngine;
using UnityEngine.AI;

public class enemy : characters
{
    private NavMeshAgent _agent;
    protected float distanceToPlayer;
    protected float detectionRadius = 30;
    private Transform _player;
    void Start()
    {
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
    void Update()
    {
        MoveEnemy();
        if (_inMove == true) // Esta moviendose?
        {
            Debug.Log("El ENEMY se esta moviendo");
        }
        else 
        {
            Debug.Log("El ENEMY se detuvo");
        }

    }
}

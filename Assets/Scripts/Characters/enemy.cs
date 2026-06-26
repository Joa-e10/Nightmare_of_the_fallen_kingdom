using System.Collections;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class enemy : characters
{
    private float _delay = 0;
    private int _contAttack = 0;
    [SerializeField]private GameObject _weaponMeele;
    private NavMeshAgent _agent;
    protected float distanceToPlayer;
    protected float detectionRadius = 30;
    private Transform _player;
    private Player _playerScript;
    private Transform _rangeCheck;
    private bool _isAttacking;

    public float rangeDistance = 2f;
    private RaycastHit _hit;
    public LayerMask hitLayer;

    void Start()
    {
        _rangeCheck = GameObject.Find("RangeCheck").GetComponent<Transform>(); //Tomamos el Transform del objeto RANGECHECK.
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.Find("Player").GetComponent<Transform>();
        _playerScript = GetComponent<Player>();//Tomamos el Transform del objeto PLAYER.
        _agent.speed = _speed;//Cambiamos la velocidad del agente.
    }

    public void MoveEnemy() //Movimiento del enemigo.
    {
        distanceToPlayer = Vector3.Distance(transform.position, _player.position); // Distancia del player con respecto al enemy.

        if (distanceToPlayer < detectionRadius)//La distancia del player es menor a radio?
        {
            _inMove = true;
            _agent.SetDestination(_player.position); // Lo dirigimos hasta la posicion del PLAYER

            if (distanceToPlayer <= 2)
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

    IEnumerator Delay() 
    {
        yield return new WaitForSeconds(2f);
    }
    public void AttackEnemy() //Ataque del enemigo.
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
        if (_isAttacking == false)
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
        Debug.Log("Delay: "+_delay);
       
    }
}

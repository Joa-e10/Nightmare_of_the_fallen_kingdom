using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : characters
{
    public List<Transform> ListContactPlayers1 = new List<Transform>();
    Collider[] hitColliders = new Collider[4];

    float distance1 = 20f;
    float distance2 = 20f;
    protected float _delay = 0;
    [SerializeField]protected GameObject _weaponMeele;
    protected NavMeshAgent _agent;
    protected float distanceToPlayer;
    protected float detectionRadius = 20f;
    protected Transform _player;//Este se reemplaza por ---> Una variable nueva.
    protected Transform _newTarget;
    protected Player _playerScript;
    protected Transform _rangeCheck;
    protected Transform _rangeTarget;
    // protected Transform _convertToPosition;
    int _cantColliders;
    public float rangeDistance = 2f;
    protected RaycastHit _hit;
    public LayerMask hitLayer;

    //Level System
    [Header("Exp Reward")]
    [SerializeField] protected int _xpReward = 20;

    public abstract void MoveEnemy(); //Movimiento del enemigo.
    public abstract void AttackEnemy(); //Ataque del enemigo.

    public void TakeDamage(int amount) // Usamos este metodo en public para que pueda ser llamado y bajar vida.
    {
        _currentHealth -= amount; // Se resta el daño de la cantidad de vida actual.
        Debug.Log("Vida actual: " + _currentHealth); // Mostramos en consola cuánta vida queda.

        if (_currentHealth <= 0) // Cuando la vida menor o igual a cero morimos.
        {
            Die(); // morimos.
        }
    }
    public void Target() 
    {   
        _cantColliders = Physics.OverlapSphereNonAlloc(transform.position, detectionRadius, hitColliders, hitLayer);
        int indexp = 0;

        for (int i = 0; i < _cantColliders; i++)
        {
          _player = hitColliders[i].GetComponent<Transform>();

              if (_cantColliders != 1 && _cantColliders > 0)
              {

                if (distance1 != distance2)
                {
                    Debug.Log("Las distancias son diferentes!");

                    //_player = hitColliders[i].GetComponent<Transform>();
                    distance2 = Vector3.Distance(transform.position, _player.position);

                    if (distance1 < distance2)
                    {
                        Debug.Log("Distancia1 es menor a distancia2");
                        _player = hitColliders[indexp].GetComponent<Transform>();

                        //distance2 = detectionRadius;
                        _newTarget = _player;
                        distance2 = detectionRadius;
                        distance1 = detectionRadius;
                    }
                    else
                    {
                        indexp = i;
                        Debug.Log("Distancia2 es menor a distancia1");

                        _player = hitColliders[indexp].GetComponent<Transform>();
                        //distance1 = Vector3.Distance(transform.position, _player.position);
                        _newTarget = _player;
                        distance1 = detectionRadius;
                        distance2 = detectionRadius;
                    }
                }
                else
                {
                    Debug.Log("Las distancias siguen en el mismo lugar.");

                    //_player = hitColliders[i].GetComponent<Transform>();
                    distance1 = Vector3.Distance(transform.position, _player.position);

                    //_newTarget = _player;

                    indexp = i;
                }
              }
              else
              {
                Debug.Log("TENEMOS UN SOLO PLAYER EN LA DETECCION");
                _newTarget = _player;
                //Debug.Log("Colinsiones dentro: " + hitColliders[i]);
              }
        }

       // Debug.Log("Hay un total de: " + _cantColliders + " de players en la colision");
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    /// <summary>
    /// </summary>
    public virtual void Die()
    {
        Player killer = null;
        // Si no se especificó un jugador, intentamos obtener el script del último objetivo enfocado
        if (killer == null && _newTarget != null)
        {
            killer = _newTarget.GetComponent<Player>();
        }

        // Le otorgamos la XP al jugador si se encontró
        if (killer != null)
        {
            killer.AddXP(_xpReward);
        }
        Debug.Log("El killer es: "+killer);
        GetComponent<NetworkObject>().Despawn(true);//Se destruye el item.
    }
}
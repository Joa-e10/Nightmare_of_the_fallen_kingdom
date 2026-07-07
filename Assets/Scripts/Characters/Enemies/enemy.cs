using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
public abstract class Enemy : characters
{
    //public List<Transform> ListContactPlayers = new List<Transform>();

    float distance1 = 20f;
    float distance2 = 20f;
    protected float _delay = 0;
    [SerializeField]protected GameObject _weaponMeele;
    protected NavMeshAgent _agent;
    protected float distanceToPlayer;
    protected float detectionRadius = 7f;
    [SerializeField]protected Transform _player;//Este re reemplaza por ---> Una variable nueva.
    protected Transform _newTarget;
    protected Player _playerScript;
    protected Transform _rangeCheck;
    protected Transform _rangeTarget;

    public float rangeDistance = 2f;
    protected RaycastHit _hit;
    public LayerMask hitLayer;
    public abstract void MoveEnemy(); //Movimiento del enemigo.
    public abstract void AttackEnemy(); //Ataque del enemigo.

    public void Target() 
    {
        _newTarget = _player;
        int countPlayers = 0;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, hitLayer);
       // List<Transform> ListContactPlayers = new List<Transform>();
        foreach (var hitCollider in hitColliders)
        {

           Player hitPlayer = hitCollider.GetComponent<Player>();
            if (hitPlayer != null)
            {
                List<Transform> ListContactPlayers = new List<Transform>();

                _player = hitCollider.GetComponent<Transform>();
                ListContactPlayers.Add(_player);

                countPlayers = ListContactPlayers.Count;
                if (countPlayers == 1)
                {
                    
                    _newTarget = _player;

                }
                else
                {
                    int indexp = 0;
                    for (int i = 0; i <= countPlayers; i++)
                    {
                        
                        if (distance1 != distance2)
                        {
                            Debug.Log("Las distancias son diferentes!");
                            distance2 = Vector3.Distance(transform.position, ListContactPlayers[i].position);
                            if (distance1 < distance2)
                            {
                                
                                Debug.Log("Distancia1 es menor a distancia2");
                                _newTarget = ListContactPlayers[indexp];
                                distance2 = detectionRadius;
                            }
                            else
                            {
                                Debug.Log("Distancia2 es menor a distancia1");
                                _newTarget = ListContactPlayers[i];

                                  distance1 = distance2;
                                  indexp = i;
                                  distance2 = detectionRadius;
                            }
                        }
                        else
                        {
                            Debug.Log("Las distancias siguen en el mismo lugar.");
                            distance1 = Vector3.Distance(transform.position, ListContactPlayers[i].position);
                             indexp = i;
                        }
                        Debug.Log("Colinsiones dentro: " + hitColliders[i]);
                    }
                }
                Debug.Log("Hay un total de: " + countPlayers + " de players en la colision");
            }
            else 
            {
                if (countPlayers > 0) 
                {
                    countPlayers = 0;
                }
            }
        }

        
    }
}

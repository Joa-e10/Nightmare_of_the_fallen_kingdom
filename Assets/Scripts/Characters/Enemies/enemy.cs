using System.Collections.Generic;
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
    public abstract void MoveEnemy(); //Movimiento del enemigo.
    public abstract void AttackEnemy(); //Ataque del enemigo.

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
}

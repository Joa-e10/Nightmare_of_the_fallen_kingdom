using System.Collections;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
public abstract class Enemy : characters
{
    protected float _delay = 0;
    [SerializeField]protected GameObject _weaponMeele;
    protected NavMeshAgent _agent;
    protected float distanceToPlayer;
    protected float detectionRadius = 30;
    protected Transform _player;
    protected Player _playerScript;
    protected Transform _rangeCheck;

    public float rangeDistance = 2f;
    protected RaycastHit _hit;
    public LayerMask hitLayer;
    public abstract void MoveEnemy(); //Movimiento del enemigo.
    public abstract void AttackEnemy(); //Ataque del enemigo.
}

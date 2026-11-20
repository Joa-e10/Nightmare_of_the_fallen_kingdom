using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class OffensiveSkill : Skill
{
    [SerializeField] private OffensiveData _data;
    private int _damage;
    private Vector3 _direction;
    private Transform _transformPlayer;
    private Rigidbody _rb;
    private float _delayDirection;
    private int _speed;

    private void OnEnable()
    {
        _cooldown = _data.cooldown;

        _rb = GetComponent<Rigidbody>();
        _speed = _data.speed;
        _delayDirection = _data.timeDirection;
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy != null)
        {
            Debug.Log("Le hicimos daño al enemigo con: "+_damage+ "de daño");
            enemy.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }

    public void setDirectionSkill(Vector3 direction)
    {
        _direction = direction;
    }

    public void setPosition(Transform transformPlayer)
    {
        _transformPlayer = transformPlayer;
    }
    void Update()
    {
        _rb.linearVelocity = _direction * _speed;

        _delayDirection -= Time.deltaTime;

        if (_delayDirection <= 0)
        {
            Destroy(gameObject);
            _delayDirection = 1.5f;
        }
    }

    private void DamageUpgrades(Player playerComponent)
    {
        PlayerStats stats = playerComponent.GetComponent<PlayerStats>();
        if (stats == null) return;

        foreach (KeyValuePair<string, int> attribute in stats.GetAttributes())
        {
            if (attribute.Key == "Mana")
            {
                _damage = _data.damage;
                Debug.Log("Se le suma al attaque: " + attribute.Value + "de Mana");
                _damage = _damage + attribute.Value;
            }
            else
            {
                Debug.Log("El atributo: " + attribute.Key + "no coincide.");
            }
        }
    }

    public override void ActivateAbility(Player playerOwner)
    {
        GameObject skillSpawn = Instantiate(_data._skillObject, _transformPlayer.position, Quaternion.identity);
        OffensiveSkill component = skillSpawn.GetComponent<OffensiveSkill>();
        component.setDirectionSkill(_direction);
        component.DamageUpgrades(playerOwner);
    }
}

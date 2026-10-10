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
        _damage = _data.damage;
        _speed = _data.speed;
        _delayDirection = _data.timeDirection;
    }
    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy != null)
        {
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

    public override void ActivateAbility()
    {
        GameObject skillSpawn = Instantiate(_data._skillObject, _transformPlayer.position, Quaternion.identity);
        OffensiveSkill component = skillSpawn.GetComponent<OffensiveSkill>();
       component.setDirectionSkill(_direction);
    }
}

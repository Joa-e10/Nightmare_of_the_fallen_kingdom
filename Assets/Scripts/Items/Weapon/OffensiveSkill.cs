using UnityEngine;

public class OffensiveSkill : Skill
{
    private int _damage;
    private Vector3 _direction;
    private Rigidbody _rb;
    private float _delayDirection;
    private int _speed;

    private void OnEnable()
    {
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
    void Update()
    {
        _rb.linearVelocity = _direction * _speed;

        _delayDirection -= Time.deltaTime;


        // Si se cumple la condicion, la bala se destruira y el tiempo vuelve a ser de valor 1.5
        if (_delayDirection <= 0)
        {
            Destroy(gameObject);
            _delayDirection = 1.5f;
        }
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class player : characters
{
    [SerializeField] private float _playerSpeed = 5f;
    [SerializeField] private GameObject _attackPoint;
    [SerializeField] private float _damage = 20f;
    [SerializeField] private float _attackRange = 2f;
    [SerializeField] private LayerMask _targetLayer;

    private float _currentHealth = 100f;

    void Start()
    {
        _speed = (int)_playerSpeed;
    }

    private void OnAttack(InputValue value)
    {
        if (value.isPressed && _attackPoint != null) PerformAttack();
    }

    private void PerformAttack()
    {
        if (Physics.Raycast(_attackPoint.transform.position, _attackPoint.transform.forward, out RaycastHit hit, _attackRange, _targetLayer))
        {
            if (hit.transform.TryGetComponent(out characters target))
            {
                target.TakeDamage(_damage);
            }
        }
    }

    public override void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        if (_currentHealth <= 0) Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(_attackPoint.transform.position, _attackPoint.transform.forward * _attackRange);
    }
}
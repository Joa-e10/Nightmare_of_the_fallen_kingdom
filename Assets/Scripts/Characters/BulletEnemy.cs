using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    private int _damage = 15;
    private float _delayBullet = 1.5f;
    private Vector3 _direction;
    private int speed = 10;
    private Rigidbody _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody>(); // Toma el componente rigidbody2D del objeto.
    }

    public void setDirectionBullet(Vector3 direction)
    {
        _direction = direction;
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(_damage);
            Destroy(gameObject);
        }
        else 
        {
        }
    }

    void Update()
    {
        _rb.linearVelocity = _direction * speed;

        _delayBullet -= Time.deltaTime;


        // Si se cumple la condicion, la bala se destruira y el tiempo vuelve a ser de valor 1.5
        if (_delayBullet <= 0)
        {
            Destroy(gameObject);
            _delayBullet = 1.5f;
        }
    }
}

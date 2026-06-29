using UnityEngine;

public class SwordEnemy : MonoBehaviour
{
    private BoxCollider _bc;
    private Player _player;
    private int _damage = 50;
    void Start()
    {
        _bc = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
       Player player = other.GetComponent<Player>();
        if (player != null) 
        {
            player.TakeDamage(_damage);
        }
    }

    void Update()
    {
        
    }
}

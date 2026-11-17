using UnityEngine;

public class WeaponEnemyMele : MonoBehaviour
{
    [SerializeField]private int _damage;

    private void OnEnable()
    {
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

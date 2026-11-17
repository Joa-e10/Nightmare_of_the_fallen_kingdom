
using UnityEngine;

public class WeaponPlayerMele : MonoBehaviour
{
    int _damageWeapon = 20;
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        var damageable = collision.gameObject.GetComponent<IDamageable>();


        if (damageable != null)
        {
            damageable.TakeDamage(_damageWeapon);
        }
        else
        {
            Debug.Log("Es nulo pibe");
        }
        Debug.Log("La colision es: " + damageable);
    }

    void Update()
    {
        
    }
}

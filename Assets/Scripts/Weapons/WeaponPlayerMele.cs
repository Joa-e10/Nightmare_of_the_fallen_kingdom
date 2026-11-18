
using System.Collections.Generic;
using UnityEngine;

public class WeaponPlayerMele : MonoBehaviour
{
    [SerializeField] private Player _playerOwner;
    private int _damageWeapon;
    void Start()
    {
        
    }

    private void DamageUpgrades() 
    {
        foreach (KeyValuePair<string, int> attribute in _playerOwner.GetAttributes()) 
        {
            if (attribute.Key == "Attack")
            {
                _damageWeapon += attribute.Value;
            }
            else
            {
                Debug.Log("El atributo: "+attribute.Key+ "no coincide.");
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        DamageUpgrades();
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
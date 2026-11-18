using System.Collections.Generic;
using UnityEngine;

public class WeaponPlayerMele : MonoBehaviour
{
    [SerializeField] private Player _playerOwner;
    private int _damageWeapon = 0;
    void Start()
    {
        
    }

    private void DamageUpgrades()
    {
        foreach (KeyValuePair<string, int> attribute in _playerOwner.GetAttributes()) 
        {
            if (attribute.Key == "Attack")
            {
                _damageWeapon = _damageWeapon + attribute.Value;
            }
            else
            {
                Debug.Log("El atributo: "+attribute.Key+ "no coincide.");
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("La colision esta entrando");
        var damageable = collision.gameObject.GetComponent<IDamageable>();

        if (damageable != null)
        {
            DamageUpgrades();
            damageable.TakeDamage(_damageWeapon);
            _damageWeapon = 0;
        }
        else
        {
        }
    }

    void Update()
    {
        
    }
}
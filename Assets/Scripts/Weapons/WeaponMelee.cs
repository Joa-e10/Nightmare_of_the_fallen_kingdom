using Unity.VisualScripting;
using UnityEngine;

public class WeaponMelee : MonoBehaviour
{
    int _damageWeapon = 20;
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();

        if (enemy != null)
        {
            Debug.Log("Attaca a un enemigo");
            enemy.TakeDamage(_damageWeapon);
            gameObject.SetActive(false);
        }
        else 
        {
            Debug.Log("Es nulo pibe");
        }
        Debug.Log("La colision es: " + enemy);
    }

    void Update()
    {
        
    }
}

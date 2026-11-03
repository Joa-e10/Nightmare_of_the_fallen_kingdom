using UnityEngine;
using Unity.Netcode;

public abstract class characters : NetworkBehaviour, IDamageable
{
    //Atributos de los personajes
    public bool _inMove { get; protected set; }
    protected bool _isDie;
    public int _speed = 4;
    protected NetworkVariable<int> _maxHealth = new NetworkVariable<int>();
    [SerializeField] protected int _currentHealth = 100; // Permite controlar la vida del player.

    public int getlives()//Metodo GET para enviar el valor actual de "_currentHealth"
    {
        return _currentHealth;
    }

    public void setlives(int amount)//Metodo SET para actualizar el valor de "_currentHealth"
    {
        _currentHealth = _currentHealth + amount;
    }

    public void TakeDamage(int amount) // Usamos este metodo en public para que pueda ser llamado y bajar vida.
    {
        _currentHealth -= amount; // Se resta el daño de la cantidad de vida actual.
        Debug.Log("Vida actual: " + _currentHealth); // Mostramos en consola cuánta vida queda.

        if (_currentHealth <= 0) // Cuando la vida menor o igual a cero morimos.
        {
            Die(); // morimos.
        }
    }

    protected abstract void Die();
}

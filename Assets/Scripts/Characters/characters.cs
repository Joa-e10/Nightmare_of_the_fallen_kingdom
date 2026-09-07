using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public abstract class characters : NetworkBehaviour
{
    //Atributos de los personajes
    protected bool _inMove;
    public int _speed = 4;
    //protected int _maxHealth = 100; // Se declara la vida maxima y se hace visible en el inspector.
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
    /*public void TakeDamage(int amount) // Usamos este metodo en public para que pueda ser llamado y bajar vida.
    {
        _currentHealth -= amount; // Se resta el daño de la cantidad de vida actual.
        Debug.Log("Vida actual: " + _currentHealth); // Mostramos en consola cuánta vida queda.

        if (_currentHealth <= 0) // Cuando la vida menor o igual a cero morimos.
        {
            Die(); // morimos.
        }
    }*/
    public void Die()
    {
        if (_currentHealth <= 0) 
        {
            GetComponent<NetworkObject>().Despawn(true);//Se destruye el character.
            SceneManager.LoadScene("SampleScene");
        }  
    }
}

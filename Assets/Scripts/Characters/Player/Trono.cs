using UnityEngine;
using UnityEngine.SceneManagement;

public class Trono : MonoBehaviour
{

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other) //Metodo con el que verificamos la colision de entrada.
    {
        Player player = other.gameObject.GetComponent<Player>(); // Tomamos el componente "Item" del objeto colisionado, en caso de tenerlo.

        if (player != null) //Si "item" tiene un valor distinto de null.
        {
            SceneManager.LoadScene("SampleScene");
        }


    }
    void Update()
    {
        
    }
}

using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;
public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject _potionPrefab;
    [SerializeField] private GameObject _helmetPrefab;
    [SerializeField] private GameObject _EnemyEye;
    [SerializeField] private GameObject _EnemyShoot;
    void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
    }

    private void OnClientConnected(ulong clientId) 
    {
        Debug.Log("Se conecto el jugador nro: " + clientId);
        if (clientId == 0)
        {
            Debug.Log("Tenemos dentro a un HOST");
            //Iniciar la escena o dejarlo en un lobby???
        }
        else
        {
            Debug.Log("Tenemos dentro a un CLIENTE");
            //Iniciar la escena en este punto de ejecucion?
        }
    }

    public void GameHostStart()
    {
        NetworkManager.Singleton.StartHost(); //Iniciamos el Host.
    }

    public void GameClientStart()
    {
        NetworkManager.Singleton.StartClient(); //Iniciamos como client.
    }

    public void InstantiateObject()
    {
        GameObject item1 = Instantiate(_potionPrefab);
        GameObject item2 = Instantiate(_helmetPrefab);

        item1.GetComponent<NetworkObject>().Spawn();
        item2.GetComponent<NetworkObject>().Spawn();
        Debug.Log("Spawnearon objetos en escena");
    }

    public void InstantiateEnemies() 
    {
        GameObject enemy1 = Instantiate(_EnemyEye);
        GameObject enemy2 = Instantiate(_EnemyShoot);

        enemy1.GetComponent<NetworkObject>().Spawn();
        enemy2.GetComponent<NetworkObject>().Spawn();
        Debug.Log("Spawnearon objetos en escena");
    }

    void Update()
    {
        if (Keyboard.current.hKey.wasPressedThisFrame)
        {
            NetworkManager.Singleton.StartHost(); //Iniciamos el Host.
            InstantiateEnemies();
            InstantiateObject();
        }
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            NetworkManager.Singleton.StartClient(); //Iniciamos como client

        }
    }
}

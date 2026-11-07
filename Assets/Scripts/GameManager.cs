using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject _potionPrefab;
    [SerializeField] private GameObject _helmetPrefab;
    [SerializeField] private GameObject _axePrefab;
    [SerializeField] private GameObject _EnemyEye;
    [SerializeField] private GameObject _EnemyShoot;
    [SerializeField] private GameObject trackerContainerUI;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }
    }

    void Start()
    {
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        }
    }

    private void OnDisable()
    {
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
        }
    }

    private void OnClientConnected(ulong clientId)
    {
        Debug.Log("Se conecto el jugador nro: " + clientId);
        if (clientId == 0)
        {
            Debug.Log("Tenemos dentro a un HOST");
        }
        else
        {
            Debug.Log("Tenemos dentro a un CLIENTE");
        }
    }

    public void GameHostStart()
    {
        NetworkManager.Singleton.StartHost();
        if (trackerContainerUI != null)
        {
            trackerContainerUI.SetActive(true);
        }
    }

    public void GameClientStart()
    {
        NetworkManager.Singleton.StartClient();
        if (trackerContainerUI != null)
        {
            trackerContainerUI.SetActive(true);
        }
    }

    public void InstantiateObject()
    {
        GameObject item1 = Instantiate(_potionPrefab);
        GameObject item2 = Instantiate(_helmetPrefab);
        GameObject item3 = Instantiate(_axePrefab);

        item1.GetComponent<NetworkObject>().Spawn();
        item2.GetComponent<NetworkObject>().Spawn();
        item3.GetComponent<NetworkObject>().Spawn();
        Debug.Log("Spawnearon objetos en escena");
    }

    public void InstantiateEnemies()
    {
        GameObject enemy1 = Instantiate(_EnemyEye);
        GameObject enemy2 = Instantiate(_EnemyShoot);

        enemy1.GetComponent<NetworkObject>().Spawn();
        enemy2.GetComponent<NetworkObject>().Spawn();
        Debug.Log("Spawnearon enemigos en escena");
    }

    void Update()
    {
        if (Keyboard.current.hKey.wasPressedThisFrame)
        {
            GameHostStart();
            InstantiateEnemies();
            InstantiateObject();
        }
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            GameClientStart();
        }
    }
}
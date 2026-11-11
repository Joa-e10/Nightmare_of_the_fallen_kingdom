using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _potionPrefab;
    [SerializeField] private GameObject _helmetPrefab;
    [SerializeField] private GameObject _EnemyEye;
    [SerializeField] private GameObject _EnemyShoot;
    [SerializeField] private CanvasManager _canvasManager;

    [Header("Paneles UI")]
    [SerializeField] private GameObject _panelMainMenu;
    [SerializeField] private GameObject _panelOptions;


    [Header("Botones")]
    [SerializeField] private GameObject _hostButton;
    [SerializeField] private GameObject _clientButton;
    [SerializeField] private GameObject _startGame;
    [SerializeField] private GameObject _playButtonObject;
    [SerializeField] private GameObject _OptionButtonObject;
    [SerializeField] private GameObject _exitButtonObject;
    [SerializeField] private GameObject _backButtonObject;

    [Header("Componentes Button")]
    [SerializeField] private Button _startHostButton;
    [SerializeField] private Button _startClientButton;
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _OptionsButton;
    [SerializeField] private Button _ExitButton;

    [Header("Texto/Input")]
    [SerializeField] private GameObject _InputField;
    [SerializeField] private TMP_InputField _clientInputField;

    private void OnEnable()
    {
        _playButton.onClick.AddListener(PlayActiveButtons);
        _OptionsButton.onClick.AddListener(PanelOptionActive);
        _startHostButton.onClick.AddListener(StartHost);
        _startClientButton.onClick.AddListener(ShowLoadIp);
        _startGameButton.onClick.AddListener(StartGame);
        _OptionsButton.onClick.AddListener(PanelOptionActive);
        _ExitButton.onClick.AddListener(CloseTheGame);
    }

    public void OnClientConnected(ulong clientId) 
    {

        Debug.Log("Se conecto el jugador nro: " + clientId);

    }

    public void PlayActiveButtons() 
    {
        _hostButton.SetActive(true);
        _clientButton.SetActive(true);
        _playButtonObject.SetActive(false);
    }

    public void PanelOptionActive()
    {
        _panelOptions.SetActive(true);
        _panelMainMenu.SetActive(false);
    }

    public void CloseTheGame() 
    {
        Application.Quit();
    }

    public void StartHost() 
    {
        NetworkManager.Singleton.StartHost();
        //InstantiateEnemies();
        //InstantiateObject();
        _panelMainMenu.SetActive(false);
        _canvasManager.PanelHudActive(false);
    }

    public void ShowLoadIp() 
    {
        _InputField.SetActive(true);
        _startGame.SetActive(true);

        _hostButton.SetActive(false);
        _clientButton.SetActive(false);
    }

    public void StartGame() 
    {
        if (_clientInputField != null)
        {
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(_clientInputField.text, (ushort)7777);
            NetworkManager.Singleton.StartClient();
            _panelMainMenu.SetActive(false);
            _canvasManager.PanelHudActive(false);
        }
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
        
    }
}

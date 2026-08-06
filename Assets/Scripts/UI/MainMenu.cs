using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{


    [Header("Paneles UI")]
    [SerializeField] private GameObject _panelMainMenu;

    [Header("Botones")]
    [SerializeField] private GameObject _hostButton;
    [SerializeField] private GameObject _clientButton;
    [SerializeField] private GameObject _startGame;

    [Header("Componentes Button")]
    [SerializeField] private Button _startHostButton;
    [SerializeField] private Button _startClientButton;
    [SerializeField] private Button _startGameButton;

    [Header("Texto/Input")]
    [SerializeField] private GameObject _InputField;
    [SerializeField] private TMP_InputField _clientInputField;

    private void OnEnable()
    {
        _startHostButton.onClick.AddListener(StartHost);
        _startClientButton.onClick.AddListener(ShowLoadIp);
        _startGameButton.onClick.AddListener(StartGame);
    }

    public void OnClientConnected(ulong clientId) 
    {

        Debug.Log("Se conecto el jugador nro: " + clientId);

    }
    void Start()
    {
        
    }

    public void StartHost() 
    {
        NetworkManager.Singleton.StartHost();
        _panelMainMenu.SetActive(false);
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
        }
    }

    void Update()
    {
        
    }
}

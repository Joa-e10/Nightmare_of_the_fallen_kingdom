using System;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelReturnUI : MonoBehaviour
{
    [SerializeField] private GameObject _panelReturn;
    [SerializeField] private GameObject _panelHud;
    [SerializeField] private Button _buttonReplay;
    [SerializeField] private TextMeshProUGUI _titleText;
    private string _winnerText = "¡Thanks for playing!";
    private string _loserText = "You Lose";
    [SerializeField] private Button _buttonExit;
    private NetworkObject _playerObject;
    private Player _player;
    private bool _isDie;

    public static event Action OnRespawnPlayer;
    void OnEnable()
    {
        _buttonReplay.onClick.AddListener(PlayerReplay);
        _buttonExit.onClick.AddListener(ReturnToMainMenu);

        TakeOwner();
    }

    void OnDisable()
    {
    }

    protected void TakeOwner()

    {
        foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
        {
            if (client.PlayerObject.IsOwner)
            {
                _playerObject = client.PlayerObject;
                _player = _playerObject.GetComponent<Player>();
            }
        }
    }

    public void RefreshTextEnd() 
    {

        if (_player.GetStateDie())
        {
            Debug.Log("Estamos mueltos pa");
            _titleText.text = _loserText;
        }
        else
        {
            Debug.Log("Estamos ganando pa");
            _titleText.text = _winnerText;
        }
    }

    public void PlayerReplay() 
    {
        _player.SetStateDie(true);
        _player.setlives(100);
        _player.SetPositionRespawn(_playerObject);
        _panelReturn.SetActive(false);
        _panelHud.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainScene");
        NetworkManager.Singleton.Shutdown();
    }

     void Update()
    {
    }
}

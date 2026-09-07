using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelHudUI : MonoBehaviour
{
    private Player _player;
    [SerializeField] private Slider _healthBar;
    private NetworkObject _playerObject;
    [SerializeField] private Slider _expBar;
    [SerializeField] private TextMeshProUGUI _level;
    public void TakeOwner()

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
    private void RefreshHealthBar()
    {
        TakeOwner();
        _healthBar.value = _player.getlives();
    }
        private void RefresexpBar()
    {
        TakeOwner();
        _level.text = _player.currentLevel.ToString();
        _expBar.value = _player.currentXP;
    }
    void Update()
    {
        RefreshHealthBar();
    }
}

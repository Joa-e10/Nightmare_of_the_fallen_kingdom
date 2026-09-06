using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class PanelHudUI : MonoBehaviour
{
    private Player _player;
    [SerializeField] private Slider _healthBar;
    private NetworkObject _playerObject;

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
    void Update()
    {
        RefreshHealthBar();
    }
}

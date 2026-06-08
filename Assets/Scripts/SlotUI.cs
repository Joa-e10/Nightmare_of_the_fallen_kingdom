using TMPro;
using Unity.Netcode;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
   public TextMeshProUGUI _textname;
    public TextMeshProUGUI _textamount;
    public Image _icon;
    public ItemData _itemData;
    private NetworkClient _client;
    public NetworkObject _playergo;
    public Player _player;
    void Start()
    {

    }

    public void TakeClient()
    {
        foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
        {
            _client = client;
            _playergo = client.PlayerObject;
            _player = client.PlayerObject.GetComponent<Player>();
        }
    }

    public void ButtomUse() 
    {
        TakeClient();
        _itemData.ItemUse(_playergo);
        Destroy(gameObject);
    }

    void Update()
    {
        
    }
}

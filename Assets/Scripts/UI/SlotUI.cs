using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
   public TextMeshProUGUI _textname;
    public TextMeshProUGUI _textamount;
    public Image _icon;
    public ItemData _itemData;
    public NetworkObject _playerObject;
    [SerializeField]private InventoryUI _playerInventoryUi;
    public Player _player;
    [SerializeField] private Button _buttonUse;

     void OnEnable()
    {
        _buttonUse.onClick.AddListener(ButtomUse);
        _playerInventoryUi = GameObject.Find("BackgroundInventory").GetComponent<InventoryUI>();
    }
    void OnDisable()
    {
        _buttonUse.onClick.RemoveListener(ButtomUse);
    }

    public void TakeOwner()
    {
        if (_playerObject == null) 
        {
            foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
            {
                if (client.PlayerObject.IsOwner)
                {
                    _playerObject = client.PlayerObject;
                    _player = client.PlayerObject.GetComponent<Player>();
                }
            }
        }
    }

    public void ButtomUse() 
    {
        Item itemComponent;

        TakeOwner();
        itemComponent = _itemData._itemPrefab.GetComponent<Item>();
        bool wasUsed = itemComponent.ItemAction(_playerObject);

        if (wasUsed)
        {
            _playerInventoryUi.RefreshPanel_UI();
        }
    }
}

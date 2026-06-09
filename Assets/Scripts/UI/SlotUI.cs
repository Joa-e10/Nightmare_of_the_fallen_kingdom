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
    private Inventory _playerInventory;
    private InventoryUI _playerInventoryUi;
    public Player _player;
    public void TakeOwner()
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

    public void ButtomUse() 
    {
        TakeOwner();
        _itemData.ItemUse(_playerObject);
        if ( _player.GetUsingItem() == true)
        {
            _playerInventory = _playerObject.GetComponent<Inventory>();
            _playerInventoryUi = _playerObject.GetComponent<InventoryUI>();
            _playerInventory.UpdateItem(_itemData);
            _playerInventoryUi.RefreshInventoryUI();
            
        }
        else
        {
            
        }
    }
}

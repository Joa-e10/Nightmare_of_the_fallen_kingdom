using Unity.Netcode;
using UnityEngine;

public class ConsumableItem : Item
{
    [SerializeField] private int _amountHeal;
    [SerializeField] private InventoryUI _inventoryUi;

    public override bool ItemAction(NetworkObject PlayerNet)
    {
        Player PlayerP = PlayerNet.GetComponent<Player>();

        if (PlayerP.getlives() >= 100)
        {
            Debug.Log("NO se usa el item");
            return false;
        }
        else
        {
            Inventory _playerInventory = PlayerNet.GetComponent<Inventory>();
            Debug.Log("SI se usa el item");

            PlayerP.setlives(_amountHeal);
            _playerInventory.UpdateItem(_itemData, 1);
            return true;
        }
    }
} 
    
  



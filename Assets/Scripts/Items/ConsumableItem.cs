using Unity.Netcode;
using UnityEngine;

public class ConsumableItem : Item
{
    [SerializeField] private ConsumableData _consumableData;
    public int _amountUpgrade;
    [SerializeField] private InventoryUI _inventoryUi;

    public override bool ItemAction(NetworkObject PlayerNet)
    {

        Player PlayerP = PlayerNet.GetComponent<Player>();
        Inventory _playerInventory = PlayerNet.GetComponent<Inventory>();
        if (_itemData._type.ToString() == "Regenerate")
        {

            if (PlayerP.getlives() >= 100)
            {
                Debug.Log("NO se usa el item");
                return false;
            }
            else
            {
                Debug.Log("SI se usa el item");

                PlayerP.setlives(_consumableData.amount);
                _playerInventory.UpdateItem(_itemData, 1);
                return true;
            }
        }
        else 
        {
            if (PlayerP.GetAttributes().ContainsKey(_consumableData.attributeType.ToString()))
            {
                PlayerP.UpgradeAttributes(_consumableData.attributeType.ToString(), _consumableData.amount);

                PlayerP.BuffAttributes(_consumableData.attributeType.ToString(), _consumableData.amount);
                Debug.Log("Suma la cantidad de: "+ _consumableData.amount + " cantidad a: "+ _consumableData.attributeType.ToString());
                _playerInventory.UpdateItem(_itemData, 1);
            }
            return true;
        }
    }
} 
    
  



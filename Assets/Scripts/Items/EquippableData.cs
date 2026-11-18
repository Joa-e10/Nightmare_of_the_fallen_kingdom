using UnityEngine;
using Unity.Netcode;
[CreateAssetMenu(fileName = "EquippableData", menuName = "Scriptable Objects/EquippableData")]
public class EquippableData : ItemData
{
    public int idEquipItem;
    public enum AtributteType { Attack, Mana, Defense };
    public AtributteType enhancedAttribute;
    public int upgradeValue;
    public string bodyPart;
    public GameObject prefabEquip;
   /* public override void ItemUse(NetworkObject PlayerNet)
    {
        Inventory PlayerInventory = PlayerNet.GetComponent<Inventory>();
        PlayerInventory.Equip(prefabSpawn, bodyPart, PlayerNet);
        EquippableItem it = _itemPrefab.GetComponent<EquippableItem>();
        it.ItemAction(PlayerNet);

    }*/
}

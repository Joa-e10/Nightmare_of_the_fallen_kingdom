using Unity.Netcode;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ItemData")]
public abstract class ItemData : ScriptableObject
{
    public string _name;
    public int _accumulatedAmount;
    public string _description;
    public enum ItemType {consumable, equippable, crafting};
    public ItemType _type;
    public Sprite _icon;
    public GameObject _itemPrefab;
    public abstract void ItemUse(NetworkObject playerT);
}

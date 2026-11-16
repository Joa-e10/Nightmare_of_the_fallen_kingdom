using UnityEngine;
[CreateAssetMenu(fileName = "ConsumableData", menuName = "Scriptable Objects/ConsumableData")]
public class ConsumableData : ItemData
{
    public enum consumableType { Regenerate, Buff};
    public consumableType type;
    public int amount;
 public enum Attribute { Attack, Mana, Defense };
    public Attribute attributeType;
    /* public override void ItemUse(NetworkObject PlayerNet)
     {
         ConsumableItem it = _itemPrefab.GetComponent<ConsumableItem>();
         it.ItemAction(PlayerNet);
     }*/
}

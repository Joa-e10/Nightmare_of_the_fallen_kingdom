using UnityEngine;
using Unity.Netcode;
[CreateAssetMenu(fileName = "ConsumableData", menuName = "Scriptable Objects/ConsumableData")]
public class ConsumableData : ItemData
{
    public override void ItemUse(NetworkObject PlayerNet)
    {
        ConsumableItem it = _itemPrefab.GetComponent<ConsumableItem>();
        it.ItemAction(PlayerNet);
    }
}

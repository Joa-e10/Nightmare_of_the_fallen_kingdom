using UnityEngine;
using Unity.Netcode;
[CreateAssetMenu(fileName = "EquippableData", menuName = "Scriptable Objects/EquippableData")]
public class EquippableData : ItemData
{
    public GameObject _itemPrefab;
    public override void ItemUse(NetworkObject playerT)
    {
        Transform t = playerT.transform;
        GameObject item = Instantiate(_itemPrefab, t.position, Quaternion.identity);
        item.transform.parent = t.transform;
    }
}

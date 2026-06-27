using UnityEngine;
using Unity.Netcode;
[CreateAssetMenu(fileName = "EquippableData", menuName = "Scriptable Objects/EquippableData")]
public class EquippableData : ItemData
{
    public GameObject _itemPrefab;
    public override void ItemUse(GameObject PlayerNet)
    {
        Player PlayerP = PlayerNet.GetComponent<Player>();
        Transform T = PlayerNet.transform;
        GameObject Item = Instantiate(_itemPrefab, T.position, Quaternion.identity);
        Item.transform.parent = T.transform;
        PlayerP.SetUsingItem(true);
    }
}

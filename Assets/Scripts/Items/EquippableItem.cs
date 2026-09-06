using Unity.Netcode;
using UnityEngine;

public class EquippableItem : Item
{
    public Transform _playerT;
    public string _bodyPart;
    public GameObject _itemPrefab;
    public uint _itemId;
    private EquippableData _equipData;
    public override void ItemAction(NetworkObject PlayerNet)
    {
        Player PlayerP = PlayerNet.GetComponent<Player>();
        _equipData = _itemData as EquippableData;
        string nameAttribute = _equipData.enhancedAttribute.ToString();
        PlayerP.UpgradeAttributes(nameAttribute, _equipData.upgradeValue);
        Debug.Log("Tenemos nuevo valor!! para: " + nameAttribute);

        PlayerP.SetUsingItem(true);

    }
}
       
    


using Unity.Netcode;
using UnityEditor;
using UnityEngine;

public class EquippableItem : Item
{
    public Transform _playerT;
    public string _bodyPart;
    public GameObject _itemPrefab;
    public uint _itemId;
    private EquippableData _equipData;

    public override void OnNetworkSpawn()
    {
        _itemPrefab = _itemData._itemPrefab;
    }
    public override bool ItemAction(NetworkObject PlayerNet)
    {
        Inventory inventoryPlayer = PlayerNet.GetComponent<Inventory>();
        _equipData = _itemData as EquippableData;
        _bodyPart = _equipData.bodyPart;
        string nameAttribute = _equipData.enhancedAttribute.ToString();
        inventoryPlayer.Equip(_equipData, _equipData._name, PlayerNet);
        Debug.Log("Tenemos nuevo valor!! para: " + nameAttribute);

        return true;
    }
}
       
    


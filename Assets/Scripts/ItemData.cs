using UnityEngine;

[CreateAssetMenu (fileName = "New Item Data",menuName = "ItemData")]
public class ItemData : ScriptableObject
{
    [SerializeField] private string _itemName;
    [SerializeField] private int _itemAmount;
    [SerializeField] private string _itemType;
}

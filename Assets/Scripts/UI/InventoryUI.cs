using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : PanelUI
{
    [Header("Buttons")]
    [SerializeField] private GameObject _inventoryDropB;

    [Header("Button Components")]
    [SerializeField] private Button _DropButtonComponent;

    [Header("Player components")]
    private Inventory _playerInventory;
    [Header("Others")]
    [SerializeField]private GameObject _slotPrefab;
    [SerializeField]private Transform _inventoryLimit;
    [SerializeField] private GameObject _inventoryLimitObject;
    [SerializeField]private Image _backgroundInventory;
    private GameObject  _newSlot;
    [SerializeField] private GameObject _ItemSelected;
    public override void RefreshPanel_UI()
    {
        TakeOwner();

        if (_playerInventory == null)
        {
            _playerInventory = _playerObject.GetComponent<Inventory>();
        }

        _DropButtonComponent.onClick.AddListener(_playerInventory.DropItem);
        foreach (Transform t in _inventoryLimit)
        {
            Destroy(t.gameObject);
        }

        for (int i = 0; i < _playerInventory._inventoryNames.Length; i++)
        {
            if (_playerInventory._inventoryNames[i] != null)
            {
                _newSlot = Instantiate(_slotPrefab, _inventoryLimit);
                SlotUI slotAttributes = _newSlot.GetComponent<SlotUI>();

                slotAttributes._textname.text = _playerInventory._inventoryNames[i]._name;
                slotAttributes._textamount.text = _playerInventory._inventoryQuantity[i].ToString();
                slotAttributes._icon.sprite = _playerInventory._inventoryNames[i]._icon;
                slotAttributes._itemData = _playerInventory._inventoryNames[i];
                Debug.Log("Recorremos la activacion del prefab");
            }
        }
    }

    public override void ActivatedItems_UI()
    {
        if (_backgroundInventory.enabled == true)
        {
            _inventoryDropB.SetActive(true);
            _inventoryLimitObject.SetActive(true);
        }
        else 
        {
            _inventoryDropB.SetActive(false);
            _inventoryLimitObject.SetActive(false);
        }
    }
}
using System.Collections.Generic;
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
    public Dictionary<ItemData, int> _inventory = new Dictionary<ItemData, int>();
    public int _limitStack;
    private int _maxEquipSlot = 1;
    private int _slotsQuantity;

    private void OnEnable()
    {
        ActivatedItems_UI();
    }
    public override void RefreshPanel_UI()
    {
        TakeOwner();

        _slotsQuantity = 0;

        if (_playerInventory == null)
        {
            _playerInventory = _playerObject.GetComponent<Inventory>();
        }

        _inventory = _playerInventory.GetPlayerInventory();

        _DropButtonComponent.onClick.AddListener(_playerInventory.DropItem);
        foreach (Transform t in _inventoryLimit)
        {
            Destroy(t.gameObject);
        }

        foreach (KeyValuePair<ItemData, int> item in _inventory)
        {
            Debug.Log("Entra en el else");
            SlotUI slotAttributes;
            int cantForSlot = item.Value;
            int cont = 0;

            if (item.Key._type.ToString() != "equippable" && item.Value < _limitStack)
            {
                Debug.Log("Entra en el if");
                _newSlot = Instantiate(_slotPrefab, _inventoryLimit);
                slotAttributes = _newSlot.GetComponent<SlotUI>();

                slotAttributes._textname.text = item.Key._name;
                slotAttributes._textamount.text = item.Value.ToString();
                slotAttributes._icon.sprite = item.Key._icon;
                slotAttributes._itemData = item.Key;

                _slotsQuantity++;
            }
            else
            {
                if (item.Key._type.ToString() == "equippable")
                {
                    while (cantForSlot >= _maxEquipSlot)
                    {
                        cantForSlot--;

                        _newSlot = Instantiate(_slotPrefab, _inventoryLimit);
                        slotAttributes = _newSlot.GetComponent<SlotUI>();

                        slotAttributes._textname.text = item.Key._name;
                        slotAttributes._textamount.text = _maxEquipSlot.ToString();
                        slotAttributes._icon.sprite = item.Key._icon;
                        slotAttributes._itemData = item.Key;

                        _slotsQuantity++;
                    }
                }
                else 
                {
                    while (cantForSlot > _limitStack && item.Key._type.ToString() != "equippable")
                    {
                        cantForSlot = cantForSlot - _limitStack;
                        Debug.Log("Entra en el while: " + cont);
                        _newSlot = Instantiate(_slotPrefab, _inventoryLimit);
                        slotAttributes = _newSlot.GetComponent<SlotUI>();

                        slotAttributes._textname.text = item.Key._name;
                        slotAttributes._textamount.text = _limitStack.ToString();
                        slotAttributes._icon.sprite = item.Key._icon;
                        slotAttributes._itemData = item.Key;

                        _slotsQuantity++;
                    }

                    Debug.Log("Sale en el while: ");
                    _newSlot = Instantiate(_slotPrefab, _inventoryLimit);
                    slotAttributes = _newSlot.GetComponent<SlotUI>();

                    slotAttributes._textname.text = item.Key._name;
                    slotAttributes._textamount.text = cantForSlot.ToString();
                    slotAttributes._icon.sprite = item.Key._icon;
                    slotAttributes._itemData = item.Key;

                    _slotsQuantity++;
                }
            }
        }

        _playerInventory.CheckInventorySlots(_slotsQuantity);
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
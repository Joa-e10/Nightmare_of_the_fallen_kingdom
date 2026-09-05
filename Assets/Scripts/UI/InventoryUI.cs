using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]private GameObject _slotPrefab;
    [SerializeField]private Transform _inventoryLimit;
    private Image _backgroundInventory;
    private Inventory _playerInventory;
    private GameObject  _newSlot;
    private Player _player;
   // public Canvas _canvasManager;


    void Start()
    {
    }

    private void OnSubmit(InputValue inputValue)
    {
        _player = GetComponent<Player>();
        _inventoryLimit = _player._inventoryLimit;
        _backgroundInventory = _player._backgroundInventory;
        if (inputValue.isPressed)
        {
            Debug.Log("Esta activando el canvas");
            _backgroundInventory.enabled = true;
            RefreshInventoryUI();
        }
    }

    private void OnCancel(InputValue inputValue) 
    {
        if (inputValue.isPressed) 
        {
            _backgroundInventory.enabled = false;

            foreach (Transform t in _inventoryLimit)
            {
                Debug.Log("Entramos a limpiar");
                Destroy(t.gameObject);
            }
        }
        
    }

    public void RefreshInventoryUI()
    {
            _playerInventory = GetComponent<Inventory>();

            Debug.Log("Entramos para refrescar");
            foreach (Transform t in _inventoryLimit)
            {
                Debug.Log("Entramos a limpiar");
                Destroy(t.gameObject);
            }

            for (int i = 0; i< _playerInventory._inventoryNames.Length;i++) 
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
            /*foreach (ItemData item in _playerInventory._inventoryNames)
            {
                _newSlot = Instantiate(_slotPrefab, _inventoryLimit);
                SlotUI slotAttributes = _newSlot.GetComponent<SlotUI>();
                slotAttributes._textname.text = item.name;
                slotAttributes._textamount.text = item._accumulatedAmount.ToString();
                slotAttributes._icon.sprite = item._icon;
                slotAttributes._itemData = item;
                Debug.Log("Recorremos la activacion del prefab");
            }*/
       
    }

    void Update()
    {
        
    }
}
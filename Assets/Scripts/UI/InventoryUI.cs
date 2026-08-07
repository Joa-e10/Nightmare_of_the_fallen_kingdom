using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
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

            foreach (KeyValuePair<ItemData, int> item in _playerInventory._hud)
            {
                _newSlot = Instantiate(_slotPrefab, _inventoryLimit);
                SlotUI slotAttributes = _newSlot.GetComponent<SlotUI>();
                slotAttributes._textname.text = item.Key._name;
                slotAttributes._textamount.text = item.Value.ToString();
                slotAttributes._icon.sprite = item.Key._icon;
                slotAttributes._itemData = item.Key;
                Debug.Log("Recorremos la activacion del prefab");
            }
       
    }

    void Update()
    {
        
    }
}
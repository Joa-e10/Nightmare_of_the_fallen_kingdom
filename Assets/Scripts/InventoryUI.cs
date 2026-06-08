using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]private GameObject _slotPrefab;
    [SerializeField]private Transform _inventoryLimit;
    [SerializeField]private Canvas _canvasManager;
    private Inventory _playerInventory;
    private GameObject  _newSlot;


    void Start()
    {
        _canvasManager.enabled = false;
    }

    private void OnSubmit(InputValue inputValue) 
    {
        if (inputValue.isPressed) 
        {
            Debug.Log("Esta activando el canvas");
            _canvasManager.enabled = true;
            RefreshInventoryUI();
        }
    }

    private void OnCancel(InputValue inputValue) 
    {
        if (inputValue.isPressed) 
        {
            _canvasManager.enabled = false;
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

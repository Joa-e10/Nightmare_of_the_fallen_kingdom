using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerControllerUI : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private CanvasManager _canvasManager;
    [SerializeField] private InventoryUI _inventoryUI;
    private GameObject _panelCrafting;
    private Player _player;
    [SerializeField] private Inventory _inventory;
    private Image _backgroundInventory;
    [SerializeField] private Transform _inventoryLimit;
    public GameObject _currentItem;
    public ItemData _currentItemData;
    private CraftingUI _componentCrafting;
    void Awake()
    {
        ActivateMapPlayer();
    }

    private void OnChange(InputValue input) 
    {
        _canvasManager = GameObject.Find("BackgroundInventory").GetComponent<CanvasManager>();
        _panelCrafting = _canvasManager.GetPanelCraftingUI();
        _componentCrafting = _panelCrafting.GetComponent<CraftingUI>();
        if (input.isPressed)
        {
            _canvasManager.NewStateBackground(true);
            _canvasManager.PanelHudActive(true);
        }
        _canvasManager.ButtonActive();
        ActivateMapUI();
    }

    private void OnCancel(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            //_canvasManager.PanelsDisabled();
            _canvasManager.NewStateBackground(false);
            _canvasManager.PanelHudActive(false);
        }
        _canvasManager.ButtonActive();
        ActivateMapPlayer();

    }

    private void OnClick(InputValue input)
    {
        if (input.isPressed)
        {
            //InventoryUI _componentInventory = _panelInventory.GetComponent<InventoryUI>();
            _currentItem = EventSystem.current.currentSelectedGameObject;
     
            if (_currentItem == null) return;

            _inventory.SetItemSelected(_currentItem);
            if (_currentItem.GetComponent<SlotCraftingUI>() == null)
            {
                _currentItemData = null;
            }
            else 
            {
                _currentItemData = _currentItem.GetComponent<SlotCraftingUI>()._itemData;
                if (_currentItemData != null)
                {
                    //_componentCrafting.ShowItemSelected(_currentItemData);
                    Debug.Log($"Objeto seleccionado en UI: {_currentItemData}");
                }
                else
                {
                    // _componentCrafting.ShowItemSelected(_currentItemData);
                    Debug.Log($"Objeto seleccionado en UI es nulo pai");
                }
            }
            _componentCrafting.ShowItemSelected(_currentItemData);
            //_currentItemData = null;
        }

    }

    public GameObject GetItemSelected() 
    {
    
        return _currentItem;
    }

    public void ActivateMapUI()
    {
        _playerInput.actions.FindActionMap("Player").Disable();
        _playerInput.actions.FindActionMap("UI").Enable();
        _playerInput.SwitchCurrentActionMap("UI");
    }
    public void ActivateMapPlayer()
    {
        _playerInput.actions.FindActionMap("UI").Disable();
        _playerInput.actions.FindActionMap("Player").Enable();
        _playerInput.SwitchCurrentActionMap("Player");
    }
}

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Unity.Netcode;
using System.Collections.Generic;

public class PlayerControllerUI : NetworkBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private CanvasManager _canvasManager;
    [SerializeField] private InventoryUI _inventoryUI;
    private GameObject _panelCrafting;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Transform _inventoryLimit;
    public GameObject _currentItem;
    public ItemData _currentItemData;
    private CraftingUI _componentCrafting;
    void Awake()
    {
        ActivateMapPlayer();
    }

    public override void OnNetworkSpawn() 
    {
        _canvasManager = GameObject.Find("BackgroundInventory").GetComponent<CanvasManager>();
    }

    private void OnChange(InputValue input) 
    {
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
            _canvasManager.NewStateBackground(false);
            _canvasManager.PanelHudActive(false);

        }
        _canvasManager.ButtonActive();
        ActivateMapPlayer();

    }

    private void OnPause(InputValue inputValue) 
    {
        if (inputValue.isPressed) 
        {
            Debug.Log("Toma el valos perrito");
            _canvasManager.MenuActivation(true);
        }
    }
    private GameObject MouseClickDetected()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current);

        pointerData.position = Mouse.current.position.ReadValue();

        List<RaycastResult> results = new List<RaycastResult>();

        EventSystem.current.RaycastAll(pointerData, results);

        if (results.Count > 0) return results[0].gameObject;

        return null;
    }

    private void OnClick(InputValue input)
    {
        if (input.isPressed)
        {
            _currentItem = MouseClickDetected();
     
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
                    Debug.Log($"Objeto seleccionado en UI: {_currentItemData}");
                }
                else
                {
                    Debug.Log($"Objeto seleccionado en UI es nulo pai");
                }
            }
            _componentCrafting.ShowItemSelected(_currentItemData);
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

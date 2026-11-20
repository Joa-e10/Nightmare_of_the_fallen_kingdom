using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Unity.Netcode;

public class PlayerControllerUI : NetworkBehaviour
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

    public override void OnNetworkSpawn()
    {
        if (_canvasManager == null)
        {
            GameObject canvasObj = GameObject.Find("CanvasManager");
            if (canvasObj != null)
            {
                _canvasManager = canvasObj.GetComponent<CanvasManager>();
            }
        }

        if (_canvasManager == null)
        {
            GameObject bgInventory = GameObject.Find("BackgroundInventory");
            if (bgInventory != null)
            {
                _canvasManager = bgInventory.GetComponent<CanvasManager>();
            }
        }

        // [CAMBIO] Asegura que al spawnear en red el dueño tenga activo el mapa de movimiento "Player"
        if (IsOwner)
        {
            ActivateMapPlayer();
        }
    }

    private void OnChange(InputValue input)
    {
        if (!IsOwner) return;

        if (input.isPressed)
        {
            Debug.Log($"OnChange -> IsOwner: {IsOwner}, CanvasManager: {_canvasManager != null}");

            if (_canvasManager != null)
            {
                _panelCrafting = _canvasManager.GetPanelCraftingUI();
                if (_panelCrafting != null)
                {
                    _componentCrafting = _panelCrafting.GetComponent<CraftingUI>();
                }

                _canvasManager.NewStateBackground(true);
                _canvasManager.PanelHudActive(true);
                _canvasManager.SetSpPanelActive(true);
                _canvasManager.ButtonActive();
            }
        }

        ActivateMapUI();
    }

    private void OnCancel(InputValue inputValue)
    {
        if (!IsOwner) return;

        if (inputValue.isPressed)
        {
            //_canvasManager.PanelsDisabled();
            if (_canvasManager != null)
            {
                _canvasManager.NewStateBackground(false);
                _canvasManager.PanelHudActive(false);
                _canvasManager.SetSpPanelActive(false);
                _canvasManager.ButtonActive();
            }
        }

        ActivateMapPlayer();
    }

    private void OnPause(InputValue inputValue)
    {
        if (!IsOwner) return;

        if (inputValue.isPressed)
        {
            Debug.Log("Toma el valos perrito");
            _canvasManager?.MenuActivation(true);
        }
    }

    private void OnClick(InputValue input)
    {
        if (!IsOwner) return;

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

            if (_componentCrafting != null)
            {
                _componentCrafting.ShowItemSelected(_currentItemData);
            }
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

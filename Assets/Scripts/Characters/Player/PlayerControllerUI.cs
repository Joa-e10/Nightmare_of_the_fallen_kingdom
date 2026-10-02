using Unity.Netcode;
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
    //private string _actionMap;
    void Awake()
    {
        ActivateMapPlayer();
    }

    private void OnDropItem(InputValue input) 
    {
        if (input.isPressed)
        {
            if (_currentItem == null) return;

            if (_currentItem.GetComponent<SlotUI>() == null) return;

                _currentItemData = _currentItem.GetComponent<SlotUI>()._itemData;

                if (_currentItemData == null) return;
                
                    _inventory.UpdateItem(_currentItemData, 1);
                    GameObject _itemDrop = Instantiate(_currentItemData._itemPrefab, transform.position, Quaternion.identity);
                    _itemDrop.GetComponent<NetworkObject>().Spawn();
                    Debug.Log($"Se dropeo el item requerido");

                    _inventoryUI.RefreshInventoryUI();
        }
    }

    private void OnChange(InputValue input) 
    {
        _canvasManager = GameObject.Find("BackgroundInventory").GetComponent<CanvasManager>();
        _player = GetComponent<Player>();
        _inventoryUI = GameObject.Find("BackgroundInventory").GetComponent<InventoryUI>();
        _inventoryLimit = _player._inventoryLimit;
        _backgroundInventory = _player._backgroundInventory;
        _panelCrafting = _canvasManager.GetPanelCraftingUI();
        if (input.isPressed)
        {
            _backgroundInventory.enabled = true;
            _canvasManager.PanelHudActive(_backgroundInventory.enabled);
            _inventoryUI.RefreshInventoryUI();
        }
        _canvasManager.ButtonActive();
        ActivateMapUI();
    }

    private void OnCancel(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            _backgroundInventory.enabled = false;
            _canvasManager.PanelHudActive(_backgroundInventory.enabled);

            foreach (Transform t in _inventoryLimit)
            {
                Destroy(t.gameObject);
            }

            if (_backgroundInventory.enabled == false) 
            {
                _panelCrafting.SetActive(false);
            }
        }
        _canvasManager.ButtonActive();
        ActivateMapPlayer();

    }

    private void OnClick(InputValue input)
    {
        if (input.isPressed)
        {
            CraftingUI _componentCrafting = _panelCrafting.GetComponent<CraftingUI>();
            _currentItem = EventSystem.current.currentSelectedGameObject;
     
            if (_currentItem == null) return;

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
        // Desactiva el mapa "Player" por completo
        _playerInput.actions.FindActionMap("Player").Disable();

        // Activa el mapa "UI"
        _playerInput.actions.FindActionMap("UI").Enable();

        // Le indicamos al PlayerInput cuál es el mapa activo
        _playerInput.SwitchCurrentActionMap("UI");
    }

    public void ActivateMapPlayer()
    {
        _playerInput.actions.FindActionMap("UI").Disable();
        _playerInput.actions.FindActionMap("Player").Enable();
        _playerInput.SwitchCurrentActionMap("Player");
    }
}

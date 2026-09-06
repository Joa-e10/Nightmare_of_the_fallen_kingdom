using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class PlayerControllerUI : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private CanvasManager _canvasManager;
    [SerializeField] private InventoryUI _inventoryUI;
    private GameObject _panelCrafting;
    private Player _player;
    private Image _backgroundInventory;
    [SerializeField] private Transform _inventoryLimit;
    public GameObject _currentItem;
    public ItemData _currentItemData;
    //private string _actionMap;
    void Awake()
    {
        ActivateMapPlayer();
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
    void Update()
    {
        //Debug.Log("Tenemos el mapa: "+_actionMap);
    }
}

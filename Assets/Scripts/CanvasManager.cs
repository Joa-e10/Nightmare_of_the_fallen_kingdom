using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    private bool _newState;

    [Header("Componentes Owner")]
    [SerializeField]private Inventory _playerInventory;

    [Header("Paneles UI")]
    [SerializeField] private GameObject _panelHud;
    [SerializeField] private Image _backgroundInventory;
    [SerializeField] private GameObject _panelInventory;
    [SerializeField] private GameObject _panelCrafting;

    [Header("componentes UI")]
    [SerializeField] private CraftingUI _craftingUI;
    [SerializeField] private InventoryUI _inventoryUI;

    [Header("Buttons")]
    [SerializeField] private GameObject _craftingPanelB;
    [SerializeField] private GameObject _inventoryPanelB;

    [Header("Componentes Button")]
    [SerializeField] private Button _craftingPanelActivation;
    [SerializeField] private Button _inventoryPanelActivation;

    private void OnEnable()
    {
        //_inventoryPanelActivation.onClick.AddListener();
        _craftingPanelActivation.onClick.AddListener(CraftingActivation);
        _inventoryPanelActivation.onClick.AddListener(InventoryActivation);
    }

    void Start()
    {
        
    }

    public void PanelHudActive(bool state) 
    {
        if (state == true)
        {
            _panelHud.SetActive(false);
        }
        else 
        {
            _panelHud.SetActive(true);
        }
    }

    public GameObject GetPanelCraftingUI()
    {
        return _panelCrafting;
    }
    private void CraftingActivation()
    {
        _panelCrafting.SetActive(true);
        _backgroundInventory.enabled = false;
        _craftingUI.RefreshCraftingUI();
        _craftingUI.RequiredItemUI();
    }

    private void InventoryActivation()
    {
        _backgroundInventory.enabled = true;
        _panelCrafting.SetActive(false);
        _inventoryUI.RefreshInventoryUI();
    }

    public void ButtonActive() 
    {
        if (_backgroundInventory.enabled == true)
        {
            _inventoryPanelB.SetActive(true);
            _craftingPanelB.SetActive(true);
        }
        else 
        {
            _inventoryPanelB.SetActive(false);
            _craftingPanelB.SetActive(false);
        }
    }

    void Update()
    {
    }
}

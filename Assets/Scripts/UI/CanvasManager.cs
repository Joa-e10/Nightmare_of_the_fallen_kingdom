using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [Header("Componentes Owner")]
    [SerializeField]private Inventory _playerInventory;

    [Header("Panels UI")]
    [SerializeField] private GameObject _panelHud;
    [SerializeField] private Image _backgroundInventory;
    //[SerializeField] private GameObject _panelInventory;
    [SerializeField] private GameObject _panelCrafting;

    [Header("components UI")]
    [SerializeField] private CraftingUI _craftingUI;
    [SerializeField] private InventoryUI _inventoryUI;

    [Header("Buttons")]
    [SerializeField] private GameObject _craftingPanelB;
    [SerializeField] private GameObject _inventoryPanelB;
    [SerializeField] private GameObject _inventoryDropB;

    [Header("Button Components")]
    [SerializeField] private Button _craftingPanelActivation;
    [SerializeField] private Button _inventoryPanelActivation;

    private void OnEnable()
    {
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
    public void NewStateBackground(bool newState) 
    {
        _backgroundInventory.enabled = newState;

        if (_backgroundInventory.enabled == true)
        {
            _inventoryUI.RefreshPanel_UI();
            _inventoryUI.ActivatedItems_UI();
        }
        else 
        {
            _inventoryUI.RefreshPanel_UI();
            _inventoryUI.ActivatedItems_UI();

            if (_panelCrafting.activeInHierarchy)
            {
                _panelCrafting.SetActive(false);
            }
            else
            {
                Debug.Log("Tenemos apagado el panel crafting");
            }
        }
    }

    public void PanelsDisabled() 
    {
        if (_backgroundInventory.enabled == true) 
        {
            _inventoryUI.ActivatedItems_UI();
            _backgroundInventory.enabled = false;

            if (_panelCrafting.activeInHierarchy) 
            {
                _panelCrafting.SetActive(false);
            }
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

        _craftingUI.ActivatedItems_UI();
        _inventoryUI.ActivatedItems_UI();

        _craftingUI.RefreshPanel_UI();
        _craftingUI.RequiredItemUI();
    }

    public void InventoryActivation()
    {
        _backgroundInventory.enabled = true;
        _panelCrafting.SetActive(false);

        _craftingUI.ActivatedItems_UI();
        _inventoryUI.ActivatedItems_UI();

        _inventoryUI.RefreshPanel_UI();
    }

    public void ButtonActive() 
    {
        if (_backgroundInventory.enabled == true)
        {
            _inventoryPanelB.SetActive(true);
            _craftingPanelB.SetActive(true);
            //_inventoryDropB.SetActive(true);
        }
        else 
        {
            _inventoryPanelB.SetActive(false);
            _craftingPanelB.SetActive(false);
           // _inventoryDropB.SetActive(false);
        }
    }
}

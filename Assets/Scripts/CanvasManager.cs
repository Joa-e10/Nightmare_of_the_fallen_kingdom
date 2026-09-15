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
    [SerializeField] private GameObject _panelSkillTree;

    [Header("componentes UI")]
    [SerializeField] private CraftingUI _craftingUI;
    [SerializeField] private InventoryUI _inventoryUI;
    [SerializeField] private SkillTreeUI _skillTreeUI;

    [Header("Buttons")]
    [SerializeField] private GameObject _craftingPanelB;
    [SerializeField] private GameObject _inventoryPanelB;
    [SerializeField] private GameObject _treePanelB;

    [Header("Componentes Button")]
    [SerializeField] private Button _craftingPanelActivation;
    [SerializeField] private Button _inventoryPanelActivation;
    [SerializeField] private Button _treePanelActivation;

    private void OnEnable()
    {
        //_inventoryPanelActivation.onClick.AddListener();
        _craftingPanelActivation.onClick.AddListener(CraftingActivation);
        _inventoryPanelActivation.onClick.AddListener(InventoryActivation);
        _treePanelActivation.onClick.AddListener(SkillTreeActivation);
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
        _panelSkillTree.SetActive(false);
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

    private void SkillTreeActivation() 
    {
        _backgroundInventory.enabled = false;
        _panelCrafting.SetActive(false);
        _panelSkillTree.SetActive(true);
        _skillTreeUI.RefreshTreeUI();
    }

    public void ButtonActive() 
    {
        if (_backgroundInventory.enabled == true)
        {
            _inventoryPanelB.SetActive(true);
            _craftingPanelB.SetActive(true);
            _treePanelB.SetActive(true);
        }
        else 
        {
            _inventoryPanelB.SetActive(false);
            _craftingPanelB.SetActive(false);
            _treePanelB.SetActive(false);
        }
    }

    void Update()
    {
    }
}

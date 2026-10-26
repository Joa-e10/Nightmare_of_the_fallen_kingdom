using UnityEngine;
using UnityEngine.UI;

public class CraftingUI : PanelUI
{
    [Header("Panels")]
    [SerializeField] private GameObject _panelCraftingUI;

    [Header("Button")]
    [SerializeField]private GameObject _createButton;
    [SerializeField] private GameObject _iconItemSelect;

    [Header("Button Components")]
    [SerializeField] private Button _createB;
    [SerializeField] private Button _craftingPanelActivation;

    [Header("Owner Components")]
    private PlayerCrafting _playerCrafting;
    private Inventory _playerInventory;

    [Header("Others")]
    [SerializeField] private GameObject _slotCraftingPrefab;
    [SerializeField] private GameObject _slotRequiredPrefab;
    [SerializeField] private Transform _craftingLimit;
    [SerializeField] private Transform _requiredItemLimit;
    private GameObject _newSlot;
    [SerializeField] private Sprite _defaultIcon;
    [SerializeField] private Image _iconItemBuild;
    private ItemData _itemSelected;

    public void ShowItemSelected(ItemData itemSelected) 
    {
        _itemSelected = itemSelected;
        if (_itemSelected == null)
        {
            _iconItemBuild.sprite = _defaultIcon;
        }
        else
        {
            _iconItemBuild.sprite = _itemSelected._icon;
        }

        RequiredItemUI();
    }

    public void RequiredItemUI() 
    {
        if (_itemSelected != null)
        {
            foreach (Transform t in _requiredItemLimit)
            {
                Destroy(t.gameObject);
            }

            for (int i = 0; i < _itemSelected._recipeData._nameRequiredItem.Count; i++)
            {
                if (_itemSelected._recipeData._nameRequiredItem[i] != null)
                {
                    _newSlot = Instantiate(_slotRequiredPrefab, _requiredItemLimit);
                    SlotRequiredUI slotAttributes = _newSlot.GetComponent<SlotRequiredUI>();
                    slotAttributes._quantityNeeded.text = _itemSelected._recipeData._quantityRequiredItem[i].ToString();
                    slotAttributes._icon.sprite = _itemSelected._recipeData._nameRequiredItem[i]._icon;
                    slotAttributes._itemData = _itemSelected._recipeData._nameRequiredItem[i];

                    /*for (int j = 0; j < _playerInventory._inventoryNames.Length; j++)
                    {
                        if (_playerInventory._inventoryNames[j] == _itemSelected._recipeData._nameRequiredItem[i])
                        {
                            slotAttributes._currentAmount.text = _playerInventory._inventoryQuantity[j].ToString();
                        }
                    }*/
                }
            }

        }
        else 
        {
            foreach (Transform t in _requiredItemLimit)
            {
                Destroy(t.gameObject);
                Debug.Log("No podemos sumar slots a los requisitos");
            }
        }
        
    }

    public override void RefreshPanel_UI()
    {
        TakeOwner();

        if (_playerCrafting == null) 
        {
            _playerCrafting = _playerObject.GetComponent<PlayerCrafting>();
            _playerInventory = _playerObject.GetComponent<Inventory>();
        }

        foreach (Transform t in _craftingLimit)
        {
            Destroy(t.gameObject);
        }

        _createB.onClick.AddListener(_playerCrafting.CreateNewItem);
        foreach (ItemData item in _playerCrafting._listOfItems)
        {
            GameObject _newSlot = Instantiate(_slotCraftingPrefab, _craftingLimit);
            SlotCraftingUI slotAttributes = _newSlot.GetComponent<SlotCraftingUI>();
            slotAttributes._textname.text = item._name;
            slotAttributes._icon.sprite = item._icon;
            slotAttributes._itemData = item;
        }
    }

    public override void ActivatedItems_UI()
    {
        if (_panelCraftingUI.activeInHierarchy)
        {
            _createButton.SetActive(true);
            _iconItemSelect.SetActive(true);
        }
        else 
        {
            _createButton.SetActive(false);
            _iconItemSelect.SetActive(false);
        }
    }
}

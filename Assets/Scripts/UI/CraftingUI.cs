using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUI : MonoBehaviour
{
    [SerializeField] private Button _createButton;
    [SerializeField] private PlayerCrafting _playerCrafting;
    [SerializeField] private GameObject _slotCraftingPrefab;
    [SerializeField] private GameObject _slotRequiredPrefab;
    [SerializeField] private Transform _craftingLimit;
    [SerializeField] private Transform _requiredItemLimit;
    private GameObject _newSlot;
    [SerializeField] private Button _craftingPanelActivation;
    [SerializeField] private Sprite _defaultIcon;
    [SerializeField] private Image _iconItemBuild;
    private ItemData _itemSelected;
    private NetworkObject _playerObject;
    private Inventory _playerInventory;

    private void OnEnable()
    {
    }

    public void TakeOwner()
    {
        foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
        {
            if (client.PlayerObject.IsOwner)
            {
                _playerObject = client.PlayerObject;
                _playerCrafting = client.PlayerObject.GetComponent<PlayerCrafting>();
                _playerInventory = client.PlayerObject.GetComponent<Inventory>();
            }
        }
    }

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

    public void RefreshCraftingUI()
    {
        TakeOwner();

        foreach (Transform t in _craftingLimit)
        {
            Destroy(t.gameObject);
        }

        _createButton.onClick.AddListener(_playerCrafting.CreateNewItem);
        foreach (ItemData item in _playerCrafting._listOfItems)
        {
            GameObject _newSlot = Instantiate(_slotCraftingPrefab, _craftingLimit);
            SlotCraftingUI slotAttributes = _newSlot.GetComponent<SlotCraftingUI>();
            slotAttributes._textname.text = item._name;
            slotAttributes._icon.sprite = item._icon;
            slotAttributes._itemData = item;
        }

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

                    for (int j = 0; j < _playerInventory._inventoryNames.Length; j++)
                    {
                        if (_playerInventory._inventoryNames[j] == _itemSelected._recipeData._nameRequiredItem[i])
                        {
                            slotAttributes._currentAmount.text = _playerInventory._inventoryQuantity[j].ToString();
                        }
                    }
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
        //Debug.Log("El item seleccionado es: "+_itemSelected);
        
    }
}

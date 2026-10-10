using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCrafting : MonoBehaviour
{
    private Inventory _playerInventory;
    private PlayerControllerUI _ControllerUI;
    public List<ItemData> _listOfItems = new List<ItemData>();
    private Dictionary<ItemData, int> _recyclingList = new Dictionary<ItemData, int>();
    public SlotCraftingUI _slotComponent;
    public ItemData _itemSelected;
    private RecipeData _currentRecipeData;
    private CraftingUI _craftingUI;
    public void CreateNewItem() 
    {
        _playerInventory = GetComponent<Inventory>();
        _ControllerUI = GetComponent<PlayerControllerUI>();
        _slotComponent = _ControllerUI.GetItemSelected().GetComponent<SlotCraftingUI>();
        _craftingUI = GameObject.Find("PanelCrafting").GetComponent<CraftingUI>();

            _itemSelected = _slotComponent._itemData;
            _currentRecipeData = _itemSelected._recipeData;

            if (_currentRecipeData != null) 
            {
               int matches = 0;
               int itemIndex = 0;
               //_itemSelectedActually = _itemSelected;

                foreach (ItemData itemRequired in _currentRecipeData._nameRequiredItem) 
                {

                    for (int i = 0; i < _playerInventory._inventoryNames.Length; i++)
                    {
                        if (_playerInventory._inventoryNames[i] == itemRequired)
                        {
                            if (_playerInventory._inventoryQuantity[i] >= _currentRecipeData._quantityRequiredItem[itemIndex]) 
                            {
                                _recyclingList[itemRequired] = _currentRecipeData._quantityRequiredItem[itemIndex];
                                matches++;
                            }
                        
                        }
                    }
                    itemIndex++;
                }

                   if (matches >= itemIndex)
                   {
                     foreach (KeyValuePair<ItemData, int> item in _recyclingList) 
                     {
                         _playerInventory.UpdateItem(item.Key, item.Value);
                     }
                        _playerInventory.AddItem(_itemSelected, 1);
                        //_craftingUI.ShowItemSelected(_itemSelectedActually);
                        _recyclingList.Clear();
                Debug.Log("Se puede crear el item");
                   }
                   else 
                   {
                Debug.Log("NO se puede crear el item deseado, faltan ingredientes");
                        _recyclingList.Clear();
                   }
                    _craftingUI.ShowItemSelected(_itemSelected);
            }
    }

    private void RetrieveItems(ItemData itemRemoved, int amount) 
    {
        if (itemRemoved != null)
        {
            _recyclingList[itemRemoved] = amount;
        }
    }

    public ItemData GetItemSelected() 
    {
       return _itemSelected; 
    }
}

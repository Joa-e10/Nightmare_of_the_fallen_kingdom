using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

   private Item _insertedObject;
   private int _itemAmount;
   public Dictionary<ItemData, int> _hud = new Dictionary<ItemData, int>();
    void Start()
    {
        
    }

    public void setInsertedObject(Item name)
    {
        _insertedObject = name;
        Debug.Log("Se cargo un TIPO de objeto para el inventaro " +_insertedObject);
    }

    public Item getInsertedObject()
    {
        return _insertedObject;
      
    }
    public void AddItem(ItemData itemName, int itemAmount) 
    {
        if (itemName == null)
            return;
        Debug.Log("Entró a AddItem");
        Debug.Log(itemName);
        Debug.Log(itemAmount);
        if (_hud.ContainsKey(itemName))
        {
            _hud[itemName] += itemAmount;
            Debug.Log("Se actualizó la cantidad del objeto");
        }
        else
        {
            _hud.Add(itemName, itemAmount);
            Debug.Log("Se cargó un nuevo objeto");
        }

        Debug.Log("Count actual: " + _hud.Count);
        /* if (itemName != null) 
         {
             foreach (KeyValuePair<Item, int> item in _hud) 
             {
                 if (itemName == item.Key)
                 {
                     _hud[item.Key] += itemAmount;
                     Debug.Log("Se actualizo la cantidad del objeto");
                 }
                 else
                 {
                     _hud.Add(itemName, itemAmount);
                     Debug.Log("Se cargo un nuevo objeto");
                 }
             }
         }*/

    }

    void Update()
    {
        Debug.Log("Hash Update: " + GetHashCode());
        Debug.Log("Count en Update: " + _hud.Count);
        
    }
}

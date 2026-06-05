using System;
using System.Collections.Generic;
//using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

   //private Item _insertedObject;
   private int _itemAmount;
   public Dictionary<ItemData, int> _hud = new Dictionary<ItemData, int>();
    void Start()
    {
        
    }

   /* public void setInsertedObject(Item name)
    {
        _insertedObject = name;
        Debug.Log("Se cargo un TIPO de objeto para el inventaro " +_insertedObject);
    }

    public Item getInsertedObject()
    {
        return _insertedObject;
      
    }*/
    public void AddItem(ItemData itemName, int itemAmount) 
    {
            if (itemName == null) return;

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
    }


    void Update()
    {   
    }
}

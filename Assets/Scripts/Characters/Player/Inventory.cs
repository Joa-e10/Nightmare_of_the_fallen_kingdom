using System;
using System.Collections.Generic;
//using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
   public Dictionary<ItemData, int> _hud = new Dictionary<ItemData, int>();
    void Start()
    {
        
    }
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

    public void UpdateItem(ItemData ItemData)
    {
        if (_hud.ContainsKey(ItemData)) 
        {
            _hud[ItemData]--;

            if (_hud[ItemData] <= 1)
            {
                Debug.Log("El item esta vacio");
                _hud.Remove(ItemData);
            }
            else 
            {
                Debug.Log("El item tiene cantidad todavia: " + _hud[ItemData]);
            }
            
        }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

   private Item _insertedObject;
   private int _itemAmount;
   //public List<Item> _hud = new List<Item>();
   public Dictionary<Item, int> _hud = new Dictionary<Item, int>();
    //public Dictionary<string, int> hud = new Dictionary<string, int>();
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
    /*public void setInsertedValue(int amount)
    {
        _insertedValue = amount;
        Debug.Log("Se cargo una cantidad para el inventario " + _insertedValue);
    }*/
    public void addItem(Item itemName, int itemAmount) 
    {
        if (itemName != null) 
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
        }



            /*foreach(KeyValuePair<string, int> item in hud)
            {
                if (_insertedObject != null)
                {
                    Debug.Log("Objeto insertado: " + _insertedObject);

                    if (hud.ContainsKey(_insertedObject))
                    {
                        hud[_insertedObject] += _insertedValue;
                        Debug.Log("se le sumo una cantidad Al objeto: " + hud[_insertedObject]);
                    }
                    else
                    {
                        hud.Add(_insertedObject, _insertedValue);
                        Debug.Log("Se guardo el objeto!: " + hud[_insertedObject]);

                    }
                }
            }*/

        }

    void Update()
    {

    }
}

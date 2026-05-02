using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

   private string _insertedObject;
   private int _insertedValue;

    public Dictionary<string, int> hud = new Dictionary<string, int>();
    void Start()
    {
        
    }

    public void setInsertedObject(string name)
    {
        _insertedObject = name;
        Debug.Log("Se cargo un TIPO de objeto para el inventaro " +_insertedObject);
    }
    public void setInsertedValue(int amount)
    {
        _insertedValue = amount;
        Debug.Log("Se cargo una cantidad para el inventaro " + _insertedValue);
    }

    private void addObject() 
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

            _insertedObject = null;
        }
        
    }

    void Update()
    {
        addObject();

    }
}

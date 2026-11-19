using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    [SerializeField] private ItemData _key;
    public enum axisChanged {X,Y,Z};
    public axisChanged _axis;
    [SerializeField] private Transform _transformMove;
    private float _limitOpen = 3f;
    void Start()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        Inventory playerInventory = other.GetComponent<Inventory>();

        if (playerInventory != null)
        {
            foreach (KeyValuePair<ItemData, int> item in playerInventory.GetPlayerInventory())
            {
                if (item.Key._name == _key._name && _axis.ToString() == "Y")
                {
                    _transformMove.position = new Vector3(transform.position.x, transform.position.y + _limitOpen, transform.position.z);
                }
                else if (item.Key._name == _key._name && _axis.ToString() == "X")
                {
                    _transformMove.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + _limitOpen);
                }
                else 
                {
                    _transformMove.position = new Vector3(transform.position.x + _limitOpen, transform.position.y, transform.position.z);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Inventory playerInventory = other.GetComponent<Inventory>();

        if (playerInventory != null)
        {
            foreach (KeyValuePair<ItemData, int> item in playerInventory.GetPlayerInventory())
            {
                if (item.Key._name == _key._name && _axis.ToString() == "Y")
                {
                    _transformMove.position = new Vector3(transform.position.x, _transformMove.position.y - _limitOpen, transform.position.z);
                }
                else if (item.Key._name == _key._name && _axis.ToString() == "X")
                {
                    _transformMove.position = new Vector3(transform.position.x, transform.position.y, _transformMove.position.z - _limitOpen);
                }
                else
                {
                    _transformMove.position = new Vector3(_transformMove.position.x - _limitOpen, transform.position.y, transform.position.z);
                }
            }
        }
    }

    void Update()
    {
    }
}

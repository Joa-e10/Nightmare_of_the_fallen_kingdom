using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    [SerializeField] private ItemData _key;
    [SerializeField] private Transform _gridTransform;
    private float _limitOpen = 3f;
    private bool _isOpen;
    void Start()
    {
        
    }


    private void OnCollisionEnter(Collision other)
    {
        Inventory playerInventory = other.collider.GetComponent<Inventory>();

        if (playerInventory != null)
        {
            foreach (KeyValuePair<ItemData, int> item in playerInventory.GetPlayerInventory())
            {
                if (item.Key._name == _key._name)
                {
                    _isOpen = true;
                }
            }
        }
    }

    void Update()
    {
        if (_isOpen)
        {
            _gridTransform.position = new Vector3(transform.position.x, _limitOpen, transform.position.z);
        }
    }
}

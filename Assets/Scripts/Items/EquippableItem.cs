using UnityEngine;

public class EquppableItem : Item
{
    private bool _isEquipped;
    private GameObject _armorPrefab;

    void Start()
    {
        _nameItem = gameObject.tag;
        _player = GameObject.Find("Player").GetComponent<characters>();
        _playerInventory = GameObject.Find("Player").GetComponent<Inventory>();
        _armorPrefab = GameObject.Find("ArmorInPlayer");
        _armorPrefab.SetActive(false);
    }

    public override void collected()
    {
        _inInventory = _player.getIsCollectObject();

        if (_inInventory == true)
        {

            Debug.Log("Lo has recogido!");
            _playerInventory.setInsertedValue(_amountObject);
            _playerInventory.setInsertedObject(_nameItem);
            _armorPrefab.SetActive(true);
            _isEquipped = true;
            Destroy(gameObject);

        }
    }

    void Update()
    {
        collected();
    }
}
       
    


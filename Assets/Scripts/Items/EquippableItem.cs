using UnityEngine;

public class EquppableItem : Item
{
    
    private GameObject _armorPrefab;

    void Start()
    {
        _amountObject = 1; // instanciamos la cantidad de items que tendra el objeto.
        _nameItem = gameObject.tag; // instanciamos el nombre del item igualandolo al TAG del objeto.
        _player = GameObject.Find("Player").GetComponent<characters>(); //Tomamos el componente "characters" del player.
        _playerInventory = GameObject.Find("Player").GetComponent<Inventory>(); //Tomamos el componente "Inventory" del player.
        _armorPrefab = GameObject.Find("ArmorInPlayer");
        
    }
    void Update()
    {

        
    }
}
       
    


using UnityEngine;

public class EquppableItem : Item
{
    
    private MeshRenderer _armorPlayer;
    

    void Start()
    {
        _amountObject = 1; // instanciamos la cantidad de items que tendra el objeto.
        _nameItem = gameObject.tag; // instanciamos el nombre del item igualandolo al TAG del objeto.
        _player = GameObject.Find("Player").GetComponent<characters>(); //Tomamos el componente "characters" del player.
        _playerInventory = GameObject.Find("Player").GetComponent<Inventory>(); //Tomamos el componente "Inventory" del player.
        _armorPlayer = GameObject.Find("ArmorInPlayer").GetComponent<MeshRenderer>();

    }

    public override void collected()
    {
        Debug.Log("ES: "+_armorPlayer);

        base.collected();

        _armorPlayer.enabled = true;

        
    }

    void Update()
    {

        
    }
}
       
    


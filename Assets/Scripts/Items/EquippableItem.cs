using UnityEngine;

public class EquppableItem : Item
{
    
    private MeshRenderer _armorPlayer;
    

    void Start()
    {
        
        _nameItem = gameObject.tag; // instanciamos el nombre del item igualandolo al TAG del objeto.
        _player = GameObject.Find("Player").GetComponent<Player>(); //Tomamos el componente "characters" del player.
        _playerInventory = GameObject.Find("Player").GetComponent<Inventory>(); //Tomamos el componente "Inventory" del player.
        _armorPlayer = GameObject.Find("ArmorInPlayer").GetComponent<MeshRenderer>(); //Tomamos el componente "MeshRenderer" de la armadura.

    }

    public override void collected() //Metodo que permite que se recolecte el item.
    {
        Debug.Log("ES: "+_armorPlayer);

        base.collected();//Tomamos como base el codigo "Collected" del script Item.

        _armorPlayer.enabled = true;//Activamos el MeshRenderer de la armadura.

    }

    void Update()
    {

        
    }
}
       
    


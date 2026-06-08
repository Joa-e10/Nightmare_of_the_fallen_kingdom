using UnityEngine;

public class EquppableItem : Item
{
    
    public MeshRenderer _armorPlayer;

    public override void OnNetworkSpawn()
    {
        _armorPlayer = GameObject.Find("ArmorInPlayer").GetComponent<MeshRenderer>();
    }

    void Start()
    {
       /* 
        _nameItem = gameObject.tag; // instanciamos el nombre del item igualandolo al TAG del objeto.
        _player = GameObject.Find("Player").GetComponent<Player>(); //Tomamos el componente "characters" del player.
        _playerInventory = GameObject.Find("Player").GetComponent<Inventory>(); //Tomamos el componente "Inventory" del player.
        _armorPlayer = GameObject.Find("ArmorInPlayer").GetComponent<MeshRenderer>(); //Tomamos el componente "MeshRenderer" de la armadura.
       */
    }

    public void SetStateButtom(bool state) 
    {
        
    }

    public override void collected() //Metodo que permite que se recolecte el item.
    {
         base.collected();//Tomamos como base el codigo "Collected" del script Item.
    }

    void Update()
    {
        Debug.Log("EL IN INVENTORY DA: "+_inInventory);
    }
}
       
    


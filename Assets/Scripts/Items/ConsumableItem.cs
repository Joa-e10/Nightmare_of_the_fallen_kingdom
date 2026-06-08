using Unity.Services.Matchmaker.Models;
using UnityEngine;

public class ConsumableItem : Item
{
    [SerializeField] private int _amountHeal = 1;

    void Start()
    {
        /*
        _nameItem = gameObject.tag; // instanciamos el nombre del item igualandolo al TAG del objeto.
        _player = GameObject.Find("Player").GetComponent<Player>(); //Tomamos el componente "characters" del player.
        _playerInventory = GameObject.Find("Player").GetComponent<Inventory>(); //Tomamos el componente "Inventory" del player.
        */
    }

    public override void collected() 
    {
        base.collected();//Tomamos como base el codigo "Collected" del script Item.
    }
        
} 
    
  



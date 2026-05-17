using Unity.Services.Matchmaker.Models;
using UnityEngine;

public class ConsumableItem : Item
{
    [SerializeField] private int _amountHeal = 1;
    void Start()
    {
        _nameItem = gameObject.tag; // instanciamos el nombre del item igualandolo al TAG del objeto.
        _player = GameObject.Find("Player").GetComponent<characters>(); //Tomamos el componente "characters" del player.
        _playerInventory = GameObject.Find("Player").GetComponent<Inventory>(); //Tomamos el componente "Inventory" del player.
    }

    public override void collected() //Metodo que permite que se recolecte el item.
    {
        if (_player.getlives() >= 100) //La vida actual es mayor o igual a 100.
        {
            Debug.Log("La vida esta al amximo, usala mas tarde rey: "+_player.getlives());
        }
        else
        {
            base.collected(); //Tomamos como base el codigo "Collected" del script Item.
            _player.setlives(_amountHeal); //Actualizamos el valor de "_currentHealt".
            Debug.Log("La vida ahora es: "+_player.getlives()+" Por que se le cargo: "+_amountHeal);
        }
    }   
}


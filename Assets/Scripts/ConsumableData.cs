using UnityEngine;
using Unity.Netcode;
[CreateAssetMenu(fileName = "ConsumableData", menuName = "Scriptable Objects/ConsumableData")]
public class ConsumableData : ItemData
{
    public int _amountHeal;
    public override void ItemUse(NetworkObject playerT)
    {
        Player playerP = playerT.GetComponent<Player>();
        if (playerP.getlives() >= 100) //La vida actual es mayor o igual a 100.
        {
            Debug.Log("La vida esta al amximo, usala mas tarde rey: " + playerP.getlives());
        }
        else
        {
            playerP.setlives(_amountHeal); //Actualizamos el valor de "_currentHealt".
            Debug.Log("La vida ahora es: " + playerP.getlives() + " Por que se le cargo: " + _amountHeal);
        }
    }
}

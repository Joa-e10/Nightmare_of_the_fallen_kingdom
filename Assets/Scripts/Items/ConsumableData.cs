using UnityEngine;
using Unity.Netcode;
[CreateAssetMenu(fileName = "ConsumableData", menuName = "Scriptable Objects/ConsumableData")]
public class ConsumableData : ItemData
{
    public int _amountHeal;
    
    public override void ItemUse(GameObject PlayerNet)
    {
        Player PlayerP = PlayerNet.GetComponent<Player>();
        Debug.Log("Vida antes: " + PlayerP.getlives());

        if (PlayerP.getlives() >= 100)
        {
            Debug.Log("NO se usa el item");

            PlayerP.SetUsingItem(false);
        }
        else
        {
            Debug.Log("SI se usa el item");

            PlayerP.SetUsingItem(true);

            PlayerP.setlives(_amountHeal);
        }
    }
}

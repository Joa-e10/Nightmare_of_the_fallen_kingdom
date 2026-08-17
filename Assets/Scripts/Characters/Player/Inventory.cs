using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Inventory : NetworkBehaviour
{
   public Dictionary<ItemData, int> _hud = new Dictionary<ItemData, int>();
    private Transform _transformPlayer;
    void Start()
    {
        
    }

    
    public void AddItem(ItemData itemName, int itemAmount) 
    {
            if (itemName == null) return;

            if (_hud.ContainsKey(itemName))
            {
                _hud[itemName] += itemAmount;
                Debug.Log("Se actualizó la cantidad del objeto");
            }
            else
            {
                _hud.Add(itemName, itemAmount);
                Debug.Log("Se cargó un nuevo objeto");
            }
    }

    public void UpdateItem(ItemData ItemData)
    {
        if (_hud.ContainsKey(ItemData)) 
        {
            _hud[ItemData]--;

            if (_hud[ItemData] <= 1)
            {
                Debug.Log("El item esta vacio");
                _hud.Remove(ItemData);
            }
            else 
            {
                Debug.Log("El item tiene cantidad todavia: " + _hud[ItemData]);
            }
            
        }
    }

    //UTILIZACION DE ITEMS

    [ServerRpc]
    public void EquipItemServerRpc(uint itemId, string bodyPart, ulong idPlayer)
    {
        foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
        {
            if (client.ClientId == idPlayer) 
            {
                _transformPlayer = client.PlayerObject.GetComponent<Transform>();
                foreach (var networkPrefab in NetworkManager.Singleton.NetworkConfig.Prefabs.Prefabs)
                {
                    if (itemId == networkPrefab.SourcePrefabGlobalObjectIdHash)
                    {
                        Transform _transformPart = _transformPlayer.Find("MeshPlayer/" + bodyPart).GetComponent<Transform>(); //Tomamos el objeto en escena de la parte del cuerpo requerida.
                        GameObject itemEquiped = Instantiate(networkPrefab.Prefab, _transformPart);
                        itemEquiped.GetComponent<NetworkObject>().Spawn();
                        itemEquiped.GetComponent<NetworkObject>().TrySetParent(this.NetworkObject);
                        Debug.Log("Tenemos el transformPart de: " + _transformPart);
                    }
                }
            }
        }
    }

    public void Equip(GameObject itemRecived, string bodyPart, NetworkObject PlayerNet)
    {
        if (itemRecived != null) 
        {
            ulong newId = PlayerNet.OwnerClientId;
            _transformPlayer = this.GetComponent<Transform>();

            var idHash = itemRecived.GetComponent<NetworkObject>().PrefabIdHash;
           
            EquipItemServerRpc(idHash, bodyPart, newId);
            Debug.Log("Tenemos el Id: " + newId);
            Debug.Log("Tenemos la parte: " + bodyPart);
            Debug.Log("Tenemos el transform de: " + _transformPlayer);
        }

    }
}

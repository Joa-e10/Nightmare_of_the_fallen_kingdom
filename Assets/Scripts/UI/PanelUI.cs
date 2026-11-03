using Unity.Netcode;
using UnityEngine;

public abstract class PanelUI : MonoBehaviour
{
    protected NetworkObject _playerObject;
    protected void TakeOwner()

    {
        foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
        {
            if (client.PlayerObject.IsOwner)
            {
                _playerObject = client.PlayerObject;
            }
        }
    }

    public NetworkObject GetObjectOwner() 
    {
        return _playerObject;
    }

    public abstract void RefreshPanel_UI();

    public abstract void ActivatedItems_UI();
}

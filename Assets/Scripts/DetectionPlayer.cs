using System;
using UnityEngine;

public class DetectionPlayer : MonoBehaviour
{
    [SerializeField] private BoxCollider _bc;
    public static event Action OnPlayerDetected;
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null) 
        {
            OnPlayerDetected?.Invoke();
            _bc.enabled = false;
        }
    }
}

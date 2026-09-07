using UnityEngine;

public class Trono : MonoBehaviour
{

    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null) 
        {
            player.Die();
        }
    }
    void Update()
    {
        
    }
}

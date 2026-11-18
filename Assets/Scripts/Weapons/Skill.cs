using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    protected int _cooldown;

    public abstract void ActivateAbility(Player playerOwner);
    void Update()
    {
        
    }
}

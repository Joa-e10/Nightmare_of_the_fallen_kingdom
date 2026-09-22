using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    protected int _cooldown;

    public abstract void ActivateAbility();
    void Update()
    {
        
    }
}

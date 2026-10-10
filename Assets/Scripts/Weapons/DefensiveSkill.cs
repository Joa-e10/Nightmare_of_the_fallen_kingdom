using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DefensiveSkill : Skill
{
    public int upgradeValue;
    public float activeTime;
    [SerializeField] private DefensiveData _data;
    private Player _player;

    private void OnEnable() 
    {
        _cooldown = _data.cooldown;
        upgradeValue = _data.upgradeValue; 
        activeTime = _data.activeTime;
    }
    public void SetComponentPlayer(Player playerComponent) 
    {
        _player = playerComponent;
    }

    public override void ActivateAbility()
    {
        if (_data.typeEfect == DefensiveData.TypeOfEfect.gradual)
        {
            StartCoroutine(Regenerationxseg());
        }
    }

    private IEnumerator Regenerationxseg()
    {
        while (activeTime > 0)
        {
            yield return new WaitForSeconds(1f);
            _player.setlives(upgradeValue);
            Debug.Log("Le damos mas vida");

            activeTime--;
        }
        Destroy(gameObject);
    }

    void Update()
    {
    }
}

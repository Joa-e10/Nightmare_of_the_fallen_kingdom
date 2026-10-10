using UnityEngine;

[CreateAssetMenu(fileName = "DefensiveData", menuName = "Scriptable Objects/DefensiveData")]
public class DefensiveData : SkillData
{
   public int upgradeValue;
    public enum Attribute{live, attack, defense, mana, speed};
    public Attribute upgradeAttribute;
    public enum TypeOfEfect{instant, gradual};
    public TypeOfEfect typeEfect;
    public float activeTime;
}

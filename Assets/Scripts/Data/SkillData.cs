using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "Scriptable Objects/SkillData")]
public abstract class SkillData : ScriptableObject
{
    public string name;
    public Sprite icon;
    public int cooldown;
    public enum TypeSkill {offensive, defensive}
    public TypeSkill type;
    public GameObject _skillObject;
}

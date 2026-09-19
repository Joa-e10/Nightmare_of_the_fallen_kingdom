using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "Scriptable Objects/SkillData")]
public class SkillData : ScriptableObject
{
    public string name;
    public Sprite icon;
    public int damage;
    public int cooldown;
    public float timeDirection;
    public int speed;
    public GameObject _skillObject;
}

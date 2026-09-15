using UnityEngine;

[CreateAssetMenu(fileName = "DataSkill", menuName = "Scriptable Objects/DataSkill")]
public class DataSkill : ScriptableObject
{
    public Sprite icon;
    public GameObject parentBranch;
    public int quantityPoints;
    public enum skillType {Upgrade,etc};
    public skillType type;
}

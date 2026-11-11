using UnityEngine;

public enum QuestType { Kill, Collect, Trigger }
public enum RewardType { Exp, Potion, AttributePoint }
public enum EnemyType { enemy_eye_RED_, B, C, D }

[CreateAssetMenu(fileName = "NewQuestData", menuName = "Quests/Quest Data")]
public class QuestData : ScriptableObject
{
    [Header("ID")]
    public string questID;
    public string title;
    [TextArea(2, 4)]
    public string description;

    [Header("Objective")]
    public QuestType questType;
    public int requiredAmount = 1;

    [Header("Specific Objectives")]
    public EnemyType enemyType;
    public string targetID;

    [Header("Rewards")]
    public int rewardXP; // Recompensas
    public RewardType rewardType;
    [Tooltip("Cantidad para pociones o puntos de estadística")]
    public int rewardAmount = 1;
}

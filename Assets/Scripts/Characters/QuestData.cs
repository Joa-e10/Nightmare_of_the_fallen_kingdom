using UnityEngine;

public enum QuestType { Kill , Collect, Trigger } // Tipo de mision
public enum RewardType { Exp, Potion, SkillPoint } // Tipo de recompensa
public enum EnemyType { A, B, C, D } // Tipo de enemigo 


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
    public string targetID;
    public int requiredAmount = 1;
    [Header("Quest Kill")]
    public EnemyType enemyType;

    [Header("Rewards")]
    public int rewardXP; // Experiencia que se obtiene al completar la mision.
    public RewardType RewardType; 
}

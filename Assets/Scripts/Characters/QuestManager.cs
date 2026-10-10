using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    private Dictionary<string, QuestInstance> activeQuests = new Dictionary<string, QuestInstance>();

    public event Action<QuestInstance> OnQuestUpdated;
    public event Action<QuestInstance> OnQuestCompleted;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void RegisterQuest(QuestData questData)
    {
        if (questData == null) return;

        if (!activeQuests.ContainsKey(questData.questID))
        {
            QuestInstance newInstance = new QuestInstance(questData);
            activeQuests.Add(questData.questID, newInstance);
        }
    }

    public QuestInstance GetQuest(string questID)
    {
        activeQuests.TryGetValue(questID, out QuestInstance instance);
        return instance;
    }

    public void ReportProgress(QuestType type, string targetID, int amount = 1)
    {
        foreach (var pair in activeQuests)
        {
            QuestInstance quest = pair.Value;

            if (!quest.isCompleted && quest.data.questType == type && quest.data.targetID == targetID)
            {
                quest.AddProgress(amount);
                OnQuestUpdated?.Invoke(quest);

                if (quest.isCompleted)
                {
                    // Otorgar la experiencia configurada en el QuestData
                    if (PlayerExperience.Instance != null && quest.data.rewardXP > 0)
                    {
                        PlayerExperience.Instance.AddExperience(quest.data.rewardXP);
                    }

                    OnQuestCompleted?.Invoke(quest);
                }
            }
        }
    }
}

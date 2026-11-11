using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    [Header("--- CONFIG ---")]
    [SerializeField] private List<QuestData> availableTestQuests;
    public Transform trackerContainer;
    public GameObject trackerItemPrefab;

    private readonly Dictionary<string, QuestInstance> activeQuests = new();
    private readonly Dictionary<string, GameObject> spawnedTrackers = new();

    public event Action<QuestInstance> OnQuestUpdated;
    public event Action<QuestInstance> OnQuestCompleted;

    private void Awake() => Instance = Instance == null ? this : null;

    private void Update()
    {
        if (Keyboard.current == null) return;

        // Tecla M: Alterna la visibilidad del prefab/los prefabs de misiones activos
        if (Keyboard.current.mKey.wasPressedThisFrame)
        {
            if (spawnedTrackers.Count == 0)
            {
                Debug.LogWarning("QuestManager: Presionaste 'M', pero no hay misiones activas creadas aún. Presiona 'N' primero.");
                return;
            }

            foreach (var obj in spawnedTrackers.Values)
            {
                if (obj != null)
                {
                    bool state = !obj.activeSelf;
                    obj.SetActive(state);
                    Debug.Log($"QuestManager: Visibilidad de ActiveQuestUI cambiada a: {state}");
                }
            }
        }

        // Tecla N: Asigna una misión aleatoria
        if (Keyboard.current.nKey.wasPressedThisFrame) GiveRandomQuest();
    }

    public void GiveRandomQuest()
    {
        if (availableTestQuests == null || availableTestQuests.Count == 0)
        {
            Debug.LogError("QuestManager: La lista 'Available Test Quests' está vacía en el Inspector.");
            return;
        }

        QuestData randomQuest = availableTestQuests[UnityEngine.Random.Range(0, availableTestQuests.Count)];
        RegisterQuest(randomQuest);
    }

    public void RegisterQuest(QuestData data)
    {
        if (data == null) return;
        if (activeQuests.ContainsKey(data.questID))
        {
            Debug.LogWarning($"QuestManager: La misión '{data.title}' ya está activa.");
            return;
        }

        var instance = new QuestInstance(data);
        activeQuests.Add(data.questID, instance);

        if (trackerContainer && trackerItemPrefab)
        {
            GameObject obj = Instantiate(trackerItemPrefab, trackerContainer);
            obj.SetActive(true); // Forzamos que se muestre al asignarse
            SetQuestTexts(obj, instance);
            spawnedTrackers[data.questID] = obj;
            Debug.Log($"QuestManager: Misión '{data.title}' registrada e instanciada con éxito.");
        }
        else
        {
            Debug.LogError("QuestManager: Falta asignar Tracker Container o Tracker Item Prefab en el Inspector.");
        }
    }

    public QuestInstance GetQuest(string id) => activeQuests.TryGetValue(id, out var q) ? q : null;

    public void ReportKillProgress(EnemyType enemy, int amount = 1) =>
        Process(q => !q.isCompleted && q.data.questType == QuestType.Kill && q.data.enemyType == enemy, amount);

    public void ReportProgress(QuestType type, string targetID, int amount = 1) =>
        Process(q => !q.isCompleted && q.data.questType == type && q.data.targetID == targetID, amount);

    private void Process(Func<QuestInstance, bool> condition, int amount)
    {
        foreach (var quest in activeQuests.Values)
        {
            if (!condition(quest)) continue;
            quest.AddProgress(amount);
            UpdateUI(quest);
            OnQuestUpdated?.Invoke(quest);

            if (!quest.isCompleted) continue;
            GrantRewards(quest.data);
            OnQuestCompleted?.Invoke(quest);
            RemoveUI(quest);
        }
    }

    private void GrantRewards(QuestData data)
    {
        if (PlayerExperience.Instance && data.rewardXP > 0)
            PlayerExperience.Instance.AddExperience(data.rewardXP);
        if (data.rewardType == RewardType.AttributePoint && PlayerStats.Instance)
            PlayerStats.Instance.AddUnassignedPoint(data.rewardAmount);
    }

    private void UpdateUI(QuestInstance quest)
    {
        if (spawnedTrackers.TryGetValue(quest.data.questID, out GameObject obj)) SetQuestTexts(obj, quest);
    }

    private void RemoveUI(QuestInstance quest)
    {
        if (!spawnedTrackers.TryGetValue(quest.data.questID, out GameObject obj)) return;
        Destroy(obj, 1.5f);
        spawnedTrackers.Remove(quest.data.questID);
    }

    private void SetQuestTexts(GameObject obj, QuestInstance quest)
    {
        TextMeshProUGUI[] texts = obj.GetComponentsInChildren<TextMeshProUGUI>();
        if (texts.Length == 0) return;

        texts[0].text = quest.data.title;
        if (texts.Length < 2) return;

        texts[1].text = quest.isCompleted ? "¡Completada!" :
            $"{quest.data.questType switch { QuestType.Kill => $"Derrotar a {quest.data.enemyType}:", QuestType.Collect => $"Recolectar {quest.data.targetID}:", _ => "Objetivo:" }} {quest.currentProgress} / {quest.data.requiredAmount}";
    }

    public List<QuestData> GetListQuest() 
    {
        return availableTestQuests;
    }
}

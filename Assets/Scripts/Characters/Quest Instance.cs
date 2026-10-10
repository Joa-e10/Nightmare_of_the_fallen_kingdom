using UnityEngine;

[System.Serializable]
public class QuestInstance
{
    public QuestData data; // Referencia al ScriptableObject
    public int currentAmount;
    public bool isCompleted;

    public QuestInstance(QuestData baseData)
    {
        data = baseData;
        currentAmount = 0;
        isCompleted = false;
    }

    public void AddProgress(int amount)
    {
        if (isCompleted) return;

        currentAmount += amount;
        if (currentAmount >= data.requiredAmount)
        {
            currentAmount = data.requiredAmount;
            isCompleted = true;
        }
    }
}

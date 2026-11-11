using UnityEngine;

[System.Serializable]
public class QuestInstance: MonoBehaviour
{
    public QuestData data;
    public int currentProgress; // Progreso actual de la misión
    public bool isCompleted;    // Estado de la misión

    public QuestInstance(QuestData questData)
    {
        data = questData;
        currentProgress = 0;
        isCompleted = false;
    }

    public void AddProgress(int amount)
    {
        if (isCompleted) return;

        currentProgress += amount;
        if (currentProgress >= data.requiredAmount)
        {
            currentProgress = data.requiredAmount;
            isCompleted = true;
        }
    }
}
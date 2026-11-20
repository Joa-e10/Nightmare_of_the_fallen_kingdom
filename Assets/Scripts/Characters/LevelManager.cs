using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelData
{
    public int levelNumber;
    public int requiredXP;
    public int skillPointsReward = 1;
}

[CreateAssetMenu(fileName = "LevelManager", menuName = "Stats/Level Manager")]
public class LevelManager : ScriptableObject
{
    [Header("Configuración de Niveles")]
    public List<LevelData> levels = new List<LevelData>();

    [Header("Estado Actual (Runtime)")]
    public int currentLevel = 1;
    public int currentXP = 0;
    public int availableSkillPoints = 0;

    public event Action<int> OnLevelUp;
    public event Action<int, int> OnXPChanged;
    public event Action<int> OnSkillPointsChanged;

    public void Initialize()
    {
        currentLevel = 1;
        currentXP = 0;
        availableSkillPoints = 0;
    }

    public void AddXP(int amount)
    {
        currentXP += amount;

        LevelData nextLevelData = GetLevelData(currentLevel + 1);
        while (nextLevelData != null && currentXP >= nextLevelData.requiredXP)
        {
            currentXP -= nextLevelData.requiredXP;
            LevelUp();
            nextLevelData = GetLevelData(currentLevel + 1);
        }

        int required = nextLevelData != null ? nextLevelData.requiredXP : 0;
        OnXPChanged?.Invoke(currentXP, required);
    }

    private void LevelUp()
    {
        currentLevel++;

        LevelData reachedLevelData = GetLevelData(currentLevel);
        int pointsEarned = reachedLevelData != null ? reachedLevelData.skillPointsReward : 1;

        availableSkillPoints += pointsEarned;

        OnLevelUp?.Invoke(currentLevel);
        OnSkillPointsChanged?.Invoke(availableSkillPoints);

        Debug.Log($"¡Felicidades! Subiste al nivel {currentLevel}");
        Debug.Log($"Obtuviste {pointsEarned} SP");
    }

    public void AddSkillPoints(int amount)
    {
        if (amount <= 0) return;

        availableSkillPoints += amount;
        OnSkillPointsChanged?.Invoke(availableSkillPoints);
    }

    public LevelData GetLevelData(int level)
    {
        int index = level - 1;
        if (index >= 0 && index < levels.Count)
        {
            return levels[index];
        }
        return null;
    }

    public void ConsumeSkillPoint()
    {
        if (availableSkillPoints > 0)
        {
            availableSkillPoints--;
            OnSkillPointsChanged?.Invoke(availableSkillPoints);
        }
    }
}
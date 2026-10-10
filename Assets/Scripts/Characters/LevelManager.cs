using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelData
{
    public int levelNumber;
    public int requiredXP;
}

[CreateAssetMenu(fileName = "LevelManager", menuName = "Stats/Level Manager")]
public class LevelManager : ScriptableObject
{
    [Header("Configuración de Niveles")]
    public List<LevelData> levels = new List<LevelData>();


    public LevelData GetLevelData(int level)
    {
        int index = level - 1;
        if (index >= 0 && index < levels.Count)
        {
            return levels[index];
        }
        return null;
    }
}
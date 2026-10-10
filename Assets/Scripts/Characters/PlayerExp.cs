using System;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    public static PlayerExperience Instance { get; private set; }

    [Header("Configuración de los Niveles")]
    [SerializeField] private LevelManager levelManager; 

    [Header("Nivel Actual")]
    public int currentLevel = 1;
    public int currentXP = 0;

    public event Action OnExperienceChanged;
    public event Action<int> OnLevelUp;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // suma la cantidad de xp
    public void AddExperience(int amount)
    {
        currentXP += amount;
        Debug.Log($"XP Ganada: +{amount}. XP Total: {currentXP}");

        CheckLevelUp();
        OnExperienceChanged?.Invoke();
    }

    private void CheckLevelUp()
    {
        LevelData nextLevelData = levelManager.GetLevelData(currentLevel);

        // Si existe configuración para el siguiente nivel y alcanzamos la XP requerida
        while (nextLevelData != null && currentXP >= nextLevelData.requiredXP)
        {
            currentLevel++;
            Debug.Log($"¡Subiste al nivel {currentLevel}!");
            OnLevelUp?.Invoke(currentLevel);

            // Obtener datos del nuevo siguiente nivel
            nextLevelData = levelManager.GetLevelData(currentLevel);
        }
    }
}

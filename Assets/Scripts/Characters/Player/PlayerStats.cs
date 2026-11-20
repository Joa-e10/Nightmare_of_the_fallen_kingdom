using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    [Header("Referencias")]
    [SerializeField] private LevelManager levelManager;

    [Header("Incremento por Punto (SK)")]
    [SerializeField] private int damagePerPoint = 2;
    [SerializeField] private int healthPerPoint = 20;
    [SerializeField] private int manaPerPoint = 10;

    [Header("Estadísticas Base")]
    public int damage = 10;
    public int maxHealth = 100;
    public int maxMana = 50;

    [Header("Puntos Invertidos en Cada Stat")]
    public int investedStrength = 0;
    public int investedMana = 0;
    public int investedHP = 0;

    public event Action OnStatsChanged;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void OnEnable()
    {
        if (levelManager != null)
        {
            levelManager.OnSkillPointsChanged += HandleSkillPointsChanged;
            levelManager.OnLevelUp += HandleLevelUp;
        }
    }

    private void OnDisable()
    {
        if (levelManager != null)
        {
            levelManager.OnSkillPointsChanged -= HandleSkillPointsChanged;
            levelManager.OnLevelUp -= HandleLevelUp;
        }
    }

    private void HandleLevelUp(int newLevel)
    {
        OnStatsChanged?.Invoke();
    }

    private void HandleSkillPointsChanged(int newSkillPointsAmount)
    {
        OnStatsChanged?.Invoke();
    }

    public void AddUnassignedPoint(int amount)
    {
        if (levelManager == null)
        {
            Debug.LogError("LevelManager no está asignado en PlayerStats.");
            return;
        }

        levelManager.AddSkillPoints(amount);
        OnStatsChanged?.Invoke();
    }

    public bool UpgradeStat(string statName)
    {
        if (levelManager == null)
        {
            Debug.LogError("LevelManager no está asignado en PlayerStats.");
            return false;
        }

        if (levelManager.availableSkillPoints <= 0)
        {
            Debug.LogWarning("No tienes puntos de skill (SK) disponibles.");
            return false;
        }

        switch (statName.ToLower())
        {
            case "strength":
            case "damage":
                investedStrength++;
                damage += damagePerPoint;
                break;

            case "mana":
            case "maxmana":
                investedMana++;
                maxMana += manaPerPoint;
                break;

            case "hp":
            case "health":
            case "maxhealth":
                investedHP++;
                maxHealth += healthPerPoint;
                break;

            default:
                Debug.LogError($"Estadística desconocida: {statName}");
                return false;
        }

        levelManager.ConsumeSkillPoint();
        OnStatsChanged?.Invoke();
        Debug.Log($"Estadística {statName} mejorada con éxito.");
        return true;
    }
}

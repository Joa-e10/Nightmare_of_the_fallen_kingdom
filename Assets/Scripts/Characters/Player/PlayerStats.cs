using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    [Header("Puntos")]
    public int availableAttributePoints = 0;

    [Header("Estadísticas Base")]
    public int damage = 10;
    public int maxHealth = 100;
    public int maxMana = 50;
    public int maxStamina = 100;

    public event Action OnStatsChanged;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddUnassignedPoint(int amount)
    {
        availableAttributePoints += amount;
        Debug.Log($"¡Has ganado {amount} punto(s) de estadística! Puntos totales disponibles: {availableAttributePoints}");
        OnStatsChanged?.Invoke();
    }

    // Métodos para gastar el punto en la estadística que elija el jugador
    public bool UpgradeStat(string statName)
    {
        if (availableAttributePoints <= 0)
        {
            Debug.LogWarning("No tienes puntos de atributo disponibles.");
            return false;
        }

        switch (statName.ToLower())
        {
            case "damage":
                damage += 2;
                break;
            case "health":
                maxHealth += 20;
                break;
            case "mana":
                maxMana += 10;
                break;
            case "stamina":
                maxStamina += 15;
                break;
            default:
                Debug.LogError("Estadística desconocida.");
                return false;
        }

        availableAttributePoints--;
        OnStatsChanged?.Invoke();
        Debug.Log($"Estadística {statName} mejorada con éxito.");
        return true;
    }
}

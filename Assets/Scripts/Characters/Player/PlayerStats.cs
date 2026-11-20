using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    [Header("Referencias")]
    [SerializeField] private LevelManager levelManager;

    [Header("Incremento por Punto (SK)")]
    [SerializeField] private int attackPerPoint = 1;
    [SerializeField] private int manaPerPoint = 1;
    [SerializeField] private int defensePerPoint = 1;

    [Header("Valores Iniciales")]
    [SerializeField] private int baseAttack = 10;
    [SerializeField] private int baseMana = 10;
    [SerializeField] private int baseDefense = 10;
    private int _skillpointplayer = 0;
    public event Action OnStatsChanged;

    private bool _isEndBuff = false;

    private Dictionary<string, int> _attributes = new Dictionary<string, int>()
    {
        {"Attack", 10},
        {"Mana", 10},
        {"Defense", 10}
    };

    private Dictionary<string, int> _investedPoints = new Dictionary<string, int>()
    {
        {"Attack", 0},
        {"Mana", 0},
        {"Defense", 0}
    };

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        ResetStats();
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

    public void SetSkillPoint(int newvalue)
    {
        _skillpointplayer += newvalue;
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
            case "attack":
            case "damage":
                _investedPoints["Attack"]++;
                UpgradeAttributes("Attack", attackPerPoint);
                break;

            case "mana":
                _investedPoints["Mana"]++;
                UpgradeAttributes("Mana", manaPerPoint);
                break;

            case "defense":
            case "def":
                _investedPoints["Defense"]++;
                UpgradeAttributes("Defense", defensePerPoint);
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

    // MEJORA DE ATRIBUTOS
    public void UpgradeAttributes(string nameAttribute, int newValue)
    {
        if (_attributes.ContainsKey(nameAttribute))
        {
            Debug.Log("Se actualizaron los valores");
            _attributes[nameAttribute] += newValue;
            OnStatsChanged?.Invoke();
        }
    }

    public void BuffAttributes(string nameAttribute, int afterValue)
    {
        if (!_attributes.ContainsKey(nameAttribute)) return;

        if (!_isEndBuff)
        {
            _isEndBuff = true;
            _attributes[nameAttribute] += afterValue;
            OnStatsChanged?.Invoke();
            StartCoroutine(BuffTime(nameAttribute, afterValue));
        }
        else
        {
            _attributes[nameAttribute] -= afterValue;
            _isEndBuff = false;
            OnStatsChanged?.Invoke();
        }
    }

    private IEnumerator BuffTime(string nameAttribute, int afterValue)
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("Pasaron los 3 seg de buff");
        BuffAttributes(nameAttribute, afterValue);
    }

    public int GetAttributeValue(string nameAttribute)
    {
        return _attributes.TryGetValue(nameAttribute, out int value) ? value : 0;
    }

    public Dictionary<string, int> GetAttributes()
    {
        return _attributes;
    }

    public int GetInvestedPoints(string nameAttribute)
    {
        return _investedPoints.TryGetValue(nameAttribute, out int points) ? points : 0;
    }

    public void ResetStats()
    {
        _isEndBuff = false;
        StopAllCoroutines();

        _attributes["Attack"] = baseAttack;
        _attributes["Mana"] = baseMana;
        _attributes["Defense"] = baseDefense;

        _investedPoints["Attack"] = 0;
        _investedPoints["Mana"] = 0;
        _investedPoints["Defense"] = 0;

        OnStatsChanged?.Invoke();
    }

    public void OnPlayerDeath()
    {
        ResetStats();
    }
}

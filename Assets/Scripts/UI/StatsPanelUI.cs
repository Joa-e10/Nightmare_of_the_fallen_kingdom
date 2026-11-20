using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsPanelUI : PanelUI
{
    [Header("Buttons")]
    [SerializeField] private Button _strengthButton;
    [SerializeField] private Button _manaButton;
    [SerializeField] private Button _defenseButton;

    [Header("Text Components")]
    [SerializeField] private TextMeshProUGUI _availablePointsText;
    [SerializeField] private TextMeshProUGUI _strengthCounterText;
    [SerializeField] private TextMeshProUGUI _manaCounterText;
    [SerializeField] private TextMeshProUGUI _defenseCounterText;

    [Header("References")]
    [SerializeField] private LevelManager _levelManager;
    private PlayerStats _playerStats;

    private void OnEnable()
    {
        ResolvePlayerStats();
        RegisterButtonListeners();
        SubscribeEvents();

        ActivatedItems_UI();
        RefreshPanel_UI();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void ResolvePlayerStats()
    {
        TakeOwner();

        if (_playerStats == null && _playerObject != null)
        {
            _playerStats = _playerObject.GetComponent<PlayerStats>();
        }

        if (_playerStats == null)
        {
            _playerStats = PlayerStats.Instance;
        }

        if (_playerStats == null)
        {
            _playerStats = FindFirstObjectByType<PlayerStats>();
        }
    }

    private void RegisterButtonListeners()
    {
        if (_strengthButton != null)
        {
            _strengthButton.onClick.RemoveAllListeners();
            _strengthButton.onClick.AddListener(() => OnUpgradeStatClicked("strength"));
        }

        if (_manaButton != null)
        {
            _manaButton.onClick.RemoveAllListeners();
            _manaButton.onClick.AddListener(() => OnUpgradeStatClicked("mana"));
        }

        if (_defenseButton != null)
        {
            _defenseButton.onClick.RemoveAllListeners();
            _defenseButton.onClick.AddListener(() => OnUpgradeStatClicked("defense"));
        }
    }

    private void SubscribeEvents()
    {
        if (_levelManager != null)
        {
            _levelManager.OnSkillPointsChanged -= HandlePointsOrLevelChanged;
            _levelManager.OnSkillPointsChanged += HandlePointsOrLevelChanged;

            _levelManager.OnLevelUp -= HandlePointsOrLevelChanged;
            _levelManager.OnLevelUp += HandlePointsOrLevelChanged;
        }

        if (_playerStats != null)
        {
            _playerStats.OnStatsChanged -= RefreshPanel_UI;
            _playerStats.OnStatsChanged += RefreshPanel_UI;
        }
    }

    private void UnsubscribeEvents()
    {
        if (_levelManager != null)
        {
            _levelManager.OnSkillPointsChanged -= HandlePointsOrLevelChanged;
            _levelManager.OnLevelUp -= HandlePointsOrLevelChanged;
        }

        if (_playerStats != null)
        {
            _playerStats.OnStatsChanged -= RefreshPanel_UI;
        }
    }

    private void HandlePointsOrLevelChanged(int _)
    {
        RefreshPanel_UI();
    }

    private void OnUpgradeStatClicked(string statName)
    {
        ResolvePlayerStats();

        if (_playerStats == null)
        {
            Debug.LogError("StatsPanelUI: No se encontró PlayerStats en la escena.");
            return;
        }

        if (_playerStats.UpgradeStat(statName))
        {
            RefreshPanel_UI();
        }
    }

    public override void RefreshPanel_UI()
    {
        ResolvePlayerStats();

        int availablePoints = _levelManager != null ? _levelManager.availableSkillPoints : 0;

        if (_availablePointsText != null)
        {
            _availablePointsText.text = availablePoints.ToString();
        }

        if (_playerStats != null)
        {
            foreach (KeyValuePair<string, int> attr in _playerStats.GetAttributes())
            {
                switch (attr.Key)
                {
                    case "Attack":
                        if (_strengthCounterText != null)
                            _strengthCounterText.text = attr.Value.ToString();
                        break;

                    case "Mana":
                        if (_manaCounterText != null)
                            _manaCounterText.text = attr.Value.ToString();
                        break;

                    case "Defense":
                        if (_defenseCounterText != null)
                            _defenseCounterText.text = attr.Value.ToString();
                        break;
                }
            }
        }

        bool canSpend = availablePoints > 0;
        if (_strengthButton != null) _strengthButton.interactable = canSpend;
        if (_manaButton != null) _manaButton.interactable = canSpend;
        if (_defenseButton != null) _defenseButton.interactable = canSpend;
    }

    public override void ActivatedItems_UI()
    {
        bool isActive = gameObject.activeInHierarchy;

        if (_strengthButton != null) _strengthButton.gameObject.SetActive(isActive);
        if (_manaButton != null) _manaButton.gameObject.SetActive(isActive);
        if (_defenseButton != null) _defenseButton.gameObject.SetActive(isActive);
    }
}

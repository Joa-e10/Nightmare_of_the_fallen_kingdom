using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class PanelHudUI : PanelUI
{
    private Player _player;

    [SerializeField] private Slider _healthBar;
    [SerializeField] private Slider _expBar;
    [SerializeField] private TextMeshProUGUI _level;
    [SerializeField] private Transform _questContainer;
    [SerializeField] private GameObject _questPrefab;
    public List<QuestData> _listOfQuest = new List<QuestData>();
    public QuestManager _questManager;
    public LevelManager _levelManager;
    public int _previousLevel = 0;
    private void RefreshHealthBar()
    {
        _healthBar.value = _player.getlives();
    }
    private void RefreshexpBar()
    {
       _level.text = _player.currentLevel.ToString();

        if (_player.currentLevel == _previousLevel)
        {
            _expBar.value = _player.currentXP;
        }
        else 
        {
            _previousLevel = _player.currentLevel;
            _expBar.value = 0;
        }
    }

    public override void RefreshPanel_UI()
    {
        TakeOwner();
        if (_player == null) 
        {
            _player = _playerObject.GetComponent<Player>();
            _levelManager = _playerObject.GetComponent<LevelManager>();
        }
        RefreshHealthBar();
        RefreshexpBar();
        RefreshContainerQuest();
    }

    public void RefreshContainerQuest() 
    {
        foreach (Transform t in _questContainer)
        {
            Destroy(t.gameObject);
        }
         _questManager = _player.GetComponent<QuestManager>();
        _listOfQuest = _questManager.GetListQuest();


        foreach (QuestData quest in _listOfQuest)
        {
            SlotUI slotQuest;

               GameObject _newSlot = Instantiate(_questPrefab, _questContainer);
                slotQuest = _newSlot.GetComponent<SlotUI>();

                slotQuest._textname.text = quest.title;
            }
        }

    void Update()
    {
        RefreshPanel_UI();
    }

    public override void ActivatedItems_UI()
    {
    }
}

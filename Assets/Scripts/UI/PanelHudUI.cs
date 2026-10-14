using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelHudUI : PanelUI
{
    private Player _player;

    [SerializeField] private Slider _healthBar;
    [SerializeField] private Slider _expBar;
    [SerializeField] private TextMeshProUGUI _level;
    private void RefreshHealthBar()
    {
        _healthBar.value = _player.getlives();
    }
    private void RefreshexpBar()
    {
       _level.text = _player.currentLevel.ToString();
       _expBar.value = _player.currentXP;
    }

    public override void RefreshPanel_UI()
    {
        TakeOwner();
        if (_player == null) 
        {
            _player = _playerObject.GetComponent<Player>();
        }
        RefreshHealthBar();
        RefreshexpBar();
    }
    void Update()
    {
        RefreshPanel_UI();
    }

    public override void ActivatedItems_UI()
    {
    }
}

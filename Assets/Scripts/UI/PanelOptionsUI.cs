using UnityEngine;
using UnityEngine.UI;

public class PanelOptionsUI : MonoBehaviour
{
    [SerializeField] private GameObject _panelMainMenu;
    [SerializeField] private GameObject _panelOptions;
    [SerializeField] private GameObject _panelMenu;

    [SerializeField] private Button _backButton;
    void Start()
    {
        
    }
    void OnEnable()
    {
        _backButton.onClick.AddListener(ReturnPanelOption);
    }

    public void ReturnPanelOption()
    {
        _panelOptions.SetActive(false);
        if (_panelMenu.activeInHierarchy)
        {
            _panelMainMenu.SetActive(true);
        }
        else
        {
            _panelMainMenu.SetActive(true);
        }
    }
}

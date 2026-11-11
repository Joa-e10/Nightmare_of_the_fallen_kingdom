using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _panelOptions;
    [SerializeField] private GameObject _panelMenu;
    [SerializeField] private CanvasManager _canvasManager;
    [SerializeField] private Button _buttonReturn;
    [SerializeField] private Button _buttonOptions;
    [SerializeField] private Button _buttonExit;
    void Start()
    {
        
    }

     void OnEnable()
    {
        _buttonReturn.onClick.AddListener(PanelMenuDisable);
        _buttonOptions.onClick.AddListener(PanelOptionsActivation);
        _buttonExit.onClick.AddListener(ReturnMainMenu);
    }

    public void PanelMenuDisable() 
    {
        _canvasManager.MenuActivation(false);
    }

    public void PanelOptionsActivation() 
    {
       _panelOptions.SetActive(true);
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("MainScene");
        NetworkManager.Singleton.Shutdown();
    }

    void Update()
    {
        
    }
}

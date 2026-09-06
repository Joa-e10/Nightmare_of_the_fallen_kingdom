using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelUI : MonoBehaviour
{
    [Header("Componentes de UI")]
    [SerializeField] private Slider _xpSlider;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _xpText;

    [Header("Referencia al Manager")]
    [SerializeField] private LevelManager _levelManager;

    private Player _targetPlayer;

    private void Update()
    {
        // Si aún no tenemos al jugador local asignado, lo buscamos
        if (_targetPlayer == null)
        {
            FindLocalPlayer();
        }
    }

    private void FindLocalPlayer()
    {
        // Buscamos entre los jugadores en escena cuál le pertenece al cliente local
        Player[] players = FindObjectsOfType<Player>();
        foreach (Player player in players)
        {
            if (player.IsOwner) // Es el jugador local
            {
                _targetPlayer = player;
                UpdateUI(_targetPlayer.CurrentLevel, _targetPlayer.CurrentXP);
                break;
            }
        }
    }

    // Actualiza los elementos visuales en pantalla
    public void UpdateUI(int currentLevel, int currentXP)
    {
        if (_levelManager == null || _targetPlayer == null) return;

        // Actualizar el texto del nivel
        if (_levelText != null)
        {
            _levelText.text = "Nivel " + currentLevel;
        }

        // Obtener datos del nivel actual y del siguiente para la barra
        LevelData currentLevelData = _levelManager.GetLevelData(currentLevel);
        LevelData nextLevelData = _levelManager.GetLevelData(currentLevel + 1);

        if (nextLevelData != null)
        {
            int minXP = currentLevelData != null ? currentLevelData.requiredXP : 0;
            int maxXP = nextLevelData.requiredXP;

            // Configurar el Slider (Barra)
            if (_xpSlider != null)
            {
                _xpSlider.minValue = minXP;
                _xpSlider.maxValue = maxXP;
                _xpSlider.value = currentXP;
            }

            // Configurar el texto numérico (Ejemplo: 150 / 300)
            if (_xpText != null)
            {
                _xpText.text = $"{currentXP} / {maxXP} XP";
            }
        }
        else
        {
            // Nivel Máximo
            if (_xpSlider != null) _xpSlider.value = _xpSlider.maxValue;
            if (_xpText != null) _xpText.text = "MAX";
        }
    }
}

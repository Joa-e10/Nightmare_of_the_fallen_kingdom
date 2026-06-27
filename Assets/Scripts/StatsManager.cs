using UnityEngine;
using TMPro;
using UnityEngine.InputSystem; 

public class StatsManager : MonoBehaviour
{

    [Header("Toggle Canvas Configuration")]
    [SerializeField] private GameObject panelStatsObjeto; // El objeto hijo que se activa/desactiva
    [SerializeField] private InputActionReference toggleStatsAction; 

    [Header("Skill Points Available")]
    public int skillPoints = 0;
    [SerializeField] private TMP_Text skillPointsText;

    // Estadísticas solicitadas configurables desde el Inspector
    [Header("Player Stats")]
    public int health = 100;
    public int mana = 50;
    public int attack = 15;
    public int stamina = 100;

    [Header("UI Text References")]
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text manaText;
    [SerializeField] private TMP_Text attackText;
    [SerializeField] private TMP_Text staminaText;


    private void OnEnable()
    {
        if (toggleStatsAction != null)
        {
            toggleStatsAction.action.Enable();
            toggleStatsAction.action.performed += OnToggleStats; // Nos suscribimos al botón
        }
    }

    private void OnDisable()
    {
        if (toggleStatsAction != null)
        {
            toggleStatsAction.action.performed -= OnToggleStats; // Limpiamos la suscripción
            toggleStatsAction.action.Disable();
        }
    }

    private void Start()
    {
        UpdateUI();
    }

    private void OnToggleStats(InputAction.CallbackContext context)
    {
        if (panelStatsObjeto != null)
        {
            // Niega el estado actual: si está activo lo apaga, si está apagado lo prende
            panelStatsObjeto.SetActive(!panelStatsObjeto.activeSelf);
        }
    }

    // Esta función la sigue llamando el ExpManager al subir de nivel
    public void AddSkillPoint()
    {
        skillPoints++;
        UpdateUI();
    }

    // Botones para el canvas

    public void UpgradeHealth()
    {
        if (skillPoints > 0)
        {
            health += 10; 
            skillPoints--;
            UpdateUI();
        }
    }

    public void UpgradeMana()
    {
        if (skillPoints > 0)
        {
            mana += 5; 
            skillPoints--;
            UpdateUI();
        }
    }

    public void UpgradeAttack()
    {
        if (skillPoints > 0)
        {
            attack += 2; 
            skillPoints--;
            UpdateUI();
        }
    }

    public void UpgradeStamina()
    {
        if (skillPoints > 0)
        {
            stamina += 10; 
            skillPoints--;
            UpdateUI();
        }
    }

    // Actualiza los textos en pantalla
    public void UpdateUI()
    {
        if (skillPointsText != null) skillPointsText.text = "Points: " + skillPoints;
        
        if (healthText != null) healthText.text = health.ToString();
        if (manaText != null) manaText.text = mana.ToString();
        if (attackText != null) attackText.text = attack.ToString();
        if (staminaText != null) staminaText.text = stamina.ToString();
    }
}

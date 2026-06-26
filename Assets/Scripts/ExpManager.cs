using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem; 

public class ExpManager : MonoBehaviour
{
    [Header("Experience Settings")]
    public int level = 1; 
    public int currentExp;
    public int expToLevel = 10;
    public float expGrowtheMultiplier = 1.2f;

    [Header("UI References")]
    public Slider expSlider;
    public TMP_Text currentLevelText;
    public TMP_Text expAmountText; 

    [Header("Input Settings")]
    [SerializeField] private InputAction gainExpAction;

    private void OnEnable()
    {
        gainExpAction.Enable();
        gainExpAction.performed += OnGainExpPressed;
    }

    private void OnDisable()
    {
        gainExpAction.Disable();
        gainExpAction.performed -= OnGainExpPressed;
    }

    private void Start()
    {
        UpdateUI();
    }

    private void OnGainExpPressed(InputAction.CallbackContext context)
    {
        Btn_GainExperience(2);
    }

    public void Btn_GainExperience(int amount = 2)
    {
        currentExp += amount;
        
        while (currentExp >= expToLevel)
        {
            LevelUp();
        }
        
        UpdateUI();
    }

    private void LevelUp()
    {
        level++;
        currentExp -= expToLevel;
        expToLevel = Mathf.RoundToInt(expToLevel * expGrowtheMultiplier);
    }

    public void UpdateUI()
    {
        if (expSlider != null)
        {
            expSlider.maxValue = expToLevel;
            expSlider.value = currentExp;
        }
        
        if (currentLevelText != null)
        {
            currentLevelText.text = level.ToString();
        }

        // 2. ACTUALIZAMOS EL TEXTO DE LA CANTIDAD DE EXP
        if (expAmountText != null)
        {
            expAmountText.text = currentExp + " / " + expToLevel;
        }
    }
}

using UnityEngine;
using TMPro;

namespace PowerTycoon.UI
{
    /// <summary>
    /// Haupt-HUD für das Gameplay. Zeigt wichtige Informationen an.
    /// </summary>
    public class GameHUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI moneyText;
        [SerializeField] private TextMeshProUGUI energyText;
        [SerializeField] private TextMeshProUGUI productionText;
        [SerializeField] private TextMeshProUGUI consumptionText;
        [SerializeField] private TextMeshProUGUI priceText;

        private void Start()
        {
            // Subscribe zu Events
            Systems.EconomySystem.Instance.OnMoneyChanged += UpdateMoneyDisplay;
            Systems.EnergySystem.Instance.OnEnergyChanged += UpdateEnergyDisplay;
            Systems.EnergySystem.Instance.OnProductionChanged += UpdateProductionDisplay;
            Systems.EnergySystem.Instance.OnConsumptionChanged += UpdateConsumptionDisplay;
            Systems.EconomySystem.Instance.OnEnergyPriceChanged += UpdatePriceDisplay;
        }

        private void OnDestroy()
        {
            // Unsubscribe
            if (Systems.EconomySystem.Instance != null)
                Systems.EconomySystem.Instance.OnMoneyChanged -= UpdateMoneyDisplay;
            if (Systems.EnergySystem.Instance != null)
            {
                Systems.EnergySystem.Instance.OnEnergyChanged -= UpdateEnergyDisplay;
                Systems.EnergySystem.Instance.OnProductionChanged -= UpdateProductionDisplay;
                Systems.EnergySystem.Instance.OnConsumptionChanged -= UpdateConsumptionDisplay;
            }
        }

        private void UpdateMoneyDisplay(float amount)
        {
            if (moneyText != null)
                moneyText.text = $"💰 ${amount:F2}";
        }

        private void UpdateEnergyDisplay(float energy)
        {
            if (energyText != null)
            {
                float max = Systems.EnergySystem.Instance.GetMaxStorage();
                float percentage = (energy / max) * 100f;
                energyText.text = $"⚡ {energy:F0}/{max:F0} MWh ({percentage:F1}%)";
            }
        }

        private void UpdateProductionDisplay(float production)
        {
            if (productionText != null)
                productionText.text = $"📈 Produktion: {production:F0} MW";
        }

        private void UpdateConsumptionDisplay(float consumption)
        {
            if (consumptionText != null)
                consumptionText.text = $"📉 Verbrauch: {consumption:F0} MW";
        }

        private void UpdatePriceDisplay(float price)
        {
            if (priceText != null)
                priceText.text = $"💵 ${price:F2}/MWh";
        }
    }
}

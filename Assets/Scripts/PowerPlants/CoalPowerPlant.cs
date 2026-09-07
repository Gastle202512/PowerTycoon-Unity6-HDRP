using UnityEngine;
using PowerTycoon.Systems;

namespace PowerTycoon.PowerPlants
{
    /// <summary>
    /// Kohle-Kraftwerk: Hohe Produktion, hohe Wartung, Umweltbelastung.
    /// </summary>
    public class CoalPowerPlant : PowerPlant
    {
        [SerializeField] private float coalConsumption = 5f; // Kohle pro Stunde
        [SerializeField] private float maintenanceCost = 100f; // $ pro Stunde
        [SerializeField] private float pollutionLevel = 0.8f; // 0-1 Skala

        private float coalSupply = 100f;
        private float timeSinceLastMaintenance = 0f;

        protected override void Start()
        {
            base.Start();
            PlantName = "Kohle-Kraftwerk";
            maxProduction = 500f; // 500 MW
        }

        private void Update()
        {
            if (!IsActive) return;

            // Kohle-Verbrauch
            coalSupply -= coalConsumption * Time.deltaTime / 3600f;
            if (coalSupply <= 0)
            {
                IsActive = false;
                Debug.LogWarning($"{PlantName}: Keine Kohle verfügbar!");
                return;
            }

            // Wartung-Kosten
            timeSinceLastMaintenance += Time.deltaTime;
            if (timeSinceLastMaintenance >= 3600f) // Alle Stunde (1 Spielstunde)
            {
                EconomySystem.Instance.AddExpense(maintenanceCost);
                timeSinceLastMaintenance = 0f;
            }

            UpdateProduction();
        }

        public override void UpdateProduction()
        {
            if (IsActive)
            {
                currentProduction = maxProduction;
            }
            else
            {
                currentProduction = 0f;
            }
        }

        public void AddCoal(float amount)
        {
            coalSupply += amount;
            Debug.Log($"{PlantName}: +{amount} Kohle hinzugefügt. Gesamt: {coalSupply}");
        }

        public float GetCoalSupply() => coalSupply;
        public float GetPollutionLevel() => pollutionLevel;
    }
}

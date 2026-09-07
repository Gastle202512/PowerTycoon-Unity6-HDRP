using UnityEngine;
using PowerTycoon.Systems;

namespace PowerTycoon.PowerPlants
{
    /// <summary>
    /// Kernkraft-Kraftwerk: Höchste Produktion, sehr teuer, hohe Wartung, aber konstant.
    /// </summary>
    public class NuclearPowerPlant : PowerPlant
    {
        [SerializeField] private float maintenanceCost = 500f; // $ pro Stunde (sehr teuer!)
        [SerializeField] private float fuelConsumption = 0.1f; // Uran pro Stunde
        [SerializeField] private float safetyRisk = 0.05f; // 0-1 Skala

        private float uranSupply = 1000f; // Viel Uran pro Ladung
        private float timeSinceLastMaintenance = 0f;
        private bool isOverheating = false;

        protected override void Start()
        {
            base.Start();
            PlantName = "Kernkraft-Kraftwerk";
            maxProduction = 1000f; // 1000 MW - sehr leistungsstark!
            efficiency = 0.95f; // Sehr effizient
        }

        private void Update()
        {
            if (!IsActive) return;

            // Uran-Verbrauch (sehr langsam)
            uranSupply -= fuelConsumption * Time.deltaTime / 3600f;
            if (uranSupply <= 0)
            {
                IsActive = false;
                Debug.LogWarning($"{PlantName}: Uran aufgebraucht!");
                return;
            }

            // Hohe Wartung
            timeSinceLastMaintenance += Time.deltaTime;
            if (timeSinceLastMaintenance >= 3600f)
            {
                EconomySystem.Instance.AddExpense(maintenanceCost);
                timeSinceLastMaintenance = 0f;

                // Geringe Chance für Zwischenfall
                if (Random.value < safetyRisk)
                {
                    TriggerMaintenance();
                }
            }

            UpdateProduction();
        }

        public override void UpdateProduction()
        {
            if (isOverheating)
            {
                currentProduction = maxProduction * 0.5f; // Gedrosselt bei Überhitzung
            }
            else if (IsActive)
            {
                currentProduction = maxProduction;
            }
            else
            {
                currentProduction = 0f;
            }
        }

        private void TriggerMaintenance()
        {
            isOverheating = true;
            Debug.LogWarning($"{PlantName}: Überhitzung erkannt! Produktion gedrosselt.");
            Invoke(nameof(ResetOverheating), 10f); // Nach 10 Sekunden normale Leistung
        }

        private void ResetOverheating()
        {
            isOverheating = false;
            Debug.Log($"{PlantName}: Wartung abgeschlossen. Normal betrieb.");
        }

        public void AddUran(float amount)
        {
            uranSupply += amount;
            Debug.Log($"{PlantName}: +{amount} Uran hinzugefügt. Gesamt: {uranSupply}");
        }

        public float GetUranSupply() => uranSupply;
        public bool IsOverheating() => isOverheating;
    }
}

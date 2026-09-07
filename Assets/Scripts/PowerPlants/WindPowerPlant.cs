using UnityEngine;
using PowerTycoon.Systems;

namespace PowerTycoon.PowerPlants
{
    /// <summary>
    /// Wind-Kraftwerk: Variable Produktion (wetterabhängig), sauber, geringe Wartung.
    /// </summary>
    public class WindPowerPlant : PowerPlant
    {
        [SerializeField] private float maintenanceCost = 40f; // $ pro Stunde
        [SerializeField] private float windVariability = 0.5f; // Wie sehr Produktion variiert

        private float timeSinceLastMaintenance = 0f;
        private float currentWindStrength = 0.5f;

        protected override void Start()
        {
            base.Start();
            PlantName = "Wind-Kraftwerk";
            maxProduction = 350f; // 350 MW (bei starkem Wind)
        }

        private void Update()
        {
            if (!IsActive) return;

            // Wind-Stärke variiert (Perlin Noise)
            currentWindStrength = Mathf.PerlinNoise(Time.time * 0.2f, 0f);

            // Wartung-Kosten
            timeSinceLastMaintenance += Time.deltaTime;
            if (timeSinceLastMaintenance >= 3600f)
            {
                EconomySystem.Instance.AddExpense(maintenanceCost);
                timeSinceLastMaintenance = 0f;
            }

            UpdateProduction();
        }

        public override void UpdateProduction()
        {
            // Produktion hängt von Wind ab
            currentProduction = maxProduction * currentWindStrength;
        }

        public float GetWindStrength() => currentWindStrength;
    }
}

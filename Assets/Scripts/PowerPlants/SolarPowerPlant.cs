using UnityEngine;
using PowerTycoon.Systems;

namespace PowerTycoon.PowerPlants
{
    /// <summary>
    /// Solar-Kraftwerk: Variable Produktion (tagesabhängig), sauber, geringe Wartung.
    /// </summary>
    public class SolarPowerPlant : PowerPlant
    {
        [SerializeField] private float maintenanceCost = 30f; // $ pro Stunde
        [SerializeField] private AnimationCurve dayNightCycle = AnimationCurve.EaseInOut(0, 0, 1, 1);

        private float timeSinceLastMaintenance = 0f;
        private float dayTime = 0.5f; // 0 = Nacht, 1 = Tag

        protected override void Start()
        {
            base.Start();
            PlantName = "Solar-Kraftwerk";
            maxProduction = 300f; // 300 MW (bei Vollsonne)
        }

        private void Update()
        {
            if (!IsActive) return;

            // Tageszeit-Zyklus (vereinfacht)
            dayTime = (Mathf.Sin(Time.time * 0.1f) + 1f) * 0.5f;

            // Wartung-Kosten (geringer als Kohle)
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
            // Produktion variiert je nach Tageszeit
            float dayFactor = dayNightCycle.Evaluate(dayTime);
            currentProduction = maxProduction * dayFactor;
        }

        public float GetDayTime() => dayTime;
    }
}

using UnityEngine;
using System;
using System.Collections.Generic;

namespace PowerTycoon.Systems
{
    /// <summary>
    /// Verwaltet die Energieproduktion und den Energiefluss im Spiel.
    /// </summary>
    public class EnergySystem : MonoBehaviour
    {
        public static EnergySystem Instance { get; private set; }

        [SerializeField] private float maxEnergyStorage = 10000f;
        private float currentEnergy = 0f;
        private float energyProduction = 0f;
        private float energyConsumption = 0f;

        private List<PowerPlant> activePowerPlants = new List<PowerPlant>();

        // Events
        public event Action<float> OnEnergyChanged;
        public event Action<float> OnProductionChanged;
        public event Action<float> OnConsumptionChanged;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            currentEnergy = maxEnergyStorage * 0.5f; // Start mit 50%
        }

        private void Update()
        {
            UpdateEnergyFlow();
        }

        private void UpdateEnergyFlow()
        {
            // Berechne Produktion von allen aktiven Kraftwerken
            energyProduction = 0f;
            foreach (var plant in activePowerPlants)
            {
                if (plant.IsActive)
                {
                    energyProduction += plant.GetCurrentProduction();
                }
            }

            // Netto-Energiefluss
            float netFlow = energyProduction - energyConsumption;
            currentEnergy += netFlow * Time.deltaTime;
            currentEnergy = Mathf.Clamp(currentEnergy, 0f, maxEnergyStorage);

            OnEnergyChanged?.Invoke(currentEnergy);
            OnProductionChanged?.Invoke(energyProduction);
            OnConsumptionChanged?.Invoke(energyConsumption);
        }

        public void RegisterPowerPlant(PowerPlant plant)
        {
            if (!activePowerPlants.Contains(plant))
            {
                activePowerPlants.Add(plant);
                Debug.Log($"Kraftwerk registriert: {plant.PlantName}");
            }
        }

        public void UnregisterPowerPlant(PowerPlant plant)
        {
            activePowerPlants.Remove(plant);
            Debug.Log($"Kraftwerk entfernt: {plant.PlantName}");
        }

        public void SetEnergyConsumption(float consumption)
        {
            energyConsumption = Mathf.Max(0f, consumption);
        }

        public float GetCurrentEnergy() => currentEnergy;
        public float GetEnergyProduction() => energyProduction;
        public float GetEnergyConsumption() => energyConsumption;
        public float GetMaxStorage() => maxEnergyStorage;
        public float GetEnergyPercentage() => currentEnergy / maxEnergyStorage;
    }

    /// <summary>
    /// Basis-Klasse für Kraftwerke.
    /// </summary>
    public abstract class PowerPlant : MonoBehaviour
    {
        public string PlantName { get; protected set; }
        public bool IsActive { get; protected set; }

        protected float currentProduction = 0f;
        protected float maxProduction = 100f;
        protected float efficiency = 1f;

        protected virtual void Start()
        {
            EnergySystem.Instance.RegisterPowerPlant(this);
        }

        protected virtual void OnDestroy()
        {
            EnergySystem.Instance.UnregisterPowerPlant(this);
        }

        public virtual float GetCurrentProduction()
        {
            return currentProduction * efficiency;
        }

        public abstract void UpdateProduction();
    }
}

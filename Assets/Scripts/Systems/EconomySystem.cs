using UnityEngine;
using System;

namespace PowerTycoon.Systems
{
    /// <summary>
    /// Verwaltet Finanzen, Preise und Wirtschaft des Spielers.
    /// </summary>
    public class EconomySystem : MonoBehaviour
    {
        public static EconomySystem Instance { get; private set; }

        [SerializeField] private float startingMoney = 50000f;
        [SerializeField] private float energyPrice = 0.5f; // $ pro MWh
        [SerializeField] private float priceFluctuation = 0.1f;

        private float currentMoney;
        private float totalRevenue = 0f;
        private float totalExpenses = 0f;

        // Events
        public event Action<float> OnMoneyChanged;
        public event Action<float> OnRevenueUpdated;
        public event Action<float> OnExpensesUpdated;
        public event Action<float> OnEnergyPriceChanged;

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
            currentMoney = startingMoney;
            OnMoneyChanged?.Invoke(currentMoney);
        }

        private void Update()
        {
            // Energiepreise schwanken
            UpdateEnergyPrice();
        }

        private void UpdateEnergyPrice()
        {
            float fluctuation = Mathf.Sin(Time.time * 0.5f) * priceFluctuation;
            float newPrice = energyPrice + fluctuation;
            OnEnergyPriceChanged?.Invoke(newPrice);
        }

        public void AddRevenue(float amount)
        {
            if (amount < 0) return;

            totalRevenue += amount;
            currentMoney += amount;
            OnMoneyChanged?.Invoke(currentMoney);
            OnRevenueUpdated?.Invoke(totalRevenue);

            Debug.Log($"Einnahme: ${amount:F2} | Gesamt: ${currentMoney:F2}");
        }

        public void AddExpense(float amount)
        {
            if (amount < 0) return;

            totalExpenses += amount;
            currentMoney -= amount;
            OnMoneyChanged?.Invoke(currentMoney);
            OnExpensesUpdated?.Invoke(totalExpenses);

            Debug.Log($"Ausgabe: ${amount:F2} | Gesamt: ${currentMoney:F2}");
        }

        public bool TrySpendMoney(float amount)
        {
            if (amount <= 0 || currentMoney < amount)
            {
                Debug.LogWarning($"Nicht genug Geld! Benötigt: ${amount:F2}, Verfügbar: ${currentMoney:F2}");
                return false;
            }

            AddExpense(amount);
            return true;
        }

        public float GetCurrentMoney() => currentMoney;
        public float GetEnergyPrice() => energyPrice;
        public float GetTotalRevenue() => totalRevenue;
        public float GetTotalExpenses() => totalExpenses;
        public float GetBalance() => totalRevenue - totalExpenses;
    }
}

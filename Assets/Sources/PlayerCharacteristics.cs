using UnityEngine;

namespace CS25
{
    public class PlayerCharacteristics : MonoBehaviour
    {
        [SerializeField] private float BaseOxygenDecrease;
        [SerializeField] private float BaseOxygenIncrease;
        [SerializeField] private float MaxOxygen_;

        public static PlayerCharacteristics Instance
        {
            get; private set;
        }

        public long Money
        {
            get; private set;
        }

        public float MaxOxygen
        {
            get { return MaxOxygen_; }
        }

        public float Oxygen
        {
            get; private set;
        }

        public void FillOxygen()
        {
            Oxygen += BaseOxygenIncrease;

            if(Oxygen > MaxOxygen) Oxygen = MaxOxygen;
        }

        public void AddMoney(long value) => Money += value;
        

        private void Start()
        {
            Instance = this;
            Oxygen = MaxOxygen;
        }

        private void FixedUpdate()
        {
            Oxygen -= BaseOxygenDecrease * GameDifficulty.Instance.GetDifficulty();
        }
    }
}
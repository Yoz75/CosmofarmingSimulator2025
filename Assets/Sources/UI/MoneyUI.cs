using TMPro;
using UnityEngine;

namespace CS25
{
    public class MoneyUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text ScoreText;

        private void OnGUI()
        {
            ScoreText.text = PlayerCharacteristics.Instance.Money.ToString();
        }
    }
}
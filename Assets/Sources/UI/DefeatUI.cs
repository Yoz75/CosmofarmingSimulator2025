using TMPro;
using UnityEngine;

namespace CS25
{
    public class DefeatUI : MonoBehaviour
    {
        [SerializeField] private GameObject DefeatScreen;
        [SerializeField] private TMP_Text DefeatScoreText;

        private void Awake()
        {
            DefeatScreen.SetActive(false);
        }

        private void Start()
        {
            GameState.Instance.StateChanged += (state) => { if(state == State.Death) ShowDefeatScreen(); };            
        }

        private void ShowDefeatScreen()
        {
            DefeatScreen.SetActive(true);
            DefeatScoreText.text = PlayerCharacteristics.Instance.Money.ToString();
        }
    }
}
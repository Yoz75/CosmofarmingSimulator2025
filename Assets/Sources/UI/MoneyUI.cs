using System.Collections;
using TMPro;
using UnityEngine;

namespace CS25
{
    public class MoneyUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text ScoreText;
        [SerializeField] private float PointsPerSecond;

        private long Score;
        private bool IsAddScoreCoroutineRunning;

        private void OnGUI()
        {
            //full line would be too huge
            var playerChars = PlayerCharacteristics.Instance;

            if(Score < playerChars.Money && !IsAddScoreCoroutineRunning)
            {
                var difference = playerChars.Money - Score;
                
                StartCoroutine(AddScoreCoroutine(difference));
            }
        }

        private IEnumerator AddScoreCoroutine(long score)
        {
            IsAddScoreCoroutineRunning = true;

            float waitTime = 1 / PointsPerSecond;

            for(long i = 0; i < score; i++)
            {
                Score++;
                ScoreText.text = Score.ToString();
                yield return new WaitForSeconds(waitTime);
            }

            IsAddScoreCoroutineRunning = false;
        }
    }
}
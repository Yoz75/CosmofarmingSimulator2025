using UnityEngine;
using UnityEngine.UI;

namespace CS25
{
    public class OxygenUI : MonoBehaviour
    {
        [SerializeField] private Image OxygenImage;

        private void Update()
        {
            const float min = 0;
            const float oxygenMin = 0;
            const float max = 1;

            //19th line would be too huge without this
            var playerChars = PlayerCharacteristics.Instance;

            OxygenImage.fillAmount = Remap(playerChars.Oxygen, oxygenMin, playerChars.MaxOxygen, min, max);
        }

        private static float Remap(float value, float fromMin, float fromMax, float toMin, float toMax)
        {
            return (value - fromMin) / (fromMax - fromMin) * (toMax - toMin) + toMin;
        }
    }
}
using UnityEngine;
using UnityEngine.UI;

namespace Podeli
{
    [RequireComponent(typeof(Button))]
    public class ButtonSoundOnClick : MonoBehaviour
    {
        [SerializeField] private AudioSource Source;
        [SerializeField] private AudioClip Clip;

        private Button Button;

        private void Start()
        {
            Button = GetComponent<Button>();
            Source.clip = Clip;

            Button.onClick.AddListener(Source.Play);
        }
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace CS25.UI
{
    public class Scroller : MonoBehaviour
    {
        [SerializeField] private RawImage Image;
        [SerializeField] private Vector2 Velocity;
        [SerializeField] private float VelocityCoefficient;

        private bool IsScrolling = true;

        private void Start()
        {
            GameState.Instance.StateChanged += (state) => { if(state == State.Death) IsScrolling = false; };
            Velocity *= VelocityCoefficient;
        }

        public void Wait(float seconds)
        {
            StartCoroutine(WaitCoroutine(seconds));
        }

        private IEnumerator WaitCoroutine(float seconds)
        {
            IsScrolling = false;
            yield return new WaitForSeconds(seconds);
            IsScrolling = true;
        }

        private void Update()
        {
            if(IsScrolling)
            {
                Image.uvRect = new Rect(
                    Image.uvRect.position + Velocity * Time.deltaTime, Image.uvRect.size);
            }
        }
    }
}
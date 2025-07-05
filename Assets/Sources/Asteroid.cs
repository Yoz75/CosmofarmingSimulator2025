using System.Collections;
using UnityEngine;

namespace CS25
{
    [RequireComponent(typeof(AudioSource))]
    public class Asteroid : MonoBehaviour
    {
        [SerializeField] AudioClip DestroySound;

        private AudioSource Source;
        private const float MinYPosition = 0;


        private void Start()
        {
            Source = GetComponent<AudioSource>();
            Source.clip = DestroySound;

            StartCoroutine(FallCoroutine());
        }

        private IEnumerator FallCoroutine()
        {
            bool shoudsUpdate = true;
            while(shoudsUpdate)
            {
                if(transform.position.y < MinYPosition)
                {
                    Source.Play();
                    GameState.Instance.Die();
                    shoudsUpdate=false;
                }

                yield return null;
            }
        }
    }
}
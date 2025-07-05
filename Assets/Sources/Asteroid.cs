using UnityEngine;

namespace CS25
{
    [RequireComponent(typeof(AudioSource))]
    public class Asteroid : MonoBehaviour
    {
        [SerializeField] AudioClip DestroySound;

        private AudioSource Source;
        private const float MinYPosition = 2;


        private void Start()
        {
            Source = GetComponent<AudioSource>();
            Source.clip = DestroySound;
        }

        private void Update()
        {
            if(transform.position.y < MinYPosition)
            {
                Source.Play();
                GameState.Instance.Die();
            }
        }
    }
}
using System.Collections;
using UnityEngine;

namespace CS25
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class AsteroidSpawner : CSBehaviour
    {
        [SerializeField] private GameObject AsteroidPrefab;
        [SerializeField] private Sprite WarningSprite;
        [SerializeField] private float SpawnChancePerSecond = 0.2f;
        [SerializeField] private float DefuseTime = 2f;
        [SerializeField] private AudioClip WarningSound;

        private SpriteRenderer SpriteRenderer;
        private AudioSource AudioSource;

        private bool IsSpawning;

        protected override void OnStart()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
            AudioSource = gameObject.AddComponent<AudioSource>();

            GameState.Instance.StateChanged += state =>
            {
                if(state == State.Death)
                    Destroy(gameObject);
            };

            StartCoroutine(SpawnLoopCoroutine());
        }

        private IEnumerator SpawnLoopCoroutine()
        {
            while(true)
            {
                if(!IsSpawning && ShouldSpawn())
                {
                    StartCoroutine(AsteroidWarningCoroutine());
                }

                yield return new WaitForSeconds(1f);
            }
        }

        private bool ShouldSpawn()
        {
            float difficulty = GameDifficulty.Instance.GetDifficulty();
            return Random.value < SpawnChancePerSecond * difficulty;
        }

        private IEnumerator AsteroidWarningCoroutine()
        {
            IsSpawning = true;
            SpriteRenderer.sprite = WarningSprite;

            if(WarningSound != null)
                AudioSource.PlayOneShot(WarningSound);

            float timer = 0f;
            bool defused = false;

            while(timer < DefuseTime)
            {
                if(!IsSpawning)
                {
                    defused = true;
                    break;
                }

                timer += Time.deltaTime;
                yield return null;
            }

            if(!defused)
            {
                Instantiate(AsteroidPrefab, transform.position, Quaternion.identity);
            }

            SpriteRenderer.sprite = null;
            IsSpawning = false;
        }

        protected override void OnClickEnter()
        {
            if(IsSpawning)
            {
                IsSpawning = false; // Обозначает, что игрок успел кликнуть
            }
        }
    }
}

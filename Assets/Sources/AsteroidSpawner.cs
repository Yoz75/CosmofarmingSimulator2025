using System.Collections;
using UnityEngine;

namespace CS25
{
    //I HAVE 10 MINUTES BEFORE END, THIS IS GOVNOCODE
    [RequireComponent(typeof(SpriteRenderer))]
    public class AsteroidSpawner : CSBehaviour
    {
        [SerializeField] private GameObject AsteroidPrefab;
        [SerializeField] private Sprite PizdecSprite;
        [SerializeField] private float AsteroidSpawnChancePerSecond;
        [SerializeField] private float AsteroidDefuseTime;

        private bool IsAsteroidSpawning;
        private bool IsAsteroidDestroyed;

        private SpriteRenderer SpriteRenderer;

        protected override void OnStart()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
            StartCoroutine(SpawnCoroutine());

            GameState.Instance.StateChanged += (state) => { if(state == State.Death) Destroy(gameObject); };
        }

        protected override void OnClickEnter()
        {
            if(IsAsteroidSpawning) IsAsteroidDestroyed = true;
        }

        private IEnumerator SpawnCoroutine()
        {
            while(true)
            {
                if(IsAsteroidSpawning) yield return null;

                if(AsteroidSpawnChancePerSecond * GameDifficulty.Instance.GetDifficulty() > Random.value)
                {
                    SpriteRenderer.sprite = PizdecSprite;
                    IsAsteroidSpawning = true;
                    StartCoroutine(DefuseCoroutine());
                }

                yield return new WaitForSeconds(1);
            }
        }

        private IEnumerator DefuseCoroutine()
        {
            yield return new WaitForSeconds(AsteroidDefuseTime);

            if(IsAsteroidDestroyed)
            {
                IsAsteroidDestroyed = false;
                IsAsteroidSpawning = false;
                SpriteRenderer.sprite = null;
            }
            else
            {
                Instantiate(AsteroidPrefab, transform);
            }
        }
    }
}

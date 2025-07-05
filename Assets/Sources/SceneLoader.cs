using System.Collections;
using UnityEngine;

namespace CS25
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private float Delay = 0.25f;

        public void Load(string name)
        {
            StartCoroutine(LoadCoroutine(name));
        }

        private IEnumerator LoadCoroutine(string name)
        {
            yield return new WaitForSeconds(Delay);
            UnityEngine.SceneManagement.SceneManager.LoadScene(name);
        }
    }
}
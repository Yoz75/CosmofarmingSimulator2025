using UnityEngine;

namespace CS25
{
    public class SceneLoader : MonoBehaviour
    {
        public void Load(string name)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(name);
        }
    }
}
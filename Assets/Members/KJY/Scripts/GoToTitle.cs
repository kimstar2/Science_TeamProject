using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Members.KJY.Scripts
{
    public class GoToTitle : MonoBehaviour
    {
        [Scene] [SerializeField] private int scene;

        public void GoToTitleS()
        {
            SceneManager.LoadScene(scene);
        }
    }
}
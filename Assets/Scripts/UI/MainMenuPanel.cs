using UnityEngine;

namespace UI
{
    /// <summary>
    /// Initial menu, stops game on start
    /// </summary>
    public class MainMenuPanel : MonoBehaviour
    {
        private static bool isFirstLaunch = true;

        private void Awake()
        {
            if (!isFirstLaunch)
            {
                gameObject.SetActive(false);
                return;
            }

            Time.timeScale = 0;
            isFirstLaunch = false;
        }

        public void StartGame()
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}

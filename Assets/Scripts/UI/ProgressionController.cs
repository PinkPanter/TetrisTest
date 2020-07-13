using Michsky.UI.ModernUIPack;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// Handle UI progression view and progression above levels
    /// </summary>
    public class ProgressionController : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI levelText;
        [SerializeField]
        private Image radialSlider;

        [SerializeField]
        [Range(0, 100)]
        private float progressPerLine;

        public int CurrentLevel {get; protected set; }
        public float CurrentLevelProgress { get; protected set; }

        private void Awake()
        {
            CurrentLevelProgress = 0.1f;
            CurrentLevel = 1;
            UpdateUI();
        }

        public int AddScore(int count)
        {
            CurrentLevelProgress += count * progressPerLine;

            if (CurrentLevelProgress >= 100)
            {
                var levelsEarned = (int) (CurrentLevelProgress / 100);
                CurrentLevel += levelsEarned;
                CurrentLevelProgress -= levelsEarned * 100;

                UpdateUI();
                return levelsEarned;
            }

            UpdateUI();
            return 0;
        }

        private void UpdateUI()
        {
            levelText.text = CurrentLevel.ToString();

            //TODO: ModernUI bug
            radialSlider.fillAmount = CurrentLevelProgress / 100;
        }
    }
}

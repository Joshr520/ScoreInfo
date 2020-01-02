using BeatSaberMarkupLanguage.Attributes;
using BS_Utils.Utilities;

namespace ScoreInfo.UI
{
    class Settings : PersistentSingleton<Settings>
    {
        private Config config;

        [UIValue("menuEnable")]
        public bool isMenuEnabled
        {
            get => config.GetBool("ScorePercentage", nameof(isMenuEnabled), false, true);
            set => config.SetBool("AdaptiveSFXRemover", nameof(isMenuEnabled), value);
        }

        [UIValue("endEnable")]
        public bool isEndEnabled
        {
            get => config.GetBool("ScorePercentage", nameof(isEndEnabled), false, true);
            set => config.SetBool("AdaptiveSFXRemover", nameof(isEndEnabled), value);
        }

        [UIValue("avgEnable")]
        public bool isAvgEnabled
        {
            get => config.GetBool("ScorePercentage", nameof(isAvgEnabled), false, true);
            set => config.SetBool("AdaptiveSFXRemover", nameof(isAvgEnabled), value);
        }

        [UIValue("listEnable")]
        public bool isListEnabled
        {
            get => config.GetBool("ScorePercentage", nameof(isListEnabled), false, true);
            set => config.SetBool("AdaptiveSFXRemover", nameof(isListEnabled), value);
        }

        public void Awake()
        {
            config = new Config("AdaptiveSFXRemover");
        }
    }
}
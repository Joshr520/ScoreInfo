using IPA;
using IPA.Config;
using IPA.Utilities;
using UnityEngine.SceneManagement;
using IPALogger = IPA.Logging.Logger;
using Harmony;
using UnityEngine;


namespace ScoreInfo
{
    public class Plugin : IBeatSaberPlugin
    {
        public static string PluginName => "ScorePercentage";
        internal static IConfigProvider configProvider;

        internal static HarmonyInstance harmony;
        ScorePercentage avgScore;

        public void Init(IPALogger logger, [Config.Prefer("json")] IConfigProvider cfgProvider)
        {
            Logger.log = logger;
        }

        public void OnApplicationStart()
        {
            Logger.log.Debug("Starting ScorePercentage Plugin");
            harmony = HarmonyInstance.Create("com.Idlebob.BeatSaber.ScorePercentage");
            //Patch Classes
            harmony.PatchAll(System.Reflection.Assembly.GetExecutingAssembly());
        }

        public void OnApplicationQuit()
        {
            Logger.log.Debug("Stopping ScorePercentage Plugin");
            harmony.UnpatchAll("com.Idlebob.BeatSaber.ScorePercentage");
        }

        public void OnFixedUpdate()
        {

        }

        public void OnUpdate()
        {

        }

        public void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
        {
            if (nextScene.name == "GameCore")
            {
                avgScore = new GameObject("Average Score Per Hit").AddComponent<ScorePercentage>();
                ScorePercentage.totalScore = 0;
                ScorePercentage.numNotes = 0;
            }
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
        {

        }

        public void OnSceneUnloaded(Scene scene)
        {

        }
    }
}
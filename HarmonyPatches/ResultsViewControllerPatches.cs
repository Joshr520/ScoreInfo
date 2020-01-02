﻿using Harmony;

namespace ScoreInfo.HarmonyPatches
{
    [HarmonyPatch(typeof(ResultsViewController))]
    [HarmonyPatch("SetDataToUI", MethodType.Normal)]
    class ResultsViewControllerPatches : ResultsViewController
    {
        static void Postfix(ref ResultsViewControllerPatches __instance)
        {
            if (UI.Settings.instance.isEndEnabled)
            {
                return;
            }

            int maxScore;
            double resultPercentage;
            double resultAvgPercentage;
            int resultScore;

            //Only calculate percentage, if map was successfully cleared
            if (__instance._levelCompletionResults.levelEndStateType == LevelCompletionResults.LevelEndStateType.Cleared)
            {

                maxScore = ScorePercentage.calculateMaxScore(__instance._difficultyBeatmap.beatmapData.notesCount);
                //use modifiedScore with negative multipliers
                if (__instance._levelCompletionResults.gameplayModifiers.noFail || __instance._levelCompletionResults.gameplayModifiers.noObstacles || __instance._levelCompletionResults.gameplayModifiers.noArrows || __instance._levelCompletionResults.gameplayModifiers.noBombs)
                {
                    resultScore = __instance._levelCompletionResults.modifiedScore;
                }
                //use rawScore without and with positive modifiers to avoid going over 100% without recalculating maxScore
                else
                {
                    resultScore = __instance._levelCompletionResults.rawScore;
                }
                //$resultScore = 
                resultPercentage = ScorePercentage.calculatePercentage(maxScore, resultScore);
                resultAvgPercentage = ScorePercentage.calculatePercentage(115 * ScorePercentage.numNotes, ScorePercentage.totalScore);

                //disable wrapping and autosize. format string and overwite rankText
                __instance._rankText.autoSizeTextContainer = false;
                __instance._rankText.enableWordWrapping = false;
                if (UI.Settings.instance.isAvgEnabled)
                {
                    __instance._rankText.text = "<size=70%>" + resultPercentage.ToString() + "<size=50%>" + "%\n" + "<size=70%>" + ((resultAvgPercentage * 115) / 100).ToString() + "<size=50%>" + "/" + 115.ToString();
                }
                else
                {
                    __instance._rankText.text = "<size=70%>" + resultPercentage.ToString() + "<size=50%>";
                }
                Logger.log.Debug("Total Score: " + ScorePercentage.totalScore.ToString());
                Logger.log.Debug((115*ScorePercentage.numNotes).ToString());
                Logger.log.Debug(ScorePercentage.numNotes.ToString());
            }
        }
    }
}

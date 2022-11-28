using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

public class LogManager : MonoBehaviour
{
    [SerializeField] private TMP_Text logText;
    public void HardReset()
    {
        ScorePrefs.EraseScoreData();
        CosmeticPrefs.EraseSkinsData();
        Logger.SendLog("Game Hard Reseted!\n");
        logText.text += "Game Hard Reseted!\n";
    }

    public void SkinsReset()
    {
        CosmeticPrefs.EraseSkinsData();
    }

    public void ScoreReset()
    {
        ScorePrefs.EraseScoreData();
    }

    public void UnlockSkins()
    {
        CosmeticPrefs.UnlockAllSkins();
    }

    public void ClearLogs()
    {
        logText.text = "";
        Logger.CleanLog();
        Logger.SendLog("Cleared Log.\n");
    }

    public void GiveCurrency()
    {
        int addedAmount = Random.Range(1, 6); 
        int actualMaxPoints = ScorePrefs.GetMaxPointsReached();
        int storedPoints = ScorePrefs.GetStoredPoints() + addedAmount;

        ScorePrefs.SaveActualScore(storedPoints, actualMaxPoints);

        string msg = "Added " + addedAmount + " jumps to currency.(" + storedPoints + ")\n";

        logText.text += msg;
        Logger.SendLog(msg);
    }

}

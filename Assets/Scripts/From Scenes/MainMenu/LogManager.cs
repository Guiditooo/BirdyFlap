using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

public class LogManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField logText;
    public void HardReset()
    {
        ScorePrefs.EraseScoreData();
        CosmeticPrefs.EraseSkinsData();

        logText.text += "Game Reseted!\n";
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
    }

    public void GiveCurrency()
    {
        int addedAmount = Random.Range(1, 6); 
        int actualMaxPoints = ScorePrefs.GetMaxPointsReached();
        int storedPoints = ScorePrefs.GetStoredPoints() + addedAmount;

        ScorePrefs.SaveActualScore(storedPoints, actualMaxPoints);

        logText.text += "Added " + addedAmount + " jumps to currency.\n";
    }

}

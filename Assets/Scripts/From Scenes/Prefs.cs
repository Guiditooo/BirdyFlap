using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct CosmeticPrefs
{
    private static string actualHatKey = "aHat";
    private static string actualBeakKey = "aBeak";
    private static string actualEyesKey = "aEyes";

    public int actualHat;
    public int actualBeak;
    public int actualEyes;    

    public static void SaveActualCosmetics(int aHat, int aBeak, int aEyes)
    {
        PlayerPrefs.SetInt(actualHatKey, aHat);
        PlayerPrefs.SetInt(actualBeakKey, aBeak);
        PlayerPrefs.SetInt(actualEyesKey, aEyes);
    }

    public static CosmeticPrefs GetActualCosmetics()
    {
        CosmeticPrefs actualCosmetics;
        actualCosmetics.actualHat = PlayerPrefs.HasKey(actualHatKey) ? PlayerPrefs.GetInt(actualHatKey) : 0;
        actualCosmetics.actualBeak = PlayerPrefs.HasKey(actualBeakKey) ? PlayerPrefs.GetInt(actualBeakKey) : 0;
        actualCosmetics.actualEyes = PlayerPrefs.HasKey(actualEyesKey) ? PlayerPrefs.GetInt(actualEyesKey) : 0;
        return actualCosmetics;
    }


}

public struct ScorePrefs
{
    private static string actualTotalPointsKey = "aTotalPoints";
    private static string actualMaxPointskKey = "aMaxPoints";

    public int actualMaxPoints;
    public int actualTotalPoints;

    public static void SaveActualScore(int aTotalPoints, int aMaxPoints)
    {
        PlayerPrefs.SetInt(actualTotalPointsKey, aTotalPoints);
        PlayerPrefs.SetInt(actualMaxPointskKey, aMaxPoints);
    }

    public static ScorePrefs GetActualCosmetics()
    {
        ScorePrefs actualScores;
        actualScores.actualMaxPoints = PlayerPrefs.HasKey(actualTotalPointsKey) ? PlayerPrefs.GetInt(actualTotalPointsKey) : 0;
        actualScores.actualTotalPoints = PlayerPrefs.HasKey(actualMaxPointskKey) ? PlayerPrefs.GetInt(actualMaxPointskKey) : 0;
        return actualScores;
    }

}
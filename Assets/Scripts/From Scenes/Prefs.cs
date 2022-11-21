using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct CosmeticPrefs
{
    private const string ABLE_TO_BUY_LETTER = "A";
    private const string BOUGHT_LETTER = "B";
    private const string EQUIPPED_LETTER = "E";

    private static string equippedHatKey = "aHat";
    private static string equippedBeakKey = "aBeak";
    private static string equippedEyesKey = "aEyes";
    private static string skinStatesKey = "aSkins";

    private static string defaultSkinsState = "EAAAAEAEA";


    public int actualHat;
    public int actualBeak;
    public int actualEyes;    

    public static void SaveEquippedCosmetics(int aHat, int aBeak, int aEyes)
    {
        PlayerPrefs.SetInt(equippedHatKey, aHat);
        PlayerPrefs.SetInt(equippedBeakKey, aBeak);
        PlayerPrefs.SetInt(equippedEyesKey, aEyes);
    }

    public static CosmeticPrefs GetEquippedCosmetics()
    {
        CosmeticPrefs actualCosmetics;
        actualCosmetics.actualHat = PlayerPrefs.HasKey(equippedHatKey) ? PlayerPrefs.GetInt(equippedHatKey) : 0;
        actualCosmetics.actualBeak = PlayerPrefs.HasKey(equippedBeakKey) ? PlayerPrefs.GetInt(equippedBeakKey) : 0;
        actualCosmetics.actualEyes = PlayerPrefs.HasKey(equippedEyesKey) ? PlayerPrefs.GetInt(equippedEyesKey) : 0;
        return actualCosmetics;
    }
    public static int GetEquippedHatSkinID()
    {
        int actualHat;
        actualHat = PlayerPrefs.HasKey(equippedHatKey) ? PlayerPrefs.GetInt(equippedHatKey) : 0;
        return actualHat;
    }
    public static int GetEquippedBeakSkinID()
    {
        int actualBeak;
        actualBeak = PlayerPrefs.HasKey(equippedBeakKey) ? PlayerPrefs.GetInt(equippedBeakKey) : 0;
        return actualBeak;
    }
    public static int GetEquippedEyesSkinID()
    {
        int actualEyes;
        actualEyes = PlayerPrefs.HasKey(equippedEyesKey) ? PlayerPrefs.GetInt(equippedEyesKey) : 0;
        return actualEyes;
    }

    public static CosmeticPrefs GetDefaultSkin()
    {
        CosmeticPrefs defaultSkin;
        defaultSkin.actualHat = 0;
        defaultSkin.actualBeak = 0;
        defaultSkin.actualEyes = 0;
        return defaultSkin;
    }

    public static void SaveSkinsState(List<Cosmetic> cosmeticList)
    {
        string wordToSave = "";
        string nextLetter = "";
        for (int i = 0; i < cosmeticList.Count; i++)
        {
            if(cosmeticList[i].IsEquipped())
            {
                nextLetter = EQUIPPED_LETTER;
            }
            else
            {
                nextLetter = cosmeticList[i].IsBought() ? BOUGHT_LETTER : ABLE_TO_BUY_LETTER ;
            }
            wordToSave += nextLetter;
        }

        PlayerPrefs.SetString(skinStatesKey, wordToSave);
    }

    public static void LoadSkinsState()
    {

        string wordToLoad;

        if (PlayerPrefs.HasKey(skinStatesKey))
        {
            wordToLoad = PlayerPrefs.GetString(skinStatesKey);
        }
        else
        {
            wordToLoad = defaultSkinsState;
        }

        for (int i = 0; i < Manager.GetSkinList().Count; i++)
        {

            Manager.GetSkinList()[i].SetIfBougth(false);
            Manager.GetSkinList()[i].SetIfEquiped(false);

            if (wordToLoad[i].ToString() == BOUGHT_LETTER)
            {
                Manager.GetSkinList()[i].SetIfBougth(true);
            }
            if (wordToLoad[i].ToString() == EQUIPPED_LETTER)
            {
                Manager.GetSkinList()[i].SetIfBougth(true);
                Manager.GetSkinList()[i].SetIfEquiped(true);
            }

        }

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

    public static void SaveActualTotalPoints(int aTotalPoints)
    {
        PlayerPrefs.SetInt(actualTotalPointsKey, aTotalPoints);
    }

    public static int GetMaxPointsReached()
    {
        int maxPointsReached;
        maxPointsReached = PlayerPrefs.HasKey(actualTotalPointsKey) ? PlayerPrefs.GetInt(actualTotalPointsKey) : 0;
        return maxPointsReached;
    }

    public static int GetStoredPoints()
    {
        int storedPoints;
        storedPoints = PlayerPrefs.HasKey(actualMaxPointskKey) ? PlayerPrefs.GetInt(actualMaxPointskKey) : 0;
        return storedPoints;
    }

}
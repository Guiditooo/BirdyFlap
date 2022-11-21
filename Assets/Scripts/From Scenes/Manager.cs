﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkinHatColor
{
    Red,
    LightBlue,
    Purple,
    Blue,
    Green
}
public enum SkinBeakColor
{
    Gray,
    Black
}
public enum SkinEyeColor
{
    Black,
    LightBlue
}
public enum CurrencyType
{
    Points,
    Coins
}
public struct Bird
{
    public int hat_color;
    public int eyes;
    public int beak;
}
public struct Skin
{
    public Bird bird;
    public int tube;
}
public class Manager : MonoBehaviour
{
    
    private static Manager instance = null;

    private const int SkinHatColorCount = 5;
    private const int SkinBeakColorCount = 2;
    private const int SkinEyeColorCount = 2;

    [SerializeField] private List<Cosmetic> skinList;

    private static List<Cosmetic> hatSkinList = new List<Cosmetic>(SkinHatColorCount);
    private static List<Cosmetic> beakSkinList = new List<Cosmetic>(SkinBeakColorCount);
    private static List<Cosmetic> eyesSkinList = new List<Cosmetic>(SkinEyeColorCount);
    private static List<Cosmetic> staticSkinList = new List<Cosmetic>();

    

    public Skin skin;

    private int points;
    private int maxPointsEarned;

    public static Skin GetDefaultSkin()
    {

        Debug.Log("Entregando las skins por default 0 - 5 - 7.");

        Skin def = new Skin();

        def.bird.beak = 5;
        def.bird.hat_color = 0;
        def.bird.eyes = 7;

        return def;
    }

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;

            foreach (Cosmetic skin in skinList)
            {
                switch (skin.GetCosmeticType())
                {
                    case CosmeticType.Hat:
                        hatSkinList.Add(skin);
                        break;
                    case CosmeticType.Beak:
                        beakSkinList.Add(skin);
                        break;
                    case CosmeticType.Eyes:
                        eyesSkinList.Add(skin);
                        break;
                    default:
                        break;
                }
            }

            skin.tube = 0;

            staticSkinList = skinList;

            DontDestroyOnLoad(gameObject);
        }
    }

    public static Manager GetInstance()
    {
        return instance;
    }
    public int GetPoints()
    {
        return points;
    }
    public int GetMaxPoints()
    {
        return maxPointsEarned;
    }
    public void SetMaxPoints(int newMaxPoints)
    {
        Debug.Log("Puntos maximos hechos: " + newMaxPoints);
        maxPointsEarned = points;
    }

    public void SetPoints(int newPoints)
    {
        Debug.Log("Ahora se tienen " + newPoints + " puntos.");
        points = newPoints;
    }

    public void SelectTubeSkin(int index)
    {
        skin.tube = index;
    }
    public Skin GetSkins() => skin;
    public static List<Cosmetic> GetHatSkins() => hatSkinList;
    public static List<Cosmetic> GetBeakSkins() => beakSkinList;
    public static List<Cosmetic> GetEyesSkins() => eyesSkinList;
    public static List<Cosmetic> GetSkinList() => staticSkinList;

    public static void CheckPointAchievement(int realizedPoints)
    {
        if (realizedPoints >= 20)
        {
            Auth.UnlockAchievement(GPGSIds.achievement_si);
        }
        if (realizedPoints >= 35)
        {
            Auth.UnlockAchievement(GPGSIds.achievement_eso_es);
        }
        if (realizedPoints >= 50)
        {
            Auth.UnlockAchievement(GPGSIds.achievement_vamos_tu_puedes);
        }
        if (realizedPoints >= 75)
        {
            Auth.UnlockAchievement(GPGSIds.achievement_lo_has_conseguido_75_puntos);
        }
    }

    public static void CheckAccumultarionAchievement(int totalAccumulated)
    {
        if (totalAccumulated >= 100)
        {
            Auth.UnlockAchievement(GPGSIds.achievement_acaparador);
        }
        if (totalAccumulated >= 500)
        {
            Auth.UnlockAchievement(GPGSIds.achievement_gran_acaparador);
        }
    }

}

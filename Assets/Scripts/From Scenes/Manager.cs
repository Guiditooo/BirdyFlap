using System.Collections;
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

    public const int SKIN_HAT_COLOR_COUNT = 5;
    public const int SKIN_BEAK_COLOR_COUNT = 2;
    public const int SKIN_EYES_COLOR_COUNT = 2;

    [SerializeField] private List<Cosmetic> skinList;

    private static List<Cosmetic> hatSkinList = new List<Cosmetic>(SKIN_HAT_COLOR_COUNT);
    private static List<Cosmetic> beakSkinList = new List<Cosmetic>(SKIN_BEAK_COLOR_COUNT);
    private static List<Cosmetic> eyesSkinList = new List<Cosmetic>(SKIN_EYES_COLOR_COUNT);
    private static List<Cosmetic> staticSkinList = new List<Cosmetic>();

    public Skin skin;

    private int points;
    private int maxPointsEarned;

    public static Skin GetDefaultSkin()
    {

        Logger.SendLog("Entregando las skins por default 0 - 5 - 7.\n");

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

            skin.tube = 0;

            staticSkinList = skinList;

            ReloadSkins();

            DontDestroyOnLoad(gameObject);
        }

    }

    public static Manager GetInstance() => instance;
    public int GetPoints() => points;
    public int GetMaxPoints() => maxPointsEarned;
    
    public void SetMaxPoints(int newMaxPoints)
    {
        Logger.SendLog("Puntos maximos hechos: " + newMaxPoints + "\n");
        maxPointsEarned = points;
    }

    public void SetPoints(int newPoints)
    {
        Logger.SendLog("Ahora se tienen " + newPoints + " puntos.\n");
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

    private static string myAchievement1 = GPGSIds.achievement_si;
    private static string myAchievement2 = GPGSIds.achievement_eso_es;
    private static string myAchievement3 = GPGSIds.achievement_vamos_tu_puedes;
    private static string myAchievement4 = GPGSIds.achievement_lo_has_conseguido_75_puntos;
    private static string myAchievement5 = GPGSIds.achievement_acaparador;
    private static string myAchievement6 = GPGSIds.achievement_gran_acaparador;

    public static void CheckPointAchievement(int realizedPoints)
    {
        


        if (realizedPoints >= 20)
        {
            Auth.UnlockAchievement(myAchievement1);
        }
        if (realizedPoints >= 35)
        {
            Auth.UnlockAchievement(myAchievement2);
        }
        if (realizedPoints >= 50)
        {
            Auth.UnlockAchievement(myAchievement3);
        }
        if (realizedPoints >= 75)
        {
            Auth.UnlockAchievement(myAchievement4);
        }
    }

    public static void CheckAccumultarionAchievement(int totalAccumulated)
    {
        if (totalAccumulated >= 100)
        {
            Auth.UnlockAchievement(myAchievement5);
        }
        if (totalAccumulated >= 500)
        {
            Auth.UnlockAchievement(myAchievement6);
        }
    }

    public static void ReloadSkins()
    {
        hatSkinList.Clear();
        beakSkinList.Clear();
        eyesSkinList.Clear();

        int eqHatID = 0;
        int eqBeakID = 0;
        int eqEyesID = 0;

        CosmeticPrefs.LoadSkinsState();

        foreach (Cosmetic skin in staticSkinList)
        {
            switch (skin.GetCosmeticType())
            {
                case CosmeticType.Hat:
                    hatSkinList.Add(skin);
                    if (skin.IsEquipped())
                        eqHatID = hatSkinList.Count - 1;
                    break;
                case CosmeticType.Beak:
                    beakSkinList.Add(skin);
                    if (skin.IsEquipped())
                        eqBeakID = beakSkinList.Count - 1;
                    break;
                case CosmeticType.Eyes:
                    eyesSkinList.Add(skin);
                    if (skin.IsEquipped())
                        eqEyesID = eyesSkinList.Count - 1;
                    break;
                default:
                    break;
            }
        }

        CosmeticPrefs.SaveEquippedCosmetics(eqHatID, eqBeakID, eqEyesID);
    }

}

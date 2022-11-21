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

    public static int SkinHatColorCount = 5;
    public static int SkinBeakColorCount = 2;
    public static int SkinEyeColorCount = 2;

    //public static string DefaultSaveFileText = "0_0_0_0-t_f_f_f_f_t_f_t_f-t_f_f_f_f_t_f_t_f";

    public List<Cosmetic> cosmetics = new List<Cosmetic>(SkinBeakColorCount + SkinEyeColorCount + SkinHatColorCount);

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

            if (Application.platform == RuntimePlatform.Android)
            { 
                
            }
            else
            {
            points = 0;
            maxPointsEarned = 0;

            skin.bird.eyes = 7;
            skin.bird.hat_color = 0;
            skin.bird.beak = 5;

            }

            skin.tube = 0;


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
    
    public Skin GetSkins()
    {
        return skin;
    }
    public void SelectTubeSkin(int index)
    {
        skin.tube = index;
    }
    public List<Cosmetic> GetCosmeticList()
    {
        return cosmetics;
    }

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

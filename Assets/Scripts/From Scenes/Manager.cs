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

public struct Currency
{
    public int points;
    public int coins;
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

    public static string DefaultSaveFileText = "0_0_0_0-t_f_f_f_f_t_f_t_f-t_f_f_f_f_t_f_t_f";

    public List<Cosmetic> cosmetics = new List<Cosmetic>(SkinBeakColorCount + SkinEyeColorCount + SkinHatColorCount);

    private Currency currency;
    public Skin skin;

    private Currency maxCurrencyEarned;

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
                GetFileParameters();          
            }
            else
            {

            currency.coins = 0;
            currency.points = 0;
            maxCurrencyEarned.coins = 0;
            maxCurrencyEarned.points = 0;

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
    public Currency GetCurrency()
    {
        return currency;
    }
    public Currency GetMaxPoints()
    {
        return maxCurrencyEarned;
    }
    public void SetMaxPoints(int points)
    {
        Debug.Log("Puntos maximos hechos: " + points);
        maxCurrencyEarned.points = points;
    }
    public void SetMaxCoins(int coins)
    {
        Debug.Log("Monedas maximas agarradas: " + coins);
        maxCurrencyEarned.coins = coins;
    }
    public void SetPoints(int points)
    {
        Debug.Log("Ahora se tienen " + points + " puntos.");
        currency.points = points;
    }
    
    public void SetCoins(int coins)
    {
        Debug.Log("Ahora se tienen " + coins + " monedas."); 
        currency.coins = coins;
    }
    public Skin GetSkins()
    {
        return skin;
    }
    public void SelectBirdSkin(int index)
    {
        //skin.bird = index;
    }
    public void SelectTubeSkin(int index)
    {
        skin.tube = index;
    }
    public List<Cosmetic> GetCosmeticList()
    {
        return cosmetics;
    }
    public void GetFileParameters()
    {
        string data = "";

        if (Application.platform == RuntimePlatform.Android)
            data = Logger.DebugReadedFile();
        else
        data = DefaultSaveFileText;

        string[] types = data.Split('-');
        string[] cur = types[0].Split('_');

        currency.points = int.Parse(cur[0]);
        currency.coins = int.Parse(cur[1]);
        maxCurrencyEarned.points = int.Parse(cur[2]);
        maxCurrencyEarned.coins = int.Parse(cur[3]);

        string[] equipped = types[1].Split('_');

        for (int i = 0; i < cosmetics.Count; i++)
        {
            cosmetics[i].SetIfEquiped(equipped[i].Equals('t'));
            if (cosmetics[i].IsEquipped())
            {
                switch (cosmetics[i].cosmetic)
                {
                    case CosmeticType.Hat:
                        skin.bird.hat_color = i;
                        break;
                    case CosmeticType.Beak:
                        skin.bird.beak = i;
                        break;
                    case CosmeticType.Eyes:
                        skin.bird.eyes = i;
                        break;
                    default:
                        break;
                }
            }
        }

        string[] bought = types[2].Split('_');

        for (int i = 0; i < cosmetics.Count; i++)
        {
            cosmetics[i].SetIfBougth(bought[i].Equals('t'));
            
        }
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

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Store : MonoBehaviour
{

    [SerializeField] private CanvasGroup panel;

    [SerializeField] private TMP_Text pointCurr;

    [SerializeField] private TMP_Text price;
    [SerializeField] private GameObject pricePointsCurr;

    [SerializeField] private GameObject hat;
    [SerializeField] private GameObject eyes;
    [SerializeField] private GameObject beak;

    private Image hatSprite;
    private Image eyesSprite;
    private Image beakSprite;

    private List<Cosmetic> cosmetics;

    private int totalCoins;
    private int totalPoints;

    private int actualIndex = 0; 
    private int nextIndex = 0;


    void Awake()
    {
        
        //totalCoins = 9999;
        totalPoints = ScorePrefs.GetStoredPoints();
        //totalPoints = 9999;
        pointCurr.text = totalPoints.ToString();

        hatSprite = hat.GetComponent<Image>();
        beakSprite = beak.GetComponent<Image>();
        eyesSprite = eyes.GetComponent<Image>();


        price.text = Manager.GetSkinList()[actualIndex].IsEquipped() ? "EQUIPPED" : "BOUGHT";

    }

    void Start()
    {
        LoadPanelCoroutine(panel);
        cosmetics = Manager.GetSkinList();
    }

    public void GoToMenu()
    {  
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator LoadPanelCoroutine(CanvasGroup panel)
    {
        float t = 0;
        while (t < 1)
        {
            panel.alpha = Mathf.Lerp(0, 1, t);
            t += Time.deltaTime;
            yield return null;
        }
        panel.alpha = 1;
        panel.interactable = true;
        panel.blocksRaycasts = true;
    }
    int GetACorrectIndex(int i)
    {
        if (i >= cosmetics.Count) 
        { 
            i -= cosmetics.Count; 
        }
        if (i < 0) 
        { 
            i += cosmetics.Count; 
        }
        return i;
    }
    public void GetRightIndex()
    {
        ResetSkin(actualIndex);
        int aux = actualIndex+1;
        nextIndex = GetACorrectIndex(aux);
        ShowSkin(nextIndex);
    }
    public void GetLeftIndex()
    {
        ResetSkin(actualIndex);
        int aux = actualIndex-1;
        nextIndex = GetACorrectIndex(aux);
        ShowSkin(nextIndex);
    }
    void ShowSkin(int index)
    {
        actualIndex = index;

        Logger.SendLog("Mostrando Skin i="+index+ ".\n");

        switch (cosmetics[index].GetCosmeticType())
        {
            case CosmeticType.Hat:
                hatSprite.sprite = cosmetics[index].GetSprite();
                break;
            case CosmeticType.Beak:
                beakSprite.sprite = cosmetics[index].GetSprite();
                break;
            case CosmeticType.Eyes:
                eyesSprite.sprite = cosmetics[index].GetSprite();
                break;
            default:
                break;
        }
        if (!cosmetics[index].IsBought())
        {
            if (cosmetics[index].GetPrice().currencyType.Equals(CurrencyType.Points))
            {
                pricePointsCurr.SetActive(true);
            }
            price.text = cosmetics[index].GetPrice().quantity.ToString();
        }
        else
        {
            price.text = cosmetics[index].IsEquipped() ? "EQUIPPED" : "BOUGHT";
        }
    }
    void ResetSkin(int i)
    {
        CosmeticPrefs defaultSkin = CosmeticPrefs.GetDefaultSkin();
        pricePointsCurr.SetActive(false);
        price.text = "";
        hatSprite.sprite = Manager.GetHatSkins()[defaultSkin.actualHat].GetSprite();
        beakSprite.sprite = Manager.GetBeakSkins()[defaultSkin.actualBeak].GetSprite();
        eyesSprite.sprite = Manager.GetEyesSkins()[defaultSkin.actualEyes].GetSprite();
    }

    public void BuySkin()
    {
        if(!cosmetics[actualIndex].IsBought())
        {
            if(cosmetics[actualIndex].GetPrice().currencyType.Equals(CurrencyType.Points))
            {
                if (totalPoints >= cosmetics[actualIndex].GetPrice().quantity)
                {

                    totalPoints -= cosmetics[actualIndex].GetPrice().quantity;
                    cosmetics[actualIndex].Buy();
                    ScorePrefs.SaveActualTotalPoints(totalPoints);
                    CosmeticPrefs.SaveSkinsState(cosmetics);
                    pointCurr.text = totalPoints.ToString();
                    Logger.SendLog("Comprado item " + actualIndex + " a " + cosmetics[actualIndex].GetPrice().quantity + " puntos.\n");

                }
            }
            if (cosmetics[actualIndex].IsBought())
            {

                pricePointsCurr.SetActive(false);
                price.text = "BOUGHT";
                Logger.SendLog("Item " + actualIndex + " cambia su estado a 'COMPRADO'.\n");

            }
        } 
    }
    public void EquipSkin()
    {
        if (cosmetics[actualIndex].IsBought())
        {
            if (!cosmetics[actualIndex].IsEquipped())
            {
                for (int i = 0; i < cosmetics.Count; i++)
                {
                    if (cosmetics[i].GetCosmeticType().Equals(cosmetics[actualIndex].GetCosmeticType()))
                    {
                        cosmetics[i].UnEquip();
                    }
                }
                cosmetics[actualIndex].Equip();
                CosmeticPrefs.SaveSkinsState(cosmetics);
                if (cosmetics[actualIndex].IsEquipped())
                {
                    price.text = "EQUIPPED";
                    Logger.SendLog("Equipado el item " + actualIndex + ".\n");
                }
            }
        }
    }
}

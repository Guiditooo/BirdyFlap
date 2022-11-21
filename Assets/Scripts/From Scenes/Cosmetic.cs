using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CosmeticType
{
    Hat,
    Beak,
    Eyes
}
[System.Serializable]
public struct Price
{
    public int quantity;
    public CurrencyType currencyType;
}

[System.Serializable]
public class Cosmetic
{
    [SerializeField] private CosmeticType cosmeticType;
    [SerializeField] private Sprite sprite;
    [SerializeField] private Price price;

    [SerializeField] private bool bought = false;
    [SerializeField] private bool equipped = false;

    public void Buy() => bought = true;
    public void Equip() => equipped = true;
    public void UnEquip() => equipped = false;
    public void SetIfEquiped(bool e) => equipped = e;
    public void SetIfBougth(bool b) => bought = b;
    public bool IsEquipped() => equipped;
    public bool IsBought() => bought;
    public Price GetPrice() => price;
    public Sprite GetSprite() => sprite;
    public CosmeticType GetCosmeticType() => cosmeticType;

}

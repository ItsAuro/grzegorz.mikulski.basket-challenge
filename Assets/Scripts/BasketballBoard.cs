using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;



public class BasketballBoard : MonoBehaviour
{

    [SerializeField]
    TextMeshPro _TMP_bonus;

    private int _rollFrequency = GameConfig.BACKBOARD_FREQUENCY;
    public int PointBonus { get; private set;} = 1;


    BonusRarity SelectRarity()
    {
        int gen = Random.Range(0, 100);
        if (gen < GameConfig.RARITY_METADATA[BonusRarity.Common]  .threshold) return BonusRarity.Common;
        if (gen < GameConfig.RARITY_METADATA[BonusRarity.Uncommon].threshold) return BonusRarity.Uncommon;
        if (gen < GameConfig.RARITY_METADATA[BonusRarity.Rare]    .threshold) return BonusRarity.Rare;
        if (gen < GameConfig.RARITY_METADATA[BonusRarity.Epic]    .threshold) return BonusRarity.Epic;
        return BonusRarity.Legendary;
    }
    void UpdateText(BonusRarity rarity)
    {
        PointBonus = GameConfig.RARITY_METADATA[rarity].pointBonus;
        _TMP_bonus.text = $"<color=#{ColorUtility.ToHtmlStringRGB(GameConfig.RARITY_METADATA[rarity].color)}>+{PointBonus}";
    }
    void SelectApplyRarity()
    {
        UpdateText(SelectRarity());
    }

    void Start()
    {
        InvokeRepeating(nameof(SelectApplyRarity), 0, _rollFrequency);
    }
}

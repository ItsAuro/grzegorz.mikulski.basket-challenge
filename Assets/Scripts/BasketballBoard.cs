using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;


enum Rarity 
{ 
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}
public class BasketballBoard : MonoBehaviour
{

    [SerializeField]
    TextMeshPro _TMP_multiplier;


    void Start()
    {
        _TMP_multiplier.text = $"<color=white>{1}x";

        InvokeRepeating(nameof(SelectApplyRarity), 0, 5);
    }

    void SelectApplyRarity()
    {
        ApplyMultiplier(SelectRarity());
    }

    Rarity SelectRarity()
    {
        int gen = Random.Range(0, 100);
        if (gen < 50) return Rarity.Common;
        if (gen < 75) return Rarity.Uncommon;
        if (gen < 87) return Rarity.Rare;
        if (gen < 93) return Rarity.Epic;
        return Rarity.Legendary;
    }

    void ApplyMultiplier(Rarity rarity)
    {
        switch (rarity) {
            case Rarity.Common:
                _TMP_multiplier.text = $"<color=white>{1}x";
                break;
            case Rarity.Uncommon:
                _TMP_multiplier.text = $"<color=green>{2}x";
                break;
            case Rarity.Rare:
                _TMP_multiplier.text = $"<color=blue>{4}x";
                break;
            case Rarity.Epic:
                _TMP_multiplier.text = $"<color=purple>{8}x";
                break;
            case Rarity.Legendary:
                _TMP_multiplier.text = $"<color=orange>{16}x";
                break;
        }
    }


}

using System.Collections.Generic;
using UnityEngine;

public enum BonusRarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}
public static class GameConfig
{
    // class acts like a global datastructure for parameters
    // it must not have any dependency other than Unity itself

    public static int MAX_TIME = 60;
    public static readonly int FIREBALL_MULTIPLIER = 2;
    public static readonly int FIREBALL_INCREMENT = 1;
    public static readonly int FIREBALL_THRESHOLD = 5;
    public static readonly int BACKBOARD_FREQUENCY = 5;


    public readonly struct RarityMetadata
    {
        public RarityMetadata(Color c, int t, int p)
        {
            color = c;
            threshold = t;
            pointBonus = p;
        }
        public readonly Color color;
        public readonly int threshold;
        public readonly int pointBonus;
    }

    public static readonly Dictionary<BonusRarity, RarityMetadata> RARITY_METADATA = new Dictionary<BonusRarity, RarityMetadata>
    {
        {BonusRarity.Common,    new RarityMetadata(Color.white ,  50,  1 )},
        {BonusRarity.Uncommon,  new RarityMetadata(Color.green ,  75,  2 )},
        {BonusRarity.Rare,      new RarityMetadata(Color.blue  ,  87,  4 )},
        {BonusRarity.Epic,      new RarityMetadata(Color.magenta, 93,  8 )},
        {BonusRarity.Legendary, new RarityMetadata(Color.yellow,  100, 16)}
    };
}

using System.Collections.Generic;
using System.Drawing;

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


    public readonly struct RarityMetadata
    {
        public RarityMetadata(Color c, int t)
        {
            color = c;
            threshold = t;
        }
        public readonly Color color;
        public readonly int threshold;
    }

    public static readonly Dictionary<BonusRarity, RarityMetadata> RARITY_METADATA = new Dictionary<BonusRarity, RarityMetadata>
    {
        {BonusRarity.Common,    new RarityMetadata(Color.White , 50)},
        {BonusRarity.Uncommon,  new RarityMetadata(Color.Green , 75)},
        {BonusRarity.Rare,      new RarityMetadata(Color.Blue  , 87)},
        {BonusRarity.Epic,      new RarityMetadata(Color.Purple, 93)},
        {BonusRarity.Legendary, new RarityMetadata(Color.Orange, 100)}
    };
}

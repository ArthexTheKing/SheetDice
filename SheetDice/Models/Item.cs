using SQLite;

namespace SheetDice.Models
{
    public enum ItemType
    {
        None,
        AdventuringGear,
        LightArmor,
        MediumArmor,
        HeavyArmor,
        Shield,
        MeleeWeapon,
        RangedWeapon,
        Ammunition,
        Rod,
        Staff,
        Wand,
        Ring,
        Potion,
        Scroll,
        WondrousItem
    }

    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public string Name { get; set; }
        public string Description { get; set; }
        public int Value { get; set; }
        public int Quantity { get; set; } = 1;
        public double Weight { get; set; } = 0.0;
        public ItemType Category { get; set; } = ItemType.None;
        public bool IsMagical { get; set; } = false;
    }
}

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
        WondrousItem,
        Wealth
    }

    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }
        public int Value { get; set; }
        public int Quantity { get; set; }
        public ItemType Category { get; set; }
        public bool IsMagical { get; set; }
        public string Description { get; set; }
        public bool IsEquipped { get; set; }
        
    }
}

namespace SheetDice.Models
{
    public class Item : object
    {
        public string Name { get; set; } //Name of the item
        public float Weight { get; set; } //Weight of the item (in lb?)
        public int Value { get; set; } //value of the item (in gp)
        public int Quantity { get; set; } //how many item you have of this type
        public string Category { get; set; } //TODO make this to an Enum type (this type are constant)
        public bool IsMagical { get; set; } //if this item is a magical item
        public string Description { get; set; } //the description of this item
        public bool IsEquipped { get; set; } //this item is or is not equipped, and it is in your bag
        
        public Item(string name, string description, string category)
        {
            this.Name = name;
            this.Description = description;
            this.Category = category;
            this.Value = 0;
            this.Quantity = 0;
            this.Weight = 0F;
            this.IsMagical = false;
            this.IsEquipped = false;
        }
        
    }
}

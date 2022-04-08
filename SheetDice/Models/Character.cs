using SQLite;

namespace SheetDice.Models
{
    public class Character
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Classe { get; set; }
        public string Level { get; set; }
        public string Alignment { get; set; }
        public string Race { get; set; }
        public string Background { get; set; }
        public string Subclass { get; set; }
        public string Subrace { get; set; }
        
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace SheetDice.Models
{
    public enum ConditionType
    {
        Blinded,
        Charmed,
        Concentrated,
        Deafened,
        Exhaustion,
        Frightened,
        Grapped,
        Incapacitated,
        Invisible,
        Paralyzed,
        Petrified,
        Poisoned,
        Prone,
        Restrained,
        Stunned,
        Unconscius
    }

    public class Condition
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public ConditionType ConditionType { get; set; } //Tipo di condizione
    }
}

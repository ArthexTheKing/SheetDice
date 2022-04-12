using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SheetDice.Models
{
    public enum DamageType
    {
        None,
        Acid,
        Bludgeoning,
        Cold,
        Fire,
        Force,
        Lightning,
        Necrotic,
        Piercing,
        Poison,
        Psychic,
        Radiant,
        Slashing,
        Thunder
    }
    public class Attack
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }  //nome dell'arma
        public string Damage { get; set; } //danno dell'arma
        public DamageType Type { get; set; } //tipo di danno
        public bool IsMagic { get; set; } //è magica
    }
}

using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SheetDice.Models
{
    public class Attack
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }  //nome dell'arma
        public string Damage { get; set; } //danno dell'arma
        public string Type { get; set; } //tipo di danno
    }
}

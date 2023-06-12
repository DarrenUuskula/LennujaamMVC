﻿namespace Lennujaam_MVC.Models
{
    public class Lend
    {
        public int ID { get; set; }
        public int KohtadeArv { get; set; }
        public int ReisijateArv { get; set; }
        public string Otspunkt { get; set; }
        public string Sihtpunkt { get; set; }
        public DateTime ValjumisAeg { get; set; }
        public bool Lopetatud { get; set; }
        public TimeSpan Kestvus { get; set; }
    }
}
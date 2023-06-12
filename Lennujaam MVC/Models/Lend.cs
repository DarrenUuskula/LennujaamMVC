using System.ComponentModel.DataAnnotations;

namespace Lennujaam_MVC.Models
{
    public class Lend
    {
        public int ID { get; set; }
        public int LennuNR { get; set; }
        [Range(1, 853)]
        public int KohtadeArv { get; set; }
        public int ReisijateArv { get; set; } = 0;
        public string Otspunkt { get; set; }
        public string Sihtpunkt { get; set; }
        public DateTime ValjumisAeg { get; set; }
        public bool Lopetatud { get; set; } = false;
        public TimeSpan Kestvus { get; set; } = TimeSpan.Zero;
    }
}

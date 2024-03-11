using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LoginPage.Models
{
    public class Player
    {
        public Darga Darga { get; set; }
        public int PlayerId { get; set; }
        public string PlayerMail { get; set; }
        public string PlayerPass { get; set; }
        public int PLayerPoints { get; set; }
        public string PlayerName { get; set; }
    }
}

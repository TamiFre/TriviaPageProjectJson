using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginPage.Models
{
    public class SubjectQ
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public virtual ICollection<Q> Qs { get; } = new List<Q>();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoginPage.Models;

namespace LoginPage.Service;

public class SubjectQService
{
    public SubjectQ[] SubjectQs { get; set; }
    public SubjectQService()
    {
        SubjectQs = new SubjectQ[5];
        SubjectQs[0] = new SubjectQ() { SubjectId = 1, SubjectName = "Sports" };
        SubjectQs[1] = new SubjectQ() { SubjectId = 2, SubjectName = "Pop Culture" };
        SubjectQs[2] = new SubjectQ() { SubjectId = 3, SubjectName = "Geo" };
        SubjectQs[3] = new SubjectQ() { SubjectId = 4, SubjectName = "Books" };
        SubjectQs[4] = new SubjectQ() { SubjectId = 5, SubjectName = "History" };
    }
    //למאללא
    //
}

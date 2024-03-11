using LoginPage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginPage.Service
{
    public class StatusQService
    {
        public StatusQ[] StatusQs { get; set; }
        //1 - app 2 - dec 3 - pend

        public StatusQService()
        {
             StatusQs = new StatusQ[3];
            StatusQs[0] = new StatusQ() { StatusId = 1, StatusName = "Approved" };
            StatusQs[1] = new StatusQ() { StatusId = 2, StatusName = "Declined" };
            StatusQs[2] = new StatusQ() { StatusId = 3, StatusName = "Pending" };

        }
        

    }
}

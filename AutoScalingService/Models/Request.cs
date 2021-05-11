using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoScalingService.Models
{
    public class Request
    {
        public string Id { get; set; }
        public int TaskPerformed { get; set; }
        public int AdditionalVms{ get; set; }

    }
}

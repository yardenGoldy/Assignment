using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoScalingService.Services
{
    public interface IVmManager
    {
        void Increase(int vms);
        void Decrease(int vms);
    }
}

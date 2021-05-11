using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoScalingService.Services
{
    public interface ITaskManager
    {

        string Perform(int numberOfRequest);
        string Notify(string requestId);
    }
}

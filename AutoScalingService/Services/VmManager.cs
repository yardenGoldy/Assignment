using AutoScalingService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AutoScalingService.Services
{
    public class VmManager: IVmManager
    {
        private readonly string path = "C:\\VmsLog.json";
        public VmManager()
        {
            if (!System.IO.File.Exists(path))
            {
                System.IO.File.Create(path);
            }
        }

        public void Increase(int vms)
        {
            VmLog log = new VmLog() { NumberOfVms = vms, Timestamp = DateTime.Now, Action = VmAction.INCREASE };
            string json = JsonConvert.SerializeObject(log);
            System.IO.File.AppendAllText(path, json);
        }

        public void Decrease(int vms)
        {
            VmLog log = new VmLog() { NumberOfVms = vms, Timestamp = DateTime.Now, Action = VmAction.DECREASE };
            string json = JsonConvert.SerializeObject(log);
            System.IO.File.AppendAllText(path, json);
        }
    }
}

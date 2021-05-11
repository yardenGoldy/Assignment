using AutoScalingService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoScalingService.Services
{
    public class TaskManager: ITaskManager
    {
        public const int MAX_VMS_AT_THE_SAME_TIME = 300;
        public const int MAX_REQUEST_FOR_MACHINE = 100;
        public int NumberOfRequestForCluster { get; set; }
        public int NumberOfVms { get; set; }
        static readonly object _object = new object();

        private IRequestHandler requestHandler;
        private IVmManager vmManager;

        public TaskManager(int numberOfVms, IRequestHandler requestHandler, IVmManager vmManager)
        {
            this.NumberOfVms = numberOfVms;
            this.NumberOfRequestForCluster = this.NumberOfVms * MAX_REQUEST_FOR_MACHINE;
            this.requestHandler = requestHandler;
            this.vmManager = vmManager;
        }

        public TaskManager(int numberOfVms, IRequestHandler requestHandler) :this(numberOfVms, requestHandler, new VmManager())
        {
        }

        public TaskManager()
        {
        }

        public string Perform(int numberOfRequest)
        {
            Request newRequest;
            lock(_object)
            {
                if (numberOfRequest <= NumberOfRequestForCluster)
                {
                    newRequest = new Request() { TaskPerformed = numberOfRequest, AdditionalVms = 0 };
                    this.requestHandler.Push(newRequest);
                    NumberOfRequestForCluster -= numberOfRequest;
                }
                else
                {

                    try
                    {
                        int AdditionalVms = this.ComputeNumberOfVms(numberOfRequest);
                        newRequest = new Request() { TaskPerformed = NumberOfRequestForCluster, AdditionalVms = AdditionalVms };
                        this.requestHandler.Push(newRequest);
                        this.vmManager.Increase(newRequest.AdditionalVms);
                        NumberOfRequestForCluster = 0;
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }

                }
            }
            

            return newRequest.Id;
        }

        public string Notify(string requestId)
        {
            Request request;
            try
            {
                request = this.requestHandler.Get(requestId);
            }
            catch(Exception e)
            {
                throw e;
            }
            
            this.requestHandler.Delete(requestId);
            this.NumberOfRequestForCluster += request.TaskPerformed;
            if (request.AdditionalVms > 0)
            {
                this.vmManager.Decrease(request.AdditionalVms);
            }

            return requestId;
        }


        private int ComputeNumberOfVms(int numberOfRequest)
        {
            double vms = (numberOfRequest - NumberOfRequestForCluster) / MAX_REQUEST_FOR_MACHINE;
            int additionalVms = int.Parse(Math.Ceiling(vms).ToString());
            if(additionalVms > MAX_VMS_AT_THE_SAME_TIME)
            {
                throw new Exception("The task is too heavy, cannot proccess it");
            }
            else
            {
                return additionalVms;
            }
        }

        
    }
}
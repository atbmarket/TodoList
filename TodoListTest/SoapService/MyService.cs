using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SoapService
{
    // to test how approval works, try to comment uncommented and uncomment commented, 
    // or change the service in any other way

    [ServiceContract]
    public interface IMyService
    {
        [OperationContract]
        void DoWork();
        //void DoWork(string message);
    }

    public class MyService : IMyService
    {
        //public void DoWork(string message) { }
        public void DoWork() { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;  
namespace UniversalServichs

{
    [ServiceContract]
    public interface IUniversalServichsCallback
    {
        [OperationContract(IsOneWay = true)]
        void UpdateStatistics(StatisticsService statisticsService);


    }
}

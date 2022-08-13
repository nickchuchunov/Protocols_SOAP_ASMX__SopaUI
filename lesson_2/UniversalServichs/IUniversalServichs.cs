using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace UniversalServichs
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IUniversalServichsCallback) ) ]
    public interface IUniversalServichs
    {
        [OperationContract(IsOneWay =true)]
        void RunScript();

        [OperationContract(IsOneWay = true)]
        void UpdateAndCompileScript(string fileName);

        [OperationContract(IsOneWay = true)]
        void CompileScript();


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using UniversalServichsClient.UniversalServiceReference;


namespace UniversalServichsClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            InstanceContext instanceContext = new InstanceContext(new CallbackHandler());
            UniversalServichsClient.UniversalServiceReference.UniversalServichsClient universalServichsClient = new UniversalServichsClient.UniversalServiceReference.UniversalServichsClient(instanceContext);

            universalServichsClient.CompileScript();
            universalServichsClient.RunScript();

            Console.ReadKey();
            universalServichsClient.Close();




        }
    }
}

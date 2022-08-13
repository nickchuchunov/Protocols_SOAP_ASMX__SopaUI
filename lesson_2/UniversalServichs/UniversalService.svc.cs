using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace UniversalServichs
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Service1.svc или Service1.svc.cs в обозревателе решений и начните отладку.

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class UniversalServichs : IUniversalServichs
    {
        private readonly IScriptService _scriptService;
        private readonly IStatisticsService _statisticsService;
        private readonly ISettingsService _serviceSettings;


        UniversalServichs()
        {
            _statisticsService = new StatisticsService();
            _serviceSettings = new SettingsService();
            _scriptService = new ScriptService(_serviceSettings, _statisticsService, Callback);


        }




        public void CompileScript()
        {
            _scriptService.Compile();
        }

        public void RunScript()
        {
            _scriptService.Run();
        }

        public void UpdateAndCompileScript(string fileName)
        {
            _serviceSettings.FileName = fileName;
            _scriptService.Compile();
        }

        IUniversalServichsCallback Callback
        {
            get
            {
                return OperationContext.Current.GetCallbackChannel<IUniversalServichsCallback>();
            }
        }




    }
}

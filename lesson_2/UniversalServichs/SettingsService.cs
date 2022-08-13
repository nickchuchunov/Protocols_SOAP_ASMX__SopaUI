using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversalServichs
{
    public class SettingsService : ISettingsService
    {
        public string FileName { get; set; }


        public SettingsService() { FileName = @"L:/Учеба/Protocols_SOAP_ASMX__SopaUI/lesson_2/Sample.script"; }

    }
}
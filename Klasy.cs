using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCP
{
    class Klasy
    {
        public class DzienPracy
        {
            public string KodPracownika { get; set; }
            public DateTime Data { get; set; }
            public TimeSpan GodzinaWejscia { get; set; }
            public TimeSpan GodzinaWyjscia { get; set; }

            public DzienPracy(string kodPracownika, DateTime data, TimeSpan godzinaWejscia, TimeSpan godzinaWyjscia)
            {
                KodPracownika = kodPracownika;
                Data = data;
                GodzinaWejscia = godzinaWejscia;
                GodzinaWyjscia = godzinaWyjscia;
            }
        }

    }
}

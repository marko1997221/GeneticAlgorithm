using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IspitniZadatak
{
     public class GenerationElement
    {
        public List<Data> Kombinacija { get; set; }
        public double VrednostCostFunkcije { get; set; }
        public double VerovatnocaElementa { get; set; }
        public GenerationElement(List<Data> _kombinacija, double _vrednost)
        {
            Kombinacija = _kombinacija;
            VrednostCostFunkcije = _vrednost;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IspitniZadatak
{
     public class BackgroundClass
    {
        public StringBuilder sb = new StringBuilder();
        public double MinimalanVrednostCostFunkcije = double.MaxValue;
        private const double _verovatnocaUkrstanja = 1;
        private double _verovatnocaMutacije = 0;
        public List<Data> NajboljeResenje = new List<Data>();
        Random rnd = new Random();
        public List<Data> Osnovna_lista = new List<Data>();
        private const string loadingString = @"C:\Users\Marko\Desktop\Ispit_Iz_optimizacionihAlgoritama\IspitniZadatak\IspitniZadatak\bin\Debug\in263.txt";
        public BackgroundClass()
        {
             ReadingData(loadingString);

        }
        void ReadingData(string dataString)
        {
            int counter = 1;
            var data = File.ReadAllLines(dataString);
            foreach (var item in data)
            {
                var newitem = item.Split(' ');
                Data podatak = new Data();
                podatak.X = double.Parse(newitem[0]);
                podatak.Y = double.Parse(newitem[2]);
                podatak.ID = counter;
                counter++;
                Osnovna_lista.Add(podatak);
            }
            
            
        }
        private  int CompareVerovatnoce(Data num1, Data num2)
        {
            return num1.VerovatnocaRedosleda.CompareTo(num2.VerovatnocaRedosleda);
        }
        public List<GenerationElement> FirstGeneration()
        {
            List<GenerationElement> prvaGeneracija = new List<GenerationElement>();
            List<Data> novaLista = new List<Data>();
            for (int i = 0; i < 100; i++)
            {
            novaLista = Osnovna_lista;
            foreach (var item in novaLista)
            {
                item.VerovatnocaRedosleda = rnd.NextDouble();
            }
                novaLista.Sort(CompareVerovatnoce);
                var vrednost = RacunanjeCostFunkcije(novaLista);
                if (vrednost<=MinimalanVrednostCostFunkcije)
                {
                    MinimalanVrednostCostFunkcije = vrednost;
                    NajboljeResenje = novaLista;
                    
                }
            GenerationElement ge = new GenerationElement(novaLista, vrednost);
                ge.VerovatnocaElementa = rnd.NextDouble() / 263;
                prvaGeneracija.Add(ge);
                sb.AppendLine(MinimalanVrednostCostFunkcije.ToString());
            }
            return prvaGeneracija;

        }
        private int UporedjivanjeVerovatnoce(GenerationElement el1, GenerationElement el2)
        {
            return el1.VerovatnocaElementa.CompareTo(el2.VerovatnocaElementa);
        }
        public List<GenerationElement> Selekcija(List<GenerationElement> lista)
        {
            lista.Sort(UporedjivanjeVerovatnoce);
            lista.Reverse();
            return lista.Take(250).ToList();
        }
        public List<GenerationElement> Ukrstanje_i_mutacija(List<GenerationElement> lista)
        {
            List<Data> roditeljski_deo = new List<Data>();
            List<GenerationElement> nova_lista = new List<GenerationElement>();
            List<int> indeksi_mutacije = new List<int>();
            var prvi = rnd.Next(1, 25);
            var drugi = rnd.Next(1, 25);
            GenerationElement ge1 = lista[prvi - 1];
            GenerationElement ge2 = lista[drugi - 1];
            var verovatnocaUkrstanja = rnd.NextDouble();
            var broj_gena_nasumicni = rnd.Next(1, 263);
            int prvo_mesto = rnd.Next(1, 263);
            int drugo_mesto = rnd.Next(1, 263);
            double verovatnocaMutacije = rnd.NextDouble();
            int broj_indeksi_koji_mutira = rnd.Next(0, 1);
            for (int j = 0; j < 100; j++)
            {
                indeksi_mutacije.Clear();
                 prvi = rnd.Next(1, 25);
                 drugi = rnd.Next(1, 25);
                 ge1 = lista[prvi - 1];
                 ge2 = lista[drugi - 1];
                 verovatnocaUkrstanja = rnd.NextDouble();
                 broj_gena_nasumicni = rnd.Next(1, 263);
                 prvo_mesto = rnd.Next(1, 263);
                 drugo_mesto = rnd.Next(1, 263);
                 verovatnocaMutacije = rnd.NextDouble();
                broj_indeksi_koji_mutira = rnd.Next(0, 1);
                var broj = rnd.Next(0, 262);
                for (int i = 0; i < broj_indeksi_koji_mutira; i++)
                {
                    //ispitavanje da li se onavlajju indeski koji muiraju
                    if (!indeksi_mutacije.Any(p=>p==broj))
                    {
                        indeksi_mutacije.Add(broj);
                        broj = rnd.Next(0, 262);
                    }
                    else
                    {
                        broj = rnd.Next(0, 262);
                        i--;
                    }
                    
                }
                //while (prvi == drugi || prvo_mesto == drugo_mesto || verovatnocaUkrstanja > _verovatnocaUkrstanja)
                //{
                //    indeksi_mutacije.Clear();
                //    prvi = rnd.Next(1, 250);
                //    drugi = rnd.Next(1, 250);
                //    ge1 = lista[prvi - 1];
                //    ge2 = lista[drugi - 1];
                //    verovatnocaUkrstanja = rnd.NextDouble();
                //    broj_gena_nasumicni = rnd.Next(1, 263);
                //    prvo_mesto = rnd.Next(1, 263);
                //    drugo_mesto = rnd.Next(1, 263);
                //    verovatnocaMutacije = rnd.NextDouble();
                //    broj_indeksi_koji_mutira = rnd.Next(0, 2);
                //    for (int i = 0; i < broj_indeksi_koji_mutira; i++)
                //    {
                //        if (!indeksi_mutacije.Any(p => p == broj))
                //        {
                //            indeksi_mutacije.Add(broj);
                //            broj = rnd.Next(0, 262);
                //        }
                //        else
                //        {
                //            broj = rnd.Next(0, 262);
                //            i--;
                //        }
                //    }

                //}
                if (verovatnocaUkrstanja <= _verovatnocaUkrstanja)
                {
                    if (prvo_mesto < drugo_mesto)
                    {
                        roditeljski_deo = ge1.Kombinacija.Where(p => ge1.Kombinacija.IndexOf(p) >= prvo_mesto - 1 && ge1.Kombinacija.IndexOf(p) <= drugo_mesto - 1).ToList();
                        var ostatak = ge2.Kombinacija.Where((p) =>
                        {
                            if (roditeljski_deo.Any(p1 => p.ID == p1.ID))
                            {
                                return false;
                            }
                            return true;

                        }).ToList();
                        roditeljski_deo=roditeljski_deo.Concat(ostatak).ToList();
                    }
                    else
                    {
                        roditeljski_deo = ge1.Kombinacija.Where(p => ge1.Kombinacija.IndexOf(p) >= drugo_mesto - 1 && ge1.Kombinacija.IndexOf(p) <= prvo_mesto - 1).ToList();
                        var ostatak = ge2.Kombinacija.Where((p) =>
                        {
                            if (roditeljski_deo.Any(p1 => p.ID == p1.ID))
                            {
                                return false;
                            }
                            return true;

                        }).ToList();
                        roditeljski_deo = roditeljski_deo.Concat(ostatak).ToList();
                        
                    }

                }
                else
                {
                    roditeljski_deo = lista[prvi - 1].Kombinacija;
                }
                if (verovatnocaMutacije < _verovatnocaMutacije)
                {
                    if (broj_indeksi_koji_mutira % 2 == 0)
                    {
                        var couter = 0;
                        while (indeksi_mutacije.Count != 0)
                        {
                            var temp1 = roditeljski_deo.FindIndex(p => p.ID == indeksi_mutacije[couter]);
                            indeksi_mutacije.Remove(indeksi_mutacije[couter]);
                            var temp2 = roditeljski_deo.FindIndex(p => p.ID == indeksi_mutacije[couter]);
                            indeksi_mutacije.Remove(indeksi_mutacije[couter]);
                            if (temp1 == -1 || temp2 == -1)
                            {
                                break;
                            }
                            var temp3 = roditeljski_deo[temp1];
                            roditeljski_deo[temp1] = roditeljski_deo[temp2];
                            roditeljski_deo[temp2] = temp3;
                            
                        }


                    }
                    else
                    {
                        indeksi_mutacije.Remove(indeksi_mutacije[0]);
                        var couter = 0;
                        while (indeksi_mutacije.Count != 0)
                        {
                            var temp1 = roditeljski_deo.FindIndex(p => p.ID == indeksi_mutacije[couter]);
                            indeksi_mutacije.Remove(indeksi_mutacije[couter]);
                            var temp2 = roditeljski_deo.FindIndex(p => p.ID == indeksi_mutacije[couter]);
                            indeksi_mutacije.Remove(indeksi_mutacije[couter]);
                            if (temp1==-1 || temp2==-1)
                            {
                                break;
                            }
                            var temp3 = roditeljski_deo[temp1];
                            roditeljski_deo[temp1] = roditeljski_deo[temp2];
                            roditeljski_deo[temp2] = temp3;
                        }
                    }
                    
                }
                var test = Osnovna_lista.Except(roditeljski_deo).ToList();
                if (test.Count != 0)
                {
                    test = null;
                }
                indeksi_mutacije.Clear();
                GenerationElement ge = new GenerationElement(roditeljski_deo, RacunanjeCostFunkcije(roditeljski_deo));
                ge.VerovatnocaElementa = rnd.NextDouble() / 263;
                if (ge.VrednostCostFunkcije<MinimalanVrednostCostFunkcije)
                {
                    MinimalanVrednostCostFunkcije = ge.VrednostCostFunkcije;
                    NajboljeResenje = ge.Kombinacija;
                    
                }
                nova_lista.Add(ge);
                sb.AppendLine(MinimalanVrednostCostFunkcije.ToString());
            }
            return nova_lista;
            
        }
        private double RacunanjeCostFunkcije(List<Data> datas)
        {
            double sum = 0;
            for (int i = 0; i < datas.Count-1; i++)
            {
                sum += Math.Sqrt(Math.Pow(Math.Abs(datas[i].X - datas[i + 1].X), 2) + Math.Pow(Math.Abs(datas[i].Y - datas[i + 1].Y), 2));
            }
            return sum;
        }
        public List<GenerationElement> SelekcijaTurnir(List<GenerationElement> datas)
        {
            List<GenerationElement> selektovana = new List<GenerationElement>();
            var index1 = rnd.Next(0, 99);
            var index2 = rnd.Next(0, 99);
            for (int i = 0; i < 25; i++)
            {
                if (index1!=index2)
                {
                    if (datas[index1].VrednostCostFunkcije<datas[index2].VrednostCostFunkcije)
                    {
                        selektovana.Add(datas[index1]);
                    }
                    else
                    {
                        selektovana.Add(datas[index2]);
                    }
                }
                else
                {
                    selektovana.Add(datas[index1]);
                }
                index1 = rnd.Next(0, 99);
                index2 = rnd.Next(0, 99);
            }
            return selektovana;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IspitniZadatak
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        List<GenerationElement> Generacija = new List<GenerationElement>();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StringBuilder sbNajbolje = new StringBuilder();
            string fileName = null;
            string read = Directory.GetCurrentDirectory() + @"\inkrement.txt";
            var kombinacija_najbolja = Directory.GetCurrentDirectory() + @"\TurnirSelekcija\2000000\Kombinacija.txt";
            var a1 = int.Parse(File.ReadAllText(read));
            if (a1 != 0)
            {
                fileName = Directory.GetCurrentDirectory() + @"\TurnirSelekcija\2000000\Mahesh" + a1.ToString() + ".txt";
                kombinacija_najbolja = Directory.GetCurrentDirectory() + @"\TurnirSelekcija\2000000\Kombinacija" + a1.ToString() +".txt";
            }
            else
            {
                fileName = Directory.GetCurrentDirectory() + @"\TurnirSelekcija\2000000\Mahesh.txt";
                kombinacija_najbolja = Directory.GetCurrentDirectory() + @"\TurnirSelekcija\2000000\Kombinacija.txt";
            }
            a1++;   
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            BackgroundClass background = new BackgroundClass();
            Generacija = background.FirstGeneration();
            int generacija = 1;
            do
            {
                Generacija=background.Ukrstanje_i_mutacija(background.SelekcijaTurnir(Generacija));
                generacija++;
                File.AppendAllText(fileName, background.sb.ToString());
                background.sb.Clear();

            } while (generacija<2000);
            var a = background.MinimalanVrednostCostFunkcije;
            var b = background.NajboljeResenje;
            //File.
            //File.WriteAllText(fileName, background.sb.ToString());
            File.WriteAllText(read, a1.ToString());
            foreach (var item in background.NajboljeResenje)
            {
                //sbNajbolje.Append(item.ID.ToString() + " ");
                sbNajbolje.Append(item.X.ToString());
                sbNajbolje.Append(" " + item.Y.ToString() + "\n");
            }
            File.WriteAllText(kombinacija_najbolja, sbNajbolje.ToString());
            //#region pokretanje aplikacije 20 puta
            //if (a1 <= 10)
            //{
            //    Process.Start("IspitniZadatak");
            //}

            //#endregion
        }
    }
}

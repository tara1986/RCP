using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RCP
{
    class MainWindowViewModel:INPC
    {

        public MainWindowViewModel()
        {
            VisibilityStart = Visibility.Hidden;   
            Visibility = Visibility.Hidden;
            EnabledSciezka = true;
         
        }

        #region Pola
       

        private Visibility visibilityStart;
        public Visibility VisibilityStart
        {
            get { return visibilityStart; }
            set
            {
                visibilityStart = value;
                OnPropertyChanged("VisibilityStart");
            }
        }

        private Visibility visibility;
        public Visibility Visibility
        {
            get { return visibility; }
            set
            {
                visibility = value;
                OnPropertyChanged("Visibility");
            }
        }

        private string sciezka;
        public string Sciezka
        {
            get { return sciezka; }
            set
            {
                sciezka = value;
                if (sciezka != "") VisibilityStart = Visibility.Visible;
                else VisibilityStart = Visibility.Hidden;
                OnPropertyChanged("Sciezka");

            }
        }

        private IEnumerable<Klasy.DzienPracy> siatka;
        public IEnumerable<Klasy.DzienPracy> Siatka
        {
            get
            {
                return siatka;
            }
            set
            {
                siatka = value;
                OnPropertyChanged("Siatka");
            }
        }

        private Boolean enabledSciezka;
        public Boolean EnabledSciezka
        {
            get { return enabledSciezka; }
            set
            {
                enabledSciezka = value;
                OnPropertyChanged("EnabledSciezka");
            }
        }
        #endregion//Pola


        #region Polecenia
        private ICommand wybierz;
        private ICommand zamknij;
        private ICommand wczytaj;
        public ICommand Wybierz
        {
            get
            {
                if (wybierz == null)
                    wybierz = new RelayCommand(argument =>
                    {
                        Sciezka = WybierzSciezke(); // działanie
                    });
                return wybierz;
            }
        }

       
        public ICommand Zamknij
        {
            get
            {
                if (zamknij == null)
                    zamknij = new RelayCommand(argument =>
                    {
                        Window window = argument as Window;                       
                        window.Close();
                    });
                return zamknij;
            }
        }

        

        public ICommand Wczytaj
        {
            get
            {
                if (wczytaj == null)
                    wczytaj = new RelayCommand(argument =>
                    {
                        if (WalidujPrzedWczytaniem())
                            WczytajDane(); // działanie
                    });
                return wczytaj;
            }
        }
        #endregion//Polecenia

        #region Funkcje
        private string WybierzSciezke()
        {
            OpenFileDialog folderBrowser = new OpenFileDialog();
            folderBrowser.ValidateNames = false;
            folderBrowser.CheckFileExists = false;
            folderBrowser.CheckPathExists = true;
            folderBrowser.FileName = "Folder Selection.";
            folderBrowser.ShowDialog();
            return Path.GetDirectoryName(folderBrowser.FileName);
        }

        private bool WalidujPrzedWczytaniem()
        {
            if (!Directory.Exists(Sciezka))
            {
                MessageBox.Show("Podana ścieżka do folderu nie istnieje.", "Komunikat.", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
                else
            {
                var liczbaPlikowWFolderze = Directory.GetFiles(Sciezka, "*", SearchOption.AllDirectories).Length;
                if (liczbaPlikowWFolderze<=0)
                {
                    //uwaga! można jeszcze w dodatkowym kroku sprawdzić, czy pliki znajdujące się w folderze mają odpowiednie rozszerzenie (np. csv)
                    MessageBox.Show("W podanym folderze nie ma plików.", "Komunikat.", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else
                {
                    if (Siatka != null)
                    {
                        MessageBoxResult result = MessageBox.Show("Po ponownym wczytaniu obecne dane zostaną zastąpione. Czy chcesz kontynuować?", "Pytanie", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes)
                        {
                            Siatka = null;
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("Przerwano wczytywanie.", "Komunikat.", MessageBoxButton.OK, MessageBoxImage.Information);
                            return false;
                        }
                       
                    }
                    return true;
                }
                  
            }
        }

        private void WczytajDane()
        {
            EnabledSciezka = false;
            VisibilityStart = Visibility.Hidden;
            //wczytywanie danych
            string[] nazwyPlikow = Directory.GetFiles(Sciezka);
            for (int i=0; i< nazwyPlikow.Count(); i++)
            {
                var wynik = ReadCSV(nazwyPlikow[i]);
                if (Siatka == null) Siatka = wynik;
                else
                {
                    Siatka = Siatka.Concat(wynik);
                    
                }

            }
            Visibility = Visibility.Visible;
            EnabledSciezka = true;
            VisibilityStart = Visibility.Visible;
        }

        private IEnumerable<Klasy.DzienPracy> ReadCSV(string fileName)
        {
            string[] lines = File.ReadAllLines(System.IO.Path.ChangeExtension(fileName, ".csv"));
            //uwaga! - można dodatkowo sprawdzać, czy plik nie jest pusty
            int iloscElementowWiersza = lines[0].Split(';').Count();

            switch (iloscElementowWiersza)
            {
                case 4:
                    List<Klasy.DzienPracy> dane = new List<Klasy.DzienPracy>();

                    DataTable WeWY = ConvertCSVtoDataTable(fileName);

                    DataRow[] results = WeWY.Select("Typ = 'WE'");

                    foreach (DataRow row in results)
                    {
                        var godzina = WeWY.AsEnumerable()
                                      .Where(r => r.Field<string>("Kod") == row[0].ToString()
                                      && r.Field<string>("Data") == (row[1]).ToString()
                                      && r.Field<string>("Typ") == "WY")
                                      .Select(x => x.Field<string>("Godzina")).FirstOrDefault();
                        if (godzina != null)
                        dane.Add(new Klasy.DzienPracy(row[0].ToString(), Convert.ToDateTime(row[1].ToString()), TimeSpan.Parse(row[2].ToString()), TimeSpan.Parse(godzina.ToString())));

                    }
                    return dane;

                case 5:

                    return lines.Select(line =>
                    {
                        string[] data = line.Split(';');
                        return new Klasy.DzienPracy(data[0],Convert.ToDateTime(data[1]), TimeSpan.Parse(data[2]), TimeSpan.Parse(data[3]));
                    });

                default:
                    MessageBox.Show("Struktura plików odbiega od ustalonego standardu. Nie można wczytać.", "Komunikat.", MessageBoxButton.OK, MessageBoxImage.Error);
                    return new List<Klasy.DzienPracy>();                         
            }
           
        }

        public static DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(strFilePath))
            {
                //string[] headers = sr.ReadLine().Split(';');
                string[] headers = { "Kod", "Data","Godzina","Typ" };
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }
                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(';');
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i];
                    }
                    dt.Rows.Add(dr);
                }

            }


            return dt;
        }
        #endregion //Funkcje
    }
}

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using vipief.Model;
using vipief.ViewModel.Helpers;

namespace vipief.ViewModel
{
    internal class MainViewModel : BindingHelper
    {
        #region Команды
        public BindableCommand AddCommand { get; set; }
        public BindableCommand DeleteCommand { get; set; }
        public BindableCommand SohranenieCommand { get; set; }
        #endregion

        #region Свойства

        private Colour selected = new Colour();
        public Colour Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Colour> colours;
        public ObservableCollection<Colour> Colours
        {
            get { return colours; }
            set
            {
                colours = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public void AddItems()
        {
            Colours.Add(Selected);
        }

        public void DeleteItems()
        {
            Colours.Remove(Selected);
        }

        public void Sohranenie()
        {
            ObservableCollection<Colour> cc = new ObservableCollection<Colour>();
            cc = Colours;
            string json = JsonConvert.SerializeObject(cc);
            File.WriteAllText("C:\\Users\\Алексей\\OneDrive\\Рабочий стол\\Result.json", json);
        }

        public MainViewModel()
        {
            string text = File.ReadAllText("C:\\Users\\Алексей\\OneDrive\\Рабочий стол\\Result.json");
            List<Colour> result = JsonConvert.DeserializeObject<List<Colour>>(text);
            ObservableCollection<Colour> colours = new ObservableCollection<Colour>();
            for (int i = 0; i <= result.Count-1; i++)
            {
                colours.Add(result[i]);
            }
            Colours = colours;
            AddCommand = new BindableCommand(_ => AddItems());
            DeleteCommand = new BindableCommand(_ => DeleteItems());
            SohranenieCommand = new BindableCommand(_ => Sohranenie());
        }
    }
}
using System.IO;
using System.Windows;
using Newtonsoft.Json;
using Excursions.Converters;
using Excursions.DataTransferObject;

namespace Excursions
{
    public partial class MainWindow 
    {
        private readonly Tour _tour;
        private const string JsonFilePath = "excursions.json";

        public MainWindow()
        {
            InitializeComponent();
            _tour = File.Exists(JsonFilePath) ? DeserializeFromJson(JsonFilePath) : new Tour(DateTime.Now);
            UpdateExcursionList();
            
            MaxWidth = MinWidth = 900;
            MaxHeight = MinHeight = 600;
        }

        private void UpdateExcursionList()
        {
            ExcursionsListBox.Items.Clear();
            
            foreach (var excursion in _tour.Excursions)
            {
                ExcursionsListBox.Items.Add(excursion.ToString());
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Organizer organizer = new Organizer("New", "Organizer");
            Excursion excursion = new Excursion(organizer, FormOfConduct.BusExcursion, 10, "A", DateTime.Now);
            ExcursionWindow excursionWindow = new ExcursionWindow(excursion);
            
            if (excursionWindow.ShowDialog() == true)
            {
                _tour.AddExcursion(excursion);
                UpdateExcursionList();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (ExcursionsListBox.SelectedItem != null)
            {
                int selectedIndex = ExcursionsListBox.SelectedIndex;
                Excursion excursion = _tour.Excursions[selectedIndex];
                ExcursionWindow excursionWindow = new ExcursionWindow(excursion);
                
                if (excursionWindow.ShowDialog() == true)
                {
                    UpdateExcursionList();
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ExcursionsListBox.SelectedItem != null)
            {
                int selectedIndex = ExcursionsListBox.SelectedIndex;
                _tour.Excursions.RemoveAt(selectedIndex);
                UpdateExcursionList();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SerializeToJson(_tour, JsonFilePath);
        }

        private void SerializeToJson(Tour tour, string filePath)
        {
            TourDTO dto = tour.ToDTO();
            string json = JsonConvert.SerializeObject(dto, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        private Tour DeserializeFromJson(string filePath)
        {
            string json = File.ReadAllText(filePath);
            TourDTO? dto = JsonConvert.DeserializeObject<TourDTO>(json);
            return dto!.FromDTO();
        }
    }
}

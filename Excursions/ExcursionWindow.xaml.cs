using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Excursions
{
    public partial class ExcursionWindow : Window
    {
        private Excursion _excursion;
        private readonly Excursion _originalExcursion;

        public ExcursionWindow(Excursion excursion)
        {
            InitializeComponent();
            _excursion = excursion;
            _originalExcursion = (Excursion)excursion.Clone();
            FormOfConductComboBox.ItemsSource = Enum.GetValues(typeof(FormOfConduct));
            PopulateFields(); // Заповнення полів вікна даними екскурсії
        }

        private void PopulateFields()
        {
            // Заповнення полів вікна даними екскурсії
            FormOfConductComboBox.SelectedItem = _excursion.FormOfConduct;
            FirstNameTextBox.Text = _excursion.Organizer.FirstName;
            LastNameTextBox.Text = _excursion.Organizer.LastName;
            PriceTextBox.Text = _excursion.Price.ToString();
            LocationTextBox.Text = _excursion.Location;
            DatePicker.SelectedDate = _excursion.Date;
        }
        
        private void NumericTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextNumeric(e.Text);
        }

        private bool IsTextNumeric(string text)
        {
            return Regex.IsMatch(text, @"^[0-9]*(?:\[0-9]*)?$");
        }
        
        private void DatePicker_SelectedDateChanged(object? sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            if (DatePicker.SelectedDate.HasValue && DatePicker.SelectedDate.Value.Date < DateTime.Today)
            {
                DatePicker.SelectedDate = DateTime.Today;
            }
        }
        
        private bool ValidateInputs()
        {
            return false;
        }
        
        private void SaveAndCloseButton_Click(object sender, RoutedEventArgs e)
        {
            SaveExcursionData();
            
            DialogResult = true;
            Close();
        }

        private void CancelAndCloseButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
        
        
        private void SaveExcursionData()
        {
            var newOrganizer = new Organizer(FirstNameTextBox.Text, LastNameTextBox.Text);
            _excursion.Organizer = newOrganizer;
            _excursion.FormOfConduct = (FormOfConduct)FormOfConductComboBox.SelectedItem;
            _excursion.Price = int.Parse(PriceTextBox.Text);
            _excursion.Location = LocationTextBox.Text;
            _excursion.Date = DatePicker.SelectedDate.GetValueOrDefault();
        }
        
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DialogResult != true)
            {
                if (!_excursion.Equals(_originalExcursion))
                {
                    var result = MessageBox.Show("Do you want to keep the changes?", "Confirmation", MessageBoxButton.YesNoCancel);
                    if (result == MessageBoxResult.Yes)
                    {
                        SaveExcursionData();
                        DialogResult = true;
                    }
                    else if (result == MessageBoxResult.No)
                    {
                        _excursion = (Excursion)_originalExcursion.Clone();
                        DialogResult = false;
                    }
                    else if (result == MessageBoxResult.Cancel)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }
    }
}
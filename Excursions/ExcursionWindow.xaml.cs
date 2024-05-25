using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace Excursions
{
    public partial class ExcursionWindow
    {
        private Excursion _excursion;
        private readonly Excursion _originalExcursion;

        public ExcursionWindow(Excursion excursion)
        {
            InitializeComponent();
            _excursion = excursion;
            _originalExcursion = (Excursion)excursion.Clone();
            FormOfConductComboBox.ItemsSource = Enum.GetValues(typeof(FormOfConduct));
            PopulateFields();

            MaxWidth = MinWidth = 400;
            MaxHeight = MinHeight = 380;
        }

        private void PopulateFields()
        {
            FormOfConductComboBox.SelectedItem = _excursion.FormOfConduct;
            FirstNameTextBox.Text = _excursion.Organizer.FirstName;
            LastNameTextBox.Text = _excursion.Organizer.LastName;
            PriceTextBox.Text = _excursion.Price.ToString();
            LocationTextBox.Text = _excursion.Location;
            DatePicker.SelectedDate = _excursion.Date;
        }
        
        private void SaveAndCloseButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInputs())
            {
                SaveExcursionData();
                DialogResult = true;
                Close();
            }
        }
        
        private void CancelAndCloseButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
        
        private void SaveExcursionData()
        {
            Organizer newOrganizer = new Organizer(FirstNameTextBox.Text, LastNameTextBox.Text);
            _excursion.Organizer = newOrganizer;
            _excursion.FormOfConduct = (FormOfConduct)FormOfConductComboBox.SelectedItem;
            _excursion.Price = int.Parse(PriceTextBox.Text);
            _excursion.Location = LocationTextBox.Text;
            _excursion.Date = DatePicker.SelectedDate.GetValueOrDefault();
        }
        
        private bool ValidateInputs()
        {
            if (FormOfConductComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a form of conduct!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(FirstNameTextBox.Text))
            {
                MessageBox.Show("Please enter the first name of the organizer!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(LastNameTextBox.Text))
            {
                MessageBox.Show("Please enter the last name of the organizer!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(LocationTextBox.Text))
            {
                MessageBox.Show("Please enter the location!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            if (!int.TryParse(PriceTextBox.Text, out _))
            {
                MessageBox.Show("Please enter a valid price!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            if (!DatePicker.SelectedDate.HasValue)
            {
                MessageBox.Show("Please select a date!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }
        
        private void PriceTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(PriceTextBox.Text, out int value))
            {
                _excursion.Price = value;
            }
        }
        
        private void FirstNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string value = FirstNameTextBox.Text;
            _excursion.Organizer.FirstName = value;
        }
        
        private void LastNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string value = LastNameTextBox.Text;
            _excursion.Organizer.LastName = value;
        }
        
        private void LocationTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string value = LocationTextBox.Text;
            _excursion.Location = value;
        }
        
        private void DatePicker_SelectedDateChanged(object? sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            if (DatePicker.SelectedDate.HasValue && DatePicker.SelectedDate.Value.Date < DateTime.Today)
            {
                DatePicker.SelectedDate = DateTime.Today;
            }
        }
        
        private void NumericTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextNumeric(e.Text);
        }

        private bool IsTextNumeric(string text)
        {
            return Regex.IsMatch(text, @"^[0-9]*(?:\[0-9]*)?$");
        }
        
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DialogResult != true)
            {
                if (!_excursion.Equals(_originalExcursion))
                {
                    MessageBoxResult result = MessageBox.Show("Do you want to keep the changes?", "Confirmation", MessageBoxButton.YesNoCancel);
                    
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
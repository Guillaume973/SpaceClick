using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SpaceClicker
{
    public partial class MainWindow : Window
    {
        private bool _autoClickState = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void AutoClickStart(object sender, RoutedEventArgs e)
        {
            if (_autoClickState) return;

            if (!ParseOrZero(RegisterSec.Text,out int seconds) || !ParseOrZero(RegisterMin.Text, out int min) || !ParseOrZero(RegisterHour.Text, out int hours))
            {
                Error_Unity.Visibility = Visibility.Visible;
                return;
            }
            Error_Unity.Visibility = Visibility.Collapsed;

            _autoClickState = true;
           
            int totalSeconds = seconds + min * 60 + hours * 3600;
            if(totalSeconds == 0)
            {
                return;
            }

            State_Off.Visibility = Visibility.Collapsed;
            State_On.Visibility = Visibility.Visible;
            while (_autoClickState)
            {
                await Task.Delay(TimeSpan.FromSeconds(totalSeconds));
                if (_autoClickState) ClickAction.LeftClick();
            }
        }

        private void StopAutoClick(object sender, RoutedEventArgs e)
        {
            State_Off.Visibility = Visibility.Visible;
            State_On.Visibility = Visibility.Collapsed;
            Error_Unity.Visibility = Visibility.Collapsed;

            _autoClickState = false;
        }

        private void HideText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox != null)
            {
                Console.WriteLine("Not null");
                Visibility visibility = textBox.Text == "" ? Visibility.Visible : Visibility.Collapsed;

                if (textBox.Name.Contains("Min"))
                {
                    EnterMin.Visibility = visibility;
                }

                if (textBox.Name.Contains("Sec"))
                {
                    EnterSec.Visibility = visibility;
                }

                if (textBox.Name.Contains("Hour"))
                {
                    EnterHour.Visibility = visibility;
                }
            }
        }
        private bool ParseOrZero(string value, out int number)
        {
            if (value == null || value == "")
            {
                number = 0;
                return true;
            } 
            if (int.TryParse(value, out number)) return true;
            else return false;
        }
    }
}
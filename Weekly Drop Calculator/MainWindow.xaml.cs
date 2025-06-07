using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Weekly_Drop_Calculator
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

        private void eingabe_XP_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Nur Ziffern (0-9) und optional ein Minuszeichen zulassen
            Regex regex = new Regex("[^0-9-]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void eingabe_XP_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Verhindern, dass die Leertaste eingegeben wird
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void bttn_calculate_Click(object sender, RoutedEventArgs e)
        {
            int xp = 0;
            try
            {
                if (Convert.ToInt64(eingabe_XP.Text) > 5000)
                {
                    xp = 5000;
                    eingabe_XP.Text = "5000"; // Setze den Text auf 5000, wenn der Wert größer ist
                }
                else
                xp = Convert.ToInt32(eingabe_XP.Text);

                if (eingabe_XP.Text == "")
                {

                    MessageBox.Show("Bitte XP eingeben", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (Convert.ToInt32(eingabe_XP.Text) <= 0)
                {

                    MessageBox.Show("XP muss größer als 0 sein", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                else
                {

                    int roundXp = 120;
                    if (!chBox_xbBoost.IsChecked == true)
                    {
                        roundXp -= 90;
                    }

                    double leftRounds = (double)xp / roundXp; // Explizite Konvertierung von xp zu double
                    double leftWins = 0;
                    while (leftRounds / 13 >= 1)
                    {
                        leftWins += 1;
                        leftRounds /= 13;
                    }
                    if(leftWins >0 && leftRounds >13)
                    {
                        leftRounds -= 13;
                    }

                    int rounds = (int)Math.Ceiling(leftRounds);
                    ausgabe.Content = $"es  Fehlt {leftWins} Win/s und  {rounds} Runde/n";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Die Fehlende XP Anzahl darf nicht mehr als 5000 betragen", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            

           
        }

       

        private void eingabe_XP_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                bttn_calculate_Click(sender, e);
            }
        }
    }
}
using System;
using System.Collections.Generic;
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

namespace EldenRingHelper
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

        //Opens an instance of a MonsterFinder form upon clicking the button in the main window
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MonsterFinder.MainWindow monsterFinderWin = new MonsterFinder.MainWindow();
            monsterFinderWin.Show();
        }
    }
}

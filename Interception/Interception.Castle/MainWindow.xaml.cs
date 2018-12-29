using System;
using System.Windows;

namespace Interception.Castle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(ViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}

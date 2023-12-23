using BlueByte.Wpf.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace DynamicTextBox.Sandbox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var col = new ObservableCollection<object>();


            this.DynamicControl.Data.Add(new Item() { Text = @"C:\" });
            this.DynamicControl.Data.Add(new DynamicVariable() { Text = @"Description" });
            this.DynamicControl.Data.Add(new Item() { Text = @"\FileName" });


            var DynamicVariableMenuItems = new ObservableCollection<DynamicVariableMenuItem>();
            var topItem = new DynamicVariableMenuItem();
            topItem.Variable = new DynamicVariable() { Text = "Variables" };
            var firstItem = new DynamicVariableMenuItem();
            firstItem.Variable = new DynamicVariable() { Text = "Description" };
            var secondItem = new DynamicVariableMenuItem();
            secondItem.Variable = new DynamicVariable() { Text = "Cost" };

            topItem.Add(firstItem);
            topItem.Add(secondItem);
           

            this.DynamicControl.DynamicVariableMenuItems.Add(topItem);
        }

    }
}

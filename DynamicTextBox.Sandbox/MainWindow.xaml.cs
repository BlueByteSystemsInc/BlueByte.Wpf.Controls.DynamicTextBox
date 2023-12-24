using BlueByte.SOLIDWORKS.PDMProfessional.Extensions;
using BlueByte.Wpf.Controls;
using EPDM.Interop.epdm;
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



            var vault = new EdmVault5();
            vault.LoginAuto("BlueByte", 0);

            var ret = DynamicVariableMenuItem.Create(vault.GetVariableNames());


            ret.ToList().ForEach(x=> this.DynamicControl.DynamicVariableMenuItems.Add(x));
        }

    }
}

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

namespace BlueByte.Wpf.Controls
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:DynamicTextBox"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:DynamicTextBox;assembly=DynamicTextBox"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    /// 


    public class DynamicTextBox : Control
    {
        public Item LastSelectedItem { get; set; }


        static DynamicTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DynamicTextBox), new FrameworkPropertyMetadata(typeof(DynamicTextBox)));
        
        
        }

        public DynamicTextBox()
        {
            this.Data.CollectionChanged += Data_CollectionChanged;
          
        }

        private void Data_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    var elements = e.NewItems;
                    foreach (var element in elements)
                    {
                        var item = element as Item;
                        if (item != null)
                            item.PropertyChanged += Item_PropertyChanged;
                    }
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    var deletedElements = e.OldItems;
                    foreach (var element in deletedElements)
                    {
                        var item = element as Item;
                        if (item != null)
                            item.PropertyChanged -= Item_PropertyChanged;
                    }
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    break;
                default:
                    break;

                  
            }

            RefreshEvaluatedPath();
        }

        private void Item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Item.CursorPosition))
            LastSelectedItem = sender as Item; 
        }

        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand =>
            removeCommand ?? (removeCommand = new RelayCommand(ExecuteRemoveCommand));

        void ExecuteRemoveCommand(object parameter)
        {


            var index = this.Data.IndexOf(parameter);

            var previousIndex = index - 1;
            var nextIndex = index + 1;

            var previousItem = default(Item);
            var nextItem = default(Item);

            

            try
            {
                nextItem = Data[nextIndex] as Item;
            }
            catch (Exception)
            {

            }

            try
            {
                previousItem = Data[previousIndex] as Item;
            }
            catch (Exception)
            {

            }


            if (parameter != null)
              this.Data.Remove(parameter);


            if (previousItem != null && nextItem != null)
            {
                var newItem = new Item() { Text = $"{previousItem.Text}{nextItem.Text}" };
                this.Data.Insert(previousIndex, newItem);
                this.Data.Remove(previousItem);
                this.Data.Remove(nextItem);

                RefreshEvaluatedPath();
            }
        }







        public string EvaluatedPath
        {
            get { return (string)GetValue(EvaluatedPathProperty); }
            set { SetValue(EvaluatedPathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EvaluatedPath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EvaluatedPathProperty =
            DependencyProperty.Register("EvaluatedPath", typeof(string), typeof(DynamicTextBox), new PropertyMetadata(string.Empty));




        public ObservableCollection<object> Data
        {
            get { return (ObservableCollection<object>)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Data.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(ObservableCollection<object>), typeof(DynamicTextBox), new PropertyMetadata(new ObservableCollection<object>(), OnDataChanged));

        private static void OnDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as DynamicTextBox;

            control.RefreshEventHandlers();
           

        }


        public ObservableCollection<DynamicVariableMenuItem> DynamicVariableMenuItems
        {
            get { return (ObservableCollection<DynamicVariableMenuItem>)GetValue(DynamicVariableMenuItemsProperty); }
            set { SetValue(DynamicVariableMenuItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Data.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DynamicVariableMenuItemsProperty =
            DependencyProperty.Register("DynamicVariableMenuItems", typeof(ObservableCollection<DynamicVariableMenuItem>), typeof(DynamicTextBox), new PropertyMetadata(new ObservableCollection<DynamicVariableMenuItem>()));

       


        private RelayCommand addDynamicVariableCommand;
        public RelayCommand AddDynamicVariableCommand =>
            addDynamicVariableCommand ?? (addDynamicVariableCommand = new RelayCommand(ExecuteAddDynamicVariableCommand));


        public void RefreshEventHandlers()
        {
            this.Data.CollectionChanged -= Data_CollectionChanged;
            this.Data.CollectionChanged += Data_CollectionChanged;

        }

        public void RefreshEvaluatedPath()
        {
            var stringBuilder = new StringBuilder();
            foreach (var item in this.Data)
            {
                var element = item as Item;
                if (element != null)
                stringBuilder.Append(element.Text);

                var dynamicVariable = item as DynamicVariable;
                if (dynamicVariable != null)
                {
                    if (string.IsNullOrWhiteSpace(dynamicVariable.EvaluatedText))
                    {
                        stringBuilder.Append(dynamicVariable.Text);
                    }
                    else
                    {
                        stringBuilder.Append(dynamicVariable.EvaluatedText);
                    }
                }

                EvaluatedPath = stringBuilder.ToString();


            }
        }

        void ExecuteAddDynamicVariableCommand(object parameter)
        {
            var variable = parameter;

            if (variable == null)
                return;

            if (this.Data.Contains(LastSelectedItem) == false || LastSelectedItem == null)
            {    
                this.Data.Add(variable);
                RefreshEvaluatedPath();
                MakeSureEndOfControlIsEditable();
                return;
            }

            if (LastSelectedItem != null && LastSelectedItem.CursorPosition == LastSelectedItem.Text.Length)
            {
                this.Data.Insert(this.Data.IndexOf(LastSelectedItem) + 1, variable);

                MakeSureEndOfControlIsEditable();
                return;
            }
            if (LastSelectedItem != null && LastSelectedItem.CursorPosition == 0)
            {
                this.Data.Insert(this.Data.IndexOf(LastSelectedItem), variable);

                RefreshEvaluatedPath();

                MakeSureEndOfControlIsEditable();
                return;
            }

            if (LastSelectedItem != null && LastSelectedItem.CursorPosition > 0 && LastSelectedItem.CursorPosition < LastSelectedItem.Text.Length)
            {
                var text = LastSelectedItem.Text;

                var index = this.Data.IndexOf(LastSelectedItem);

                var firstPart = text.Substring(0, LastSelectedItem.CursorPosition );
                
                var lastPart = text.Substring(LastSelectedItem.CursorPosition, text.Length - LastSelectedItem.CursorPosition);

                this.Data.RemoveAt(index);

                this.Data.Insert(index , new Item() {  Text= firstPart});
                this.Data.Insert(index+1, variable);
                this.Data.Insert(index+2, new Item() { Text = lastPart });

                RefreshEvaluatedPath();
                MakeSureEndOfControlIsEditable();
                return;
            }


            MakeSureEndOfControlIsEditable();
        }


        public void MakeSureEndOfControlIsEditable()
        {

            if (this.Data.Last() is DynamicVariable)
                this.Data.Add(new Item() { Text = " " });
        }
    }


    public class RelayCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }
    }
}

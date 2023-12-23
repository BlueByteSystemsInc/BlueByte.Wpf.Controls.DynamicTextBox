// copied from https://stackoverflow.com/questions/28233878/how-to-bind-to-caretindex-aka-curser-position-of-an-textbox

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace BlueByte.Wpf.Controls.Helpers
{
    public class TextBoxCursorPositionBehavior : DependencyObject
    {
        // This strange default value is on purpose it makes the initialization problem very unlikely.
        // If the default value matches the default value of the property in the ViewModel,
        // the propertyChangedCallback of the FrameworkPropertyMetadata is initially not called
        // and if the property in the ViewModel is not changed it will never be called.
        private const int CaretIndexPropertyDefault = -485609317;

        public static void SetCaretIndex(DependencyObject dependencyObject, int i)
        {
            dependencyObject.SetValue(CaretIndexProperty, i);
        }

        public static int GetCaretIndex(DependencyObject dependencyObject)
        {
            return (int)dependencyObject.GetValue(CaretIndexProperty);
        }

        public static readonly DependencyProperty CaretIndexProperty =
            DependencyProperty.RegisterAttached(
                "CaretIndex",
                typeof(int),
                typeof(TextBoxCursorPositionBehavior),
                new FrameworkPropertyMetadata(
                    CaretIndexPropertyDefault,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    CaretIndexChanged));

        private static void CaretIndexChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (dependencyObject is TextBox   == false || eventArgs.OldValue is int == false || eventArgs.NewValue is int  == false)
            {
                return;
            }

            var textBox = dependencyObject as TextBox;
            var oldValue = (int)eventArgs.OldValue;
            var newValue = (int)eventArgs.NewValue;

            if (oldValue == CaretIndexPropertyDefault && newValue != CaretIndexPropertyDefault)
            {
                textBox.SelectionChanged += SelectionChangedForCaretIndex;
            }
            else if (oldValue != CaretIndexPropertyDefault && newValue == CaretIndexPropertyDefault)
            {
                textBox.SelectionChanged -= SelectionChangedForCaretIndex;
            }

            if (newValue != textBox.CaretIndex)
            {
                textBox.CaretIndex = newValue;
            }
        }

        private static void SelectionChangedForCaretIndex(object sender, RoutedEventArgs eventArgs)
        {
            if (sender is TextBox textBox)
            {
                SetCaretIndex(textBox, textBox.CaretIndex);
            }
        }

    }
}

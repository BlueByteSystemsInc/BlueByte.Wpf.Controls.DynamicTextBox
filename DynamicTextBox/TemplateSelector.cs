using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BlueByte.Wpf.Controls
{
    public class TemplateSelector : DataTemplateSelector
    {
       
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var element = container as FrameworkElement;

            var type = item.GetType();

            if (type.Equals(typeof(Item)))
                return element.FindResource("ItemTemplate") as DataTemplate;


            if (type.Equals(typeof(DynamicVariable)))
                return element.FindResource("DynamicVariableTemplate") as DataTemplate;


            return base.SelectTemplate(item, container); 
        }
    }

}

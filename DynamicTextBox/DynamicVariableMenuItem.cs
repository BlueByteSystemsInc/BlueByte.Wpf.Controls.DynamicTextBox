using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace BlueByte.Wpf.Controls
{
    public class DynamicVariableMenuItem
    {
        public DynamicVariable Variable { get; set; }

        public DynamicVariableMenuItem[] Children { get; set; } = new DynamicVariableMenuItem[] { };

        public void Add(DynamicVariableMenuItem item)
        {
            var temp = this.Children.ToList();
            temp.Add(item);
            this.Children = temp.ToArray();
        }

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }

        public static DynamicVariableMenuItem[] Create(string[] variableNames)
        {
            var values = Enum.GetValues(typeof(DynamicVariableType_e));
            var l = new List<DynamicVariableMenuItem>();

            foreach (DynamicVariableType_e value in values)
            {
                if (value == DynamicVariableType_e.Variable)
                    continue;

                var description = GetEnumDescription(value);
                var variable = new DynamicVariable() { EvaluatedText = value.ToString(), Text = description, Type = value };
                var instance = new DynamicVariableMenuItem() { Variable = variable };
                l.Add(instance);


              


            }


            var variableNode = new DynamicVariable() { EvaluatedText = "PDM Variables", Text = "PDM Variables", Type = DynamicVariableType_e.Variable };
            var variableMenuNode = new DynamicVariableMenuItem() { Variable = variableNode };

            foreach (var v in variableNames)
            {
                variableNode = new DynamicVariable() { EvaluatedText = $"({v})", Text = v, Type = DynamicVariableType_e.Variable };
                var variableMenu = new DynamicVariableMenuItem() { Variable = variableNode };

                variableMenuNode.Add(variableMenu);
            }

            l.Add(variableMenuNode);

            return l.ToArray();
        }
    }

}

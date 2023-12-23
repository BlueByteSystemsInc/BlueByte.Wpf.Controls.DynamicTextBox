using System.Linq;

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
    }

}

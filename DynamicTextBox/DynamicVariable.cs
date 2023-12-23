namespace BlueByte.Wpf.Controls
{


    public class DynamicVariable : Base 
    {


        private string evaluatedText;

        public string EvaluatedText
        {
            get { return evaluatedText; }
            set { evaluatedText = value; base.NotifyPropertyChanged(nameof(EvaluatedText)); }
        }

        private string text;
        public string Text
        {
            get { return text; }
            set { text = value; base.NotifyPropertyChanged(nameof(Text)); }
        }
    }

}

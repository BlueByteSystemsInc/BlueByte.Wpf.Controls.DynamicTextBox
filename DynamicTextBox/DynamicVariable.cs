using System.ComponentModel;

namespace BlueByte.Wpf.Controls
{

    public enum DynamicVariableType_e
    {
      
        [Description("File name without extension")]
        FileNameWithoutExtension,

        [Description("File name")]
        FileName,

        [Description("State")]
        State,

        [Description("Configuration name")]
        ConfigurationName,

        [Description("Version")]
        Version,

        [Description("Revision")]
        Revision,

        [Description("Variable")]
        Variable,

        [Description("Date")]
        Date,

        [Description("Extension")]
        Extension,
        [Description("User")]
        User,
        [Description("LaunchingUser")]
        LaunchingUser
    

}


    public class DynamicVariable : Base 
    {


        public DynamicVariableType_e Type { get; set; }

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

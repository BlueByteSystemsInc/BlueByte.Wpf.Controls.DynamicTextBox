namespace BlueByte.Wpf.Controls
{
    public class Item : Base 
    {
        private int cursorPosition;
        public int CursorPosition
        {
            get { return cursorPosition; }
            set { cursorPosition = value; base.NotifyPropertyChanged(nameof(CursorPosition)); }
        }

        private string text;
        public string Text
        {
            get { return text; }
            set { text = value; base.NotifyPropertyChanged(nameof(Text)); }
        }
    }

}

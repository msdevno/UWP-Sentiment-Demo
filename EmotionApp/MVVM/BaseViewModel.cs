using System.ComponentModel;

namespace EmotionApp.MVVM
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void ShoutAbout(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                return;

            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

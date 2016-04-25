using Windows.UI.Xaml.Media.Imaging;

namespace EmotionApp.Models
{
    public class Sentiment
    {        
        public string Name { get; set; }

        public float Score { get; set; }
        public WriteableBitmap Image { get; private set; }


        public Sentiment()
        {

        }


        public Sentiment(string name, float score, WriteableBitmap bitmap)
        {
            Name = name;
            Score = score;
            Image = bitmap;
        }
    }
}

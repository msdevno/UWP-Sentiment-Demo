using EmotionApp.Models;
using Microsoft.ProjectOxford.Emotion;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Media.Imaging;

namespace EmotionApp.MVVM
{
    public class MainPageViewModel : BaseViewModel
    {
        private ImageCapture _imageCapture;
        private readonly EmotionServiceClient _client;
        private WriteableBitmap _happinessImage;
        private WriteableBitmap _contemptImage;
        private WriteableBitmap _fearImage;
        private WriteableBitmap _neutralImage;
        private WriteableBitmap _sadnessImage;
        private WriteableBitmap _surpriseImage;
        private WriteableBitmap _angerImage;
        private bool _canTakePicture;

        public MainPageViewModel()
        {
            _client = new EmotionServiceClient("b9609c9cc0b848539f7f53a0f4ea5aad");
            CanTakePicture = true;
        }


        private RelayCommand _takePictureCommand { get; set; }


        public ICommand TakePictureCommand
        {
            get
            {
                if (_takePictureCommand == null)
                    _takePictureCommand = new RelayCommand(TakePicture);

                return _takePictureCommand;
            }
        }

        
        public bool CanTakePicture
        {
            get { return _canTakePicture; }
            set
            {
                if (_canTakePicture == value)
                    return;

                _canTakePicture = value;
                ShoutAbout("CanTakePicture");
            }
        }


        public async Task InitializeAsync()
        {
            _imageCapture = new ImageCapture();
            await _imageCapture.InitializeAsync();
        }


        private async void TakePicture()
        {
            CanTakePicture = false;

            var image = await _imageCapture.CaptureJpegImageAsync();            
            await ProcessImageWithEmotionApiAsync(image);

            CanTakePicture = true;
        }


        private async Task ProcessImageWithEmotionApiAsync(WriteableBitmap image)
        {
            if (image == null)
                return;

            Stream fileStream = await GetLatestCaptureAsFileStream();
            var results       = await _client.RecognizeAsync(fileStream);

            if (results == null || results.Length == 0)
                return;

            var firstResult = results[0];

            List<Sentiment> scores = CreateScoresListFromResult(firstResult);

            if (scores == null || scores.Count() == 0)
                return;

            SelectImageFromHighestScore(image, scores);

        }

        private static async Task<Stream> GetLatestCaptureAsFileStream()
        {
            var file = await Windows.Storage.KnownFolders.PicturesLibrary.CreateFileAsync("lastImageCapture.jpg", Windows.Storage.CreationCollisionOption.OpenIfExists);
            var fileStream = await file.OpenStreamForReadAsync();
            return fileStream;
        }


        private void SelectImageFromHighestScore(WriteableBitmap image, List<Sentiment> scores)
        {
            if (image == null)
                return;

            if (scores == null || scores.Count() == 0)
                return;

            var highestSentiment = scores.OrderByDescending(o => o.Score).First();

            switch (highestSentiment.Name)
            {
                case "Anger"    : AngerImage     = image; break;
                case "Contempt" : ContemptImage  = image; break;
                case "Disgust"  : DisgustImage   = image; break;
                case "Fear"     : FearImage      = image; break;
                case "Happiness": HappinessImage = image; break;
                case "Neutral"  : NeutralImage   = image; break;
                case "Sadness"  : SadnessImage   = image; break;
                case "Surprise" : SurpriseImage  = image; break;

                default         : break;
            }
        }


        private List<Sentiment> CreateScoresListFromResult(Microsoft.ProjectOxford.Emotion.Contract.Emotion firstResult)
        {
            var scores = new List<Sentiment>();

            scores.Add(new Sentiment("Anger"    , firstResult.Scores.Anger    , AngerImage));
            scores.Add(new Sentiment("Contempt" , firstResult.Scores.Contempt , ContemptImage));
            scores.Add(new Sentiment("Disgust"  , firstResult.Scores.Disgust  , DisgustImage));
            scores.Add(new Sentiment("Fear"     , firstResult.Scores.Fear     , FearImage));
            scores.Add(new Sentiment("Happiness", firstResult.Scores.Happiness, HappinessImage));
            scores.Add(new Sentiment("Neutral"  , firstResult.Scores.Neutral  , NeutralImage));
            scores.Add(new Sentiment("Sadness"  , firstResult.Scores.Sadness  , SadnessImage));
            scores.Add(new Sentiment("Surprise" , firstResult.Scores.Surprise , SurpriseImage));

            return scores;
        }





        public WriteableBitmap ContemptImage
        {
            get { return _contemptImage; }
            set
            {
                if (_contemptImage == value)
                    return;

                _contemptImage = value;
                ShoutAbout("ContemptImage");
            }
        }


        public WriteableBitmap DisgustImage
        {
            get { return _happinessImage; }
            set
            {
                if (_happinessImage == value)
                    return;

                _happinessImage = value;
                ShoutAbout("DisgustImage");
            }
        }


        public WriteableBitmap FearImage
        {
            get { return _fearImage; }
            set
            {
                if (_fearImage == value)
                    return;

                _fearImage = value;
                ShoutAbout("FearImage");
            }
        }


        public WriteableBitmap HappinessImage
        {
            get { return _happinessImage; }
            set
            {
                if (_happinessImage == value)
                    return;

                _happinessImage = value;
                ShoutAbout("HappinessImage");
            }
        }


        public WriteableBitmap NeutralImage
        {
            get { return _neutralImage; }
            set
            {
                if (_neutralImage == value)
                    return;

                _neutralImage = value;
                ShoutAbout("NeutralImage");
            }
        }


        public WriteableBitmap SadnessImage
        {
            get { return _sadnessImage; }
            set
            {
                if (_sadnessImage == value)
                    return;

                _sadnessImage = value;
                ShoutAbout("SadnessImage");
            }
        }


        public WriteableBitmap SurpriseImage
        {
            get { return _surpriseImage; }
            set
            {
                if (_surpriseImage == value)
                    return;

                _surpriseImage = value;
                ShoutAbout("SurpriseImage");
            }
        }


        public WriteableBitmap AngerImage
        {
            get { return _angerImage; }
            set
            {
                if (_angerImage == value)
                    return;

                _angerImage = value;
                ShoutAbout("AngerImage");
            }
        }



    }
}

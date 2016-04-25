using System;
using System.Threading.Tasks;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace EmotionApp
{
    public class ImageCapture 
    {
        private const string IMAGECAPTURE_FILENAME = "LastImageCapture.jpg";
        private MediaCapture _mediaCapture;
        private ImageEncodingProperties _imageEncodingProperties;

        
        public async Task InitializeAsync()
        {
            _mediaCapture = new MediaCapture();
            await _mediaCapture.InitializeAsync();

            _imageEncodingProperties = ImageEncodingProperties.CreateJpeg();
        }



        public async Task<WriteableBitmap> CaptureJpegImageAsync()
        {
            var bitmap = new WriteableBitmap(400, 300);

            using (var memoryStream = new InMemoryRandomAccessStream())
            {
                var file = await Windows.Storage.KnownFolders.PicturesLibrary.CreateFileAsync(IMAGECAPTURE_FILENAME, Windows.Storage.CreationCollisionOption.ReplaceExisting);

                try
                {
                    await _mediaCapture.CapturePhotoToStorageFileAsync(_imageEncodingProperties, file);
                    var photoStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
                    await bitmap.SetSourceAsync(photoStream);
                    return bitmap;
                }
                catch(Exception ex)
                {

                }
                return null;
            }
        }
    }


}

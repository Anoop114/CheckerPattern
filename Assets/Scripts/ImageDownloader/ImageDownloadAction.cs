using System;
using System.Collections;
using HelperFunction.Routines;
using ImageSaveLoad;
using Server;
using UnityEngine;
using Utils;

namespace ImageDownloader
{
    public class ImageDownloadAction : MonoBehaviour
    {
        public static Action<ServerData> OnImageDownloadCall;
        public static Action OnImageDownloadComplete;
        private ServerData _serverData;
        private bool _isImageDownloadComplete;
        private int _imageCount;
        private int _serverImageCount;
        private void OnEnable()
        {
            OnImageDownloadCall += ImageDownloadCall;
            OnImageDownloadComplete += ImageDownloadComplete;
        }

        private void ImageDownloadComplete()
        {
            _imageCount++;
            if (_imageCount >= _serverImageCount)
            {
                _isImageDownloadComplete = true;
            }
            
        }

        private void OnDisable()
        {
            OnImageDownloadCall -= ImageDownloadCall;
            OnImageDownloadComplete -= ImageDownloadComplete;
        }

        private void ImageDownloadCall(ServerData serverData)
        {
            _serverImageCount = serverData.data.Length;
            var i = 0;
            foreach (var data in serverData.data)
            {
                var fileName = $"/{i}.jpg";
                DownloadImage.ImageGetter
                (
                    data.url,
                    new Vector2(data.width, data.height),
                    fileName,
                    Debug.LogError
                );
                data.fileName = fileName;
                i++;
            }

            _serverData = serverData;
            CoroutineManager.Instance.StartRoutine(LoadImageCall());
        }

        private IEnumerator LoadImageCall()
        {
            yield return new WaitUntil(()=> _isImageDownloadComplete && CheckerPatternUtils.IsloadPopup);
            
            // not needed to wait for 2 sec just for feel the ui good.
            yield return new WaitForSeconds(2f);
            
            LoadImageToCheckerPattern.OnLoadImageCall?.Invoke(_serverData);
        }
    }
}
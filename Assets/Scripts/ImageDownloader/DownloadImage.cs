/* reminder
 * need to add helper package
 */
using System;
using System.Collections;
using HelperFunction.Routines;
using ImageSaveLoad;
using UnityEngine;
using UnityEngine.Networking;

namespace ImageDownloader
{
    public static class DownloadImage
    {
        public static void ImageGetter(string imageUrl,Vector2 widthHeight,string fileName, Action<string> error)
        {
            CoroutineManager.Instance.StartRoutine(GetTexture(imageUrl,widthHeight,fileName,error));
        }

        private static IEnumerator GetTexture(string imageUrl,Vector2 widthHeight,string fileName,Action<string> error) {
            var www = UnityWebRequestTexture.GetTexture(imageUrl);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                error?.Invoke(www.error);
            }
            else {
                var loadedTexture = DownloadHandlerTexture.GetContent(www);
                SaveImage(loadedTexture,fileName,widthHeight);
            }
        }

        static void SaveImage(Texture2D loadedTexture,string fileName,Vector2 widthHeight)
        {
            var sprite = Sprite.Create(loadedTexture, new Rect(0f, 0f, widthHeight.x, widthHeight.y), Vector2.zero);

            ReadWriteImage.WriteImageOnDisk(sprite,fileName);
        }
    }
}
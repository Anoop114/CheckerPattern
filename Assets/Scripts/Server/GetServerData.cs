using System;
using HelperFunction.Server;
using ImageDownloader;
using ImageSaveLoad;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Server
{
    public class GetServerData : MonoBehaviour
    {
        [SerializeField] private Image[] images;
        [SerializeField] private string url;
        [SerializeField] private ServerData serverDataHolders;
        private ServerCall _serverCall;
        private void Start()
        {
            _serverCall = new ServerCall();

            _serverCall.GetCall(url, null, (s)=>Debug.Log(s),ServerRespond);
            return;
            
            void ServerRespond(string obj)
            {
                var updateServerRespond = "{\"data\":" + obj + "}";
                serverDataHolders = JsonUtility.FromJson<ServerData>(updateServerRespond);
            }
        }
        [ContextMenu("Download Image")]
        private void DownloadImageCall()
        {
            var i = 0;
            foreach (var data in serverDataHolders.data)
            {
                DownloadImage.ImageGetter
                    (
                        data.url,
                        new Vector2(data.width,data.height),
                        $"/{i}.jpg",
                        (s) => Debug.Log(s)
                    );
                i++;
            }
        }

        [ContextMenu("LoadImage")]
        private void LoadImages()
        {
            var i = 0;
            foreach (var fileName in FileNameRecord.saveFileName)
            {
                var i1 = i;
                ReadWriteImage.LoadImageFromDisk(fileName, loadedTexture =>
                {
                    images[i1].sprite = Sprite.Create(loadedTexture, new Rect(0f, 0f, loadedTexture.width, loadedTexture.height), Vector2.zero);
                    images[i1].SetNativeSize();
                });
                i++;
            }
        }

    }
}

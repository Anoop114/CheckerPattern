using System;
using Server;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace ImageSaveLoad
{
    public class LoadImageToCheckerPattern : MonoBehaviour
    {
        public static Action<ServerData> OnLoadImageCall;

        [SerializeField] private Image loadImagePrefab;
        [SerializeField] private Transform imageParent;
        [SerializeField] private GameObject Page2;
        [SerializeField] private GameObject Page3;
        [SerializeField] private GridLayoutGroup layout;
        private void OnEnable() => OnLoadImageCall += LoadImage;
        private void OnDisable() => OnLoadImageCall -= LoadImage;

        private void LoadImage(ServerData data)
        {
            layout.cellSize = new Vector2(data.data[0].width, data.data[0].height);
            for(var i = 0;i<10;i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    var temp = Instantiate(loadImagePrefab);
                    ReadWriteImage.LoadImageFromDisk(data.data[(i+j) % 2].fileName, loadedTexture =>
                    {
                        temp.sprite = Sprite.Create(loadedTexture, new Rect(0f, 0f, loadedTexture.width, loadedTexture.height), Vector2.zero);
                        temp.SetNativeSize();
                        temp.transform.SetParent(imageParent);
                    });
                }
            }
            Page2.SetActive(false);
            Page3.SetActive(true);
        }
    }
}
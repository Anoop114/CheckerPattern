using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace ImageSaveLoad
{
    public static class ReadWriteImage
    {
        public static void WriteImageOnDisk(Sprite textureImage,string fileName)
        {
            if (textureImage == null)
            {
                return;
            }
            var textureBytes = textureImage.texture.EncodeToPNG();
            File.WriteAllBytes(Application.persistentDataPath + fileName, textureBytes);
            FileNameRecord.saveFileName.Add(fileName);
        }
        
        public static void LoadImageFromDisk(string fileName,Action<Texture2D> texture)
        {
            if(!File.Exists(Application.persistentDataPath + fileName))
            {
                Debug.LogError("File Not Exist!");
                return;
            }
            var textureBytes = File.ReadAllBytes(Application.persistentDataPath + fileName);
            var loadedTexture = new Texture2D(0, 0);
            loadedTexture.LoadImage(textureBytes);
            texture?.Invoke(loadedTexture);
        }
    }

}
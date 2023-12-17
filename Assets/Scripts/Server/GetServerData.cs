using HelperFunction.Server;
using ImageDownloader;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Server
{
    public class GetServerData : MonoBehaviour
    {
        //[SerializeField] private string url;//https://quicklook.orientbell.com/Task/gettiles.php
        [Header("Start Function.")]
        [SerializeField] private TMP_InputField urlInputFiled;
        [SerializeField] private TMP_Text errorRespond;
        [SerializeField] private Button startBtn;
        [SerializeField] private GameObject Page1;
        [SerializeField] private GameObject Page2;
        
        [SerializeField] private ServerData serverDataHolders;
        
        
        private ServerCall _serverCall;
        
        private void Start()
        {
            _serverCall = new ServerCall();

            startBtn.onClick.AddListener(DownloadImageCall);
            
        }
        private void DownloadImageCall()
        {
            _serverCall.GetCall(urlInputFiled.text, null, ErrorCall,ServerRespond);
            return;
            
            void ServerRespond(string obj)
            {
                ErrorCall("");
                try
                {
                    var updateServerRespond = "{\"data\":" + obj + "}";
                    serverDataHolders = JsonUtility.FromJson<ServerData>(updateServerRespond);
                    Page1.SetActive(false);
                    Page2.SetActive(true);
                    ImageDownloadAction.OnImageDownloadCall?.Invoke(serverDataHolders);

                }
                catch {
                    ErrorCall("Not found Image data, Check url again.");
                }
            }
            void ErrorCall(string error)
            {
                errorRespond.text = error;
            }
        }
    }
}

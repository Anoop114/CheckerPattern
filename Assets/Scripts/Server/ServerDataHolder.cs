using System;

namespace Server
{
    [Serializable]
    public class ServerData
    {
        public RawData[] data;
        
        [Serializable] public class RawData
        {
            public string url;
            public int width;
            public int height;
            public string fileName;
        }
    }

     
    
}
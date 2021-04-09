using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace Consumption.Shared.Common.Security
{
    public class CacheMack
    {

        private static string strName = "LocalCache.json";
        public static bool CreateJsonCache(CacheInfo cacheInfo)
        {
            try
            {

                string output = JsonConvert.SerializeObject(cacheInfo, Formatting.Indented);
                string path = AppDomain.CurrentDomain.BaseDirectory + strName;
                if (!File.Exists(path))
                {
                    FileStream fsvbs = new FileStream(path, FileMode.Create, FileAccess.Write);
                    fsvbs.Close();
                }

                StreamWriter runBat = new StreamWriter(path);
                runBat.Write(output);
                runBat.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
        }

        public static CacheInfo ReadJsonCache()
        {

            CacheInfo info = null;
            try
            {
                using (StreamReader file = File.OpenText(strName))
                {
                    using (JsonTextReader reader = new JsonTextReader(file))
                    {
                        JObject jsonObject = (JObject)JToken.ReadFrom(reader);
                        info = JsonConvert.DeserializeObject<CacheInfo>(JsonConvert.SerializeObject(jsonObject));
                        file.Close();
                    }
                }
                return info;
            }
            catch
            {
                return info;
            }
        }

        public static bool IsConfig()
        {
            if (File.Exists(strName))
            {
                return true;
            }
            else return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;

namespace FFLogsAnalyser
{
    class Library
    {
        #region API Key

        //my public fflogs API Key
        public const string APIKey = "api_key=6957b9fce738f61b7db2c316b29e9cdd";
        
        #endregion

        #region Get json data from fflogs API

        /// <summary>
        /// generic function to get json data from fflogs API and put it into a class
        /// </summary>
        /// <typeparam name="T">The class in which to put the json data</typeparam>
        /// <param name="url">the url of the json data</param>
        /// <returns></returns>

        public static async Task<T> _download_serialized_json_data<T>(string url) where T : new()
        {
            using (var w = new WebClient())
            {
                var json_data = string.Empty;
                // attempt to download JSON data as a string
                try
                {
                    json_data = await w.DownloadStringTaskAsync(url);
                }
                catch (Exception) { }
                // if string with JSON data is not empty, deserialize it to class and return its instance 
                return !string.IsNullOrEmpty(json_data) ? JsonConvert.DeserializeObject<T>(json_data) : new T();
            }
        }
        #endregion

        #region FFlogs Parse Url functions

        /// <summary>
        /// returns the url for all fights from a character
        /// </summary>
        /// <param name="Name">Character full name</param>
        /// <param name="Server">Server that the character is on</param>
        /// <param name="Region">Region the Server is in</param>
        /// <returns></returns>

        public static string characterparse(string FirstName, string LastName, string Server, string Region)
        {
            return "https://www.fflogs.com:443/v1/rankings/character/" + FirstName + "%20" + LastName + "/" + Server + "/" + Region + "?" + APIKey;
        }

        /// <summary>
        /// returns the Report Parse from fflogs API
        /// </summary>
        /// <param name="fightID">the fight ID from fflogs</param>
        /// <returns>Report url</returns>
 
        public static string report(string fightID)
        { 
            return "https://www.fflogs.com:443/v1/report/fights/" + fightID + "?" + APIKey;
        }

        /// <summary>
        /// returns the Report parse from fflogs API with a filter for job buffs specified in 'Buffs' enum
        /// </summary>
        /// <param name="fightID">the fight ID from fflogs</param>
        /// <param name="start_time">the start time of the fight from the report</param>
        /// <param name="end_time">the end time of the fight from the report</param>
        /// <returns>Report url</returns>

        public static string reportbuffs(string fightID, int start_time, int end_time)
        {
            string Abilities = "";
            //swaps the blank space in 'Buffs' enum for '%20' then formats it to be used in the parse filter for the API
            foreach (var item in Enum.GetValues(typeof(Buffs)))
            {
                if (item.ToString() == Enum.GetValues(typeof(Buffs)).Cast<Buffs>().Last().ToString())
                {
                    //adds the last ability in the 'Buffs' enum to the parse filter
                    Abilities += "ability.name%20%3D%20%22" + item.ToString().Replace("_", "%20");
                }
                else
                {
                    //adds the abilities in the 'Buffs' enum to the parse filter
                    Abilities += "ability.name%20%3D%20%22" + item.ToString().Replace("_", "%20") + "%22%20or%20";
                }                
            }
            //returns the url
            return "https://www.fflogs.com:443/v1/report/events/"+fightID+"?start="+start_time+"&end="+end_time+ "&filter=" + Abilities + "%22" + "&" +APIKey;
        }

        #endregion
    }
}

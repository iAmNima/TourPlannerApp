using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TourPlannerApp.Models;

namespace TourPlannerApp.DataAccessLayer
{
    public class MapQuestApiProcessor
    {
        HttpClient ApiClient { get; set; }

        private string Key;
        private String RouteImagesFolder;
        public MapQuestApiProcessor()
        {
            ApiClient = new HttpClient();
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Key = ConfigurationManager.AppSettings["ApiKey"];
            RouteImagesFolder = ConfigurationManager.AppSettings["RouteImagesFolder"];
        }

        public string CreateDirectionApiUrl(string from, string to, string tourName)
        {
            string url = $"http://www.mapquestapi.com/directions/v2/route?key={ Key }&from={ from }&to={ to }";
            return url; 
        }

        public async Task<Tuple<string, string>> DirectionsAPI(string url, string tourName)
        {

            using(HttpResponseMessage response = await ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    string directionsApiModel = await response.Content.ReadAsStringAsync();

                    // using json.Net to Deserialize json into .net object:
                    Root obj = JsonConvert.DeserializeObject<Root>(directionsApiModel);
                    
                    // Getting sessionId and boundingBox from Root Obj;
                    Tuple<string, string> t = ExtractInfoFromRootObj(obj);
                    return t;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        public Tuple<string, string> ExtractInfoFromRootObj(Root obj)
        {
            //double distance = obj.route.distance;
            string sessionId = obj.route.sessionId;
            string boundingBox = obj.route.boundingBox.ul.lat + "," +
                obj.route.boundingBox.ul.lng + "," +
                obj.route.boundingBox.lr.lat + "," +
                obj.route.boundingBox.lr.lng;

            Tuple<string, string> t = new Tuple<string, string>(sessionId, boundingBox);
            return t;
        }


        public string CreateStaticMapApiUrl(string sessionId, string boundingBox)
        {
            string size = "640,480";
            string zoom = "11";
            string url = $"https://www.mapquestapi.com/staticmap/v5/map?key={ Key }&size={ size }&zoom={ zoom }&session={ sessionId }&boundingBox={ boundingBox }";
            return url;
        }

        public void StaticMapAPI(string url, string tourName)
        {
            //string size = "640,480";
            //string zoom = "11";
            //string url = $"https://www.mapquestapi.com/staticmap/v5/map?key={ Key }&size={ size }&zoom={ zoom }&session={ sessionId }&boundingBox={ boundingBox }";

            // create a new file with the name of our Tour:
            string path = CreateImageFilePath(tourName);

            // save our route image into the file that we just created:
            using (WebClient webClient = new WebClient())
            {
                byte[] data = webClient.DownloadData(url);

                using (MemoryStream mem = new MemoryStream(data))
                {
                    using (var yourImage = Image.FromStream(mem))
                    {
                        // If you want it as Png
                        yourImage.Save(path, ImageFormat.Png);
                    }
                }
            }
        }

        public string CreateImageFilePath(string tourName)
        {
            string path = RouteImagesFolder + "\\" + tourName + ".png";
            return path;
        }


        //public void DeleteRouteImage(TourEntry tour)
        //{
        //    string path = CreateImageFilePath(tour.Name);
        //    File.Delete(path);
        //}




    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace WebApplication1.Controllers
{
    public class Links2
    {
        public string self { get; set; }
        public string follows { get; set; }
        public string commercial { get; set; }
        public string stream_key { get; set; }
        public string chat { get; set; }
        public string features { get; set; }
        public string subscriptions { get; set; }
        public string editors { get; set; }
        public string teams { get; set; }
        public string videos { get; set; }
    }
    public class Preview
    {
        public string small { get; set; }
        public string medium { get; set; }
        public string large { get; set; }
        public string template { get; set; }
    }
    public class Channel
    {
        public bool mature { get; set; }
        public string status { get; set; }
        public string broadcaster_language { get; set; }
        public string display_name { get; set; }
        public string game { get; set; }
        public string language { get; set; }
        public int _id { get; set; }
        public string name { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public object delay { get; set; }
        public string logo { get; set; }
        public object banner { get; set; }
        public string video_banner { get; set; }
        public object background { get; set; }
        public string profile_banner { get; set; }
        public string profile_banner_background_color { get; set; }
        public bool partner { get; set; }
        public string url { get; set; }
        public int views { get; set; }
        public int followers { get; set; }
        public Links2 _links { get; set; }
    }
    
    public class Stream
    {
        public Preview preview { get; set; }
        public Channel channel { get; set; }
        public string game { get; set; }
        public int viewers { get; set; }
    }
    public class StreamList
    {
        public Stream[] streams { get; set; }
    }

    public class TwitchController : Controller
    {
        // GET: Twitch
        public ActionResult Index()
        {
            return View();
        }

        //GET: Twitch/LiveFollows
        [Authorize]
        public async Task<ActionResult> LiveFollows()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("OAuth", Request.Cookies["twitchAccess"].Value);
            var json = await client.GetStringAsync("https://api.twitch.tv/kraken/streams/followed?stream_type=live");
            StreamList list = JsonConvert.DeserializeObject<StreamList>(json);
            ViewBag.streamList = list.streams;
            return View();
        }
    }
}
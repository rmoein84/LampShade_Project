using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace ServiceHost.Areas.Administration
{
    public class IndexModel : PageModel
    {
        public List<Chart> DataSet { get; set; }
        public void OnGet()
        {
            DataSet = new List<Chart>
            {
                new Chart
                {
                    Label = "Apple",
                    Data = new List<int>{100, 200, 250,170,50},
                    BackgroundColor = "#72BF78",
                    BorderColor = "#A0D683",
                },
                new Chart
                {
                    Label = "Samsung",
                    Data = new List<int>{20, 40, 120, 430, 90},
                    BackgroundColor = "#E4003A",
                    BorderColor = "#FF0000"
                }
            };
        }
    }
    public class Chart
    {
        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }
        [JsonProperty(PropertyName = "data")]
        public List<int> Data { get; set; }
        [JsonProperty(PropertyName = "backgroundColor")]
        public string BackgroundColor { get; set; }
        [JsonProperty(PropertyName = "borderColor")]
        public string BorderColor { get; set; }
    }
}

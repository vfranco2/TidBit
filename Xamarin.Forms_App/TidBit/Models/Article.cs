using Newtonsoft.Json;
using SQLite;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace TidBit.Models
{
    //string[] icons = new string[] { "phonelink.png", "directions_car.png", "sports_basketball.png", "local_mall.png", "videogame_asset.png", "local_movies.png", "restaurant.png", "headset.png", "camera_alt.png", "bar_chart.png" };
    enum categoryIcons
    {
        phonelink, 
        directions_car, 
        sports_basketball, 
        local_mall, 
        videogame_asset, 
        local_movies, 
        restaurant,
        headset,
        camera_alt, 
        bar_chart
    }
    public class Article
        //: INotifyPropertyChanged
    {
        [JsonProperty("id")]
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [JsonProperty("categoryId")]
        public int CategoryId { get; set; }
        //public int CategoryId { get; set; }

        //[JsonProperty("categoryId")]
        //string tempIcon;

        /*public string CategoryIcon
        {
            get { return tempIcon; }
            set 
            {
                tempIcon = Enum.GetName(typeof(categoryIcons), this.CategoryId);
                OnPropertyChanged();
            }
        }*/
        string tempString;

        public string CategoryIcon
        {
            get { return tempString; }
            set 
            {
                tempString = Enum.GetName(typeof(categoryIcons), this.CategoryId);
            }
        }

        [JsonProperty("articleTitle")]
        [Unique]
        public string ArticleTitle { get; set; }

        [JsonProperty("articleSource")]
        public string ArticleSource { get; set; }

        [JsonProperty("articleImageUrl")]
        public string ArticleImageUrl { get; set; }

        [JsonProperty("articleText")]
        public string ArticleText { get; set; }

        [JsonProperty("articleUrl")]
        public string ArticleUrl { get; set; }

        [JsonProperty("articleDate")]
        public string ArticleDate { get; set; }

        /*public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string tempIndex = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(tempIndex));
        }*/
    }
}

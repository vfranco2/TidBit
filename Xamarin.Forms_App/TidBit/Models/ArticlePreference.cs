using System;
using Xamarin.Essentials;

namespace TidBit.Models
{
    public class ArticlePreference
    {
        public string PreferenceTitle { get; set; }

        public string PreferenceBoxName { get; set; }

        public bool PreferenceState
        {
            get { return Convert.ToBoolean(Preferences.Get(PreferenceBoxName, true)); }
            set { }
        }

        public string PreferenceIcon { get; set; }
    }
}

using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;

namespace TidBit.Droid
{
    [Activity(Label = "TidBit", Icon = "@mipmap/icon", Theme = "@style/LaunchTheme", MainLauncher = true, NoHistory = true)]

    public class SplashActivity : AppCompatActivity
    {
        static readonly string TAG = "X:" + typeof(SplashActivity).Name;
        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.SetTheme(Resource.Style.MainTheme);
            base.OnCreate(savedInstanceState, persistentState);
            Log.Debug(TAG, "SplashActivity.OnCreate");
        }

        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(() => { SimulateStartup(); });
            startupWork.Start();
        }

        public override void OnBackPressed(){ }

        async void SimulateStartup()
        {
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}
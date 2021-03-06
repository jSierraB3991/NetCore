namespace ClientAuthentication
{
    using Android.App;
    using Android.OS;
    using Android.Runtime;
    using Android.Support.V7.App;
    using Android.Widget;
    using ClientAuthentication.Service;
    using Common;
    using EDMTDialog;
    using Refit;

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private IRefitService service;
        private EditText edUsername;
        private EditText edPass;
        private Android.Support.V7.App.AlertDialog alert;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            edUsername = FindViewById<EditText>(Resource.Id.etUserName);
            edPass = FindViewById<EditText>(Resource.Id.etUserPassword);

            var btn = FindViewById<Button>(Resource.Id.btnLogin);
            btn.Click += Btn_Click;

            var txtCreateAccount = FindViewById<TextView>(Resource.Id.txtCreateAccount);
            txtCreateAccount.Click += delegate {
                edUsername.Text = string.Empty;
                edPass.Text = string.Empty;
                StartActivity(new Android.Content.Intent(this, typeof(RegisterActivity)));
            };

            alert = new EDMTDialogBuilder().SetContext(this).Build();
            service = RestService.For<IRefitService>(GetString(Resource.String.main_url));

        }

        private async void Btn_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(edUsername.Text))
                Toast.MakeText(this, Resource.String.msgUserRequired, ToastLength.Short).Show();
            if (string.IsNullOrEmpty(edPass.Text))
                Toast.MakeText(this, Resource.String.msgPassRequired, ToastLength.Short).Show();

            alert.Show();
            var result = await service.Login(new UserRest() { UserName = edUsername.Text, Password = edPass.Text });
            edUsername.Text = string.Empty;
            alert.Dismiss();
            Toast.MakeText(this, result, ToastLength.Short).Show();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
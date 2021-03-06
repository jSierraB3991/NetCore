namespace ClientAuthentication
{
    using Android.App;
    using Android.OS;
    using Android.Support.V7.App;
    using Android.Widget;
    using ClientAuthentication.Service;
    using Common;
    using EDMTDialog;
    using Refit;

    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : AppCompatActivity
    {
        private IRefitService service;
        private EditText edUsername;
        private EditText edPass;
        private Android.Support.V7.App.AlertDialog alert;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RegisterLayout);

            edUsername = FindViewById<EditText>(Resource.Id.edtUserCreate);
            edPass = FindViewById<EditText>(Resource.Id.edtPasswordCreate);

            var btn = FindViewById<Button>(Resource.Id.btnRegister);
            btn.Click += Btn_Click;

            alert = new EDMTDialogBuilder().SetContext(this).Build();
            service = RestService.For<IRefitService>(GetString(Resource.String.main_url));
        }

        private async void Btn_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(edUsername.Text))
                Toast.MakeText(this, Resource.String.msgUserRequired, ToastLength.Short).Show();
            if (string.IsNullOrEmpty(edPass.Text))
                Toast.MakeText(this, Resource.String.msgPassRequired, ToastLength.Short).Show();

            edUsername.Text = string.Empty;
            alert.Show();
            var result = await service.RegisterUser(new UserRest() { UserName = edUsername.Text, Password = edPass.Text });
            alert.Dismiss();
            Toast.MakeText(this, result, ToastLength.Short).Show();
        }
    }
}
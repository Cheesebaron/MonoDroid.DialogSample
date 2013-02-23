using Android.App;
using Android.Content;
using Android.OS;

namespace MonoDroid.DialogSample
{
    [Activity(Label = "Dialog Fragments")]
    public class DialogFragmentActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Page_DialogFragment);

            ActionBar.SetDisplayHomeAsUpEnabled(true);

            var newFragment = new CoolDialogsFragment();
            var ft = FragmentManager.BeginTransaction();
            ft.Add(Resource.Id.fragment_container, newFragment);
            ft.Commit();
        }

        public override bool OnOptionsItemSelected(Android.Views.IMenuItem item)
        {
            switch (item.ItemId) 
            {
                case Android.Resource.Id.Home:
                    // app icon in action bar clicked; go home
                    var intent = new Intent(this, typeof(DialogActivity));
                    intent.AddFlags(ActivityFlags.ClearTop);
                    intent.AddFlags(ActivityFlags.NewTask);
                    StartActivity(intent);
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
    }
}
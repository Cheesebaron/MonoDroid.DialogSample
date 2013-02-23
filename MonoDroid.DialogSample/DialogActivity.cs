using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;

namespace MonoDroid.DialogSample
{
    [Activity(Label = "Loads of Dialogs", MainLauncher = true, Icon = "@drawable/icon")]
    public class DialogActivity : Activity
    {
        private const int AlertDialog = 1;
        private const int ListDialog = 2;
        private const int MultiChoiceDialog = 3;
        private const int CustomViewDialog = 4;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Page_Dialog);

            var yesNoDialogbutton = FindViewById<Button>(Resource.Id.YesNoDialogButton);
            yesNoDialogbutton.Click += delegate
                                {
                                    var builder = new AlertDialog.Builder(this);
                                    builder.SetMessage(Resource.String.knigts_dialog_title);
                                    builder.SetPositiveButton(Resource.String.yes, (s, e) => { });
                                    builder.SetNegativeButton(Resource.String.no, (s, e) => { }).Create();
                                    builder.Show();
                                };

            var alertDialogButton = FindViewById<Button>(Resource.Id.AlertDialogButton);
            alertDialogButton.Click += delegate { ShowDialog(AlertDialog); };

            var listDialogButton = FindViewById<Button>(Resource.Id.ListDialogButton);
            listDialogButton.Click += delegate { ShowDialog(ListDialog); };

            var multiChoiceDialogButton = FindViewById<Button>(Resource.Id.MultiChoiceDialogButton);
            multiChoiceDialogButton.Click += delegate { ShowDialog(MultiChoiceDialog); };

            var customViewDialogButton = FindViewById<Button>(Resource.Id.CustomViewDialogButton);
            customViewDialogButton.Click += delegate { ShowDialog(CustomViewDialog); };

            var fragmentDialogsButton = FindViewById<Button>(Resource.Id.FragmentDialogsButton);
            fragmentDialogsButton.Click += delegate
                                               {
                                                   var intent = new Intent(this, typeof (DialogFragmentActivity));
                                                   intent.AddFlags(ActivityFlags.ClearTop);
                                                   StartActivity(intent);
                                               };
        }


        //This is deprecated in API level 8
        //protected override Dialog OnCreateDialog(int id)

        //This is deprecated in API level 13
        protected override Dialog OnCreateDialog(int id, Bundle args)
        {
            switch(id)
            {
                case AlertDialog:
                    {
                        var builder = new AlertDialog.Builder(this);
					    builder.SetIconAttribute(Android.Resource.Attribute.AlertDialogIcon);
                        builder.SetTitle(Resource.String.bomb_dialog_title);

                        builder.SetPositiveButton(Resource.String.dialog_signal, (s, e) => { /*Do something here!*/ });
                        builder.SetNegativeButton(Resource.String.dialog_main_screen, (s, e) => { });

					    return builder.Create();
                    }
                case ListDialog:
                    {
                        var builder = new AlertDialog.Builder(this);
					    builder.SetIconAttribute(Android.Resource.Attribute.AlertDialogIcon);
                        builder.SetTitle(Resource.String.list_dialog_title);
                        builder.SetSingleChoiceItems(Resource.Array.list_dialog_items, 0, ListClicked);

                        builder.SetPositiveButton(Resource.String.dialog_ok, OkClicked);
                        builder.SetNegativeButton(Resource.String.dialog_cancel, CancelClicked);

					    return builder.Create();
                    }
                case MultiChoiceDialog:
                    {
                        var builder = new AlertDialog.Builder(this, Android.App.AlertDialog.ThemeHoloLight);
					    builder.SetIcon(Resource.Drawable.Icon);
                        builder.SetTitle(Resource.String.multi_choice_dialog_title);
                        builder.SetMultiChoiceItems(Resource.Array.multilist_dialog_items, 
                            new[] { false, true, false, true }, MultiListClicked);

					    builder.SetPositiveButton(Resource.String.dialog_ok, OkClicked);
					    builder.SetNegativeButton(Resource.String.multi_dialog_cancel, CancelClicked);

					    return builder.Create();
                    }
                case CustomViewDialog:
                    {
						var customView = LayoutInflater.Inflate (Resource.Layout.CustomDialog, null);

						var builder = new AlertDialog.Builder (this);
                        builder.SetView(customView);
						builder.SetPositiveButton (Resource.String.dialog_ok, OkClicked);
						builder.SetNegativeButton (Resource.String.dialog_cancel, CancelClicked);

						return builder.Create ();
                    }
            }

            return base.OnCreateDialog(id, args);
        }

        private void OkClicked(object sender, DialogClickEventArgs args)
        {
            var dialog = (AlertDialog) sender;
            var username = (EditText)dialog.FindViewById(Resource.Id.username);
            var password = (EditText)dialog.FindViewById(Resource.Id.password);

            if (null != username || null != password)
                Toast.MakeText(this, (username != null ? 
                    string.Format("Username: {0} ", username.Text) : "") + 
                    (password != null ? string.Format("Password: {0}", password.Text) : ""), ToastLength.Short).Show();
        }

        private void CancelClicked(object sender, DialogClickEventArgs args)
        {
        }

        private void ListClicked(object sender, DialogClickEventArgs args)
        {
            var items = Resources.GetStringArray(Resource.Array.list_dialog_items);

            Toast.MakeText(this, string.Format("You've selected: {0}, {1}", args.Which, items[args.Which]), ToastLength.Short).Show();
        }

        private void MultiListClicked(object sender, DialogMultiChoiceClickEventArgs args)
        {
            var items = Resources.GetStringArray(Resource.Array.multilist_dialog_items);

            Toast.MakeText(this, string.Format("{0} is checked {1}", items[args.Which], args.IsChecked), ToastLength.Short).Show();
        }
    }
}


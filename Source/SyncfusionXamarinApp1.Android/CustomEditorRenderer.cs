using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using SyncfusionXamarinApp1;
using SyncfusionXamarinApp1.Android;
using Mahan = Xamarin.Forms;
using Noorbahr = Android.Graphics;
using Android.Graphics;

[assembly: Mahan.ExportRenderer(typeof(CustomEditor),typeof(CustomEditorRenderer))]

namespace SyncfusionXamarinApp1.Android
{
    class CustomEditorRenderer : EditorRenderer
    {
        public CustomEditorRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Mahan.Editor> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
                return;

            Color clr = Color.Rgb(41,41,41);
 
            Control.Background = null;
            Control.SetPadding(0, 0, 0, 0);
            Control.ShowSoftInputOnFocus = false;
            Control.SetHighlightColor(clr);
            Control.SetTextIsSelectable(true);
            Control.CustomSelectionActionModeCallback = new CustomSelectionActionModeCallback();
            Control.CustomInsertionActionModeCallback = new CustomInsertionActionModeCallback();
        }

        private class CustomInsertionActionModeCallback : Java.Lang.Object, ActionMode.ICallback
        {
            public bool OnCreateActionMode(ActionMode mode, IMenu menu) => false;

            public bool OnActionItemClicked(ActionMode m, IMenuItem i) => false;

            public bool OnPrepareActionMode(ActionMode mode, IMenu menu) => true;

            public void OnDestroyActionMode(ActionMode mode) { }
        }

        private class CustomSelectionActionModeCallback : Java.Lang.Object, ActionMode.ICallback
        {


            public bool OnActionItemClicked(ActionMode m, IMenuItem i) => false;

            public bool OnCreateActionMode(ActionMode mode, IMenu menu) => true;

            public void OnDestroyActionMode(ActionMode mode) { }

            public bool OnPrepareActionMode(ActionMode mode, IMenu menu)
            {
                return true;
            }
        }
    }
}
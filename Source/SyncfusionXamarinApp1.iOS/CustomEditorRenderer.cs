using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using SyncfusionXamarinApp1;
using SyncfusionXamarinApp1.iOS;

[assembly: ExportRenderer(typeof(CustomEditor), typeof(CustomEditorRenderer))]

namespace SyncfusionXamarinApp1.iOS
{
    class CustomEditorRenderer : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (Control == null) return;

            Control.Selectable = true;
            Control.Editable = false;
            Control.ScrollEnabled = true;
            Control.TextContainerInset = UIEdgeInsets.Zero;
            Control.TextContainer.LineFragmentPadding = 0;
        }
    }
}
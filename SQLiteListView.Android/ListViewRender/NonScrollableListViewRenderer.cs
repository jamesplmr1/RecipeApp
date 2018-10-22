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
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using SQLiteListView.ViewModels;

[assembly: ExportRenderer(typeof(NonScrollableListView), typeof(NonScrollableListViewRenderer))]
public class NonScrollableListViewRenderer : ListViewRenderer
{
    public NonScrollableListViewRenderer(Context context) : base(context)
    {

    }
    protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
    {
        base.OnElementChanged(e);
        try
        {
            if (e == null || e.NewElement == null)
            {
                return;
            }
            var lit = e.NewElement;
            if (e.NewElement != null)
            {
                var listView = this.Control as Android.Widget.ListView;
                listView.NestedScrollingEnabled = true;
            }
        }
        catch (System.Exception ex)
        {

        }
    }

    public override void OnViewAdded(Android.Views.View child)
    {
        base.OnViewAdded(child);

    }


    protected override void OnDetachedFromWindow()
    {
        try
        {
            if (Control == null)
            {

            }
            if (Element == null)
            {

            }
            base.OnDetachedFromWindow();

        }
        catch (System.Exception ex)
        {

        }
    }


    protected override void Dispose(bool disposing)
    {
        disposing = false;
        if (Element == null)
        {

        }
        if (Control == null)
        {

        }
        base.Dispose(disposing);
    }
}

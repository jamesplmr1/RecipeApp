using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using SQLiteListView.iOS.ListViewRender;
using SQLiteListView.ViewModels;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NonScrollableListView), typeof(NonScrollableListViewRenderer))]
namespace SQLiteListView.iOS.ListViewRender
{
    class NonScrollableListViewRenderer : ListViewRenderer
    {
        public NonScrollableListViewRenderer()
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
                return;

            var tableView = Control as UITableView;
            tableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
            tableView.ScrollEnabled = true;

        }
    }
}

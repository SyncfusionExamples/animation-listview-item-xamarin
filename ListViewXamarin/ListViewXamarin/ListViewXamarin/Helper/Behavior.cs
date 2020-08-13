using Syncfusion.DataSource.Extensions;
using Syncfusion.GridCommon.ScrollAxis;
using Syncfusion.ListView.XForms;
using Syncfusion.ListView.XForms.Control.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ListViewXamarin
{
    #region Behavior
    public class Behaviour : Behavior<ContentPage>
    {
        #region Fields
        private SfListView listView;
        #endregion

        #region Overrides
        protected override void OnAttachedTo(ContentPage bindable)
        {
            listView = bindable.FindByName<SfListView>("listView");
            listView.ItemGenerator = new ItemGeneratorExt(listView);
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            listView = null;
            base.OnDetachingFrom(bindable);
        }
        #endregion
    }
    #endregion

    #region ItemGeneratorExt
    public class ItemGeneratorExt : ItemGenerator
    {
        public ItemGeneratorExt(SfListView listview) : base(listview)
        {

        }
        protected override ListViewItem OnCreateListViewItem(int itemIndex, ItemType type, object data = null)
        {
            if (type == ItemType.Record)
                return new ListViewItemExt();
            return base.OnCreateListViewItem(itemIndex, type, data);
        }
    }
    #endregion

    #region ListViewItemExt
    public class ListViewItemExt : ListViewItem
    {
        public ListViewItemExt()
        {
        }

        protected override void OnItemAppearing()
        {
            var item = this.BindingContext as Contacts;
            if (!item.IsAnimated)
            {
                this.Opacity = 0;
                this.FadeTo(1, 400, Easing.SinInOut);
                item.IsAnimated = true;
            }
            base.OnItemAppearing();
        }
    }
    #endregion
}
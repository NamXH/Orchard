using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Diagnostics;
using System.IO;
using PCLStorage;
using System.Linq.Expressions;
using System.Reflection;

namespace Orchard
{
    public partial class OperatorDetailPage : ContentPage
    {
        public OperatorDetailPage(Operator currItem)
        {
            InitializeComponent();
            var vm = new DetailVM<Operator>(x => x.Copy(), (x, y) =>
            {
                x.Name = y.Name;
                x.CertificationNumber = y.CertificationNumber;
                x.Note = y.Note;
            })
            {
                CurrItem = currItem
            };
            BindingContext = vm;

            ToolbarItems.Add(new ToolbarItem("Done", null, () =>
            {
                vm.DoneCmd.Execute(null);
                if (!vm.IsEditing)
                {
                    IssueNeedRefreshData();
                }
                Navigation.PopAsync();
            }));

            var vList = BuildViews<Sprayer, Sprayer.VolumeUnit>(curr => curr.TankUnit);
            if (vList != null)
            {
                foreach (var view in vList)
                {
                    _sl.Children.Insert(1, view);
                }
            }
        }

        DetailVM<Operator> VM
        {
            get { return (DetailVM<Operator>)BindingContext; }
        }

        public async void ImageClicked(object sender, EventArgs e)
        {
            var actionList = new string[] { "Take Photo", "Choose Photo" };
            var action = await DisplayActionSheet(null, "Cancel", null, actionList);
            var picker = DependencyService.Get<IMediaPicker>();
            Stream photoStream = null;

            if (action == actionList[0])
            {
                photoStream = await picker.TakePhoto();
            }
            else if (action == actionList[1])
            {
                photoStream = await picker.PickPhoto();
            }
            if (photoStream == null)
            {
                return;
            }

            await VM.ChangeImg(photoStream);

            // HACK: binding is not working, set manually.
            ((ImageButton)sender).Image = null;
            ((ImageButton)sender).Image = VM.LocalItem.Image;
        }

        public async void DelClicked(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet(null, "Cancel", "Delete?");
            if (action == "Delete?")
            {
                VM.DelCmd.Execute(null);
                IssueNeedRefreshData();
                Navigation.PopAsync();
            }
        }

        public event EventHandler<EventArgs> NeedRefreshData;

        private void IssueNeedRefreshData()
        {
            var handler = NeedRefreshData;
            if (handler != null)
            {
                handler.Invoke(this, EventArgs.Empty);
            }
        }

        private IList<View> BuildViews<ItemType, PropertyType>(Expression<Func<ItemType, PropertyType>> getter)
        {
            if (getter == null)
            {
                throw new ArgumentNullException("getter");
            }
            var expression = getter.Body;
            var unaryExpression = expression as UnaryExpression;
            if (unaryExpression != null)
            {
                expression = unaryExpression.Operand;
            }
            var memberExpression = expression as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException("getter must be a MemberExpression", "getter");
            }
            var propertyInfo = (PropertyInfo)memberExpression.Member;

            if (propertyInfo.PropertyType == typeof(string))
            {
                var ret = new List<View>();
                ret.Add(new Label() { Text = propertyInfo.Name });
                var entry = new Entry();
                entry.SetBinding(Entry.TextProperty, propertyInfo.Name);
                ret.Add(entry);
                return ret;
            }
            var dbType = ((dynamic)propertyInfo.PropertyType).BaseType;
            if (dbType == typeof(Enum))
            {
                var names = Enum.GetNames(propertyInfo.PropertyType);
                Debug.WriteLine("enum type");
                var p = new Picker();
                foreach (var n in names)
                {
                    p.Items.Add(n);
                }
                return new List<View>{ p };
            }
            return null;
        }
    }
}


using System;
using Xamarin.Forms.Platform.iOS;
using MonoTouch.UIKit;
using Xamarin.Forms;
using Orchard.iOS;
using Orchard;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(TextCellWithDisclosure), typeof(TextCellWithDisclosureRenderer))]
[assembly: ExportRenderer(typeof(ImageCellWithCheck), typeof(ImageCellWithCheckRenderer))]
namespace Orchard.iOS
{
    public class TextCellWithDisclosureRenderer : TextCellRenderer
    {
        public override UITableViewCell GetCell(Xamarin.Forms.Cell item, UITableView tv)
        {
            TextCell textCell = (TextCell)item;
            UITableViewCellStyle style = UITableViewCellStyle.Value1;
            string text = "Xamarin.Forms.TextCell";
            CellTableViewCell cellTableViewCell = tv.DequeueReusableCell(text) as CellTableViewCell;
            if (cellTableViewCell == null)
            {
                cellTableViewCell = new CellTableViewCell(style, text);
            }
            else
            {
                cellTableViewCell.Cell.PropertyChanged -= new PropertyChangedEventHandler(cellTableViewCell.HandlePropertyChanged);
            }
            cellTableViewCell.Cell = textCell;
            textCell.PropertyChanged += new PropertyChangedEventHandler(cellTableViewCell.HandlePropertyChanged);
            cellTableViewCell.PropertyChanged = new Action<object, PropertyChangedEventArgs>(this.HandlePropertyChanged);
            cellTableViewCell.TextLabel.Text = textCell.Text;
            cellTableViewCell.DetailTextLabel.Text = textCell.Detail;
            //cellTableViewCell.TextLabel.TextColor = textCell.TextColor.ToUIColor(TextCellRenderer.DefaultTextColor);
            //cellTableViewCell.DetailTextLabel.TextColor = textCell.DetailColor.ToUIColor(TextCellRenderer.DefaultDetailColor);
            base.UpdateBackground(cellTableViewCell, item);


            cellTableViewCell.Accessory = UITableViewCellAccessory.DisclosureIndicator;

            return cellTableViewCell;
        }
    }

    public class ImageCellWithCheckRenderer : ImageCellRenderer
    {
       
        public override UITableViewCell GetCell(Cell item, UITableView tv)
        {
            var cell = base.GetCell(item, tv);
            var imgWithCheckCell = (ImageCellWithCheck)item;

            cell.Accessory = imgWithCheckCell.Selected ? UITableViewCellAccessory.Checkmark : UITableViewCellAccessory.None;
            cell.SetNeedsDisplay();

            return cell;
        }

        protected override void HandlePropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            var cellTableViewCell = (CellTableViewCell)sender;
            var imgCellWithCheck = (ImageCellWithCheck)cellTableViewCell.Cell;

            base.HandlePropertyChanged(sender, args);
            if (args.PropertyName == ImageCellWithCheck.SelectedProperty.PropertyName)
            {
                cellTableViewCell.Accessory = imgCellWithCheck.Selected ? UITableViewCellAccessory.Checkmark : UITableViewCellAccessory.None;
                cellTableViewCell.SetNeedsDisplay();
            }
        }
    }
}


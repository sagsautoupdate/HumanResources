using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace HumanResources.Utilities
{
    public class MyFilterMenuTreeItem : RadMenuItemBase
    {
        public MyFilterMenuTreeElement TreeElement { get; private set; }


        protected override void OnLoaded()
        {
            TreeElement.Initialize();
            base.OnLoaded();
        }


        protected override void InitializeFields()
        {
            base.InitializeFields();
            Padding = new Padding(5, 5, 5, 0);
        }

        protected override void CreateChildElements()
        {
            base.CreateChildElements();
            TreeElement = new MyFilterMenuTreeElement();
            Children.Add(TreeElement);
        }


        protected override SizeF MeasureOverride(SizeF availableSize)
        {
            base.MeasureOverride(availableSize);
            return new SizeF(0, 40);
        }

        protected override SizeF ArrangeOverride(SizeF finalSize)
        {
            foreach (var element in Children)
            {
                var rect = new RectangleF(PointF.Empty, finalSize);
                if (element == TreeElement)
                {
                    rect = GetClientRectangle(finalSize);
                    var layout = FindAncestor<RadDropDownMenuLayout>();
                    if (layout != null)
                    {
                        rect.X += RightToLeft ? 0 : layout.LeftColumnWidth;
                        rect.Width -= layout.LeftColumnWidth;
                    }
                    TreeElement.TreeView.Size = rect.Size.ToSize();
                }
                element.Arrange(rect);
            }
            return finalSize;
        }
    }
}
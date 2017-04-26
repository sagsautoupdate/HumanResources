using System;
using System.Drawing;
using System.Windows.Forms;
using HumanResources.Properties;
using Telerik.WinControls;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.UI;

namespace HumanResources.Utilities
{
    public class SearchTextBox : RadTextBox
    {
        private readonly RadButtonElement searchButton = new RadButtonElement();

        public override string ThemeClassName
        {
            get { return typeof(RadTextBox).FullName; }
        }

        protected override void OnLoad(Size desiredSize)
        {
            base.OnLoad(desiredSize);
            searchButton.ButtonFillElement.Visibility = ElementVisibility.Collapsed;
            searchButton.ShowBorder = false;
        }

        protected override void InitializeTextElement()
        {
            base.InitializeTextElement();
            TextBoxElement.TextBoxItem.NullText = "Enter search criteria";
            searchButton.Click += button_Click;
            searchButton.Margin = new Padding(0, 0, 0, 0);
            searchButton.Text = string.Empty;
            searchButton.Image = Resources.Search_WF;
            var stackPanel = new StackLayoutElement();
            stackPanel.Orientation = Orientation.Horizontal;
            stackPanel.Margin = new Padding(1, 0, 1, 0);
            stackPanel.Children.Add(searchButton);
            var tbItem = TextBoxElement.TextBoxItem;
            TextBoxElement.Children.Remove(tbItem);
            var dockPanel = new DockLayoutPanel();
            dockPanel.Children.Add(stackPanel);
            dockPanel.Children.Add(tbItem);
            DockLayoutPanel.SetDock(tbItem, Telerik.WinControls.Layouts.Dock.Left);
            DockLayoutPanel.SetDock(stackPanel, Telerik.WinControls.Layouts.Dock.Right);
            TextBoxElement.Children.Add(dockPanel);
        }

        public event EventHandler<SearchBoxEventArgs> Search;

        private void button_Click(object sender, EventArgs e)
        {
            var newEvent = new SearchBoxEventArgs();
            newEvent.SearchText = Text;
            SearchEventRaiser(newEvent);
        }

        private void SearchEventRaiser(SearchBoxEventArgs e)
        {
            if (Search != null)
                Search(this, e);
        }

        public class SearchBoxEventArgs : EventArgs
        {
            public string SearchText { get; set; }
        }
    }
}
using System;
using System.Collections;
using System.Drawing;
using Telerik.WinControls;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Localization;

namespace HumanResources.Utilities
{
    public class MyFilterMenuTreeElement : LightVisualElement, IRadListFilterElement
    {
        private RadTreeNode allNode;
        private bool enableBlanks;
        private RadHostItem hostItem;
        private bool isFiltered;
        private RadTreeNode nonNullNode;
        private RadTreeNode nullNode;
        private Hashtable treeValuesHash;
        private RadTreeView treeView;


        public bool GroupedDateValues { get; set; }

        public RadTreeView TreeView
        {
            get { return (RadTreeView) hostItem.HostedControl; }
        }


        public bool EnableBlanks
        {
            set { enableBlanks = value; }
        }

        public RadListFilterDistinctValuesTable DistinctListValues { get; set; }

        public RadListFilterDistinctValuesTable SelectedValues { get; set; }

        public ListFilterSelectedMode SelectedMode { get; set; }

        public void Initialize()
        {
            allNode.Nodes.Clear();
            nullNode.Visible = enableBlanks;
            nonNullNode.Visible = enableBlanks;

            allNode.Text =
                RadGridLocalizationProvider.CurrentProvider.GetLocalizedString(RadGridStringId.FilterMenuSelectionAll);
            nullNode.Text =
                RadGridLocalizationProvider.CurrentProvider.GetLocalizedString(RadGridStringId.FilterMenuSelectionNull);
            nonNullNode.Text =
                RadGridLocalizationProvider.CurrentProvider.GetLocalizedString(
                    RadGridStringId.FilterMenuSelectionNotNull);

            TreeView.NodeCheckedChanged -= treeView_NodeCheckedChanged;
            TreeView.BeginUpdate();

            var checkAll = true;
            if ((SelectedValues.Count > 0) || (SelectedMode == ListFilterSelectedMode.None))
                checkAll = false;
            if ((SelectedValues.Count > 0) && (SelectedValues.Count < DistinctListValues.Count))
                allNode.CheckState = ToggleState.Indeterminate;
            if (SelectedMode == ListFilterSelectedMode.Null)
            {
                nullNode.CheckState = ToggleState.On;
                checkAll = false;
            }
            else
            {
                if (SelectedMode == ListFilterSelectedMode.NotNull)
                {
                    nonNullNode.CheckState = ToggleState.On;
                    checkAll = false;
                }
            }
            if (DistinctListValues != null)
            {
                var sortedValueList = new SortedList(DistinctListValues, new ListFilterComparer(DistinctListValues));
                treeValuesHash = new Hashtable();

                if (GroupedDateValues)
                {
                    treeView.ShowExpandCollapse = true;

                    foreach (DictionaryEntry entry in sortedValueList)
                    {
                        var val = ((ArrayList) entry.Value)[0];

                        if ((val == null) || (val == DBNull.Value))
                            continue;
                        var value = (DateTime) val;


                        RadTreeNode yearNode;
                        if (allNode.Nodes.Contains(value.Year.ToString()))
                            yearNode = allNode.Nodes[value.Year.ToString()];
                        else
                            yearNode = allNode.Nodes.Add(value.Year.ToString());
                        RadTreeNode monthNode;
                        if (yearNode.Nodes.Contains(value.Month.ToString()))
                            monthNode = yearNode.Nodes[value.Month.ToString()];
                        else
                            monthNode = yearNode.Nodes.Add(value.Month.ToString());
                        RadTreeNode dayNode;
                        if (monthNode.Nodes.Contains(value.Day.ToString()))
                            dayNode = monthNode.Nodes[value.Day.ToString()];
                        else
                            dayNode = monthNode.Nodes.Add(value.Day.ToString());
                        if (SelectedValues.ContainsFilterValue(value.Date))
                            dayNode.CheckState = ToggleState.On;
                    }

                    allNode.Expand();
                }
                else
                {
                    foreach (DictionaryEntry entry in sortedValueList)
                    {
                        var key = (string) entry.Key;
                        var blanks = false;

                        if (string.IsNullOrEmpty(key))
                        {
                            key =
                                RadGridLocalizationProvider.CurrentProvider.GetLocalizedString(
                                    RadGridStringId.FilterMenuBlanks);
                            blanks = true;
                        }

                        var node = new RadTreeNode(key);
                        allNode.Nodes.Add(node);

                        if (blanks)
                            foreach (DictionaryEntry selectedEntry in SelectedValues)
                                foreach (var value in (ArrayList) selectedEntry.Value)
                                    if ((value == null) || StringIsNullOrWhiteSpace(value.ToString()))
                                    {
                                        node.CheckState = ToggleState.Off;
                                        node.CheckState = ToggleState.On;

                                        break;
                                    }
                                    else
                                    {
                                        foreach (var value1 in (ArrayList) entry.Value)
                                            if (SelectedValues.ContainsFilterValue(value1))
                                            {
                                                node.CheckState = ToggleState.Off;
                                                node.CheckState = ToggleState.On;

                                                break;
                                            }
                                    }
                        treeValuesHash.Add(node, entry.Value);
                    }
                }
            }

            if (checkAll)
                allNode.CheckState = ToggleState.On;
            TreeView.EndUpdate();
            ValidateSelectedValues();
            OnSelectionChanged();

            TreeView.NodeCheckedChanged += treeView_NodeCheckedChanged;
        }

        public void Filter(string filter)
        {
            isFiltered = false;

            if (treeValuesHash == null)
                return;
            TreeView.BeginUpdate();

            allNode.Text = string.IsNullOrEmpty(filter)
                ? RadGridLocalizationProvider.CurrentProvider.GetLocalizedString(RadGridStringId.FilterMenuSelectionAll)
                : RadGridLocalizationProvider.CurrentProvider.GetLocalizedString(
                    RadGridStringId.FilterMenuSelectionAllSearched);

            if (GroupedDateValues)
            {
                foreach (var yearNode in allNode.Nodes)
                {
                    if (string.IsNullOrEmpty(filter) || yearNode.Text.ToLower().Contains(filter.ToLower()))
                    {
                        yearNode.Visible = true;
                        yearNode.Collapse();
                    }
                    else
                    {
                        yearNode.Visible = false;
                        isFiltered = true;
                    }

                    foreach (var monthNode in yearNode.Nodes)
                    {
                        if (string.IsNullOrEmpty(filter)
                            || yearNode.Text.ToLower().Contains(filter.ToLower())
                            || monthNode.Text.ToLower().Contains(filter.ToLower()))
                        {
                            yearNode.Visible = true;

                            if (!string.IsNullOrEmpty(filter))
                                yearNode.Expand();
                            monthNode.Visible = true;
                        }
                        else
                        {
                            monthNode.Visible = false;
                            isFiltered = true;
                        }

                        foreach (var dayNode in monthNode.Nodes)
                            if (string.IsNullOrEmpty(filter)
                                || monthNode.Text.ToLower().Contains(filter.ToLower())
                                || dayNode.Text.ToLower().Contains(filter.ToLower()))
                            {
                                yearNode.Visible = true;

                                monthNode.Visible = true;

                                if (!string.IsNullOrEmpty(filter))
                                {
                                    yearNode.Expand();
                                    monthNode.Expand();
                                }

                                dayNode.Visible = true;
                            }
                            else
                            {
                                dayNode.Visible = false;
                                isFiltered = true;
                            }
                    }
                }
            }
            else
            {
                treeView.Filter = filter;
                isFiltered = !string.IsNullOrEmpty(filter);
            }

            allNode.InvalidateOnState();
            TreeView.EndUpdate();
            ValidateSelectedValues();
        }

        public event EventHandler SelectionChanged;


        protected override void InitializeFields()
        {
            base.InitializeFields();

            BorderGradientStyle = GradientStyles.Solid;
            BorderColor = Color.FromArgb(156, 189, 232);
        }

        protected override void CreateChildElements()
        {
            base.CreateChildElements();

            treeView = new RadTreeView();

            treeView.TriStateMode = true;
            treeView.CheckBoxes = true;
            treeView.ShowExpandCollapse = false;
            treeView.ShowLines = true;
            treeView.ShowRootLines = false;
            treeView.NodeCheckedChanged += treeView_NodeCheckedChanged;
            treeView.NodeExpandedChanging += treeView_NodeExpandedChanging;
            treeView.NodeMouseClick += treeView_NodeMouseClick;

            nullNode = new RadTreeNode();
            nonNullNode = new RadTreeNode();

            treeView.Nodes.Add(nullNode);
            treeView.Nodes.Add(nonNullNode);

            allNode = new RadTreeNode();
            allNode.Expanded = true;
            treeView.Nodes.Add(allNode);

            hostItem = new RadHostItem(treeView);
            Children.Add(hostItem);
        }

        protected override void DisposeManagedResources()
        {
            treeView.NodeCheckedChanged -= treeView_NodeCheckedChanged;
            treeView.NodeExpandedChanging -= treeView_NodeExpandedChanging;
            treeView.NodeMouseClick -= treeView_NodeMouseClick;

            base.DisposeManagedResources();
        }


        private void treeView_NodeMouseClick(object sender, RadTreeViewEventArgs e)
        {
            e.Node.Checked = !e.Node.Checked;
        }

        private void treeView_NodeExpandedChanging(object sender, RadTreeViewCancelEventArgs e)
        {
            if (GroupedDateValues)
                return;
            if (e.Node.Expanded)
                e.Cancel = true;
        }

        private void treeView_NodeCheckedChanged(object sender, RadTreeViewEventArgs e)
        {
            TreeView.NodeCheckedChanged -= treeView_NodeCheckedChanged;

            if ((e.Node == allNode) &&
                ((allNode.CheckState == ToggleState.On) ||
                 (allNode.CheckState == ToggleState.Indeterminate)))
            {
                nullNode.CheckState = ToggleState.Off;
                nonNullNode.CheckState = ToggleState.Off;
            }
            else
            {
                if ((e.Node == nullNode) && (nullNode.CheckState == ToggleState.On))
                {
                    allNode.CheckState = ToggleState.Off;
                    nonNullNode.CheckState = ToggleState.Off;
                }
                else
                {
                    if ((e.Node == nonNullNode) && (nonNullNode.CheckState == ToggleState.On))
                    {
                        allNode.CheckState = ToggleState.Off;
                        nullNode.CheckState = ToggleState.Off;
                    }
                }
            }
            UpdateNodeSelectionOnCheckedChanged(e.Node);
            TreeView.NodeCheckedChanged += treeView_NodeCheckedChanged;

            OnSelectionChanged();
        }


        private void OnSelectionChanged()
        {
            if (SelectionChanged != null)
                SelectionChanged(this, new EventArgs());
        }

        private void UpdateNodeSelectionOnCheckedChanged(RadTreeNode node)
        {
            if (SelectedValues == null)
            {
                ValidateSelectedValues();
                return;
            }

            if (GroupedDateValues)
            {
                if (node.Level == 3)
                {
                    var monthNode = node.Parent;
                    var yearNode = node.Parent.Parent;
                    var date = new DateTime(Convert.ToInt32(yearNode.Text), Convert.ToInt32(monthNode.Text),
                        Convert.ToInt32(node.Text));

                    if ((node.Visible || node.IsInDesignMode) && node.Checked)
                        SelectedValues.Add(date.ToString(), date);
                    else
                        SelectedValues.Remove(date.ToString());
                }
            }
            else
            {
                if ((node.Visible || node.IsInDesignMode) &&
                    ((treeView.TreeViewElement.FilterPredicate == null) ||
                     treeView.TreeViewElement.FilterPredicate(node)) &&
                    (node.CheckState == ToggleState.On) &&
                    treeValuesHash.ContainsKey(node))
                    SelectedValues.Add(node.Text, (ArrayList) treeValuesHash[node]);
                else
                    SelectedValues.Remove(node.Text);
            }

            UpdateSelectionMode();
        }

        private void ValidateSelectedValues()
        {
            if (SelectedValues == null)
                SelectedValues = new RadListFilterDistinctValuesTable();
            SelectedValues.Clear();

            if (treeValuesHash == null)
                return;
            if (GroupedDateValues)
                foreach (var yearNode in allNode.Nodes)
                    if (yearNode.Visible || yearNode.IsInDesignMode)
                        foreach (var monthNode in yearNode.Nodes)
                            if (monthNode.Visible || monthNode.IsInDesignMode)
                                foreach (var dayNode in monthNode.Nodes)
                                    if ((dayNode.Visible || dayNode.IsInDesignMode) && dayNode.Checked)
                                    {
                                        var date = new DateTime(Convert.ToInt32(yearNode.Text),
                                            Convert.ToInt32(monthNode.Text), Convert.ToInt32(dayNode.Text));
                                        SelectedValues.Add(date.ToString(), date);
                                    }
                                    else
                                    {
                                        foreach (DictionaryEntry entry in treeValuesHash)
                                        {
                                            var node = (RadTreeNode) entry.Key;
                                            if ((node.Visible || node.IsInDesignMode) &&
                                                ((treeView.TreeViewElement.FilterPredicate == null) ||
                                                 treeView.TreeViewElement.FilterPredicate(node)) &&
                                                (node.CheckState == ToggleState.On))
                                                SelectedValues.Add(node.Text, (ArrayList) entry.Value);
                                        }
                                    }
            UpdateSelectionMode();
        }

        private void UpdateSelectionMode()
        {
            if ((allNode.CheckState == ToggleState.On) && !isFiltered)
            {
                SelectedMode = ListFilterSelectedMode.All;
            }
            else
            {
                if (nullNode.CheckState == ToggleState.On)
                {
                    SelectedMode = ListFilterSelectedMode.Null;
                }
                else
                {
                    if (nonNullNode.CheckState == ToggleState.On)
                    {
                        SelectedMode = ListFilterSelectedMode.NotNull;
                    }
                    else
                    {
                        if (SelectedValues.Count > 0)
                            SelectedMode = ListFilterSelectedMode.Custom;
                        else
                            SelectedMode = ListFilterSelectedMode.None;
                    }
                }
            }
        }

        private bool StringIsNullOrWhiteSpace(string str)
        {
            if (str == null)
                return true;
            for (var i = 0; i < str.Length; i++)
                if (!char.IsWhiteSpace(str[i]))
                    return false;
            return true;
        }
    }
}
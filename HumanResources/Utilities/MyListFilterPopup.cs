using System;
using System.Collections;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.Data;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Localization;

namespace HumanResources.Utilities
{
    public class MyListFilterPopup : BaseFilterPopup, IGridFilterPopupInteraction
    {
        private static Size Popup_Size = new Size(200, 300);

        private readonly bool groupedDateValues;
        private readonly GridTableElement tableElement;
        private IRadListFilterElement listFilterElement;


        public MyListFilterPopup(GridViewDataColumn dataColumn)
            : this(dataColumn, false)
        {
        }

        public MyListFilterPopup(GridViewDataColumn dataColumn, bool groupedDateValues)
        {
            this.groupedDateValues = groupedDateValues;
            DataColumn = dataColumn;
            tableElement = DataColumn.OwnerTemplate.MasterTemplate.Owner.TableElement;

            InitializeElements();

            Size = Popup_Size;
        }


        /// <summary>
        ///     Gets the menu item that holds the tree view.
        /// </summary>
        public MyFilterMenuTreeElement MenuTreeElement { get; private set; }

        /// <summary>
        ///     Gets the menu item that holds the OK and Canel buttons.
        /// </summary>
        public FilterMenuButtonsItem ButtonsMenuItem { get; private set; }

        /// <summary>
        ///     Gets the menu item that holds the text box.
        /// </summary>
        public FilterMenuTextBoxItem TextBoxMenuItem { get; private set; }

        protected Type ColumnFilteringDataType
        {
            get
            {
                var comboBoxColumn = DataColumn as GridViewComboBoxColumn;

                if (comboBoxColumn != null)
                {
                    var pi = comboBoxColumn.GetType()
                        .GetProperty("FilteringMemberDataType", BindingFlags.Instance | BindingFlags.NonPublic);

                    return (Type) pi.GetValue(comboBoxColumn, null);
                }

                return DataColumn.DataType;
            }
        }


        public bool IsPopupOpen
        {
            get { return PopupManager.Default.ContainsPopup(this); }
        }

        public void ProcessKey(KeyEventArgs keys)
        {
            MenuTreeElement.TreeView.CallOnKeyDown(keys);
        }

        protected override void Dispose(bool disposing)
        {
            TextBoxMenuItem.TextBox.TextChanged -= textBoxSearch_TextChanged;
            listFilterElement.SelectionChanged -= ListFilterElement_SelectionChanged;
            ButtonsMenuItem.ButtonOK.Click -= ButtonOK_Click;
            ButtonsMenuItem.ButtonCancel.Click -= ButtonCancel_Click;

            foreach (var menuItem in Items)
                if (menuItem is RadMenuItem)
                {
                    foreach (RadMenuItem childMenuItem in ((RadMenuItem) menuItem).Items)
                        menuItem.Click -= FilterMenuItem_Click;
                    menuItem.Click -= FilterMenuItem_Click;
                }
            base.Dispose(disposing);
        }


        protected override void OnPopupOpened()
        {
            base.OnPopupOpened();

            if (tableElement != null)
                tableElement.Focus();
        }

        public override bool OnMouseWheel(Control target, int delta)
        {
            if (target is RadSizableDropDownMenu || target is RadTreeView || target is RadGridView)
            {
                var item = GetMyFilterMenuTreeItem();
                if (item != null)
                {
                    if (delta < 0)
                        delta = -3;
                    else
                        delta = 3;
                    item.TreeElement.TreeView.TreeViewElement.ScrollTo(delta);
                }
            }

            return true;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            Popup_Size = Size;
            base.OnSizeChanged(e);
        }

        private MyFilterMenuTreeItem GetMyFilterMenuTreeItem()
        {
            foreach (var item in Items)
                if (item is MyFilterMenuTreeItem)
                    return (MyFilterMenuTreeItem) item;
            return null;
        }

        public override bool OnKeyDown(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (ButtonsMenuItem.ButtonOK.Enabled)
                    ButtonOK_Click(this, EventArgs.Empty);
                return true;
            }

            return base.OnKeyDown(keyData);
        }

        public override void SetTheme(string themeName)
        {
            ThemeName = themeName;
            foreach (var item in Items)
                if (item is MyFilterMenuTreeItem)
                {
                    ((MyFilterMenuTreeItem) item).TreeElement.TreeView.ThemeName = themeName;
                }
                else
                {
                    if (item is FilterMenuTextBoxItem)
                        ((FilterMenuTextBoxItem) item).TextBox.ThemeName = themeName;
                }
            if ((ThemeName == "TelerikMetroTouch") ||
                (ThemeResolutionService.ApplicationThemeName == "TelerikMetroTouch"))
            {
                Width = 300;
                Height = 400;
                ButtonsMenuItem.Margin = new Padding(0, 0, 20, 0);
            }
        }


        protected virtual void InitializeElements()
        {
            CreateFilterOperationsMenuItems();
            CreateListFilterMenuItems();
        }

        protected virtual void CreateFilterOperationsMenuItems()
        {
            var descriptor = GetFilterDescriptor();
            var compositeDescriptor = descriptor as CompositeFilterDescriptor;
            var descriptorType = CompositeFilterDescriptor.GetDescriptorType(compositeDescriptor);

            var menuItemAvailableFilters = new RadMenuItem();
            menuItemAvailableFilters.IsChecked = false;
            menuItemAvailableFilters.Text =
                RadGridLocalizationProvider.CurrentProvider.GetLocalizedString(
                    RadGridStringId.FilterMenuAvailableFilters);
            menuItemAvailableFilters.StretchVertically = false;
            Items.Add(menuItemAvailableFilters);

            var separator = new RadMenuSeparatorItem();
            separator.StretchVertically = false;
            Items.Add(separator);

            var dataType = ColumnFilteringDataType;
            var filterOperations = FilterOperationContext.GetFilterOperations(dataType);

            foreach (var context in filterOperations)
                if ((context.Operator == FilterOperator.None) || (context.Operator == FilterOperator.IsNull) ||
                    (context.Operator == FilterOperator.IsNotNull))
                {
                    var contextMenuItem = new RadFilterOperationMenuItem(context);
                    contextMenuItem.Click += FilterMenuItem_Click;
                    if (context.Operator == FilterOperator.None)
                    {
                        contextMenuItem.Enabled =
                            !(((compositeDescriptor == null) || (compositeDescriptor.Operator == FilterOperator.None)) &&
                              (contextMenuItem.Operator == descriptor.Operator));
                        contextMenuItem.Text =
                            RadGridLocalizationProvider.CurrentProvider.GetLocalizedString(
                                RadGridStringId.FilterMenuClearFilters);

                        contextMenuItem.TextImageRelation = TextImageRelation.ImageBeforeText;
                        contextMenuItem.ImageAlignment = ContentAlignment.MiddleLeft;
                        contextMenuItem.DisplayStyle = DisplayStyle.ImageAndText;
                        Items.Insert(0, contextMenuItem);
                    }
                    else
                    {
                        contextMenuItem.IsChecked = ((compositeDescriptor == null) ||
                                                     (compositeDescriptor.Operator == FilterOperator.None)) &&
                                                    (contextMenuItem.Operator == descriptor.Operator);
                        if (contextMenuItem.IsChecked)
                            menuItemAvailableFilters.IsChecked = true;
                        menuItemAvailableFilters.Items.Add(contextMenuItem);
                    }
                }
                else
                {
                    var contextMenuItem = new RadFilterComposeMenuItem();
                    contextMenuItem.Text = context.Name;
                    contextMenuItem.FilterDescriptor = descriptor.Clone() as FilterDescriptor;
                    contextMenuItem.FilterDescriptor.Operator = context.Operator;
                    contextMenuItem.Click += FilterMenuItem_Click;
                    contextMenuItem.IsChecked = ((compositeDescriptor == null) ||
                                                 (compositeDescriptor.Operator == FilterOperator.None)) &&
                                                (context.Operator == descriptor.Operator);
                    if (contextMenuItem.IsChecked)
                        menuItemAvailableFilters.IsChecked = true;
                    menuItemAvailableFilters.Items.Add(contextMenuItem);
                }
            if (GridViewHelper.IsNumeric(dataType) || (dataType == typeof(DateTime)))
            {
                var filterBetween = new RadFilterComposeMenuItem(RadGridStringId.FilterFunctionBetween);
                filterBetween.IsChecked = (compositeDescriptor != null) &&
                                          (compositeDescriptor.Operator != FilterOperator.None) &&
                                          (descriptorType == CompositeFilterDescriptor.DescriptorType.Between);
                if (filterBetween.IsChecked)
                    menuItemAvailableFilters.IsChecked = true;
                filterBetween.FilterDescriptor =
                    GetCompositeFilterDescriptor(CompositeFilterDescriptor.DescriptorType.Between, compositeDescriptor);
                filterBetween.Click += FilterMenuItem_Click;
                menuItemAvailableFilters.Items.Add(filterBetween);

                var filterNotBetween = new RadFilterComposeMenuItem(RadGridStringId.FilterFunctionNotBetween);
                filterNotBetween.IsChecked = descriptorType == CompositeFilterDescriptor.DescriptorType.NotBetween;
                if (filterNotBetween.IsChecked)
                    menuItemAvailableFilters.IsChecked = true;
                filterNotBetween.FilterDescriptor =
                    GetCompositeFilterDescriptor(CompositeFilterDescriptor.DescriptorType.NotBetween,
                        compositeDescriptor);
                filterNotBetween.Click += FilterMenuItem_Click;
                menuItemAvailableFilters.Items.Add(filterNotBetween);
            }

            if (dataType != typeof(Image))
            {
                var customFilterBetween = new RadFilterComposeMenuItem(RadGridStringId.FilterFunctionCustom);
                customFilterBetween.FilterDescriptor = descriptor.Clone() as FilterDescriptor;
                customFilterBetween.Click += FilterMenuItem_Click;
                customFilterBetween.IsChecked = (compositeDescriptor != null) &&
                                                (compositeDescriptor.Operator != FilterOperator.None) &&
                                                (descriptorType == CompositeFilterDescriptor.DescriptorType.Unknown);
                if (customFilterBetween.IsChecked)
                    menuItemAvailableFilters.IsChecked = true;
                menuItemAvailableFilters.Items.Add(customFilterBetween);
            }
        }

        protected virtual void CreateListFilterMenuItems()
        {
            TextBoxMenuItem = new FilterMenuTextBoxItem();
            TextBoxMenuItem.TextBox.TextChanged += textBoxSearch_TextChanged;
            Items.Add(TextBoxMenuItem);

            var dataType = ColumnFilteringDataType;

            var treeMenuItem = new MyFilterMenuTreeItem();
            treeMenuItem.TreeElement.GroupedDateValues = groupedDateValues &&
                                                         ((dataType == typeof(DateTime)) ||
                                                          (dataType == typeof(DateTime?)));
            listFilterElement = treeMenuItem.TreeElement;
            MenuTreeElement = treeMenuItem.TreeElement;
            listFilterElement.SelectionChanged += ListFilterElement_SelectionChanged;
            Items.Add(treeMenuItem);

            ButtonsMenuItem = new FilterMenuButtonsItem();
            ButtonsMenuItem.ButtonOK.Click += ButtonOK_Click;
            ButtonsMenuItem.ButtonCancel.Click += ButtonCancel_Click;
            Items.Add(ButtonsMenuItem);

            var distinctValues = GetDistinctValuesTable();
            listFilterElement.DistinctListValues = distinctValues;

            var selectedValueList = new RadListFilterDistinctValuesTable();
            selectedValueList.FormatString = DataColumn.FormatString;

            listFilterElement.SelectedMode = GetGridFilteredValues(DataColumn.OwnerTemplate.FilterDescriptors,
                ref selectedValueList);
            listFilterElement.SelectedValues = selectedValueList;

            if (DataColumn.DistinctValuesWithFilter != null)
                listFilterElement.EnableBlanks =
                    DataColumn.DistinctValuesWithFilter.Contains(null) ||
                    DataColumn.DistinctValuesWithFilter.Contains(DBNull.Value);
        }

        protected virtual ListFilterSelectedMode GetGridFilteredValues(FilterDescriptorCollection descriptorCollection,
            ref RadListFilterDistinctValuesTable selectedValuesList)
        {
            var selectedMode = ListFilterSelectedMode.All;

            foreach (var filterDescriptor in descriptorCollection)
                if (filterDescriptor is CompositeFilterDescriptor)
                {
                    var compositeFilterDescriptor = (CompositeFilterDescriptor) filterDescriptor;
                    selectedMode = GetGridFilteredValues(compositeFilterDescriptor.FilterDescriptors,
                        ref selectedValuesList);
                }
                else
                {
                    if (filterDescriptor.PropertyName == DataColumn.Name)
                        switch (filterDescriptor.Operator)
                        {
                            case FilterOperator.IsNull:
                                return ListFilterSelectedMode.Null;
                            case FilterOperator.IsNotNull:
                                return ListFilterSelectedMode.NotNull;
                            case FilterOperator.IsEqualTo:
                                selectedValuesList.Add(filterDescriptor.Value);
                                selectedMode = ListFilterSelectedMode.Custom;
                                break;
                            case FilterOperator.IsNotEqualTo:
                            case FilterOperator.None:
                            case FilterOperator.IsLike:
                            case FilterOperator.IsNotLike:
                            case FilterOperator.IsLessThan:
                            case FilterOperator.IsLessThanOrEqualTo:
                            case FilterOperator.IsGreaterThanOrEqualTo:
                            case FilterOperator.IsGreaterThan:
                            case FilterOperator.StartsWith:
                            case FilterOperator.EndsWith:
                            case FilterOperator.Contains:
                            case FilterOperator.NotContains:
                            case FilterOperator.IsContainedIn:
                            case FilterOperator.IsNotContainedIn:
                                selectedMode = ListFilterSelectedMode.None;
                                break;
                        }
                }
            return selectedMode;
        }

        protected virtual void EnsureButtonOK()
        {
            if (listFilterElement.SelectedMode == ListFilterSelectedMode.None)
                ButtonsMenuItem.ButtonOK.Enabled = false;
            else
                ButtonsMenuItem.ButtonOK.Enabled = true;
        }


        protected virtual void OnButtonCancelClick(EventArgs e)
        {
            ClosePopup(RadPopupCloseReason.CloseCalled);
        }

        protected virtual void OnButtonOkClick(EventArgs e)
        {
            var filterOperator = FilterOperator.IsEqualTo;

            switch (listFilterElement.SelectedMode)
            {
                case ListFilterSelectedMode.All:
                    filterOperator = FilterOperator.None;
                    break;
                case ListFilterSelectedMode.Null:
                    filterOperator = FilterOperator.IsNull;
                    break;
                case ListFilterSelectedMode.NotNull:
                    filterOperator = FilterOperator.IsNotNull;
                    break;
            }

            if (filterOperator != FilterOperator.IsEqualTo)
            {
                SetFilterOperator(filterOperator);
                ClosePopup(RadPopupCloseReason.CloseCalled);
            }
            else
            {
                var compositeFilterDescriptor = new CompositeFilterDescriptor();
                compositeFilterDescriptor.LogicalOperator = FilterLogicalOperator.Or;
                compositeFilterDescriptor.PropertyName = DataColumn.Name;
                var distinctValues = GetDistinctValuesTable();

                foreach (DictionaryEntry entry in listFilterElement.SelectedValues)
                    foreach (var value in (ArrayList) entry.Value)
                    {
                        FilterDescriptor descriptor;

                        if (DataColumn is GridViewDateTimeColumn || (DataColumn.DataType == typeof(DateTime)) ||
                            (DataColumn.DataType == typeof(DateTime?)))
                            descriptor = new DateFilterDescriptor(DataColumn.Name, FilterOperator.IsEqualTo,
                                (DateTime?) value, false);
                        else
                            descriptor = new FilterDescriptor(DataColumn.Name, FilterOperator.IsEqualTo, value);
                        compositeFilterDescriptor.FilterDescriptors.Add(descriptor);
                    }
                FilterDescriptor = compositeFilterDescriptor;
                OnFilterConfirmed();
            }
        }

        protected virtual void OnFilterListSelectionChanged(EventArgs e)
        {
            EnsureButtonOK();
        }

        protected virtual void OnTextBoxTextChanged(EventArgs e)
        {
            listFilterElement.Filter(TextBoxMenuItem.TextBox.Text);
            EnsureButtonOK();
        }

        protected virtual void OnFilterMenuItemClick(object sender, EventArgs e)
        {
            var operationMenu = sender as RadFilterOperationMenuItem;
            if ((operationMenu != null) && !operationMenu.IsChecked)
                SetFilterOperator(operationMenu.Operator);
            var composeMenu = sender as RadFilterComposeMenuItem;
            if (composeMenu != null)
                EditFilterDescriptor(composeMenu);
        }


        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            OnButtonCancelClick(e);
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            OnButtonOkClick(e);
        }

        private void ListFilterElement_SelectionChanged(object sender, EventArgs e)
        {
            OnFilterListSelectionChanged(e);
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            OnTextBoxTextChanged(e);
        }

        private void FilterMenuItem_Click(object sender, EventArgs e)
        {
            OnFilterMenuItemClick(sender, e);
        }
    }
}
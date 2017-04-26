using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Telerik.Charting;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace HumanResources.Forms.Statistic
{
    public partial class frm_Statistic : RadForm
    {
        private static frm_Statistic s_Instance;

        public frm_Statistic()
        {
            InitializeComponent();
        }

        public static frm_Statistic Instance
        {
            get
            {
                if (s_Instance == null)
                    s_Instance = new frm_Statistic();
                return s_Instance;
            }
        }

        private void frm_Statistic_Load(object sender, EventArgs e)
        {
            radChartView1.View.Margin = new Padding(20);
            radChartView1.Title = "Test CHART";
            radChartView1.ShowTitle = true;
            radChartView1.ChartElement.TitlePosition = TitlePosition.Top;
            radChartView1.ChartElement.TitleElement.TextAlignment = ContentAlignment.MiddleCenter;

            var smartPie = new PieSeries();
            smartPie.LabelMode = PieLabelModes.Horizontal;
            smartPie.ShowLabels = true;
            smartPie.DrawLinesToLabels = true;
            smartPie.SyncLinesToLabelsColor = true;
            foreach (var dataItem in GetPieData())
            {
                var point = new PieDataPoint(dataItem.Key, dataItem.Value.ToString());

                point = new PieDataPoint(dataItem.Key, dataItem.Value.ToString());
                point.Label = dataItem.Value + $" ({dataItem.Key})";
                smartPie.DataPoints.Add(point);
            }
            radChartView1.SelectionMode = ChartSelectionMode.SingleDataPoint;
            radChartView1.Series.Add(smartPie);
            radChartView1.ShowSmartLabels = true;

            radChartView1.LabelFormatting += radChartView_LabelFormatting;
            radChartView1.SelectedPointChanged += selectionController_SelectedPointChanged;
            radChartView1.SelectedPointChanged += selectionController_SelectedPointChanged;
            FormatChart();
        }

        private void selectionController_SelectedPointChanged(object sender, ChartViewSelectedPointChangedEventArgs args)
        {
            UpdadateOffsetFromCenter(args.OldSelectedPoint as PieDataPoint);
            UpdadateOffsetFromCenter(args.NewSelectedPoint as PieDataPoint);
        }

        private void UpdadateOffsetFromCenter(PieDataPoint point)
        {
            if (point != null)
                point.OffsetFromCenter = point.IsSelected ? 0.1 : 0;
        }

        private void FormatChart()
        {
            for (var i = 0; i < radChartView1.Series.Count; i++)
            {
                radChartView1.Series[i].DrawLinesToLabels = true;
                radChartView1.Series[i].SyncLinesToLabelsColor = true;
            }
            radChartView1.View.PerformRefresh(radChartView1.View, false);
            var theme = Theme.ReadCSSText(@"                                         
                                            theme                                         
                                            {                                            
                                                name: ControlDefault;                                            
                                                elementType: Telerik.WinControls.UI.RadChartElement;                                             
                                                controlType: Telerik.WinControls.UI.RadChartView;                                          
                                            }                                          
                                            PieSegment                                         
                                            {                                                 
                                                RadiusAspectRatio                                             
                                                {                                                 
                                                    Value: 0.5;                                                 
                                                    EndValue: 1;                                                 
                                                    MaxValue: 1;                                                 
                                                    Frames: 20;                                                 
                                                    Interval: 10;                                                 
                                                    EasingType: OutCircular;                                                 
                                                    RandomDelay: 100;                                                 
                                                    RemoveAfterApply: true;                                              
                                                }                                         
                                            }                                         
                                        ");
            ThemeRepository.Add(theme, false);
        }

        private void radChartView_LabelFormatting(object sender, ChartViewLabelFormattingEventArgs e)
        {
            e.LabelElement.BorderColor = ((DataPointElement) e.LabelElement.Parent).BackColor;
            e.LabelElement.Font = new Font(Font, FontStyle.Bold);
        }

        public List<KeyValuePair<double, object>> GetPieData()
        {
            var data = new List<KeyValuePair<double, object>>();

            data.Add(new KeyValuePair<double, object>(46, "Samsung"));
            data.Add(new KeyValuePair<double, object>(43.5, "Apple"));
            data.Add(new KeyValuePair<double, object>(19, "RIM"));
            data.Add(new KeyValuePair<double, object>(15, "Huawei"));
            data.Add(new KeyValuePair<double, object>(14, "Other"));
            data.Add(new KeyValuePair<double, object>(12, "Siemens"));
            data.Add(new KeyValuePair<double, object>(11.5, "Panasonic"));
            data.Add(new KeyValuePair<double, object>(8, "Nokia"));
            data.Add(new KeyValuePair<double, object>(6.5, "Sony"));
            data.Add(new KeyValuePair<double, object>(3.5, "Fujitsu"));
            data.Add(new KeyValuePair<double, object>(3, "HTC"));
            data.Add(new KeyValuePair<double, object>(2, "Motorola"));

            return data;
        }
    }
}
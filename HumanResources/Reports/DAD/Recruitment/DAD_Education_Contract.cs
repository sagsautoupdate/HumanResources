using System;
using System.ComponentModel;
using System.Drawing;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;

namespace HumanResources.Reports.DAD.Recruitment
{
    public class DAD_Education_Contract : ReportClass
    {
        public override string ResourceName
        {
            get { return "DAD_Education_Contract.rpt"; }
            set { }
        }

        public override bool NewGenerator
        {
            get { return true; }
            set { }
        }

        public override string FullResourceName
        {
            get { return "HumanResources.Reports.DAD.Recruitment.DAD_Education_Contract.rpt"; }
            set { }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Section Section1
        {
            get { return ReportDefinition.Sections[0]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Section Section2
        {
            get { return ReportDefinition.Sections[1]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Section Section3
        {
            get { return ReportDefinition.Sections[2]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Section Section4
        {
            get { return ReportDefinition.Sections[3]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Section Section5
        {
            get { return ReportDefinition.Sections[4]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_ContractNumber
        {
            get { return DataDefinition.ParameterFields[0]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_FromDate
        {
            get { return DataDefinition.ParameterFields[1]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_Sex
        {
            get { return DataDefinition.ParameterFields[2]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_FullName
        {
            get { return DataDefinition.ParameterFields[3]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_Birthday
        {
            get { return DataDefinition.ParameterFields[4]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_BirthPlace
        {
            get { return DataDefinition.ParameterFields[5]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_Nationality
        {
            get { return DataDefinition.ParameterFields[6]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_Education
        {
            get { return DataDefinition.ParameterFields[7]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_Resident
        {
            get { return DataDefinition.ParameterFields[8]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_IDCard
        {
            get { return DataDefinition.ParameterFields[9]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_DateOfIssue
        {
            get { return DataDefinition.ParameterFields[10]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_PlaceOfIssue
        {
            get { return DataDefinition.ParameterFields[11]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_ToDate
        {
            get { return DataDefinition.ParameterFields[12]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_Subject
        {
            get { return DataDefinition.ParameterFields[13]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_CandidateId
        {
            get { return DataDefinition.ParameterFields[14]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_Fee
        {
            get { return DataDefinition.ParameterFields[15]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_FeeVN
        {
            get { return DataDefinition.ParameterFields[16]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_EducatedPositionName
        {
            get { return DataDefinition.ParameterFields[17]; }
        }
    }

    [ToolboxBitmap(typeof(ExportOptions), "report.bmp")]
    public class CachedDAD_Education_Contract : Component, ICachedReport
    {
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsCacheable
        {
            get { return true; }
            set { }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool ShareDBLogonInfo
        {
            get { return false; }
            set { }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual TimeSpan CacheTimeOut
        {
            get { return CachedReportConstants.DEFAULT_TIMEOUT; }
            set { }
        }

        public virtual ReportDocument CreateReport()
        {
            var rpt = new DAD_Education_Contract();
            rpt.Site = Site;
            return rpt;
        }

        public virtual string GetCustomizedCacheKey(RequestContext request)
        {
            var key = (string) null;


            return key;
        }
    }
}
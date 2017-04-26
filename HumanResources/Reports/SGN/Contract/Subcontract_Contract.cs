using System;
using System.ComponentModel;
using System.Drawing;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;

namespace HumanResources.Reports.SGN.Contract
{
    public class Subcontract_Contract : ReportClass
    {
        public override string ResourceName
        {
            get { return "Subcontract_Contract.rpt"; }
            set { }
        }

        public override bool NewGenerator
        {
            get { return true; }
            set { }
        }

        public override string FullResourceName
        {
            get { return "HumanResources.Reports.SGN.Contract.Subcontract_Contract.rpt"; }
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
        public IParameterField Parameter_SubContractNumber
        {
            get { return DataDefinition.ParameterFields[0]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_Sex
        {
            get { return DataDefinition.ParameterFields[1]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_FullName
        {
            get { return DataDefinition.ParameterFields[2]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_Nationality
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
        public IParameterField Parameter_Education
        {
            get { return DataDefinition.ParameterFields[6]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_Resident
        {
            get { return DataDefinition.ParameterFields[7]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_ID
        {
            get { return DataDefinition.ParameterFields[8]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_DateOfIssue
        {
            get { return DataDefinition.ParameterFields[9]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_PlaceOfIssue
        {
            get { return DataDefinition.ParameterFields[10]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_ContractNo
        {
            get { return DataDefinition.ParameterFields[11]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_ContractFromDate
        {
            get { return DataDefinition.ParameterFields[12]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_SalaryValue
        {
            get { return DataDefinition.ParameterFields[13]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_SalaryCode
        {
            get { return DataDefinition.ParameterFields[14]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_SalaryLevel
        {
            get { return DataDefinition.ParameterFields[15]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_SubContractFromDate
        {
            get { return DataDefinition.ParameterFields[16]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_UserId
        {
            get { return DataDefinition.ParameterFields[17]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_ChangedSalary
        {
            get { return DataDefinition.ParameterFields[18]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_Detail
        {
            get { return DataDefinition.ParameterFields[19]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_Duration
        {
            get { return DataDefinition.ParameterFields[20]; }
        }
    }

    [ToolboxBitmap(typeof(ExportOptions), "report.bmp")]
    public class CachedSubcontract_Contract : Component, ICachedReport
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
            var rpt = new Subcontract_Contract();
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
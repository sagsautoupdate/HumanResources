using System;
using System.ComponentModel;
using System.Drawing;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;

namespace HumanResources.Reports.SGN.Contract
{
    public class Contract : ReportClass
    {
        public override string ResourceName
        {
            get { return "Contract.rpt"; }
            set { }
        }

        public override bool NewGenerator
        {
            get { return true; }
            set { }
        }

        public override string FullResourceName
        {
            get { return "HumanResources.Reports.SGN.Contract.Contract.rpt"; }
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
        public Section DetailSection1
        {
            get { return ReportDefinition.Sections[3]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Section DetailSection2
        {
            get { return ReportDefinition.Sections[4]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Section Section4
        {
            get { return ReportDefinition.Sections[5]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Section Section5
        {
            get { return ReportDefinition.Sections[6]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_ContractNo
        {
            get { return DataDefinition.ParameterFields[0]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_TenHD
        {
            get { return DataDefinition.ParameterFields[1]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_HoTen
        {
            get { return DataDefinition.ParameterFields[2]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_GioiTinh
        {
            get { return DataDefinition.ParameterFields[3]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_NgaySinh
        {
            get { return DataDefinition.ParameterFields[4]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_ThangSinh
        {
            get { return DataDefinition.ParameterFields[5]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_NamSinh
        {
            get { return DataDefinition.ParameterFields[6]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_NoiSinh
        {
            get { return DataDefinition.ParameterFields[7]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_NgheNghiep
        {
            get { return DataDefinition.ParameterFields[8]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_DiaChi
        {
            get { return DataDefinition.ParameterFields[9]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_CMND
        {
            get { return DataDefinition.ParameterFields[10]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_NgayCap
        {
            get { return DataDefinition.ParameterFields[11]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_NoiCap
        {
            get { return DataDefinition.ParameterFields[12]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_LoaiHD
        {
            get { return DataDefinition.ParameterFields[13]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_TNgay
        {
            get { return DataDefinition.ParameterFields[14]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_ChucDanhChuyenMon
        {
            get { return DataDefinition.ParameterFields[15]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_ChucVu
        {
            get { return DataDefinition.ParameterFields[16]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_ThoiGioLamViec
        {
            get { return DataDefinition.ParameterFields[17]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_LamThem
        {
            get { return DataDefinition.ParameterFields[18]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_MucLuong
        {
            get { return DataDefinition.ParameterFields[19]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_MaLuong
        {
            get { return DataDefinition.ParameterFields[20]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_Muc
        {
            get { return DataDefinition.ParameterFields[21]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_NgayNghi
        {
            get { return DataDefinition.ParameterFields[22]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_UserId
        {
            get { return DataDefinition.ParameterFields[23]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_CongViecPhaiLam
        {
            get { return DataDefinition.ParameterFields[24]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_QuocTich
        {
            get { return DataDefinition.ParameterFields[25]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_HDTNgay
        {
            get { return DataDefinition.ParameterFields[26]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_HDTThang
        {
            get { return DataDefinition.ParameterFields[27]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_HDTNam
        {
            get { return DataDefinition.ParameterFields[28]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_SAGS
        {
            get { return DataDefinition.ParameterFields[29]; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_ContractFullName
        {
            get { return DataDefinition.ParameterFields[30]; }
        }
    }

    [ToolboxBitmap(typeof(ExportOptions), "report.bmp")]
    public class CachedContract : Component, ICachedReport
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
            var rpt = new Contract();
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
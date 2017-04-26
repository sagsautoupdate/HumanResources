using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using AESm;
using HRMBLL.H;
using HRMBLL.H0;
using HRMUtil;
using HumanResources.Properties;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls.UI;
using Encoder = System.Drawing.Imaging.Encoder;

namespace HumanResources.Utilities
{
    public class Utilities
    {
        public static List<string> _listConnectedServer;
        public static List<string> _listIsAdminServer;
        public static string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
        public static string InfluencedTable { get; set; }
        public static string StoredProcedures { get; set; }
        public static string ChangedContent { get; set; }
        public static string OldContent { get; set; }
        [DllImport("user32.dll")]
        private static extern int GetWindowThreadProcessId(int hWnd, out int lpdwProcessId);
        public static void DisposeApplication()
        {
            Application.Exit();
            var pArry = Process.GetProcesses();
            foreach (var p in pArry)
            {
                var s = p.ProcessName;
                s = s.ToUpper();
                if (s.CompareTo("HumanResources") == 0)
                    p.Kill();
            }
        }
        public static string GetDepartmentFullName(string DepFullName, int Level)
        {
            var ReturnValue = string.Empty;

            switch (Level)
            {
                case 1:
                    ReturnValue = DepFullName.Replace(" - SAGS", "");
                    break;
                case 2:
                {
                    var Temp = DepFullName.Substring(0, 2);
                    ReturnValue = DepFullName.Replace(Temp, "").Replace(" - SAGS", "");
                }
                    break;
                case 3:
                {
                    var Temp = DepFullName.Substring(0, 3);
                    ReturnValue = DepFullName.Replace(Temp, "").Replace(" - SAGS", "");
                }
                    break;
                case 4:
                {
                    var Temp = DepFullName.Substring(0, 4);
                    ReturnValue = DepFullName.Replace(Temp, "").Replace(" - SAGS", "");
                }
                    break;
                case 5:
                {
                    var Temp = DepFullName.Substring(0, 5);
                    ReturnValue = DepFullName.Replace(Temp, "").Replace(" - SAGS", "");
                }
                    break;
            }

            return ReturnValue;
        }
        public static RadContextMenu DefaultRadContextMenu()
        {
            var _rcm = new RadContextMenu();
            _rcm.ThemeName = "Windows8";

            var _rmi = new RadMenuItem
            {
                Text = "Thêm mới",
                Image = Resources.Add,
                Name = "rmiAdd"
            };
            _rcm.Items.Add(_rmi);

            _rmi = new RadMenuItem
            {
                Text = "Chỉnh sửa",
                Image = Resources.Edit,
                Name = "rmiEdit"
            };
            _rcm.Items.Add(_rmi);

            _rmi = new RadMenuItem
            {
                Text = "Xóa",
                Image = Resources.Delete,
                Name = "rmiDelete"
            };
            _rcm.Items.Add(_rmi);

            var _rmsi = new RadMenuSeparatorItem();
            _rcm.Items.Add(_rmsi);

            _rmi = new RadMenuItem
            {
                Text = "Cập nhật",
                Image = Resources.Refresh,
                Name = "rmiRefresh"
            };
            _rcm.Items.Add(_rmi);

            return _rcm;
        }
        public static int GetDataRowCount(GridViewChildRowCollection collection)
        {
            var count = 0;

            foreach (var row in collection)
                if (row is GridViewDataRowInfo)
                    count++;
                else
                    count += GetDataRowCount(row.ChildRows);
            return count;
        }
        public static string GetEnumDescription(Enum e)
        {
            var fieldInfo = e.GetType().GetField(e.ToString());

            var enumAttributes =
                (DescriptionAttribute[]) fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (enumAttributes.Length > 0)
                return enumAttributes[0].Description;
            return e.ToString();
        }
        public static List<string> TableColumns(DataTable dt)
        {
            var strReturn = new List<string>();
            foreach (DataColumn col in dt.Columns)
                if ((col.ColumnName != string.Empty) && (col.ColumnName != "Password")
                    && (col.ColumnName != "RootId") && (col.ColumnName != "DepartmentId")
                    && (col.ColumnName != "ParentId") && (col.ColumnName != "Level")
                    && (col.ColumnName != "PositionId") && (col.ColumnName != "UserId")
                    && (col.ColumnName != "FullName") && (col.ColumnName != "PositionName")
                    && (col.ColumnName != "RootName") && (col.ColumnName != "DepartmentName") &&
                    (col.ColumnName != "DepartmentFullName")
                    && (col.ColumnName != "Status"))
                    strReturn.Add(col.ColumnName);
            return strReturn;
        }
        public static List<ListColumn> TableColumnsForGrid(DataTable dt)
        {
            var strReturn = new List<ListColumn>();
            foreach (DataColumn col in dt.Columns)
                if ((col.ColumnName != string.Empty) && (col.ColumnName != "Password")
                    && (col.ColumnName != "RootId") && (col.ColumnName != "DepartmentId")
                    && (col.ColumnName != "ParentId") && (col.ColumnName != "Level")
                    && (col.ColumnName != "PositionId") && (col.ColumnName != "UserId")
                    && (col.ColumnName != "FullName") && (col.ColumnName != "PositionName")
                    && (col.ColumnName != "RootName") && (col.ColumnName != "DepartmentName") &&
                    (col.ColumnName != "DepartmentFullName")
                    && (col.ColumnName != "Status"))
                    strReturn.Add(new ListColumn(col.ColumnName, AdministrationBLL.GetAllTranslatedColumn_ByName(col.ColumnName) == null ? "" : AdministrationBLL.GetAllTranslatedColumn_ByName(col.ColumnName)["ColumnNameInVI"].ToString()));
            return strReturn;
        }
        public static int TestConnection()
        {
            try
            {
                using (var Connection = new SqlConnection(AES.Decrypt(ConnectionString)))
                {
                    var Query = "Select 1";
                    var Command = new SqlCommand(Query, Connection);
                    Connection.Open();
                    Command.ExecuteScalar();

                    return (int) ExitCode.Success;
                }
            }
            catch
            {
                return (int) ExitCode.UnknownError;
            }
        }
        public static int TestPing(string Server)
        {
            var Result = -1;

            if (string.IsNullOrEmpty(Server))
                Result = (int) ExitCode.UnknownError;
            else
                try
                {
                    var pingSender = new Ping();
                    var options = new PingOptions
                    {
                        DontFragment = true
                    };


                    const string data = "Test";
                    var buffer = Encoding.ASCII.GetBytes(data);
                    const int timeout = 120;
                    var reply = pingSender.Send(Server, timeout, buffer, options);
                    if (reply.Status == IPStatus.Success)
                        Result = (int) ExitCode.Success;
                }
                catch
                {
                    MessageBox.Show("Can not check the connection to databases.");
                }
            return Result;
        }
        public static List<string> GetAllKeys()
        {
            var List = new List<string>();
            NameValueCollection sAll;
            sAll = ConfigurationManager.AppSettings;

            foreach (var item in sAll.AllKeys)
                if (item.Contains("Server_") || item.Equals("ConnectionString"))
                    List.Add(item);
            return List;
        }
        public static string GetValueByKey(string key)
        {
            var ReturnValue = string.Empty;

            var sName = ConfigurationManager.AppSettings;

            foreach (var item in sName.AllKeys)
                if (item == key)
                    ReturnValue = sName.Get(item);
            return ReturnValue;
        }
        public static string GetServerByKey(string key)
        {
            var Value = AES.Decrypt(GetValueByKey(key));

            var Start = Value.IndexOf("=");
            var End = Value.IndexOf(";");

            return Value.Substring(Start + 1, End - Start - 1);
        }
        public static string GetServerByKeyWithoutDecrypt(string key)
        {
            var Value = AES.Decrypt(key);

            var Start = Value.IndexOf("=");
            var End = Value.IndexOf(";");

            return Value.Substring(Start + 1, End - Start - 1);
        }
        public static List<string> GetAllActiveServer()
        {
            var ListSucceeded = new List<string>();
            foreach (var item in GetAllKeys())
            {
                var Server = GetServerByKey(item);
                if ((TestPing(Server) == 0) && (item.Equals("ConnectionString") == false))
                    ListSucceeded.Add(item);
            }
            return ListSucceeded;
        }
        public static Bitmap LoadPicture(string url)
        {
            HttpWebRequest wreq;
            HttpWebResponse wresp;
            Stream mystream;
            Bitmap bmp;

            bmp = null;
            mystream = null;
            wresp = null;
            try
            {
                wreq = (HttpWebRequest) WebRequest.Create(url);
                wreq.AllowWriteStreamBuffering = true;

                wresp = (HttpWebResponse) wreq.GetResponse();

                if ((mystream = wresp.GetResponseStream()) != null)
                    bmp = new Bitmap(mystream);
            }
            catch
            {
            }
            finally
            {
                if (mystream != null)
                    mystream.Close();
                if (wresp != null)
                    wresp.Close();
            }

            return bmp;
        }
        public static Image ResizeImage(Image image, PictureBox canvas, bool centerImage)
        {
            if ((image == null) || (canvas == null))
                return null;
            var canvasWidth = canvas.Size.Width;
            var canvasHeight = canvas.Size.Height;
            var originalWidth = image.Size.Width;
            var originalHeight = image.Size.Height;

            Image thumbnail = new Bitmap(canvasWidth, canvasHeight);
            var graphic = Graphics.FromImage(thumbnail);

            graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphic.SmoothingMode = SmoothingMode.HighQuality;
            graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphic.CompositingQuality = CompositingQuality.HighQuality;


            var ratioX = canvasWidth/(double) originalWidth;
            var ratioY = canvasHeight/(double) originalHeight;
            var ratio = ratioX < ratioY ? ratioX : ratioY;


            var newHeight = Convert.ToInt32(originalHeight*ratio);
            var newWidth = Convert.ToInt32(originalWidth*ratio);


            var posX = Convert.ToInt32((canvasWidth - image.Width*ratio)/2);
            var posY = Convert.ToInt32((canvasHeight - image.Height*ratio)/2);

            if (!centerImage)
            {
                posX = 0;
                posY = 0;
            }
            graphic.Clear(Color.White);
            graphic.DrawImage(image, posX, posY, newWidth, newHeight);


            var info =
                ImageCodecInfo.GetImageEncoders();
            EncoderParameters encoderParameters;
            encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality,
                100L);

            Stream s = new MemoryStream();
            thumbnail.Save(s, info[1],
                encoderParameters);

            return Image.FromStream(s);
        }
        public static void PopulateRootLevel(RadTreeView radTreeView)
        {
            var list = DepartmentsBLL.GetDepartmentRoot();
            PopulateNodes(list, radTreeView.Nodes, radTreeView);
            radTreeView.Nodes[0].Expand();
        }
        public static void PopulateNodes(List<DepartmentsBLL> list, RadTreeNodeCollection nodes, RadTreeView rtv)
        {
            foreach (var obj in list)
            {
                var tn = new RadTreeNode
                {
                    Text = obj.DepartmentName,
                    Value = obj.DepartmentId.ToString(),
                    ImageIndex = obj.Level
                };

                nodes.Add(tn);


                if (obj.ChildNodeCount > 0)
                {
                    PopulateSubLevel(obj.DepartmentId, tn, rtv);
                    tn.Font = new Font(rtv.Font, FontStyle.Bold);
                }
            }
        }
        public static void PopulateSubLevel(int parentid, RadTreeNode parentNode, RadTreeView rtv)
        {
            var list = DepartmentsBLL.GetAll_SubLevel(parentid);
            PopulateNodes(list, parentNode.Nodes, rtv);
        }
        public static void GridFormatting(RadGridView rgv)
        {
            rgv.MasterTemplate.ShowFilterCellOperatorText = false;

            rgv.MasterTemplate.BestFitColumns(BestFitColumnMode.AllCells);
            rgv.MasterView.TableHeaderRow.MinHeight = 35;

            for (var i = 0; i < rgv.Rows.Count; i++)
            {
                rgv.Rows[i].MinHeight = 30;
                if (rgv.Rows[i].IsPinned)
                {
                    for (var j = 0; j < rgv.Columns.Count; j++)
                    {
                        rgv.Rows[i].Cells[j].Style.BackColor = Color.Transparent;
                        rgv.Rows[i].Cells[j].Style.BackColor2 = Color.Transparent;
                        rgv.Rows[i].Cells[j].Style.BackColor3 = Color.Transparent;
                        rgv.Rows[i].Cells[j].Style.BackColor4 = Color.Transparent;
                    }
                }
            }
            rgv.AutoSizeRows = true;
        }
        public static void GridFormattingPinColor(RadGridView rgv)
        {
            for (var i = 0; i < rgv.Rows.Count; i++)
            {
                if (rgv.Rows[i].IsPinned)
                {
                    for (var j = 0; j < rgv.Columns.Count; j++)
                    {
                        rgv.Rows[i].Cells[j].Style.BackColor = Color.Transparent;
                        rgv.Rows[i].Cells[j].Style.BackColor2 = Color.Transparent;
                        rgv.Rows[i].Cells[j].Style.BackColor3 = Color.Transparent;
                        rgv.Rows[i].Cells[j].Style.BackColor4 = Color.Transparent;
                    }
                }
            }
        }
        public static void GridTemplateFormatting(GridViewTemplate rvt)
        {
            rvt.MasterTemplate.BestFitColumns(BestFitColumnMode.AllCells);
            rvt.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;

            for (var i = 0; i < rvt.Rows.Count; i++)
                rvt.Rows[i].MinHeight = 30;
        }
        public static void SetScreenResolution(RadForm rf)
        {
        }
        public static void SetScreenColor(RadForm rf)
        {
            rf.MaximizeBox = false;
            rf.MinimizeBox = false;

            rf.StartPosition = FormStartPosition.CenterScreen;

            rf.ThemeName = string.Empty;
            rf.FormElement.Border.ForeColor = Color.FromArgb(255, 144, 0);
            rf.FormElement.Border.BackColor = Color.FromArgb(255, 144, 0);
            rf.FormElement.Border.InnerColor = Color.FromArgb(255, 144, 0);


            rf.Width = SystemInformation.VirtualScreen.Width - 100;
            rf.Height = SystemInformation.VirtualScreen.Height - 150;
        }
        public static void SetFormSize(RadForm rf)
        {
            const int minHeight = 768;
            const int minWidth = 1024;
            if ((Screen.PrimaryScreen.WorkingArea.Height == minHeight) ||
                (Screen.PrimaryScreen.WorkingArea.Width == minWidth))
            {
                rf.WindowState = FormWindowState.Maximized;

                rf.Width = minWidth;
                rf.Height = minHeight;
            }
            else
            {
                rf.WindowState = FormWindowState.Normal;

                rf.Width = minWidth + 100;
                rf.Height = Screen.PrimaryScreen.WorkingArea.Height - 50;
            }
        }
        public static void SetScreenResolutionForSmallDialog(RadForm rf)
        {
        }
        public static void ShowWaiting(RadWaitingBar rwb, Control ct)
        {
            rwb.BeginInit();
            rwb.WaitingStyle = WaitingBarStyles.DotsSpinner;
            rwb.AssociatedControl = ct;
            rwb.Size = new Size(100, 100);
            rwb.WaitingSpeed = 100;
            rwb.WaitingStep = 3;
            rwb.StartWaiting();
            rwb.EndInit();
        }
        public static void SetWaitingText(RadWaitingBar rwb, string text)
        {
            rwb.ShowText = true;
            rwb.Text = text;
        }
        public static void StopWaiting(RadWaitingBar rwb, Control ct)
        {
            rwb.StopWaiting();
            rwb.AssociatedControl = null;
        }
        public static void SaveLog()
        {
            var _HistoryId = 0;
            var _UserId = clsGlobal.UserId;
            var _FullName = clsGlobal.FullName;

            var _IPAddress =
                Dns.GetHostAddresses(Dns.GetHostName())
                    .First(a => a.AddressFamily == AddressFamily.InterNetwork)
                    .ToString();

            var _MACAddress = GetMACAddress();
            var _ServerName = clsGlobal.Server;
            var _ChangedDate = DateTime.Now;

            AdministrationBLL.Save(_HistoryId, _UserId, _FullName, _IPAddress, _MACAddress, _ServerName,
                InfluencedTable, StoredProcedures, ChangedContent, _ChangedDate, OldContent);
        }
        public static void SaveHRMLog(string InfluencedTable, string StoredProcedures, string ChangedContent,
            string OldContent)
        {
            var _HistoryId = 0;
            var _UserId = clsGlobal.UserId;
            var _FullName = clsGlobal.FullName;

            var _IPAddress =
                Dns.GetHostAddresses(Dns.GetHostName())
                    .First(a => a.AddressFamily == AddressFamily.InterNetwork)
                    .ToString();

            var _MACAddress = GetMACAddress();
            var _ServerName = clsGlobal.Server;
            var _InfluencedTable = InfluencedTable;
            var _StoredProcedures = StoredProcedures;
            var _ChangedContent = ChangedContent;
            var _OldContent = OldContent;
            var _ChangedDate = DateTime.Now;

            AdministrationBLL.Save(_HistoryId, _UserId, _FullName, _IPAddress, _MACAddress, _ServerName,
                _InfluencedTable, _StoredProcedures, _ChangedContent, _ChangedDate, _OldContent);
        }
        public static string GetMACAddress()
        {
            var nics = NetworkInterface.GetAllNetworkInterfaces();
            var sMacAddress = string.Empty;
            foreach (var adapter in nics)
                if (sMacAddress == string.Empty)
                {
                    var properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            return sMacAddress;
        }
        public static List<int> GetUserRights()
        {
            var _returnStr = new List<int>();
            if (UserRolesBLL.IsAdmin(clsGlobal.UserId) || UserRolesBLL.IsHRAdmin(clsGlobal.UserId))
            {
                _returnStr.Add(24061991);
            }
            else
            {
                var dt = UserRolesBLL.GetByUserId(clsGlobal.UserId);
                foreach (var item in dt)
                    _returnStr.Add(item.RoleId);
            }
            return _returnStr;
        }
        public static bool IsAdmin()
        {
            if (UserRolesBLL.IsAdmin(clsGlobal.UserId))
                return true;
            return false;
        }
        public static bool IsHRAdmin()
        {
            if (UserRolesBLL.IsHRAdmin(clsGlobal.UserId))
                return true;
            return false;
        }
        public static bool checkConnection(string connectionString)
        {
            SqlConnection conn = new SqlConnection(AES.Decrypt(connectionString));
            try
            {
                conn.Open();
                return true;
            }
            catch (Exception ex) { return false; }
        }
        public static List<string> GetCouldBeConnectedServerName(string userName, string password)
        {
            _listConnectedServer = new List<string>();
            foreach (ConnectionStringSettings css in ConfigurationManager.ConnectionStrings)
            {
                var name = css.Name;
                var connectionString = css.ConnectionString;
                var defaultGateway = NetworkInterfaces.GetDefaultInterface().GetIPProperties().GatewayAddresses.FirstOrDefault().Address.ToString();
                switch (defaultGateway)
                {
                    case "10.10.0.1":
                        {
                            if (!name.Contains("LocalSqlServer"))
                            {
                                HRMDAL.Utilities.HRMConfig.ConnectionString = connectionString;
                                if (checkConnection(connectionString) == true)
                                {
                                    DataRow dr = null;
                                    try
                                    {
                                        dr = HRMBLL.H0.EmployeesBLL.LoginNew(userName, password);
                                    }
                                    catch
                                    {
                                        dr = null;
                                    }
                                    if (dr != null)
                                    {
                                        _listConnectedServer.Add(name);
                                    }
                                }
                            }
                        }
                        break;
                    case "10.10.77.1":
                        {
                            if (!name.Contains("LocalSqlServer"))
                            {
                                HRMDAL.Utilities.HRMConfig.ConnectionString = connectionString;
                                if (checkConnection(connectionString) == true)
                                {
                                    DataRow dr = null;
                                    try
                                    {
                                        dr = HRMBLL.H0.EmployeesBLL.LoginNew(userName, password);
                                    }
                                    catch
                                    {
                                        dr = null;
                                    }
                                    if (dr != null)
                                    {
                                        _listConnectedServer.Add(name);
                                    }
                                }
                            }
                        }
                        break;
                    case "172.16.234.1":
                        {
                            if (!name.Contains("LocalSqlServer") && !name.Contains("Server_SAGS") && !name.Contains("Server_CXR"))
                            {
                                HRMDAL.Utilities.HRMConfig.ConnectionString = connectionString;
                                if (checkConnection(connectionString) == true)
                                {
                                    DataRow dr = null;
                                    try
                                    {
                                        dr = HRMBLL.H0.EmployeesBLL.LoginNew(userName, password);
                                    }
                                    catch
                                    {
                                        dr = null;
                                    }
                                    if (dr != null)
                                    {
                                        _listConnectedServer.Add(name);
                                    }
                                }
                            }
                        }
                        break;
                    case "172.16.112.1":
                        {
                            if (!name.Contains("LocalSqlServer") && !name.Contains("Server_SAGS") && !name.Contains("Server_DAD"))
                            {
                                HRMDAL.Utilities.HRMConfig.ConnectionString = connectionString;
                                if (checkConnection(connectionString) == true)
                                {
                                    DataRow dr = null;
                                    try
                                    {
                                        dr = HRMBLL.H0.EmployeesBLL.LoginNew(userName, password);
                                    }
                                    catch
                                    {
                                        dr = null;
                                    }
                                    if (dr != null)
                                    {
                                        _listConnectedServer.Add(name);
                                    }
                                }
                            }
                        }
                        break;
                }

            }
            return _listConnectedServer;
        }
    }

    public enum ExitCode
    {
        Success = 0,
        UnknownError = 1
    }

    public class TrackingUser
    {
        public TrackingUser()
        {
        }

        public TrackingUser(string cmdText, List<string> cmdParam, List<string> cmdParamValue)
        {
            CommandText = cmdText;
            CommandParameter = cmdParam;
            CommandValue = cmdParamValue;
        }

        public string CommandText { get; set; }

        public List<string> CommandValue { get; set; }

        public List<string> CommandParameter { get; set; }
    }

    public class ListColumn
    {
        public ListColumn()
        {
        }

        public ListColumn(string colName, string colNameInVNese)
        {
            ColName = colName;
            ColNameInVNese = colNameInVNese;
        }

        public string ColName { get; set; }
        public string ColNameInVNese { get; set; }
    }

    public class BitmapFilter
    {
        private static bool Conv3x3(Bitmap b, ConvMatrix m)
        {
            if (0 == m.Factor)
                return false;
            var bSrc = (Bitmap) b.Clone();


            var bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb);
            var bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height), ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb);

            var stride = bmData.Stride;
            var stride2 = stride*2;
            var Scan0 = bmData.Scan0;
            var SrcScan0 = bmSrc.Scan0;

            unsafe
            {
                var p = (byte*) (void*) Scan0;
                var pSrc = (byte*) (void*) SrcScan0;

                var nOffset = stride + 6 - b.Width*3;
                var nWidth = b.Width - 2;
                var nHeight = b.Height - 2;

                int nPixel;

                for (var y = 0; y < nHeight; ++y)
                {
                    for (var x = 0; x < nWidth; ++x)
                    {
                        nPixel = (pSrc[2]*m.TopLeft + pSrc[5]*m.TopMid + pSrc[8]*m.TopRight +
                                  pSrc[2 + stride]*m.MidLeft + pSrc[5 + stride]*m.Pixel + pSrc[8 + stride]*m.MidRight +
                                  pSrc[2 + stride2]*m.BottomLeft + pSrc[5 + stride2]*m.BottomMid +
                                  pSrc[8 + stride2]*m.BottomRight)/m.Factor + m.Offset;

                        if (nPixel < 0)
                            nPixel = 0;
                        if (nPixel > 255)
                            nPixel = 255;
                        p[5 + stride] = (byte) nPixel;

                        nPixel = (pSrc[1]*m.TopLeft + pSrc[4]*m.TopMid + pSrc[7]*m.TopRight +
                                  pSrc[1 + stride]*m.MidLeft + pSrc[4 + stride]*m.Pixel + pSrc[7 + stride]*m.MidRight +
                                  pSrc[1 + stride2]*m.BottomLeft + pSrc[4 + stride2]*m.BottomMid +
                                  pSrc[7 + stride2]*m.BottomRight)/m.Factor + m.Offset;

                        if (nPixel < 0)
                            nPixel = 0;
                        if (nPixel > 255)
                            nPixel = 255;
                        p[4 + stride] = (byte) nPixel;

                        nPixel = (pSrc[0]*m.TopLeft + pSrc[3]*m.TopMid + pSrc[6]*m.TopRight +
                                  pSrc[0 + stride]*m.MidLeft + pSrc[3 + stride]*m.Pixel + pSrc[6 + stride]*m.MidRight +
                                  pSrc[0 + stride2]*m.BottomLeft + pSrc[3 + stride2]*m.BottomMid +
                                  pSrc[6 + stride2]*m.BottomRight)/m.Factor + m.Offset;

                        if (nPixel < 0)
                            nPixel = 0;
                        if (nPixel > 255)
                            nPixel = 255;
                        p[3 + stride] = (byte) nPixel;

                        p += 3;
                        pSrc += 3;
                    }

                    p += nOffset;
                    pSrc += nOffset;
                }
            }

            b.UnlockBits(bmData);
            bSrc.UnlockBits(bmSrc);

            return true;
        }

        public static bool GaussianBlur(Bitmap b, int nWeight)
        {
            var m = new ConvMatrix();
            m.SetAll(1);
            m.Pixel = nWeight;
            m.TopMid = m.MidLeft = m.MidRight = m.BottomMid = 2;
            m.Factor = nWeight + 12;

            return Conv3x3(b, m);
        }

        public class ConvMatrix
        {
            public int BottomLeft, BottomMid, BottomRight;
            public int Factor = 1;
            public int MidLeft, Pixel = 1, MidRight;
            public int Offset = 0;
            public int TopLeft, TopMid, TopRight;

            public void SetAll(int nVal)
            {
                TopLeft = TopMid = TopRight = MidLeft = Pixel = MidRight = BottomLeft = BottomMid = BottomRight = nVal;
            }
        }
    }

    internal class Screenshot
    {
        public static Bitmap TakeSnapshot(Control ctl)
        {
            var bmp = new Bitmap(ctl.Size.Width, ctl.Size.Height);
            var g = Graphics.FromImage(bmp);
            g.CopyFromScreen(ctl.PointToScreen(ctl.ClientRectangle.Location), new Point(0, 0), ctl.ClientRectangle.Size);
            return bmp;
        }
    }
}
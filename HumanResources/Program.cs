using System;
using System.Deployment.Application;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;
using HumanResources.Progress;
using HumanResources.Properties;
using Microsoft.Win32;
using Telerik.WinControls;

namespace HumanResources
{
    internal static class Program
    {
        public static frm_SplashScreen1 splashForm = null;

        private static void SetAddRemoveProgramsIcon()
        {
            if (ApplicationDeployment.IsNetworkDeployed
                && ApplicationDeployment.CurrentDeployment.IsFirstRun)
            {
                Settings.Default.Reset();


                try
                {
                    var code = Assembly.GetExecutingAssembly();


                    var iconSourcePath = Path.Combine(Assembly.GetExecutingAssembly().Location, "icon-hrm2014.ico");

                    if (!File.Exists(iconSourcePath))
                        return;
                    var myUninstallKey =
                        Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall");
                    var mySubKeyNames = myUninstallKey.GetSubKeyNames();
                    for (var i = 0; i < mySubKeyNames.Length; i++)
                    {
                        var myKey = myUninstallKey.OpenSubKey(mySubKeyNames[i], true);
                        var myValue = myKey.GetValue("DisplayName");
                        if ((myValue != null) && (myValue.ToString() == "HumanResources"))
                        {
                            myKey.SetValue("DisplayIcon", iconSourcePath);
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            SetAddRemoveProgramsIcon();

            using (var mutex = new Mutex(false, "HumanResources"))
            {
                var isAnotherInstanceOpen = !mutex.WaitOne(TimeSpan.Zero, false);
                if (isAnotherInstanceOpen)
                    MessageBox.Show("Only one instance of this app is allowed.", "Warning", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                else
                    RunProgram();
            }
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetProcessDPIAware();

        private static void ShowSplashScreen()
        {
            SplashScreenManager.ShowForm(typeof(frm_SplashScreen1));

            Application.DoEvents();


            Thread.Sleep(500);

            try
            {
                if (Utilities.Utilities.GetAllActiveServer().Count > 0)
                {
                    Thread.Sleep(100);
                    if (Settings.Default.UserName == string.Empty)
                        try
                        {
                            var f_Login =
                                Activator.CreateInstance(
                                        Assembly.GetExecutingAssembly().GetType("HumanResources.Login.frm_Login"))
                                    as Form;
                            f_Login.ShowDialog();
                        }
                        catch
                        {
                        }
                    else
                        try
                        {
                            var f_RLogin =
                                Activator.CreateInstance(Assembly.GetExecutingAssembly()
                                    .GetType("HumanResources.Login.frm_RLogin")) as Form;
                            f_RLogin.ShowDialog();
                        }
                        catch
                        {
                        }
                }
                else
                {
                    Thread.Sleep(200);
                    MessageBox.Show("Kiểm tra lại kết nối", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Application.Exit();
                }
                if (clsGlobal.IsLoggedIn)
                    try
                    {
                        var f_Main =
                            Activator.CreateInstance(
                                Assembly.GetExecutingAssembly().GetType("HumanResources.RadRibbonForm")) as Form;
                        Application.Run(f_Main);
                    }
                    catch
                    {
                    }
            }
            catch
            {
            }
        }

        private static void RunProgram()
        {
            if (Environment.OSVersion.Version.Major >= 6)
                SetProcessDPIAware();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ThemeResolutionService.LoadPackageResource("HumanResources.Theme.Windows8.tssp");
            ThemeResolutionService.ApplicationThemeName = "Windows8";

            var rkey = Registry.CurrentUser.OpenSubKey(@"Control Panel\International", true);
            rkey.SetValue("sShortDate", "MM/dd/yyyy");
            rkey.SetValue("sLongDate", "MM/dd/yyyy");

            ShowSplashScreen();
        }
    }

    internal class EventSubscriber
    {
        private static readonly MethodInfo HandleMethod =
            typeof(EventSubscriber)
                .GetMethod("HandleEvent",
                    BindingFlags.Instance |
                    BindingFlags.NonPublic);

        private readonly EventInfo evt;

        private EventSubscriber(EventInfo evt)
        {
            this.evt = evt;
        }

        private void HandleEvent(object sender, EventArgs args)
        {
            Console.WriteLine("Event {0} fired", evt.Name);
        }

        private void Subscribe(object target)
        {
            var handler = Delegate.CreateDelegate(
                evt.EventHandlerType, this, HandleMethod);
            evt.AddEventHandler(target, handler);
        }

        public static void SubscribeAll(object target)
        {
            foreach (var evt in target.GetType().GetEvents())
            {
                var subscriber = new EventSubscriber(evt);
                subscriber.Subscribe(target);
            }
        }
    }
}
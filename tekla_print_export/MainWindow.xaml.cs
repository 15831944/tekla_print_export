using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

using Tekla.Structures;
using TSM = Tekla.Structures.Model;
using TSD = Tekla.Structures.Drawing;
using T3D = Tekla.Structures.Geometry3d;

namespace tekla_print_export
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow _form;
        UserSettings _settings;
        Thread _thread;
        private string fileName = "settings.txt";

        public MainWindow()
        {
            InitializeComponent();
            _form = this;

            txt_console.Clear();
            lbl_kill_status.Content = UserControls.Running.ToString();
            consoleOutput("[Program started]", "L0");
            _settings = new UserSettings(fileName);
        }
        
        private void btn_do_Click(object sender, RoutedEventArgs e)
        {
            checkThread(startProgram);
        }

        private void checkThread(Action action)
        {
            if (_thread == null || _thread.ThreadState == ThreadState.Stopped)
            {
                action();
            }
            else
            {
                consoleOutput("[PROGRAM IS RUNNING]", "L0");
            }
        }

        private void startProgram()
        {
            UserControls.setControls((bool)cb_prop.IsChecked, (bool)cb_cloud.IsChecked, (bool)cb_pdf.IsChecked, (bool)cb_dwg.IsChecked, (bool)cb_list.IsChecked);
            consoleOutput("\n[Start] " + DateTime.Now.ToString("h:mm:ss"), "L0");

            try
            {
                main();
            }
            catch
            {
                consoleOutput("[ERROR] - 1", "L0");
            }
        }

        private void main()
        {
            consoleOutput("Getting objects...", "L0");
            List<TSD.Drawing> objects = TeklaObjectGetter.getSelectedDrawings();

            consoleOutput("Working...", "L0");
            MainLoop program = new MainLoop(objects);
            _thread = new Thread( program.main );
            _thread.Start();
        }

        delegate void ParametrizedMethodInvoker(string arg, string arg2);
        internal void consoleOutput(string txt, string level)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(new ParametrizedMethodInvoker(consoleOutput), txt, level);
                return;
            }

            if (level == "L0")
            {
                txt_console.AppendText(txt);
            }
            else if (level == "L1")
            {
                txt_console.AppendText("-      " + txt);
            }
            else if (level == "L2")
            {
                txt_console.AppendText("      -      " + txt);
            }

            txt_console.AppendText("\n");
            txt_console.ScrollToEnd();
        }

        private void btn_kill_Click(object sender, RoutedEventArgs e)
        {
            UserControls.toggleOnOff();
            lbl_kill_status.Content = UserControls.Running.ToString();
        }

        private void btn_openSettingsFile_Click(object sender, RoutedEventArgs e)
        {
            checkThread(_settings.openSettingsFile);
        }

        private void btn_reloadSettingsFile_Click(object sender, RoutedEventArgs e)
        {
            checkThread(_settings.resetSettings);
        }

        private void btn_deleteSettingsFile_Click(object sender, RoutedEventArgs e)
        {
            checkThread(_settings.deleteSettingsFile);
        }

        private void btn_clearConsole_Click(object sender, RoutedEventArgs e)
        {
            txt_console.Clear();
        }
    }
}

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
        private delegate void Callback();
        public static MainWindow _form;
        UserSettings _settings;
        Thread _thread;
        private string fileName = "settings.txt";

        public MainWindow()
        {
            InitializeComponent();
            _form = this;

            lbl_kill_status.Content = UserControls.Running.ToString();
            consoleOutput("[Program started]");
            _settings = new UserSettings(fileName);
        }
        
        private void btn_do_Click(object sender, RoutedEventArgs e)
        {
            if (_thread == null || _thread.ThreadState == ThreadState.Stopped)
            {
                UserControls.setControls( (bool)cb_prop.IsChecked, (bool)cb_cloud.IsChecked, (bool)cb_pdf.IsChecked, (bool)cb_dwg.IsChecked, (bool)cb_list.IsChecked);
                consoleOutput("\n[Start] " + DateTime.Now.ToString("h:mm:ss"));

                try
                {
                    main();
                }
                catch
                {
                    consoleOutput("[ERROR 1]");
                }
            }
            else
            {
                consoleOutput("[PROGRAM IS ALOREADY RUNNING]");
            }
        }

        private void main()
        {
            consoleOutput("Getting objects...");
            List<TSD.Drawing> objects = TeklaObjectGetter.getAllDrawings();

            consoleOutput("Working...");
            MainLoop program = new MainLoop(objects);
            _thread = new Thread( program.main );
            _thread.Start();
        }

        delegate void ParametrizedMethodInvoker(string arg);
        internal void consoleOutput(string txt)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(new ParametrizedMethodInvoker(consoleOutput), txt);
                return;
            }

            txt_console.AppendText(txt);
            txt_console.AppendText("\n");

            Rect rect = txt_console.GetRectFromCharacterIndex(txt_console.CaretIndex);
            txt_console.ScrollToHorizontalOffset(Math.Max((txt_console.HorizontalOffset + rect.Left - (txt_console.ActualWidth - 40)), 0.0));
        }

        internal void consoleOutputL1(string txt)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(new ParametrizedMethodInvoker(consoleOutputL1), txt);
                return;
            }

            txt_console.AppendText("-      " + txt);
            txt_console.AppendText("\n");

            Rect rect = txt_console.GetRectFromCharacterIndex(txt_console.CaretIndex);
            txt_console.ScrollToHorizontalOffset(Math.Max((txt_console.HorizontalOffset + rect.Left - (txt_console.ActualWidth - 40)), 0.0));
        }

        internal void consoleOutputL2(string txt)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(new ParametrizedMethodInvoker(consoleOutputL2), txt);
                return;
            }

            txt_console.AppendText("      -      " + txt);
            txt_console.AppendText("\n");

            Rect rect = txt_console.GetRectFromCharacterIndex(txt_console.CaretIndex);
            txt_console.ScrollToHorizontalOffset(Math.Max((txt_console.HorizontalOffset + rect.Left - (txt_console.ActualWidth - 40)), 0.0));
        }


        

        private void btn_kill_Click(object sender, RoutedEventArgs e)
        {
            UserControls.toggleOnOff();
            lbl_kill_status.Content = UserControls.Running.ToString();
        }

        private void btn_openSettingsFile_Click(object sender, RoutedEventArgs e)
        {
            _settings.openSettingsFile();
        }

        private void btn_reloadSettingsFile_Click(object sender, RoutedEventArgs e)
        {
            _settings = new UserSettings(fileName);
        }

        private void btn_deleteSettingsFile_Click(object sender, RoutedEventArgs e)
        {
            _settings.deleteSettingsFile();
        }
    }
}

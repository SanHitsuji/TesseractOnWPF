using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using NUnit.Framework;
using RM.Friendly.WPFStandardControls;
using Codeer.Friendly.Windows.Grasp;

namespace TesseractOnWPF_Test
{
   public class MainWindowDriver
    {
        public IWPFDependencyObjectCollection<DependencyObject> LogicalTree { get; }



        public MainWindowDriver(dynamic window) {
            var w = new WindowControl(window);

            this.LogicalTree = w.LogicalTree();
            

        }
    }

    [TestFixture]
    class GUITest
    {
        private Process process;
        private WindowsAppFriend app;
        private MainWindowDriver driver;
        private string exePath;


        [SetUp]
        public void Initialize() {
            var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            dir = dir.Replace(@"TesseractOnWPF_Test\", @"TesseractOnWPF\");
            exePath = Path.Combine(dir, "TesseractOnWPF.exe");
         
            

        }

        public void StartExe(string exePath) {
            this.process = Process.Start(exePath);
            this.app = new WindowsAppFriend(this.process);
            this.driver = new MainWindowDriver(this.app.Type<Application>().Current.MainWindow);
        }

        [TearDown]
        public void Cleanup() {
            this.app.Dispose();
            this.process.Kill();
        }
        
        [Test]
        public void StartUpTesst() {
            StartExe(exePath);
        }
        


    }
}
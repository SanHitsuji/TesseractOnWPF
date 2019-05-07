using System.IO;
using Microsoft.Win32;
using TesseractOnWPF.Model;

namespace TesseractOnWPF.ViewModel
{
    public  class MainViewModel:ViewModelBase
    {
        
        private void OnSelectTargetPath(string initialDirectory=null) {            
            var ofd = new OpenFileDialog();
            if (!string.IsNullOrEmpty(initialDirectory)) {
                if (Directory.Exists(initialDirectory)) {
                    ofd.InitialDirectory = initialDirectory;                    
                }
            
            }
            if (ofd.ShowDialog() != true) {
                return;
            }
            DisplayedImagePath =ofd.FileName;
        }
        
        
        
        private string _displayedImagePath;
        private string _recognizedText;
        
        public MainViewModel()
        {
            FileSelectCommand = new DelegateCommand(() => { OnSelectTargetPath(); });
            DoTextRecognizingCommand = new DelegateCommand(() =>
            {
                RecognizedText = Model.CallTesseract.Recognizing(_displayedImagePath);
            });
        }

        public string DisplayedImagePath
        {
            get { return _displayedImagePath; }
            set
            {
                _displayedImagePath = value;
                OnPropertyChanged();
            }

        }



      
        public string RecognizedText
        {
            get => _recognizedText;
            set
            {
                _recognizedText = value;
                OnPropertyChanged();
            }
        }

        

        public DelegateCommand FileSelectCommand { get; set; }
        public DelegateCommand DoTextRecognizingCommand { get; set; }
        

    }

}
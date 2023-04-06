using Prism.Mvvm;

namespace PictureViewer.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "PictureViewer";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {

        }
    }
}

using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using ModuleA.Services;
using Prism.Mvvm;
using Prism.Regions;

namespace ModuleA.ViewModels
{
    public class ViewBViewModel : BindableBase, INavigationAware
    {
        DealNavigator _Deal_Navigator_frmDataMgr;
        private string _title = "ViewB";
        private int _pageViews;
        private bool _isBusy;

        public ViewBViewModel()
        {
            _Deal_Navigator_frmDataMgr = new DealNavigator();
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public int PageViews
        {
            get => _pageViews;
            set => SetProperty(ref _pageViews, value);
        }

        public ObservableCollection<string> MessageTexts => _Deal_Navigator_frmDataMgr.MessageTexts;

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        private async Task FillForm()
        {
            await _Deal_Navigator_frmDataMgr.StartFillAsyncTask();

            // We are NOT on the UI thread, so this has to be marshaled back
            Application.Current.Dispatcher?.Invoke(() =>
            {
                IsBusy = false;
            });
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            IsBusy = true;

            Task.Run(FillForm);

            PageViews++;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}

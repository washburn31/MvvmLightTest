using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using MvvmLightTest.Model;
using MvvmLightTest.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MvvmLightTest.ViewModel
{
    public class RssViewModel : ViewModelBase
    {
        private readonly IRssService rssService;

        private string name;

        public string Name
        {
            get { return this.name; }
            set
            {
                this.Set(ref this.name, value);
                this.sayHelloCommand.RaiseCanExecuteChanged();
            }
        }

        private string message;

        public string Message
        {
            get { return this.message; }
            set { this.Set(ref this.message, value); }
        }

        private ObservableCollection<FeedItem> _news;

        public ObservableCollection<FeedItem> News
        {
            get { return this._news; }
            set { this.Set(ref this._news, value); }
        }

        public RssViewModel(IRssService rssService)
        {
            this.rssService = rssService;
        }

        private RelayCommand sayHelloCommand;

        public RelayCommand SayHelloCommand
        {
            get
            {
                return this.sayHelloCommand ?? (this.sayHelloCommand = new RelayCommand(() =>
                {
                    this.Message = $"Hello {this.Name}";
                }, () => !string.IsNullOrEmpty(this.Name)));
            }
        }

        private RelayCommand loadCommand;

        public RelayCommand LoadCommand
        {
            get
            {
                return this.loadCommand ?? (this.loadCommand = new RelayCommand(async () =>
                {
                    this.News = new ObservableCollection<FeedItem>();
                    var feeds = new ObservableCollection<FeedItem>(await this.rssService.GetNewsAsync("http://wp.qmatteoq.com/rss"));
                    foreach (var feed in feeds)
                    {
                        this.News.Add(feed);
                        await Task.Delay(500);
                    }
                }));
            }
        }

    }
}

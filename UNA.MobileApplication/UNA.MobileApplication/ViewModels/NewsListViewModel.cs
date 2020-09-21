using Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace UNA.MobileApplication.ViewModels
{
    public class NewsListViewModel : BaseViewModel
    {
        private NEWS _selectedNews;

        public string CategoryId { get; set; }

        public NEWS SelectedNews
        {
            get => _selectedNews;
            set => SetProperty(ref _selectedNews, value);
        }

        private ObservableCollection<NEWS> _news;

        public ObservableCollection<NEWS> obsCollectionNews
        {
            get => _news;
            set => SetProperty(ref _news, value);
        }

        public Command LoadNewsCommand { get; set; }

        public NewsListViewModel(string categoryID)
        {
            obsCollectionNews = new ObservableCollection<NEWS>();
            LoadNewsCommand = new Command(async () => await RunSafe(ExecuteLoadItemsCommandAsync(categoryID), true));
        }

        private async Task ExecuteLoadItemsCommandAsync(string categoryID)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                _REQUEST.LANGUAGE = "1";
                _REQUEST.USER_TOKEN = "Aa159357";
                CATEGORY objCATEGORY = new CATEGORY();
                objCATEGORY.Category_ID = categoryID;
                _REQUEST.ROW_COUNT = "10";
                _REQUEST.JSON = JsonConvert.SerializeObject(objCATEGORY);
                var result = await ApiManager.GET_NEWS_BY_CATEGORY(_REQUEST);
                _RESPONSE = HelperManger.CastToResponse(result);
                if (string.IsNullOrEmpty(_RESPONSE[0].ERROR_MESSAGE))
                {
                    List<NEWS> lstNEWS = JsonConvert.DeserializeObject<List<NEWS>>(_RESPONSE[0].JSON);
                    obsCollectionNews = new ObservableCollection<NEWS>(lstNEWS);
                }
                foreach (NEWS vNEWS in obsCollectionNews)
                {
                    vNEWS.Details = HtmlToPlainText(vNEWS.Details);
                }
                NotifyPropertyChanged(nameof(obsCollectionNews));
            }
            catch (Exception ex)
            {
            }
            finally
            {
                IsBusy = false;
            }
        }

        private static string HtmlToPlainText(string html)
        {
            const string tagWhiteSpace = @"(>|$)(\W|\n|\r)+<";//matches one or more (white space or line breaks) between '>' and '<'
            const string stripFormatting = @"<[^>]*(>|$)";//match any character between '<' and '>', even when end tag is missing
            const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";//matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
            var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
            var stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);
            var tagWhiteSpaceRegex = new Regex(tagWhiteSpace, RegexOptions.Multiline);

            var text = html;
            text = System.Net.WebUtility.HtmlDecode(text);
            text = tagWhiteSpaceRegex.Replace(text, "><");
            text = lineBreakRegex.Replace(text, Environment.NewLine);
            text = stripFormattingRegex.Replace(text, string.Empty);

            //text = "<html>" +
            //                "<body style=\"text-align: justify;\">" +
            //                String.Format("<p>{0}</p>", text) +
            //                "</body>" +
            //                "</html>";

            return text;
        }
    }
}
using Model.Mobile;
using Refit;
using System.Threading.Tasks;

namespace Helper.Interface
{
    public interface IMobileApiManager
    {
        #region UNA APP

        [Post("/UNA/GET_LATEST_NEWS")]
        Task<string> GET_LATEST_NEWS([Body] REQUEST request);

        [Post("/UNA/GET_CATEGORY")]
        Task<string> GET_CATEGORY([Body] REQUEST request);

        [Post("/UNA/GET_NEWS_BY_CATEGORY")]
        Task<string> GET_NEWS_BY_CATEGORY([Body] REQUEST request);

        [Post("/UNA/GET_NEWS_BY_NATION")]
        Task<string> GET_NEWS_BY_NATION([Body] REQUEST request);

        [Post("/UNA/GET_NEWS_DETAIL")]
        Task<string> GET_NEWS_DETAIL([Body] REQUEST request);

        [Post("/UNA/GET_TOP_NEWS")]
        Task<string> GET_TOP_NEWS([Body] REQUEST request);

        [Post("/UNA/GET_REPORT")]
        Task<string> GET_REPORT([Body] REQUEST request);

        [Post("/UNA/GET_REPORT_DETAIL")]
        Task<string> GET_REPORT_DETAIL([Body] REQUEST request);

        [Post("/UNA/GET_PHOTO_ALBUM")]
        Task<string> GET_PHOTO_ALBUM([Body] REQUEST request);

        [Post("/UNA/GET_PHOTO_ALBUM_DETAIL")]
        Task<string> GET_PHOTO_ALBUM_DETAIL([Body] REQUEST request);

        [Post("/UNA/GET_VIDEO")]
        Task<string> GET_VIDEO([Body] REQUEST request);

        [Post("/UNA/GET_FAVOURITE")]
        Task<string> GET_FAVOURITE([Body] REQUEST request);

        [Post("/UNA/GET_NATION")]
        Task<string> GET_NATION([Body] REQUEST request);

        [Post("/UNA/GET_CONTACT")]
        Task<string> GET_CONTACT([Body] REQUEST request);

        [Post("/UNA/SET_FCM_TOKEN")]
        Task<string> SET_FCM_TOKEN([Body] REQUEST request);

        #endregion UNA APP
    }
}
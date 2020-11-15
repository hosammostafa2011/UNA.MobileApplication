using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace UNA.MobileApplication.Interface
{
    public interface ILocalizeService
    {
        bool IsRightToLeft { get; }

        CultureInfo GetCurrentCultureInfo();

        Task<CultureInfo> SetLocale();
    }
}

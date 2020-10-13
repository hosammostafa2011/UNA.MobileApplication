using System;
using System.Collections.Generic;
using System.Text;

namespace UNA.MobileApplication.Interface
{
    public interface INotification
    {
        void CreateNotification(String title, String message,string image,string id);
    }
}
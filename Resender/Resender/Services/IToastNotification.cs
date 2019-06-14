using System;
using System.Collections.Generic;
using System.Text;

namespace Resender.Services
{
    public interface IToastNotification
    {
        void SendLongTime(string message);
        void SendShortTime(string message);
    }
}

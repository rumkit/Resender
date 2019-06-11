using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Resender.Services
{
    public interface IContactManager
    {
        Task<string> ChoosePhoneNumber();
    }
}

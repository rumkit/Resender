using System.Threading.Tasks;

namespace Resender.Services
{
    public interface IMessageSender
    {
        Task<bool> TrySendMessageAsync(string phoneNumber, string message);
    }
}
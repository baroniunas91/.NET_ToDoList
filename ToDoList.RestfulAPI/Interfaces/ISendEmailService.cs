using System.Threading.Tasks;
using ToDoList.RestfulAPI.Dto;

namespace ToDoList.RestfulAPI.Interfaces
{
    public interface ISendEmailService
    {
        Task Send(ForgotPasswordEmailDto forgotPasswordEmail, string resetPasswordUrl);
    }
}

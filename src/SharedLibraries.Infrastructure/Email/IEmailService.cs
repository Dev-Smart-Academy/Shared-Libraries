namespace SharedLibraries.Infrastructure.Email;

using System.Collections.Generic;
using System.Threading.Tasks;

public interface IEmailService
{
    Task<bool> Send(string from, IList<string> tos, string title, string body);
}

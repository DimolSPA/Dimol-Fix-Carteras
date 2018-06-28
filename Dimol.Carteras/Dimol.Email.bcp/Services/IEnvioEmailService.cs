using Dimol.Email.dto.MailModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dimol.Email.bcp.Services
{
    public interface IEnvioEmailService
    {
        string Render<T>(int pclid, string filename, BaseEmailModel<T> Model);
        string Render(int pclid, string filename);
        Task<bool> Send<T>(EmailDto Model, BaseEmailModel<T> Data);
        List<string> ListTemplates();
        IEnumerable<EmailTemplate> ListarEmailTemplatesCliente(int pclid);
    }
}

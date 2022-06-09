using System.Threading;
using System.Threading.Tasks;
using DI.Forms.Core;
using DI.Forms.Requests;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DI.Services.Handlers
{
    public class FormListHandler : ServiceBase, IRequestHandler<FormsListRequest, FormsListResponse>
    {
        private readonly IFormProvider _provider;

        public FormListHandler(IFormProvider provider, ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
            _provider = provider;
        }

        public async Task<FormsListResponse> Handle(FormsListRequest request, CancellationToken cancellationToken)
        {
            await Task.Delay(0);
            return new FormsListResponse
            {
                Schemas = _provider.GetSchemas()
            };
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using Boards.Domain;
using DI.Reports;
using DI.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Boards.Services.Dashboard
{
    public class DashBoardDataHandler : ServiceBase, IRequestHandler<DashboardDataRequest, DashboardDataResponse>
    {
        private readonly IBoardsContext _boardsContext;

        public DashBoardDataHandler(ILoggerFactory logFactory, IBoardsContext boardsContext) : base(logFactory)
        {
            _boardsContext = boardsContext;
        }

        public async Task<DashboardDataResponse> Handle(DashboardDataRequest request,
            CancellationToken cancellationToken)
        {
            var rs = new DashboardDataResponse();
            var ls = await _boardsContext
                .GetDashboardItems($"[dbo].[GetDashBoardData1]");

            rs.Data = ls;
            return rs;
        }
    }
}
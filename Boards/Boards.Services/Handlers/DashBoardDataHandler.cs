using Boards.Domain;
using DI.Reports;
using DI.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Boards.Services.Handlers
{
    public class DashBoardDataHandler : ServiceBase, IRequestHandler<DashboardDataRequest, DashboardDataResponse>
    {
        private readonly IBoardsContext _boardsContext;
        public DashBoardDataHandler(ILoggerFactory logFactory, IBoardsContext boardsContext) : base(logFactory)
        {
            _boardsContext = boardsContext;
        }
        public  async Task<DashboardDataResponse> Handle(DashboardDataRequest request, CancellationToken cancellationToken)
        {
            var rs = new DashboardDataResponse();
            await Task.Delay(100);

            rs.Data.Add(new DashboardItem { Title = "test", Description = "dsfsdfsdf", Icon = "se", Value = "20", Result = "-30%", ResultColor = "red" });


            return rs;
        }
    }
}

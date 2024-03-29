﻿using Boards.Domain;
using Boards.Domain.Boards;
using Boards.Services.Core;
using Microsoft.Extensions.Logging;

namespace Boards.Services.Ministers.Forms
{
    public class FormsHandler : BoardsFormHandler<Minister>
    {
        public FormsHandler(ILoggerFactory logFactory, IBoardsContext context) : base(logFactory, context)
        {
        }

        public override string SchemaKey => "minister";
    }
}
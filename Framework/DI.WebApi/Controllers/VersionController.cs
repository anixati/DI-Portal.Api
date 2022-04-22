using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DI.WebApi.Controllers
{
    [AllowAnonymous]
    [ApiVersionNeutral]
    [Route("version")]
    public  class VersionController : ControllerBase
    {
        private readonly IEnumerable<IVersionInfo> _versions;

        public VersionController(IEnumerable<IVersionInfo> versionInfos)
        {
            _versions = versionInfos;
        }

        [HttpGet("")]
        public IEnumerable<IVersionInfo> Get()
        {
            return _versions;
        }
    }
}
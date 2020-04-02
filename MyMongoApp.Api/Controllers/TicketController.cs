using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using MyMongoApp.Model;
using MyMongoApp.BusinessLogic;

namespace MyMongoApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TicketController : ControllerBase
    {
        private ITicketManager TicketManager { get; set; }

        public TicketController(ITicketManager ticketManager)
        {
            this.TicketManager = ticketManager;
        }

        [HttpGet]
        public async Task<List<Ticket>> Sync()
        {
            return await this.TicketManager.Sync(new Board()
            {
                Id = "435C6402-18E7-431C-8D0A-E0350827ACFF",

                Url = "https://*****.atlassian.net/",

                UserName = "*****",
                Password = "*****"
            });
        }
    }
}

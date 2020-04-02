using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using MyMongoApp.Model;
using MyMongoApp.DataAccess;
using MyMongoApp.Tickets;

namespace MyMongoApp.BusinessLogic
{
	public class TicketManager : ITicketManager
	{
		#region Construction and Dependency Injection

		private ITicketSystemProxy TicketSystemProxy { get; set; }
		private ITicketRepository TicketRepository { get; set; }

		public TicketManager(ITicketSystemProxy ticketSystemProxy, ITicketRepository ticketRepository)
		{
			this.TicketSystemProxy = ticketSystemProxy;
			this.TicketRepository = ticketRepository;
		}

		#endregion

		public async Task<List<Ticket>> Sync(Board board)
		{
			var remoteTickets = await this.TicketSystemProxy.GetTickets(board);
			var localTickets = this.TicketRepository.GetAll(board);

			//remove deleted tickets
			for (var i = 0; i < localTickets.Count; i++)
			{
				var lt = localTickets[i];
				var rt = remoteTickets.SingleOrDefault(rec => rec.Code == lt.Code);

				if (rt == null)
					this.TicketRepository.Delete(lt);
			}

			//save/update tickets
			for (var i = 0; i < remoteTickets.Count; i++)
			{
				var rt = remoteTickets[i];
				var lt = localTickets.SingleOrDefault(rec => rec.Code == rt.Code);

				rt.BoardId = board.Id;
				rt.Id = lt?.Id;

				//create record locally if it doesn't yet exist
				// or update local record if ticket has changed
				if (lt == null || lt != rt)
					rt = this.TicketRepository.Save(rt);
			}

			return remoteTickets;
		}
	}
}

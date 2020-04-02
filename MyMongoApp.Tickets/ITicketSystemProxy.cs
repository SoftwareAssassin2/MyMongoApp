using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using MyMongoApp.Model;

namespace MyMongoApp.Tickets
{
	public interface ITicketSystemProxy
	{
		Task<List<Ticket>> GetTickets(Board board);
	}
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using MyMongoApp.Model;

namespace MyMongoApp.BusinessLogic
{
	public interface ITicketManager
	{
		Task<List<Ticket>> Sync(Board board);
	}
}

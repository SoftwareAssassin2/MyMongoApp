using System;
using System.Collections.Generic;

using MyMongoApp.Model;

namespace MyMongoApp.DataAccess
{
	public interface ITicketRepository
	{
		List<Ticket> GetAll(Board board);
		Ticket Save(Ticket input);
		void Delete(Ticket input);
	}
}

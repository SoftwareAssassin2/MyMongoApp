using System;
using Microsoft.Extensions.DependencyInjection;

using MyMongoApp.DataAccess;
using MyMongoApp.Tickets;
using MyMongoApp.Tickets.Jira;

namespace MyMongoApp.BusinessLogic
{
	public static class DependencyInjectionSettings
	{
		public static void Configure(IServiceCollection services)
		{
			services
				.AddTransient<ITicketSystemProxy, JiraProxy>()
				.AddTransient<ITicketRepository, TicketRepository>()
				;
		}
	}
}

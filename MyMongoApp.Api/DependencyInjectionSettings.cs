using System;
using Microsoft.Extensions.DependencyInjection;

using MyMongoApp.BusinessLogic;

namespace MyMongoApp.Api
{
	public static class DependencyInjectionSettings
	{
		public static void Configure(IServiceCollection services)
		{
			services
				.AddTransient<ITicketManager, TicketManager>()
				;

			BusinessLogic.DependencyInjectionSettings.Configure(services);
		}
	}
}

using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using j = Atlassian.Jira;

using MyMongoApp.Model;

namespace MyMongoApp.Tickets.Jira
{
	public class JiraProxy : ITicketSystemProxy
	{
		public async Task<List<Ticket>> GetTickets(Board board)
		{
			const int MAX_ISSUES_PER_PAGE = 100;

			//REST client
			var client = j.Jira.CreateRestClient(board.Url, board.UserName, board.Password);

			//get all issues
			var issues = new List<Ticket>();
			j.IPagedQueryResult<j.Issue> result = null;
			var startIndex = 0;
			while ((result = await client.Issues.GetIssuesFromJqlAsync("", startAt: startIndex, maxIssues: MAX_ISSUES_PER_PAGE)).Count() > 0)
			{
				issues.AddRange(result
					.Select(r =>
					{
						//generic fields
						var ret = new Ticket()
						{
							Code = r.Key.Value,
							Type = r.Type.ToString(),

							Title = r.Summary,
							Status = r.Status.ToString(),

							Assignee = r.AssigneeUser == null ? null : r.AssigneeUser.Username,

							IssueCreatedTimeStamp = r.Created.Value
						};

						//story points
						if (r.CustomFields["Story Points"] != null && r.CustomFields["Story Points"].Values.Length > 0)
							ret.StoryPoints = Convert.ToInt32(r.CustomFields["Story Points"].Values[0]);

						return ret;
					}))
					;

				startIndex += MAX_ISSUES_PER_PAGE;
			}

			//return as List<Ticket>
			return issues
				.ToList()
				;
		}
	}
}

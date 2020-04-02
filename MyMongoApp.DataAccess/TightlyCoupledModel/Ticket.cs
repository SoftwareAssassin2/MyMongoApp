using System;

using dto = MyMongoApp.Model;

namespace MyMongoApp.DataAccess.TightlyCoupledModel
{
	public class Ticket
	{
		public string _id { get; set; }
		public string boardId { get; set; }

		public string code { get; set; }
		public string type { get; set; }

		public string title { get; set; }
		public string status { get; set; }

		public string assignee { get; set; }

		public int? storyPoints { get; set; }

		public DateTime issueCreatedTimeStamp { get; set; }
		public DateTime lastUpdated { get; set; }


		public static explicit operator dto.Ticket(Ticket input)
		{
			return new dto.Ticket()
			{
				Id = input._id,
				BoardId = input.boardId,
				Type = input.type,
				Code = input.code,
				Title = input.title,
				Status = input.status,
				StoryPoints = input.storyPoints,
				Assignee = input.assignee,
				IssueCreatedTimeStamp = input.issueCreatedTimeStamp,
				LastUpdated = input.lastUpdated
			};
		}
		public static explicit operator Ticket(dto.Ticket input)
		{
			return new Ticket()
			{
				_id = input.Id,
				boardId = input.BoardId,
				type = input.Type,
				code = input.Code,
				title = input.Title,
				status = input.Status,
				storyPoints = input.StoryPoints,
				assignee = input.Assignee,
				issueCreatedTimeStamp = input.IssueCreatedTimeStamp,
				lastUpdated = input.LastUpdated
			};
		}
	}
}

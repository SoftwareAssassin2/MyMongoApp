using System;

namespace MyMongoApp.Model
{
	public class Ticket
	{
		public string Id { get; set; }
		public string BoardId { get; set; }

		public string Code { get; set; }
		public string Type { get; set; }

		public string Title { get; set; }
		public string Status { get; set; }

		public string Assignee { get; set; }

		public int? StoryPoints { get; set; }

		public DateTime IssueCreatedTimeStamp { get; set; }
		public DateTime LastUpdated { get; set; }
	}
}

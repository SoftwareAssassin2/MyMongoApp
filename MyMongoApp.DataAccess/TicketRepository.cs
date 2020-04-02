using System;
using System.Linq;
using System.Collections.Generic;

using MongoDB.Driver;

using MyMongoApp.Model;
using t = MyMongoApp.DataAccess.TightlyCoupledModel;

namespace MyMongoApp.DataAccess
{
	public class TicketRepository : MongoDbRepositoryBase, ITicketRepository
	{
		public void Delete(Ticket input)
		{
			var collection = this.GetCollection<t.Ticket>("Tickets");
			collection.DeleteOne(Builders<t.Ticket>.Filter.Eq(r => r._id, input.Id));
		}

		public List<Ticket> GetAll(Board board)
		{
			var collection = this.GetCollection<t.Ticket>("Tickets");

			//return all documents in collection as List<Board>
			return collection.Find(r => true)
				.ToList()
				.Select(r => (Ticket)r)
				.ToList()
				;
		}

		public Ticket Save(Ticket input)
		{
			var tickets = this.GetCollection<t.Ticket>("Tickets");
			var filter = Builders<t.Ticket>.Filter.Eq(r => r._id, input.Id);

			//check if the board already exists
			var isIdProvided = !string.IsNullOrEmpty(input.Id);
			var isNew = true;
			t.Ticket ticket = null;
			if (isIdProvided)
			{
				ticket = tickets.Find(filter).FirstOrDefault();
				isNew = ticket == null;
			}

			//generate id if needed
			if (!isIdProvided)
				input.Id = Guid.NewGuid().ToString();

			//set lastUpdated timestamp
			input.LastUpdated = DateTime.UtcNow;

			//create new record
			if (isNew)
				tickets.InsertOne((t.Ticket)input);

			//update existing record
			else
			{
				var update = Builders<t.Ticket>.Update.Set(r => r.lastUpdated, input.LastUpdated);

				if (input.BoardId != ticket.boardId)
					update = update.Set(r => r.boardId, input.BoardId);
				if (input.Code != ticket.code)
					update = update.Set(r => r.code, input.Code);
				if (input.Type != ticket.type)
					update = update.Set(r => r.type, input.Type);
				if (input.Title != ticket.title)
					update = update.Set(r => r.title, input.Title);
				if (input.Status != ticket.status)
					update = update.Set(r => r.status, input.Status);
				if (input.Assignee != ticket.assignee)
					update = update.Set(r => r.assignee, input.Assignee);
				if (input.StoryPoints != ticket.storyPoints)
					update = update.Set(r => r.storyPoints, input.StoryPoints);
				if (input.IssueCreatedTimeStamp != ticket.issueCreatedTimeStamp)
					update = update.Set(r => r.issueCreatedTimeStamp, input.IssueCreatedTimeStamp);

				tickets.UpdateOne(filter, update);
			}

			return input;
		}
	}
}

using System;

using MongoDB.Bson;
using MongoDB.Driver;

namespace MyMongoApp.DataAccess
{
	public class MongoDbRepositoryBase
	{
		protected IMongoCollection<T> GetCollection<T>(string collection)
		{
			var con = new MongoClient("mongodb://localhost:27017");
			var db = con.GetDatabase("MyMongoApp");
			return db.GetCollection<T>(collection);
		}
	}
}

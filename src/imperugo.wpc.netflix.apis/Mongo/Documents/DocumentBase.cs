﻿using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace imperugo.wpc.netflix.apis.Mongo.Documents
{
	public abstract class DocumentBase
	{
		protected DocumentBase()
		{
			this.CreateAt = DateTime.UtcNow;
		}

		public string Id { get; set; }
		
		public DateTime CreateAt { get; private set; }
	}
}
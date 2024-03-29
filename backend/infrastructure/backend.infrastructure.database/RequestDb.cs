﻿using backend.infrastructure.database.entities.Request;
using Microsoft.EntityFrameworkCore;

namespace backend.infrastructure.database
{
	public class RequestDb : DbContext
	{
		public RequestDb(DbContextOptions options) : base(options) { }
		public RequestDb() : base() { }
		public DbSet<Request> Requests { get; set; }
	}
}

using StackExchange.Redis;

namespace imperugo.wpc.netflix.apis.Configuration
{
	public class RedisConfiguration
	{
		public string Password { get; set; }
		public bool AllowAdmin { get; set; } = false;
		public bool Ssl { get; set; } = false;
		public int ConnectTimeout { get; set; } = 5000;
		public int Database { get; set; } = 0;
		public RedisHost[] Hosts { get; set; }

		private static ConnectionMultiplexer connection = null;
		private static ConfigurationOptions options = null;

		public ConfigurationOptions ConfigurationOptions
		{
			get
			{
				if (options == null)
				{
					options = new ConfigurationOptions
					{
						Ssl = this.Ssl,
						AllowAdmin = this.AllowAdmin,
						Password = this.Password,
						ConnectTimeout = this.ConnectTimeout,
					};

					foreach (RedisHost redisHost in this.Hosts)
					{
						options.EndPoints.Add(redisHost.Host, redisHost.Port);
					}
				}

				return options;
			}
		}

		public ConnectionMultiplexer Connection
		{
			get
			{
				if (connection == null)
				{
					connection = ConnectionMultiplexer.Connect(options);
				}

				return connection;
			}
		}
	}

	public class RedisHost
	{
		public string Host { get; set; } = "localhost";
		public int Port { get; set; } = 6379;
	}

}

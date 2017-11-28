using System.Net;
using System.Text;

namespace imperugo.wpc.netflix.apis.Configuration
{
	public class MongoDbDatabaseConfiguration
	{
		public string DatabaseName { get; set; }
		public string ServerHost { get; set; }
		public int Port { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }

		private string connectionString = null;

		public string ConnectionString
		{
			get
			{
				if (connectionString != null)
				{
					return connectionString;
				}

				var sb = new StringBuilder();
				sb.Append("mongodb://");

				if (!string.IsNullOrEmpty(this.Username))
				{
					sb.Append(this.Username)
						.Append(":")
						.Append(WebUtility.UrlEncode(this.Password))
						.Append("@");
				}

				sb.Append(this.ServerHost)
					.Append(":")
					.Append(this.Port)
					.Append("/")
					.Append(this.DatabaseName);

				connectionString = sb.ToString();

				return connectionString;
			}
		}
	}

}

using System.Text;

namespace imperugo.wpc.netflix.apis.Configuration
{
    public class UrlBuilderConfiguration
    {
		public WebUri Www { get; set; }
		public WebUri Admin { get; set; }
	    public WebUri Auth { get; set; }
	    public WebUri Api { get; set; }
	}

	public class WebUri
	{
		private string url;

		public int Port { get; set; }
		public string Host { get; set; }
		public string Protocol { get; set; }

		public string Url
		{
			get
			{
				if (url == null)
				{
					var serviceUrl = new StringBuilder(Protocol);

					serviceUrl.AppendFormat("://{1}:{0}", Port, Host);

					url = serviceUrl.ToString();
				}

				return url;
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace imperugo.wpc.netflix.apis.Mongo.Documents
{
	public class Movie : DocumentBase
	{
		public string Title { get; set; }
		public string Content_rating { get; set; }
		public string Original_title { get; set; }
		public Metadata Metadata { get; set; }
		public string Release_date { get; set; }
		public string Director { get; set; }
		public Url Url { get; set; }
		public string Year { get; set; }
		public Trailer[] Trailer { get; set; }
		public string Length { get; set; }
		public Cast[] Cast { get; set; }
		public string Imdb_id { get; set; }
		public string Rating { get; set; }
		public string[] Genre { get; set; }
		public string Rating_count { get; set; }
		public string Storyline { get; set; }
		public string Description { get; set; }
		public string[] Writers { get; set; }
		public string[] Stars { get; set; }
		public Poster Poster { get; set; }
	}

	public class Metadata
	{
		public string[] Languages { get; set; }
		public string Asp_retio { get; set; }
		public string[] Filming_locations { get; set; }
		public string[] Also_known_as { get; set; }
		public string[] Countries { get; set; }
		public string Gross { get; set; }
		public string[] Sound_mix { get; set; }
		public string Budget { get; set; }
	}

	public class Url
	{
		public string url { get; set; }
		public string Year { get; set; }
		public string Title { get; set; }
	}

	public class Poster
	{
		public string Large { get; set; }
		public string Thumb { get; set; }
	}

	public class Trailer
	{
		public string MimeType { get; set; }
		public string Definition { get; set; }
		public string VideoUrl { get; set; }
	}

	public class Cast
	{
		public string Character { get; set; }
		public string Image { get; set; }
		public string Link { get; set; }
		public string Name { get; set; }
	}

}
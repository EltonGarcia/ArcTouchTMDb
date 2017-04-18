﻿using System;
using System.Collections.Generic;

namespace ArcTouchTMDb.Core
{
	public class Movie
	{
		public string poster_path { get; set; }
		public bool adult { get; set; }
		public string overview { get; set; }
		public DateTime? release_date { get; set; }
		public List<int> genre_ids { get; set; }
		public int id { get; set; }
		public string original_title { get; set; }
		public string original_language { get; set; }
		public string title { get; set; }
		public string backdrop_path { get; set; }
		public double popularity { get; set; }
		public int vote_count { get; set; }
		public bool video { get; set; }
		public double vote_average { get; set; }

		public string year { get{ return release_date.HasValue ? release_date.Value.Year.ToString() : string.Empty; } }
		public string poster { get { return "https://image.tmdb.org/t/p/w300" + poster_path; } }
	}
}
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace training_net.Models.Views
{
    public class MoviesAndGenresViewModel
    {
        public PaginatedList<MovieViewModel> MovieList { get; set;}
        public SelectList GenreList { get; set;}
        public string CurrentSearch { get; set;}
        public string CurrentGenre { get; set;}
        public string CurrentSort { get; set;}
        public int CurrentPageSize { get; set;}
    }
}

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace training_net.Models.Views
{
    public class MoviesAndGenresViewModel
    {
        public List<MovieViewModel> MovieList { get; set;}
        public SelectList GenreList { get; set;}
        public string SearchString { get; set;}
        public string SelectedGenre { get; set;}
        public string SelectedSort { get; set;}
    }
}

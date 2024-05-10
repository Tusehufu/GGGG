using InternJohan.Dev.Infrastructure.Models;

namespace InternJohan.Dev.Infrastructure.ViewModel
{
    public class MovieListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }        
        public string DisplayRealeaseYear { get; set; }
        public decimal AverageRating { get; set; }

        public MovieListViewModel(Movie movie)
        {
            Id = movie.Id;
            Title = movie.Title;
            AverageRating = movie.AverageRating;
            DisplayRealeaseYear = movie.ReleaseYear.ToString("yyyy");
        }
    }
}
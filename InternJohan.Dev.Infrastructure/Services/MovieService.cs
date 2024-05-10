using InternJohan.Dev.Infrastructure.Configuration;
using InternJohan.Dev.Infrastructure.Models;
using Microsoft.Extensions.Options;
using Dapper;
using Microsoft.Data.SqlClient;
using InternJohan.Dev.Infrastructure.ViewModel;

namespace InternJohan.Dev.Infrastructure.Repository
{
    public class MovieService
    {
        private readonly MovieRepository _movieRepository;
        public MovieService(MovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<IEnumerable<MovieListViewModel>> GetAll()
        {
            var all = (await _movieRepository.FindAll());

            var vm = new List<MovieListViewModel>();

            foreach (var movie in all)
            {
                var vmItem = new MovieListViewModel(movie);
                
                vm.Add(vmItem);
            }

            return vm;
        }

        public async Task<int> Add(Movie movie)
        {
            await _movieRepository.Insert(movie);

            return movie.Id;
        }
        public async Task<bool> Remove(int id)
        {
            return await _movieRepository.Delete(id);
        }
        public async Task<MovieListViewModel> FindById(int id)
        {
            return new MovieListViewModel(await _movieRepository.FindById(id));
        }
        public async Task<bool> Update(Movie movie)
        {
        return await _movieRepository.Update(movie);
        }
    }
}

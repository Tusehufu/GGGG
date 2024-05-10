using InternJohan.Dev.Infrastructure.Configuration;
using InternJohan.Dev.Infrastructure.Models;
using Microsoft.Extensions.Options;
using Dapper;
using Microsoft.Data.SqlClient;
using InternJohan.Dev.Infrastructure.ViewModel;


namespace InternJohan.Dev.Infrastructure.Repository
{
    public class SportEventService
    {
        private readonly SportEventRepository _sportEventRepository;
        public SportEventService(SportEventRepository sportEventRepository)
        {
            _sportEventRepository = sportEventRepository;
        }

        public async Task<IEnumerable<SportEvent>> GetAll()
        {
            return await  _sportEventRepository.FindAll();

        }

        public async Task<int> Add(SportEvent sportevent)
        {
            await _sportEventRepository.Insert(sportevent);

            return sportevent.Id;
        }
        public async Task<bool> Remove(int id)
        {
            return await _sportEventRepository.Delete(id);
        }
        public async Task<SportEvent> FindById(int id)
        {
            return await _sportEventRepository.FindById(id);
        }
        public async Task<bool> Update(SportEvent sportevent)
        {
            return await _sportEventRepository.Update(sportevent);
        }
    }
}


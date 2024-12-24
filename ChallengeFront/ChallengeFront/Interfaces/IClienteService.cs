using ChallengeFront.Models;

namespace ChallengeFront.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteViewModel>> GetAll();
        Task<ClienteViewModel> Get(int Id);
        Task<IEnumerable<ClienteViewModel>> Search(string Name);
        Task Insert(ClienteViewModel cliente);
        Task Update(ClienteViewModel cliente);
    }
}

using RentResidence.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentResidence.Repository
{
    public interface IRentResidenceRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;

        Task<bool> SaveChangeAsync();

        #region Clientes

        Task<Client[]> GetAllClientsAsync();
        Task<Client[]> GetClientsByNameAsync(string nome);
        Task<Client> GetClientByCPFAsync(string CPF);
        Task<Client> GetClientLastAsync();
        //Task<Client> GetClientAmountAsync();
        Task<Client[]> GetClientOrderByNomeCompletoAsync();
      

        #endregion

        #region Moradias
        Task<Residence[]> GetAllResidenceAsync();
        Task<Residence[]> GetResidenceByCEPAsync(string CEP);
        Task<Residence> GetResidenceByIdAsync(int ID);
        Task<Residence[]> GetResidenceOrderByCidadeAsync();
     

        #endregion

    }
}

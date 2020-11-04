using Microsoft.EntityFrameworkCore;
using RentResidence.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentResidence.Repository
{
    public class RentResidenceRepository : IRentResidenceRepository
    {
        private readonly RentResidenceContext _context;

        
        public RentResidenceRepository(RentResidenceContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        #region Gerais
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public async Task<bool> SaveChangeAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
        #endregion


        #region Clients
        public async Task<Client[]> GetAllClientsAsync()
        {
            IQueryable<Client> query = _context.Clients;
            
            return await query.ToArrayAsync();
        }

        public async Task<Client> GetClientLastAsync()
        {
            IQueryable<Client> query = _context.Clients;

            query = query.OrderBy(x => x.ClientId);

            return await query.LastOrDefaultAsync();

        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            IQueryable<Client> query = _context.Clients;

            query = query.Where(x => x.ClientId == id);

            return await query.FirstOrDefaultAsync();
        }
        public async Task<Client[]> GetClientOrderByNomeCompletoAsync()
        {
            IQueryable<Client> query = _context.Clients;

            query = query.OrderBy(x => x.NomeCompleto);

            return await query.ToArrayAsync();

        }

        public async Task<Client> GetClientByCPFAsync(string cpf)
        {
            IQueryable<Client> query = _context.Clients;

            query = query.Where(x => x.CPF == cpf);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Client[]> GetClientsByNameAsync(string nome)
        {
            IQueryable<Client> query = _context.Clients;

            query = query.Where(x => x.NomeCompleto == nome);

            return await query.ToArrayAsync();
        }

        #endregion

        #region Residences
        public async Task<Residence[]> GetAllResidenceAsync()
        {
            IQueryable<Residence> query = _context.Residences
                .Include(c => c.Client);

            query = query.OrderBy(x => x.ResidenceId);

            return await query.ToArrayAsync();


        }

        public async Task<Residence[]> GetResidenceByCEPAsync(string cep)
        {
            IQueryable<Residence> query = _context.Residences
                .Include(c => c.Client);

            query = query.Where(x => x.CEP == cep);

            return await query.ToArrayAsync();
        }

        public async Task<Residence[]> GetResidenceOrderByCidadeAsync()
        {
            IQueryable<Residence> query = _context.Residences
                .Include(c => c.Client);

            query = query.OrderBy(x => x.Cidade);

            return await query.ToArrayAsync();
        }

        public async Task<Residence> GetResidenceByIdAsync(int id)
        {
            IQueryable<Residence> query = _context.Residences
                .Include(c => c.Client);

            query = query.Where(x => x.ResidenceId == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Residence> GetResidenceLastAsync()
        {
            IQueryable<Residence> query = _context.Residences
                .Include(c => c.Client);

            query = query.OrderBy(x => x.ResidenceId);

            return await query.LastOrDefaultAsync();

        }
        #endregion
    }
}

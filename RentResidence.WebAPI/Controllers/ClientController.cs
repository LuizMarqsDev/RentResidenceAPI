using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentResidence.Domain;
using RentResidence.Repository;
using RentResidence.WebAPI.DTO;
using RentResidence.WebAPI.Helpers;

namespace RentResidence.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {

        private readonly IRentResidenceRepository _repo;

        private readonly IMapper _mapper;
        public ClientController(IRentResidenceRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        #region CRUD

        /// <summary>
        /// Lista todos os clientes no Banco
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {

                var results = _mapper.Map<ClientDto[]>(await _repo.GetAllClientsAsync());
                return Ok(results);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ApiReturnMessages.DbFailed + ex.Message);
            }

        }

        /// <summary>
        /// Método que irá incrementar um registro na lista de clientes
        /// </summary>
        /// <param name="clientDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(ClientDto clientDto)
        {
            try
            {
                var client = _mapper.Map<Client>(clientDto);

                _repo.Add(client);

                if (await _repo.SaveChangeAsync())
                {
                    clientDto.ClientId = client.ClientId;
                    return Created($"/api/Client/{client.ClientId}", clientDto);
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ApiReturnMessages.DbFailed + ApiReturnMessages.CPFFailed + ex.Message);
            }

            return BadRequest();
        }

        /// <summary>
        /// Método que atualiza o registro desejado 
        /// </summary>
        /// <returns></returns>
        [HttpPut("{clientId}")]
        public async Task<IActionResult> Put(int clientId, ClientDto clientDto)
        {
            try
            {
                clientDto.ClientId = clientId;
                var upd = await _repo.GetClientByIdAsync(clientId);
                if (upd == null) return NotFound();

                _mapper.Map(clientDto, upd);

                _repo.Update(upd);

                if (await _repo.SaveChangeAsync())
                {
                    clientDto.ClientId = clientDto.ClientId;
                    return Created($"/api/Client/{clientDto.ClientId}", clientDto);
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ApiReturnMessages.DbFailed + ApiReturnMessages.CPFFailed  + ex.Message);
            }

            return BadRequest();
        }

        /// <summary>
        /// Método que atualiza o nome do usuário 
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="nomeCompleto"></param>
        /// <returns></returns>
        [HttpPatch("{clientId}")]
        public async Task<IActionResult> Patch(int clientId, string nomeCompleto)
        {
            try
            {
                var upd = await _repo.GetClientByIdAsync(clientId);
                if (upd == null) return NotFound();

                upd.NomeCompleto = nomeCompleto;


                _repo.Update(upd);

                var clientDto = _mapper.Map<ClientDto>(upd);

                if (await _repo.SaveChangeAsync())
                {
                    return Created($"/api/Client/{clientId}", clientDto);
                }
            }
            catch (System.Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ApiReturnMessages.DbFailed + e.Message);
            }

            return BadRequest();
        }


        /// <summary>
        ///  Método que deleta um registro 
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        [HttpDelete("{clientId}")]
        public async Task<IActionResult> Delete(int clientId)
        {
            try
            {
                var dlt = await _repo.GetClientByIdAsync(clientId);
                if (dlt == null) return NotFound();

                _repo.Delete(dlt);

                if (await _repo.SaveChangeAsync())
                {
                    return Ok();
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ApiReturnMessages.DbFailed);
            }

            return BadRequest();
        }

        #endregion

        #region Plus

        /// <summary>
        /// Método que filtra clientes pelo nome
        /// </summary>
        /// <param name="nomeCompleto"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/Clients/{nomeCompleto}")]
        public async Task<IActionResult> GetName(string nomeCompleto)
        {
            try
            {
                var results = _mapper.Map<ClientDto[]>(await _repo.GetClientsByNameAsync(nomeCompleto));

                return Ok(results);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ApiReturnMessages.DbFailed + ex.Message);
            }
        }

        /// <summary>
        /// Método que retorna o último item 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Last")]
        public async Task<IActionResult> GetLast()
        {

            try
            {
                var results = _mapper.Map<ClientDto>(await _repo.GetClientLastAsync());
                return Ok(results);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ApiReturnMessages.DbFailed + ex.Message);
            }
        }

        /// <summary>
        ///  Método que retorna a quantidade de Clientes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("amount")]
        public async Task<IActionResult> GetAmount()
        {

            try
            {
                var results = await _repo.GetClientAmountAsync();
                return Ok(results);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ApiReturnMessages.DbFailed + ex.Message);
            }
        }


        /// <summary>
        /// Método que ordena os clientes pelo nome
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Order")]
        public async Task<IActionResult> GetOrder()
        {
            try
            {
                var results = _mapper.Map<ClientDto[]>(await _repo.GetClientOrderByNomeCompletoAsync());
                return Ok(results);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ApiReturnMessages.DbFailed + ex.Message);
            }
        }

        /// <summary>
        /// Método que filtra os clientes pelo CPF
        /// </summary>
        /// <param name="clienteCPF"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{clienteCPF}")]
        public async Task<IActionResult> GetCPF(string clienteCPF)
        {
            try
            {
                var results = _mapper.Map<ClientDto>(await _repo.GetClientByCPFAsync(clienteCPF));
                return Ok(results);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ApiReturnMessages.DbFailed + ex.Message);
            }
        }
        #endregion

    }
}
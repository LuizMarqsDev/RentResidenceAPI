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

[Route("api/[controller]")]
[ApiController]
public class ResidenceController : ControllerBase
{

    private readonly IRentResidenceRepository _repo;
    private readonly IMapper _mapper;
    public ResidenceController(IRentResidenceRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    #region CRUD
    /// <summary>
    /// Lista todas as residências do banco
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var results = _mapper.Map<ResidenceDto[]>(await _repo.GetAllResidenceAsync());
            return Ok(results);
        }
        catch (System.Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, ApiReturnMessages.DbFailed + ex.Message);
        }

    }

    /// <summary>
    /// Método que irá incrementar um registro na lista de Residências
    /// </summary>
    /// <param name="residenceDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post(ResidenceDto residenceDto)
    {
        try
        {
            var residence = _mapper.Map<Residence>(residenceDto);

            _repo.Add(residence);

            if (await _repo.SaveChangeAsync())
            {
                residenceDto.ResidenceId = residence.ResidenceId;
                return Created($"/api/Residence/{residence.ResidenceId}", residenceDto);
            }
        }
        catch (System.Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, ApiReturnMessages.DbFailed + ex.Message);
        }

        return BadRequest();
    }

    /// <summary>
    /// Método que atualiza o registro desejado
    /// </summary>
    /// <returns></returns>
    [HttpPut("{residenceId}")]
    public async Task<IActionResult> Put(int residenceId,  ResidenceDto residenceDto)
    {
        try
        {
            residenceDto.ResidenceId = residenceId;

            var upd = await _repo.GetResidenceByIdAsync(residenceId);
            if (upd == null) return NotFound();

            _mapper.Map(residenceDto, upd);

            _repo.Update(upd);

            if (await _repo.SaveChangeAsync())
            {
                return Created($"/api/Residence/{residenceDto.ResidenceId}", residenceDto);
            }

        }
        catch (System.Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, ApiReturnMessages.DbFailed + ex.Message);
        }

        return BadRequest();
    }

    /// <summary>
    /// Método que atualiza o CEP 
    /// </summary>
    /// <param name="residenceId"></param>
    /// <param name="cep"></param>
    /// <returns></returns>
    [HttpPatch("{residenceId}")]
    public async Task<IActionResult> Patch(int residenceId, string cep)
    {
        try
        {
            var upd = await _repo.GetResidenceByIdAsync(residenceId);
            if (upd == null) return NotFound();

            upd.CEP = cep;

            _repo.Update(upd);

            var residenceDto = _mapper.Map<ResidenceDto>(upd);

            if (await _repo.SaveChangeAsync())
            {
                return Created($"/api/Residence/{residenceId}", residenceDto);
            }
        }
        catch (System.Exception)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, ApiReturnMessages.DbFailed);
        }

        return BadRequest();
    }

    /// <summary>
    ///  Método que deleta um registro
    /// </summary>
    /// <param name="residenceId"></param>
    /// <returns></returns>
    [HttpDelete("{residenceId}")]
    public async Task<IActionResult> Delete(int residenceId)
    {
        try
        {
            var dlt = await _repo.GetResidenceByIdAsync(residenceId);
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
    /// Método que filtra as residências pelo CEP
    /// </summary>
    /// <param name="cep"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("/{cep}")]
    
    public async Task<IActionResult> GetCEP(string cep)
    {
        try
        {
            var results = _mapper.Map<ResidenceDto[]>(await _repo.GetResidenceByCEPAsync(cep));
            return Ok(results);
        }
        catch (System.Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, ApiReturnMessages.DbFailed + ex.Message);
        }

    }

    /// <summary>
    /// Método que filtra residências pelo ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id}")]

    public async Task<IActionResult> GetId(int id)
    {
        try
        {
            var results = _mapper.Map<ResidenceDto>(await _repo.GetResidenceByIdAsync(id));
            return Ok(results);
        }
        catch (System.Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, ApiReturnMessages.DbFailed + ex.Message);
        }

    }

    /// <summary>
    /// Método que ordena as residências pela Cidade
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("order")]

    public async Task<IActionResult> GetOrder()
    {
        try
        {
            var results = _mapper.Map<ResidenceDto[]>(await _repo.GetResidenceOrderByCidadeAsync());
            return Ok(results);
        }
        catch (System.Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, ApiReturnMessages.DbFailed + ex.Message);
        }

    }

    /// <summary>
    /// Método que retorna o último elemento da lista
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("Last")]
    public async Task<IActionResult> GetLast()
    {
        try
        {
            var results = _mapper.Map<ResidenceDto>(await _repo.GetResidenceLastAsync());
            return Ok(results);
        }
        catch (System.Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, ApiReturnMessages.DbFailed + ex.Message);
        }
    }

    /// <summary>
    ///  Método que retorna a quantidade de Residências
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("amount")]
    public async Task<IActionResult> GetAmount()
    {

        try
        {
            var results = await _repo.GetResidenceAmountAsync();
            return Ok(results);
        }
        catch (System.Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, ApiReturnMessages.DbFailed + ex.Message);
        }
    }

    #endregion

}
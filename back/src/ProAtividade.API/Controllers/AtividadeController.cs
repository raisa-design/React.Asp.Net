using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProAtividade.Data.Context;
using ProAtividade.Domain.Entities;
using ProAtividade.Domain.Interfaces.Services;

namespace ProAtividade.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class AtividadeController : ControllerBase
    {
        private readonly IAtividadeService _atividadeService;
        
        public AtividadeController(IAtividadeService atividadeService)
        {
            _atividadeService = atividadeService;
           
        }

        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
           try
            {
                var atividades = await _atividadeService.PegarTodasAtividadesAsync();
                if (atividades == null) return NoContent();

                return Ok(atividades);
            }
            catch (System.Exception ex)// ex -> Exceção
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar Atividades. Erro: {ex.Message}");
                

            }
                       
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> Get (int id)
        {
            try
            {
                var atividade = await _atividadeService.PegarAtividadePorIdAsync(id);
                if (atividade == null) return NoContent();

                return Ok(atividade);
            }
            catch (System.Exception ex)// ex -> Exceção
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar Atividades com id: ${id}. Erro: {ex.Message}");
                

            }
        }

        //  [HttpGet("{id}")]
        // public AtividadeVm Get(int id)
        // {
        //     var atividade = _context.Atividades.FirstOrDefault(ati => ati.Id == id);
        //     var Vm = new AtividadeVm()
        //     {
        //         Titulo = atividade.Titulo,
        //         Descricao = atividade.Descricao,
        //         Prioridade = atividade.Prioridade.ToString()
        //     };

        //     return  Vm;
        // }

         [HttpPost]
            public async Task<IActionResult> Post(Atividade model)
        {
           try
            {
                var atividade = await _atividadeService.AdicionarAtividade(model);
                if (atividade == null) return NoContent();

                return Ok(atividade);
            }
            catch (System.Exception ex)// ex -> Exceção
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao  Adicionar Atividades. Erro: {ex.Message}");
                

            }
        }
        // public Atividade Post(Atividade atividade)
        // {
        //     _context.Atividades.Add(atividade);
        //     if (_context.SaveChanges()>0)
        //         return _context.Atividades.FirstOrDefault(ati => ati.Id == atividade.Id);
        //     else
        //         throw new Exception("Você não conseguiu adicionar uma atividade");
        // }

        // public ActionResult<Atividade> Post(string t, string d, int p )
        // {
        //     var atividade = new Atividade();
        //     atividade.Titulo = t;
        //     atividade.Descricao = d;
        //     atividade.Prioridade = (Prioridade)p;
        //    _context.Atividades.Add(atividade);
        //    _context.SaveChangesAsync();
        //    return Ok(atividade);    
        // }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Atividade model)
        {
           try
            {
                if(model.Id != id)
                    this.StatusCode(StatusCodes.Status409Conflict,
                        "Você está tentando atualizar a atividade errada");

                var atividade = await _atividadeService.AtualizarAtividade(model);
                if (atividade == null) return NoContent();

                return Ok(atividade);
            }
            catch (System.Exception ex)// ex -> Exceção
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao  Atualizar Atividade com id: ${id}. Erro: {ex.Message}");
                

            }
        }

        // [HttpPut("{id}")]
        // public ActionResult<Atividade> Put(Atividade model)
        // {
        //     var atividade = _context.Atividades.FirstOrDefault(ati => ati.Id == model.Id);
            
        //     atividade.Titulo = model.Titulo;
        //     atividade.Descricao = model.Descricao;
        //     atividade.Prioridade = model.Prioridade;
        //     _context.Atividades.Update(atividade);
        //     _context.SaveChangesAsync();
        //     return Ok(atividade);
        // }

        // Ou assim:
       // public Atividade Put(int id, Atividade atividade)
    //    {
        
    //      if (atividade.Id != id) throw new Exception("Você está tentando atualizar a atividade errada")
    //         _context.Update(atividade);
    //         if(_context.SaveChanges()>0)
    //             return _context.Atividades.FirstOrDefault(ativ => ati.Id == id);
    //         else
    //             return new Atividade();
    //    }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
           try
            {
                var atividade = await _atividadeService.PegarAtividadePorIdAsync(id);
                if (atividade == null) 
                    this.StatusCode(StatusCodes.Status409Conflict,
                        "Você está tentando deletar a atividade que não existe"
                    );

                if(await _atividadeService.DeletarAtividade(id))
                {
                    return Ok(new {message = "Deletado"});
                }
                else
                {
                    return BadRequest("Ocorreu um problema não específico ao tentar deletar atividade");
                }

            }
            catch (System.Exception ex)// ex -> Exceção
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar deletar Atividade com id: ${id}. Erro: {ex.Message}");
                

            }
        }

        // [HttpDelete("{id}")]
        // public bool Delete(int id)
        // {
        //     var atividade = _context.Atividades.FirstOrDefault(ati => ati.Id == id) ;
        //     if(atividade == null)
        //         throw new Exception("Você está tentando deletar uma atividade que não existe");

        //     _context.Remove(atividade);
            

        //     return _context.SaveChanges() > 0;
        // }
    }
}
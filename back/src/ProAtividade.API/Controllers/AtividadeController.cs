using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProAtividade.API.Data;
using ProAtividade.API.Models;
using ProAtividade.API.ViewModels;

namespace ProAtividade.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class AtividadeController : ControllerBase
    {
        private readonly DataContext _context;
        public AtividadeController(DataContext context){
            _context = context;
            
        }

        [HttpGet]

        public IEnumerable<Atividade> GetAll()
        {
           return _context.Atividades;
                       
        }

         [HttpGet("{id}")]
        public AtividadeVm Get(int id)
        {
            var atividade = _context.Atividades.FirstOrDefault(ati => ati.Id == id);
            var Vm = new AtividadeVm()
            {
                Titulo = atividade.Titulo,
                Descricao = atividade.Descricao,
                Prioridade = atividade.Prioridade.ToString()
            };

            return  Vm;
        }

         [HttpPost]
        public ActionResult<Atividade> Post(string t, string d, int p )
        {
            var atividade = new Atividade();
            atividade.Titulo = t;
            atividade.Descricao = d;
            atividade.Prioridade = (Prioridade)p;
           _context.Atividades.Add(atividade);
           _context.SaveChangesAsync();
           return Ok(atividade);    
        }

        [HttpPut]
        public ActionResult<Atividade> Put(Atividade model)
        {
            var atividade = _context.Atividades.FirstOrDefault(ati => ati.Id == model.Id);
            
            atividade.Titulo = model.Titulo;
            atividade.Descricao = model.Descricao;
            atividade.Prioridade = model.Prioridade;
            _context.Atividades.Update(atividade);
            _context.SaveChangesAsync();
            return Ok(atividade);
        }

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



        [HttpDelete]
        public bool Delete(int id)
        {
            var atividade = _context.Atividades.FirstOrDefault(ati => ati.Id == id) ;
            if(atividade == null)
                throw new Exception("Você está tentando deletar uma atividade que não existe");

            _context.Remove(atividade);

            return _context.SaveChanges() > 0;
        }
    }
}
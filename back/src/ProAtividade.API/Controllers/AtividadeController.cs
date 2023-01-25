using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProAtividade.API.Models;

namespace ProAtividade.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class AtividadeController : ControllerBase
    {
        public IEnumerable<Atividade> Atividades = new List<Atividade>(){
            new Atividade(1),
            new Atividade(2),
            new Atividade(3),
        };

        [HttpGet]

        public IEnumerable<Atividade> Get()
        {
           return Atividades;
                       
        }

         [HttpGet("{id}")]
        public Atividade Get(int id)
        {
            return Atividades.FirstOrDefault(ati => ati.Id == id);    
        }

         [HttpPost]
        public IEnumerable<Atividade> Post(Atividade atividade)
        {
            return Atividades.Append<Atividade>(atividade);    
        }

         [HttpPut]
        public  Atividade Put (Atividade atividade)
        {   
            atividade.Id = atividade.Id + 1;
           return atividade;  
        }

         [HttpDelete]
        public string Delete(int id)
        {
            return "Meu primeiro método delete";    
        }
    }
}
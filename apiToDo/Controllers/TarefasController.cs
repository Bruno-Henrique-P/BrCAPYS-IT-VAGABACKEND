using apiToDo.DTO;
using apiToDo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace apiToDo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefasController : ControllerBase
    {  

        /// não consegui utilizar a autenticação
        ///[Authorize]
        [HttpPost("lstTarefas")]
        public ActionResult lstTarefas()
        {
            try
            {
                Tarefas tarefas = new Tarefas();

                return Ok(tarefas.getTarefaAll());
            }

            catch (Exception ex)
            {

                return StatusCode(400, new { msg = $"Ocorreu um erro em sua API {ex.Message}"});
            }
        }

        [HttpPost("InserirTarefas")]
        public ActionResult InserirTarefas([FromBody] TarefaDTO Request)
        {
            try
            {
                Tarefas tarefas = new Tarefas();
                
                /// tratamento para id de tarefa já existente
                if (tarefas.getTarefa(Request.ID_TAREFA) == null)
                {
                    tarefas.InserirTarefa(Request);
                    return Ok(tarefas.getTarefaAll());
                }
                /// explico erro no terminal 
                else
                {
                    throw new Exception($"Tarefa com Id = {Request.ID_TAREFA} já existe");
                }

            }

            catch (Exception ex)
            {
                return StatusCode(400, new { msg = $"Ocorreu um erro em sua API {ex.Message}" });
            }
        }

        [HttpGet("DeletarTarefa")]
        public ActionResult DeleteTask([FromQuery] int ID_TAREFA)
        {
            try
            {
                Tarefas tarefas = new Tarefas();
                if (tarefas.getTarefa(ID_TAREFA) != null)
                {
                    tarefas.DeletarTarefa(ID_TAREFA);
                    return Ok(tarefas.getTarefaAll());
                }
                /// explico erro no terminal 
                else
                {
                    throw new Exception($"Tarefa com Id = {ID_TAREFA} não existe");
                }

                tarefas.DeletarTarefa(ID_TAREFA);
                return Ok(200);
            }

            catch (Exception ex)
            {
                return StatusCode(400, new { msg = $"Ocorreu um erro em sua API {ex.Message}" });
            }
        }
        [HttpGet("PegarTarefa")]
        public ActionResult PegarTarefa([FromQuery] int ID_TAREFA)
        {
            try
            {
                Tarefas tarefas = new Tarefas();
                if (tarefas.getTarefa(ID_TAREFA) == null)
                {
                    tarefas.DeletarTarefa(ID_TAREFA);
                    throw new Exception($"Tarefa com Id = {ID_TAREFA} não existe");
                }
                return Ok(tarefas.getTarefa(ID_TAREFA));   
            }

            catch (Exception ex)
            {
                return StatusCode(400, new { msg = $"Ocorreu um erro em sua API {ex.Message}" });
            }
        }

        [HttpPost("AtualizarTarefa")]
        public ActionResult AtualizarTarefa([FromBody] TarefaDTO Request)
        {
            try
            {
                Tarefas tarefas = new Tarefas();
                ///if (tarefas.getTarefa(Request.ID_TAREFA) != null)
                //{
                

                tarefas.AtualizarTarefa(Request);
                return Ok(tarefas.getTarefa(Request.ID_TAREFA));
                //}
                throw new Exception($"Tarefa com Id = {Request.ID_TAREFA} não existe");
                
            }

            catch (Exception ex)
            {
                return StatusCode(400, new { msg = $"Ocorreu um erro em sua API {ex.Message}" });
            }
        }
    }
}

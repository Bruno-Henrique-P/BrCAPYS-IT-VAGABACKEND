using apiToDo.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace apiToDo.Models
{
    public class Tarefas
    {
        // criei a variavel private static tarefas onde as tarefas criadas no lstTarefas vao ser armazenadas e podem ser manipuladas por qualquer função da classe
        private static List<TarefaDTO> tarefas = new List<TarefaDTO>();
        // metodo para criar as terafas padrao
        public List<TarefaDTO> lstTarefas()
        {
            try
            {
                
                tarefas.Add(new TarefaDTO
                {
                    ID_TAREFA = 1,
                    DS_TAREFA = "Fazer Compras"
                });

                tarefas.Add(new TarefaDTO
                {
                    ID_TAREFA = 2,
                    DS_TAREFA = "Fazer Atividad Faculdade"
                });

                tarefas.Add(new TarefaDTO
                {
                    ID_TAREFA = 3,
                    DS_TAREFA = "Subir Projeto de Teste no GitHub"
                });
                // alterei a variavel pois estava retornando uma lista vazia agora retorna a lista atualizada
                return tarefas;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// meotodo criada para retorna a lista interia, subtitui em todos os metodos, já que toda vez que o lstTarefa rodava a lista resetava, agora é possivel manipular
        public List<TarefaDTO> getTarefaAll()
        {
            try
            {
                /// aqui faço um verificação para saber se os valores padrao já foram adicionados, se a lista estiver vazia ele cria os valores se conter algum item ele não cria nada 
                if (tarefas.Count == 0)
                {
                    lstTarefas();
                }
                /// retorna a lista de tarefas(variavel da classe)
                return tarefas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// metodo para retornar um tarefa especifica da lista, similar ao metodo DeletarTarefa porém na ultima linha ao inves de deltar o elemento ele retorna o mesmo
        public TarefaDTO getTarefa(int ID_TAREFA)
        {
            try
            {
                List<TarefaDTO> lstResponse = getTarefaAll();
                var Tarefa = lstResponse.FirstOrDefault(x => x.ID_TAREFA == ID_TAREFA);
                if (Tarefa == null)
                {
                    throw new Exception($"Tarefa com Id = {ID_TAREFA} já existe");
                }
                TarefaDTO Tarefa2 = lstResponse.Where(x => x.ID_TAREFA == Tarefa.ID_TAREFA).FirstOrDefault();
                return Tarefa2;
            }
            catch(Exception ex)
            { 
                throw ex; 
            }
        } 


        public void InserirTarefa(TarefaDTO Request)
        {
            try
            {
                List<TarefaDTO> lstResponse = getTarefaAll();
                lstResponse.Add(Request);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void DeletarTarefa(int ID_TAREFA)
        {
            try
            {
                List<TarefaDTO> lstResponse = getTarefaAll();
                var Tarefa = lstResponse.FirstOrDefault(x => x.ID_TAREFA == ID_TAREFA);
                if (Tarefa == null)
                {
                    throw new Exception($"Tarefa com Id = {ID_TAREFA} não existe");
                }
                TarefaDTO Tarefa2 = lstResponse.Where(x=> x.ID_TAREFA == Tarefa.ID_TAREFA).FirstOrDefault();
                lstResponse.Remove(Tarefa2);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}

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
        /// metodo para retornar um tarefa especifica da lista, similar ao metodo DeletarTarefa porém mais eficiente pois só percorre a lista uma vez 
        /// e na ultima linha ao inves de deltar o elemento ele retorna o mesmo
        public TarefaDTO getTarefa(int ID_TAREFA)
        {
            try
            {
                List<TarefaDTO> lstResponse = getTarefaAll();
                var Tarefa = lstResponse.FirstOrDefault(x => x.ID_TAREFA == ID_TAREFA);
                if (Tarefa == null)
                {
                    return null;
                }
                return Tarefa;
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
                /// uma list do tipo TarefaTDO está sendo criada o seu valor é igual ao getTarefaAll que retorna a lista de todos as tarefas
                List<TarefaDTO> lstResponse = getTarefaAll();
                /// a variavel tarefa é igual ao primeiro obj cujo o id for igual ao parametro que foi passado na requisição, no caso um numero int, o programa vai percorrer a lista 
                /// lstResponse até encontrar o id que seja valido, caso não encontre retorna Null
                var Tarefa = lstResponse.FirstOrDefault(x => x.ID_TAREFA == ID_TAREFA);
                /// adicionei essa linha de codigo, porque quando era pssado um numero invalido gerava um erro que não consegui tratar, aqui quando o numero é invalido ele joga uma 
                /// exceção dizendo que a tarefa não existe, fiz essa exceção no controler tambem para garantir uma boa funcionalidade
                if (Tarefa == null)
                {
                    throw new Exception($"Tarefa com Id = {ID_TAREFA} não existe");
                }
                /// o erro acontece aqui, porque o var tarefa quando é invalido o retorno é null e aqui o programa tenta acessar uma tarefa null, então ele joga um NullReferenceException
                /// aqui o programa faz basicamente a mesma coisa da linha de acima onde ele percorre a lista lstReponse em busca de uma tarefa com o id correspondente, percorrendo a lista novamente
                
                TarefaDTO Tarefa2 = lstResponse.Where(x=> x.ID_TAREFA == Tarefa.ID_TAREFA).FirstOrDefault();
                /// aqui o programa remove a tarefa que foi encontrada acima 
                lstResponse.Remove(Tarefa2);

                /// uma forma mais eficiente de escreve o codigo nesse metodo
                /// var tarefa = lstResponse.FirstOrDefault(x => x.ID_TAREFA == ID_TAREFA);
                /// return tarefa ?? throw new Exception($"Tarefa com ID {ID_TAREFA} não encontrada");
                /// aqui ele percorre a lista apenas um vez e caso não encontre joga a exceção
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TarefaDTO AtualizarTarefa(TarefaDTO Request)
        {
            /// atualizar lista pelo id, recebe o corpo 
            try
            {
                /// verifica se a requisição existe
                if (Request == null)
                {
                    throw new Exception($"Não foi passado nenhuma informação");
                }
                /// cria a tarefa baseado na primeiro obj com o id_tarefa igual o id da requisição
                var tarefa = tarefas.FirstOrDefault(t => t.ID_TAREFA == Request.ID_TAREFA);
                /// se encontra a tarefa edita a mesma de acordo com o corpo retorna a tarefa editada
                if (tarefa != null)
                {
                    tarefa.DS_TAREFA = Request.DS_TAREFA;
                    return tarefa;
                }
                /// se não joga uma exceção dizendo que não encontrou tarefa X
                else
                {
                    throw new Exception ($"Tarefa com ID {Request.ID_TAREFA} não encontrada");
                    
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

using EfCoreDev.Contexts;
using EfCoreDev.Domains;
using EfCoreDev.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfCoreDev.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly PedidoContext _ctx;
        public ProdutoRepository()
        {
            _ctx = new PedidoContext();
        }
        #region Leitura

        /// <summary>
        /// Lista todos os produtos cadastrados
        /// </summary>
        /// <returns>Retorna uma lista de Produtos</returns>
        public List<Produto> Listar()
        {
            try
            {
                return _ctx.Produtos.ToList();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Busca um produto pelo id
        /// </summary>
        /// <param name="id">Id do produto</param>
        /// <returns>Lista de Produtos</returns>
        public Produto BuscarPorId(Guid id)
        {
            try
            {
                //return _ctx.Produtos.FirstOrDefault( c => c.Id == id );
                return _ctx.Produtos.Find(id);


            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Busca produtos pelo nome
        /// </summary>
        /// <param name="nome">Nome do Produto</param>
        /// <returns>Lista de Produtos</returns>
        public List<Produto> BuscarPorNome(string nome)
        {
            try
            {
                return _ctx.Produtos.Where(c => c.Nome.Contains(nome)).ToList();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Gravação
        /// <summary>
        /// Edita um Produto ja existente
        /// </summary>
        /// <param name="produto">Onjeto Produto</param>
        public void Editar(Produto produto)
        {
            try
            {
                //Busca um produto pelo Id
                Produto produtoTemp = BuscarPorId(produto.Id);

                //Caso não encontre gera uma excessão
                if (produtoTemp == null)
                    throw new Exception("Produto não encontrado");

                //Caso exista altera suas propriedades
                produtoTemp.Nome = produto.Nome;
                produtoTemp.Preco = produto.Preco;

                //Altera produto no contexto
                _ctx.Produtos.Update(produtoTemp);
                //Salva o contexto
                _ctx.SaveChanges();
                


            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Adiciona um novo produto
        /// </summary>
        /// <param name="produto">Objeto do tipo Produto</param>
        public void Adicionar(Produto produto)
        {
            try
            {
                //Adiciona um objeto produto ao dbset do contexto
                _ctx.Produtos.Add(produto);
                #region Comentario
                //Outros modos de se adicionar um produto
                //_ctx.Set<Produto>().Add(produto);
                //_ctx.Entry(produto).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                #endregion

                //Salva as alterações
                _ctx.SaveChanges();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Remover um produto
        /// </summary>
        /// <param name="Id">Id do Produto</param>
        public void Remover(Guid id)
        {
            try
            {
                //Busca um produto pelo Id
                Produto produtoTemp = BuscarPorId(id);

                //Caso não encontre gera uma excessão
                if (produtoTemp == null)
                    throw new Exception("Produto não encontrado");

                //Remove um produto do DbSet
                _ctx.Produtos.Remove(produtoTemp);
                //Salva as alterações do contexto
                _ctx.SaveChanges();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        #endregion


    }
}

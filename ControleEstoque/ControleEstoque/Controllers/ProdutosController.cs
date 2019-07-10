using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ControleEstoque.Data;
using ControleEstoque.Models;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace ControleEstoque.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _he;
        public string diretorio;

        public ProdutosController(ApplicationDbContext context, IHostingEnvironment he)
        {
            _context = context;
            _he = he;
            diretorio = _he.WebRootPath + "\\img\\produtos\\";
        }

        // GET: Produtos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Produto.ToListAsync());
        }

        // GET: Produtos/Detalhes/5
        public async Task<IActionResult> Detalhes(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto
                .FirstOrDefaultAsync(m => m.IDProduto == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // GET: Produtos/Criar
        public IActionResult Criar()
        {
            return View();
        }

        // POST: Produtos/Criar
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more Detalhes see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar([Bind("IDProduto,Nome,Descricao,Quantidade,PrecoUnitario,Imagem,Categoria,DataCadastro")] Produto produto, IFormFile img)
        {
            produto.PrecoUnitario = produto.PrecoUnitario / 100;
            produto.Quantidade = 0;
            produto.DataCadastro = DateTime.Now;

            if (img != null && !string.IsNullOrEmpty(img.FileName) && img.FileName.Contains("."))
            {
                produto.Imagem = DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + Helper.SimplificarTexto(Helper.NomeSemExtensao(img.FileName)) + Helper.SomenteExtensao(img.FileName);

                try
                {
                    string fileName = Path.Combine(diretorio, produto.Imagem);

                    img.CopyTo(new FileStream(fileName, FileMode.Create));
                }
                catch(Exception e)
                {

                }
            }

            if (ModelState.IsValid)
            {
                _context.Add(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { @status = "cadastrado" });
            }
            return View(produto);
        }

        // GET: Produtos/Editar/5
        [Authorize]
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            //produto.PrecoUnitario = produto.PrecoUnitario * 100;

            return View(produto);
        }

        // POST: Produtos/Editar/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more Detalhes see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Editar(int id, [Bind("IDProduto,Nome,Descricao,Quantidade,PrecoUnitario,Imagem,Categoria,DataCadastro")] Produto produto, IFormFile img, string imagemAtual)
        {
            if (id != produto.IDProduto)
            {
                return NotFound();
            }

            produto.PrecoUnitario = produto.PrecoUnitario / 100;

            if (img != null && !string.IsNullOrEmpty(img.FileName) && img.FileName.Contains("."))
            {
                produto.Imagem = DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + Helper.SimplificarTexto(Helper.NomeSemExtensao(img.FileName)) + Helper.SomenteExtensao(img.FileName);

                try
                {
                    string fileName = Path.Combine(diretorio, produto.Imagem);

                    img.CopyTo(new FileStream(fileName, FileMode.Create));
                }
                catch (Exception e)
                {

                }
            }
            else
            {
                produto.Imagem = imagemAtual;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.IDProduto))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { @status = "alterado" });
            }
            return View(produto);
        }

        // GET: Produtos/Excluir/5
        [Authorize]
        public async Task<IActionResult> Excluir(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto
                .FirstOrDefaultAsync(m => m.IDProduto == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // POST: Produtos/Excluir/5
        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> ExcluirConfirmar(int id)
        {
            var produto = await _context.Produto.FindAsync(id);

            List<Movimentacao> movimentacoes = _context.Movimentacao.Where(x => x.IDProduto == produto.IDProduto).ToList();

            foreach (var mov in movimentacoes)
            {
                _context.Movimentacao.Remove(mov);
            }

            _context.Produto.Remove(produto);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { @status = "excluido" });
        }

        private bool ProdutoExists(int id)
        {
            return _context.Produto.Any(e => e.IDProduto == id);
        }
    }
}

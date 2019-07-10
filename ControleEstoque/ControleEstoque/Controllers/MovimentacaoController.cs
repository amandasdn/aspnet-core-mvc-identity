using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ControleEstoque.Data;
using ControleEstoque.Models;
using Microsoft.AspNetCore.Authorization;

namespace ControleEstoque.Controllers
{
    [Authorize]
    public class MovimentacaoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MovimentacaoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Movimentacao
        public async Task<IActionResult> Index()
        {
            return View(await _context.Movimentacao.ToListAsync());
        }

        // GET: Movimentacao/Detalhes/5
        public async Task<IActionResult> Detalhes(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimentacao = await _context.Movimentacao
                .FirstOrDefaultAsync(m => m.IDMovimentacao == id);

            ViewData["Produto"] = _context.Produto.Find(movimentacao.IDProduto);

            if (movimentacao.Tipo == 0)
            {
                ViewData["Fornecedor"] = _context.Fornecedor.Find(movimentacao.IDFornecedor);
            }
            else if (movimentacao.Tipo == 1)
            {
                ViewData["Cliente"] = _context.Cliente.Find(movimentacao.IDCliente);
            }

            if (movimentacao == null)
            {
                return NotFound();
            }

            return View(movimentacao);
        }

        // GET: Movimentacao/Criar
        public IActionResult Criar()
        {
            #region Preencher Selects
            ViewBag.Produtos = _context.Produto.ToList().OrderBy(x => x.Nome).Select(x => new SelectListItem()
            {
                Text = "[#" + x.IDProduto + "] " + x.Nome,
                Value = x.IDProduto.ToString()
            });

            ViewBag.Fornecedores = _context.Fornecedor.ToList().OrderBy(x => x.NomeFantasia).Select(x => new SelectListItem()
            {
                Text = x.NomeFantasia,
                Value = x.IDFornecedor.ToString()
            });

            ViewBag.Clientes = _context.Cliente.ToList().OrderBy(x => x.NomeFantasia).Select(x => new SelectListItem()
            {
                Text = x.NomeFantasia,
                Value = x.IDCliente.ToString()
            });
            #endregion

            return View();
        }

        // POST: Movimentacao/Criar
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more Detalhes see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar([Bind("IDMovimentacao,Tipo,Quantidade,PrecoTotal,DataCadastro,IDProduto,IDCliente,IDFornecedor")] Movimentacao movimentacao)
        {
            #region Preencher Selects
            ViewBag.Produtos = _context.Produto.ToList().OrderBy(x => x.Nome).Select(x => new SelectListItem()
            {
                Text = "[#" + x.IDProduto + "] " + x.Nome,
                Value = x.IDProduto.ToString()
            });

            ViewBag.Fornecedores = _context.Fornecedor.ToList().OrderBy(x => x.NomeFantasia).Select(x => new SelectListItem()
            {
                Text = x.NomeFantasia,
                Value = x.IDFornecedor.ToString()
            });

            ViewBag.Clientes = _context.Cliente.ToList().OrderBy(x => x.NomeFantasia).Select(x => new SelectListItem()
            {
                Text = x.NomeFantasia,
                Value = x.IDCliente.ToString()
            });
            #endregion

            Produto _produto = _context.Produto.Find(movimentacao.IDProduto);
            
            movimentacao.DataCadastro = DateTime.Now;

            movimentacao.PrecoTotal = _produto.PrecoUnitario * movimentacao.Quantidade;

            if (movimentacao.Tipo == 0)
            {
                movimentacao.IDCliente = 0;
                _produto.Quantidade += movimentacao.Quantidade;

                Fornecedor _forn = _context.Fornecedor.Find(movimentacao.IDFornecedor);

                float preco = (float) movimentacao.PrecoTotal;
                float desconto = _forn.Desconto;

                desconto = desconto / 100;

                desconto = desconto * (float)movimentacao.PrecoTotal;

                movimentacao.PrecoTotal = (decimal) (preco - desconto);
            }
            else if (movimentacao.Tipo == 1)
            {
                movimentacao.IDFornecedor = 0;

                if (_produto.Quantidade >= movimentacao.Quantidade)
                {
                    _produto.Quantidade -= movimentacao.Quantidade;
                }
                else
                {
                    if(_produto.Quantidade == 0)
                        TempData["ErroQuantidade"] = "O produto escolhido não possui unidades em estoque.";
                    else
                        TempData["ErroQuantidade"] = "O produto escolhido possui apenas " + _produto.Quantidade + " unidades em estoque.";

                    return View(movimentacao);
                }
            }

            // return View(movimentacao);

            if (ModelState.IsValid)
            {                
                _context.Produto.Update(_produto);

                _context.Add(movimentacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { @status = "cadastrada" });
            }
            return View(movimentacao);
        }

        // GET: Movimentacao/Editar/5
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimentacao = await _context.Movimentacao.FindAsync(id);
            if (movimentacao == null)
            {
                return NotFound();
            }
            return View(movimentacao);
        }

        // POST: Movimentacao/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("IDMovimentacao,Tipo,Quantidade,PrecoTotal,DataCadastro,IDProduto,IDCliente,IDFornecedor")] Movimentacao movimentacao)
        {
            if (id != movimentacao.IDMovimentacao)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movimentacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovimentacaoExists(movimentacao.IDMovimentacao))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movimentacao);
        }

        // GET: Movimentacao/Excluir/5
        public async Task<IActionResult> Excluir(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimentacao = await _context.Movimentacao
                .FirstOrDefaultAsync(m => m.IDMovimentacao == id);

            if (movimentacao.Tipo == 0)
            {
                ViewData["Fornecedor"] = _context.Fornecedor.Find(movimentacao.IDFornecedor);
            }
            else if (movimentacao.Tipo == 1)
            {
                ViewData["Cliente"] = _context.Cliente.Find(movimentacao.IDCliente);
            }

            if (movimentacao == null)
            {
                return NotFound();
            }

            return View(movimentacao);
        }

        // POST: Movimentacao/Excluir/5
        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExcluirConfirmacao(int id)
        {
            var movimentacao = await _context.Movimentacao.FindAsync(id);

            Produto produto = _context.Produto.Find(movimentacao.IDProduto);

            if (movimentacao.Tipo == 0)
            {
                produto.Quantidade -= movimentacao.Quantidade;
            }
            else if (movimentacao.Tipo == 1)
            {
                produto.Quantidade += movimentacao.Quantidade;
            }

            _context.Movimentacao.Remove(movimentacao);

            _context.Produto.Update(produto);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovimentacaoExists(int id)
        {
            return _context.Movimentacao.Any(e => e.IDMovimentacao == id);
        }
    }
}

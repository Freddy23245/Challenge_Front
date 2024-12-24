using ChallengeFront.Interfaces;
using ChallengeFront.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeFront.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IClienteService _clienteService;
        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }
        public async Task<IActionResult> Index()
        {
            var clientes = await _clienteService.GetAll();
            return View(clientes);
        }
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var cliente = await _clienteService.Get(id);
            return View(cliente); // Pasa el cliente específico a la vista
        }
        [HttpPost]
        public async Task<IActionResult> Search(string name)
        {
            var clientes = await _clienteService.Search(name);
            return View("Index", clientes); // Muestra los resultados de la búsqueda en la vista principal
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ClienteViewModel cliente)
        {
            if (ModelState.IsValid)
            {
                await _clienteService.Insert(cliente);
                return RedirectToAction("Index");
            }

            return View(cliente); 
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var cliente = await _clienteService.Get(id);
            return View(cliente); // Muestra el formulario con los datos del cliente
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ClienteViewModel cliente)
        {
            if (ModelState.IsValid)
            {
                await _clienteService.Update(cliente);
                return RedirectToAction("Index");
            }
             return View(cliente);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var cliente = await _clienteService.Get(id);
            return View(cliente); // Muestra el formulario con los datos del cliente
        }

    }
    }

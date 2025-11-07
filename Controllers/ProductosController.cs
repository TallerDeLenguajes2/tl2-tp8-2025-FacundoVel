using Microsoft.AspNetCore.Mvc;

public class ProductosController : Controller
{
    private readonly ProductoRepository productoRepository;

    public ProductosController()
    {
        productoRepository = new ProductoRepository();
    }

    [HttpGet]
    public IActionResult Index()
    {
        //Obtenemos todos los productos desde el repositorio
        List<Producto> productos = productoRepository.ListarProductos();
        return View(productos);
    }
}
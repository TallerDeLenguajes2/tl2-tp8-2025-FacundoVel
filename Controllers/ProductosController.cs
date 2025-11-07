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

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Producto nuevoProducto)
    {
        if (ModelState.IsValid)
        {
            productoRepository.CrearProducto(nuevoProducto);
            return RedirectToAction("Index");
        }
        return View(nuevoProducto);
    }

    //mostrar formulario con datos de producto
    [HttpGet]
    public IActionResult Edit(int id)
    {
        Producto producto = productoRepository.ObtenerPorId(id);
        if (producto == null)
        {
            return NotFound();
        }
        return View(producto);
    }

    //guardar cambios en db
    [HttpPost]
    public IActionResult Edit(int id, Producto productoActualizado)
    {
        if (ModelState.IsValid)
        {
            productoRepository.ModificarProducto(id, productoActualizado);
            return RedirectToAction("Index");
        }
        return View(productoActualizado);
    }
}
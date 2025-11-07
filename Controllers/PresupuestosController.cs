using Microsoft.AspNetCore.Mvc;

public class PresupuestosController : Controller
{
    private PresupuestoRepository presupuestoRepository;

    public PresupuestosController()
    {
        presupuestoRepository = new PresupuestoRepository();
    }

    [HttpGet]
    public IActionResult Index()
    {
        List<Presupuesto> presupuestos = presupuestoRepository.ListarPresupuestos();
        return View(presupuestos);
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        var presupuesto = presupuestoRepository.ObtenerPorId(id);
        if (presupuesto == null)
        {
            return NotFound($"No se encontró el presupuesto con ID {id}");
        }
        return View(presupuesto);
    }
}
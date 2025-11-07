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
}
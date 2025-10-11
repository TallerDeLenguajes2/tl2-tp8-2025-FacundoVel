using System;
public class Presupuesto
{
    private int idPresupuesto;
    private string nombreDestinatario;
    private DateTime fechaCreacion;
    private List<PresupuestoDetalle> detalle;


    public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value; }
    public string NombreDestinatario { get => nombreDestinatario; set => nombreDestinatario = value; }
    public DateTime FechaCreacion { get => fechaCreacion; set => fechaCreacion = value; }
    public List<PresupuestoDetalle> Detalle { get => detalle; set => detalle = value; }

    public Presupuesto(int idPresupuesto, string nombreDestinatario, DateTime fechaCreacion, List<PresupuestoDetalle> detalle)
    {
        this.IdPresupuesto = idPresupuesto;
        this.NombreDestinatario = nombreDestinatario;
        this.FechaCreacion = fechaCreacion;
        this.Detalle = detalle;
    }

    public void AgregarDetalle(PresupuestoDetalle item)
    {
        detalle.Add(item);
    }

    public double MontoPresupuesto()
    {
        double total = 0;
        foreach (var item in detalle)
        {
            total += item.Subtotal();
        }
        return total;
    }

    public int CantidadProductos()
    {
        int totalProductos = 0;
        foreach (var item in detalle)
        {
            totalProductos += item.Cantidad;
        }
        return totalProductos;
    }
}

public class PresupuestoDetalle
{
    private Producto producto;
    private int cantidad;

    public Producto Producto { get => producto; set => producto = value; }
    public int Cantidad { get => cantidad; set => cantidad = value; }

    public PresupuestoDetalle(Producto producto, int cantidad)
    {
        this.producto = producto;
        this.cantidad = cantidad;
    }
    public PresupuestoDetalle() { }
    
    public double Subtotal()
    {
        return producto.Precio * cantidad;
    }
}
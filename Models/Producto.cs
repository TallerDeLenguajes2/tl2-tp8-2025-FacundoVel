using System.Runtime.InteropServices;

public class Producto
{
    private int idProducto;
    private string descripcion;
    private int precio;

    
    public int IdProducto { get => idProducto; set => idProducto = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
    public int Precio { get => precio; set => precio = value; }

    public Producto(int idProducto, string descripcion, int precio)
    {
        this.idProducto = idProducto;
        this.descripcion = descripcion;
        this.precio = precio;
    }

    public Producto() {}
}
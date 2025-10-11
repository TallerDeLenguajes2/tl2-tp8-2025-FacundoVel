using System;
using Microsoft.Data.Sqlite;

public class ProductoRepository
{
    private string connectionString = "Data source=Tienda.db";

    //crear producto
    public void CrearProducto(Producto nuevo)
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            string query = $"INSERT INTO Productos (Descripcion, Precio) VALUES ('{nuevo.Descripcion}', '{nuevo.Precio}')";
            using (var cmd = new SqliteCommand(query, connection))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }

    //modificar producto
    public void ModificarProducto(int id, Producto actualizado)
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            string query = $"UPDATE Productos SET Descripcion = '{actualizado.Descripcion}', Precio = '{actualizado.Precio}' WHERE idProducto = '{id}'";
            using (var cmd = new SqliteCommand(query, connection))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }

    //listar Productos
    public List<Producto> ListarProductos()
    {
        var productos = new List<Producto>();
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Productos";
            using (var cmd = new SqliteCommand(query, connection))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var p = new Producto(
                        Convert.ToInt32(reader["idProducto"]),
                        reader["Descripcion"].ToString(),
                        Convert.ToInt32(reader["Precio"])
                    );
                    productos.Add(p);
                }
            }
        }
        return productos;
    }

    //obtener producto por ID
    public Producto ObtenerPorId(int id)
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            string query = $"SELECT * FORM Productos WHERE idProducto = {id}";
            using (var cmd = new SqliteCommand(query, connection))
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Producto(
                        Convert.ToInt32(reader["idProducto"]),
                        reader["Descripcion"].ToString(),
                        Convert.ToInt32(reader["Precio"])
                    );
                }
            }
        }
        return null;
    }

    //Eliminar un producto por ID
    public void EliminarProducto(int id)
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            string query = $"DELETE FROM Productos WHERE idProducto = {id}";
            using (var cmd = new SqliteCommand(query, connection))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }
}
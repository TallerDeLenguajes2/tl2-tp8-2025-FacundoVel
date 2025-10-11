using System;
using Microsoft.Data.Sqlite;

public class PresupuestoRepository
{
    private string connectionString = "Data Source=Tienda.db";

    //crear nuevo presupuesto
    public void CrearPresupuesto(Presupuesto nuevo)
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            string query = $"INSERT INTO Presupuestos (NombreDestinatario, FechaCreacion) VALUES ('{nuevo.NombreDestinatario}', '{nuevo.FechaCreacion:yyyy-MM-dd}')";
            using (var cmd = new SqliteCommand(query, connection))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }

    //listar presupuestos
    public List<Presupuesto> ListarPresupuestos()
    {
        var presupuestos = new List<Presupuesto>();
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Presupuestos";
            using (var cmd = new SqliteCommand(query, connection))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    presupuestos.Add(new Presupuesto(
                        Convert.ToInt32(reader["IdPresupuesto"]),
                        reader["NombreDestinatario"].ToString(),
                        Convert.ToDateTime(reader["FechaCreacion"]),
                        new List<PresupuestoDetalle>()
                    ));

                }
            }
        }
        return presupuestos;
    }

    //obtener un presupuesto por ID con sus detalles
    public Presupuesto ObtenerPorId(int id)
    {
        Presupuesto presupuesto = null;

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            string query = $"SELECT * FROM Presupuestos WHERE IdPresupuesto = {id}";
            using (var cmd = new SqliteCommand(query, connection))
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    presupuesto = new Presupuesto(
                        Convert.ToInt32(reader["IdPresupuesto"]),
                        reader["NombreDestinatario"].ToString(),
                        Convert.ToDateTime(reader["FechaCreacion"]),
                        new List<PresupuestoDetalle>()
                    );
                }
            }

            // Solo leo los detalles si el presupuesto existe
            if (presupuesto != null)
            {
                string queryDetalle = $"SELECT * FROM PresupuestosDetalle WHERE idPresupuesto = {id}";
                using (var cmdDetalle = new SqliteCommand(queryDetalle, connection))
                using (var readerDetalle = cmdDetalle.ExecuteReader())
                {
                    while (readerDetalle.Read())
                    {
                        presupuesto.Detalle.Add(new PresupuestoDetalle
                        {
                            Producto = new Producto
                            {
                                IdProducto = Convert.ToInt32(readerDetalle["idProducto"])
                            },
                            Cantidad = Convert.ToInt32(readerDetalle["Cantidad"])
                        });
                    }
                }
            }
        }

        return presupuesto;
    }
    public void AgregarProductoAPresupuesto(int idPresupuesto, int idProducto, int cantidad)
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            string query = $"INSERT INTO PresupuestosDetalle (idPresupuesto, idProducto, Cantidad) VALUES ({idPresupuesto}, {idProducto}, {cantidad});";
            using (var cmd = new SqliteCommand(query, connection))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }

     public void EliminarPresupuesto(int id)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string borrarDetalle = $"DELETE FROM PresupuestosDetalle WHERE idPresupuesto = {id};";
                string borrarPresupuesto = $"DELETE FROM Presupuestos WHERE IdPresupuesto = {id};";

                using (var cmd = new SqliteCommand(borrarDetalle, connection)) cmd.ExecuteNonQuery();
                using (var cmd = new SqliteCommand(borrarPresupuesto, connection)) cmd.ExecuteNonQuery();
            }
        }
}


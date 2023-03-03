using AgendaDapperProcedimientos.Models;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Data;
using System.Data.SqlClient;

namespace AgendaDapperProcedimientos.Repositorio
{
    public class RepositorioContrib : IRepositorio
    {
        private readonly IDbConnection _bd;

        public RepositorioContrib(IConfiguration configuration)
        {
            _bd = new SqlConnection(configuration.GetConnectionString("ConexionSQLLocalDB"));
        }

        public Cliente ActualizarCliente(Cliente cliente)
        {
            //var sql = "UPDATE Cliente SET Nombres=@Nombres, Apellidos=@Apellidos, Telefono=@Telefono, Email=@Email, Pais=@Pais "
            //    + "WHERE IdCliente=@IdCliente";
            //_bd.Execute(sql, cliente);
            //return cliente;

            //var parametros = new DynamicParameters();
            //parametros.Add("@ClienteId", cliente.IdCliente, DbType.Int32);
            //parametros.Add("@Nombres", cliente.Nombres);
            //parametros.Add("@Apellidos", cliente.Apellidos);
            //parametros.Add("@Telefono", cliente.Telefono);
            //parametros.Add("@Email", cliente.Email);
            //parametros.Add("@Pais", cliente.Pais);

            //this._bd.Execute("sp_ActualizarCliente", parametros, commandType: CommandType.StoredProcedure);            

            //return cliente;
            _bd.Update(cliente);
            return cliente;
        }

        public Cliente AgregarCliente(Cliente cliente)
        {
            ////Opción 1
            //var sql = "INSERT INTO Cliente(Nombres, Apellidos, Telefono, Email, Pais, FechaCreacion)VALUES(@Nombres, @Apellidos, @Telefono, @Email, @Pais, @FechaCreacion)"
            //+ " SELECT CAST(SCOPE_IDENTITY() as int);";
            //var id = _bd.Query<int>(sql, new
            //{
            //    cliente.Nombres,
            //    cliente.Apellidos,
            //    cliente.Telefono,
            //    cliente.Email,
            //    cliente.Pais,
            //    cliente.FechaCreacion
            //}).Single();
            //cliente.IdCliente = id;
            //return cliente;

            //Opción 2 más optimizada
            //var sql = "INSERT INTO Cliente(Nombres, Apellidos, Telefono, Email, Pais, FechaCreacion)VALUES(@Nombres, @Apellidos, @Telefono, @Email, @Pais, @FechaCreacion)"
            //+ " SELECT CAST(SCOPE_IDENTITY() as int);";
            //var id = _bd.Query<int>(sql, cliente).Single();
            //cliente.IdCliente = id;
            //return cliente;

            ////Con procedimiento almacenado
            //var parametros = new DynamicParameters();
            //parametros.Add("@ClienteId", 0, DbType.Int32, direction: ParameterDirection.Output);
            //parametros.Add("@Nombres", cliente.Nombres);
            //parametros.Add("@Apellidos", cliente.Apellidos);
            //parametros.Add("@Telefono", cliente.Telefono);
            //parametros.Add("@Email", cliente.Email);
            //parametros.Add("@Pais", cliente.Pais);

            //this._bd.Execute("sp_CrearCliente", parametros, commandType: CommandType.StoredProcedure);
            //cliente.IdCliente = parametros.Get<int>("ClienteId");

            //return cliente;

            //Usando Dapper Contrib
            var id = _bd.Insert(cliente);
            cliente.IdCliente = (int)id;
            return cliente;

        }

        public void BorrarCliente(int id)
        {
            //var sql = "DELETE FROM Cliente WHERE IdCliente=@IdCliente";
            //_bd.Execute("sp_BorrarCliente", new { ClienteId = id }, commandType: CommandType.StoredProcedure);
            _bd.Delete(new Cliente { IdCliente = id});
        }

        public Cliente GetCliente(int id)
        {
            //var sql = "SELECT * FROM Cliente WHERE IdCliente=@IdCliente";
            //return _bd.Query<Cliente>("sp_GetClienteId", new { ClienteId = id }, commandType: CommandType.StoredProcedure).SingleOrDefault();
            return _bd.Get<Cliente>(id);
        }

        public List<Cliente> GetClientes()
        {
            //var sql = "SELECT * FROM Cliente";
            //return _bd.Query<Cliente>("sp_GetClientes", commandType: CommandType.StoredProcedure).ToList();
            return _bd.GetAll<Cliente>().ToList();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Usuarios.Core
{
    public class UsuariosHelper : Data.Obj.DataObj
    {
        public List<Usuarios> obtenerUsuarios()
        {
            List<Usuarios> lstUsuarios = new List<Usuarios>();
            DataTable dtUsuarios = new DataTable();
            Command.CommandText = "select idusuario, usuario, nombre, activo, fecharegistro from usuarios";
            dtUsuarios = SelectData(Command);
            for (int i = 0; i < dtUsuarios.Rows.Count; i++)
            {
                Usuarios usuario = new Usuarios();
                usuario.idusuario = int.Parse(dtUsuarios.Rows[i]["idusuario"].ToString());
                usuario.usuario = dtUsuarios.Rows[i]["usuario"].ToString();
                usuario.nombre = dtUsuarios.Rows[i]["nombre"].ToString();
                usuario.activo = bool.Parse(dtUsuarios.Rows[i]["activo"].ToString());
                usuario.fecharegistro = DateTime.Parse(dtUsuarios.Rows[i]["fecharegistro"].ToString());
                lstUsuarios.Add(usuario);
            }
            return lstUsuarios;
        }


        public DataTable obtenerUsuario(int idusuario)
        {
            DataTable dtUsuario = new DataTable();
            Command.CommandText = "select idusuario, usuario, nombre, idperfil, empresas from usuarios where idusuario = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", idusuario);
            return dtUsuario = SelectData(Command);
        }

        public List<Usuarios> Usuario(int idusuario)
        {
            DataTable dtUsuario = new DataTable();
            Command.CommandText = "select idusuario, usuario, nombre, idperfil, empresas from usuarios where idusuario = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", idusuario);
            dtUsuario = SelectData(Command);
            List<Usuarios> lstUsuario = new List<Usuarios>();
            for (int i = 0; i < dtUsuario.Rows.Count; i++)
            {
                Usuarios user = new Usuarios();
                user.idusuario = int.Parse(dtUsuario.Rows[i]["idusuario"].ToString());
                user.usuario = dtUsuario.Rows[i]["usuario"].ToString();
                user.nombre = dtUsuario.Rows[i]["nombre"].ToString();
                user.idperfil = int.Parse(dtUsuario.Rows[i]["idperfil"].ToString());
                user.empresas = dtUsuario.Rows[i]["empresas"].ToString();
                lstUsuario.Add(user);
            }
            return lstUsuario;
        }

        public DataTable ValidaUsuario(Usuarios usr)
        {
            DataTable dtUsuario = new DataTable();
            Command.CommandText = "select idusuario, idperfil from usuarios where usuario = @usuario and password = @password and activo = 1";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("usuario", usr.usuario);
            Command.Parameters.AddWithValue("password", usr.password);
            dtUsuario = SelectData(Command);
            return dtUsuario;
        }


        public int insertaUsuario(Usuarios usr)
        {
            Command.CommandText = "insert into usuarios(usuario,nombre,password,activo,fecharegistro,idperfil,empresas) values (@usuario,@nombre,@password,@activo,@fecharegistro,@idperfil,@empresas)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("usuario",usr.usuario);
            Command.Parameters.AddWithValue("nombre", usr.nombre);
            Command.Parameters.AddWithValue("password", usr.password);
            Command.Parameters.AddWithValue("activo", usr.activo);
            Command.Parameters.AddWithValue("fecharegistro", usr.fecharegistro);
            Command.Parameters.AddWithValue("idperfil", usr.idperfil);
            Command.Parameters.AddWithValue("empresas", usr.empresas);
            return Command.ExecuteNonQuery();
        }

        public int modificaUsuario(Usuarios usr)
        {
            Command.CommandText = "update usuarios set usuario = @usuario, nombre = @nombre, activo = @activo, idperfil = @idperfil, empresas = @empresas where idusuario = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", usr.idusuario);
            Command.Parameters.AddWithValue("usuario", usr.usuario);
            Command.Parameters.AddWithValue("nombre", usr.nombre);
            Command.Parameters.AddWithValue("activo", usr.activo);
            Command.Parameters.AddWithValue("idperfil", usr.idperfil);
            Command.Parameters.AddWithValue("empresas", usr.empresas);
            return Command.ExecuteNonQuery();
        }

        public int bajaUsuario(Usuarios usr)
        {
            Command.CommandText = "update usuarios set activo = 0 where idusuario = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", usr.usuario);
            return Command.ExecuteNonQuery();
        }

        public int cambioPassword(Usuarios usr)
        {
            Command.CommandText = "update usuarios set password = @password where idusuario = @idusuario";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("password", usr.password);
            Command.Parameters.AddWithValue("idusuario", usr.idusuario);
            return Command.ExecuteNonQuery();
        }
    }
}

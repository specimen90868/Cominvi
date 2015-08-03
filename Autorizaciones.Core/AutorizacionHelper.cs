using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace Autorizaciones.Core
{
    public class AutorizacionHelper : Data.Obj.DataObj
    {
        public List<Autorizaciones> getAutorizacion(string idusuario)
        {
            List<Autorizaciones> auth = new List<Autorizaciones>();
            DataTable dtAuth = new DataTable();
            Command.CommandText = "call spAutorizacion (@idusuario)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idusuario", idusuario);
            dtAuth = SelectData(Command);
            for (int i = 0; i < dtAuth.Rows.Count; i++)
            {
                Autorizaciones a = new Autorizaciones();
                a.idusuario = int.Parse(dtAuth.Rows[i]["idusuario"].ToString());
                a.usuario = dtAuth.Rows[i]["usuario"].ToString();
                a.idperfil = int.Parse(dtAuth.Rows[i]["idperfil"].ToString());
                a.nombre = dtAuth.Rows[i]["nombre"].ToString();
                a.modulo = dtAuth.Rows[i]["modulo"].ToString();
                a.acceso = int.Parse(dtAuth.Rows[i]["acceso"].ToString());
                auth.Add(a);
            }
            return auth;
        }


        public List<Menus> getMenus(string idperfil)
        {
            List<Menus> menu = new List<Menus>();
            DataTable dtMenu = new DataTable();
            Command.CommandText = "select m.nombre, ver from menus m left join ediciones e on m.idmenu = e.idmenu where e.idperfil = @idperfil and m.tipomenu = 0;";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idperfil", idperfil);
            dtMenu = SelectData(Command);
            for (int i = 0; i < dtMenu.Rows.Count; i++)
            {
                Menus m = new Menus();
                m.nombre = dtMenu.Rows[i]["nombre"].ToString();
                m.ver = int.Parse(dtMenu.Rows[i]["ver"].ToString());
                menu.Add(m);
            }
            return menu;
        }

        public List<Ediciones> getEdiciones(string idperfil, string nombre)
        {
            List<Ediciones> edicion = new List<Ediciones>();
            DataTable dtEdicion = new DataTable();
            Command.CommandText = "select m.nombre, crear, consulta, modificar, baja from menus m left join ediciones e on m.idmenu = e.idmenu where e.idperfil = @idperfil and m.tipomenu = 0 and m.nombre = @nombre;";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idperfil", idperfil);
            Command.Parameters.AddWithValue("nombre", nombre);
            dtEdicion = SelectData(Command);
            for (int i = 0; i < dtEdicion.Rows.Count; i++)
            {
                Ediciones e = new Ediciones();
                e.nombre = dtEdicion.Rows[i]["nombre"].ToString();
                e.crear = int.Parse(dtEdicion.Rows[i]["crear"].ToString());
                e.consulta = int.Parse(dtEdicion.Rows[i]["consulta"].ToString());
                e.modificar = int.Parse(dtEdicion.Rows[i]["modificar"].ToString());
                e.baja = int.Parse(dtEdicion.Rows[i]["baja"].ToString());
                edicion.Add(e);
            }
            return edicion;
        }

        public int insertaAutorizacion(int perfil, List<Autorizacion> lista)
        {
            for (int i = 0; i < lista.Count; i++)
            {
                Command.CommandText = "insert into autorizaciones (idacceso, idperfil, acceso) values (@idacceso, @idperfil, @acceso)";
                Command.Parameters.Clear();
                Command.Parameters.AddWithValue("idacceso", lista[i].idacceso);
                Command.Parameters.AddWithValue("idperfil", perfil);
                Command.Parameters.AddWithValue("acceso", lista[i].acceso);
                Command.ExecuteNonQuery();
            }
            return 1;
        }
    }
}


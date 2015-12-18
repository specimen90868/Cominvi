using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Autorizaciones.Core
{
    public class AutorizacionHelper : Data.Obj.DataObj
    {
        public List<Autorizaciones> getAutorizacion(int idusuario)
        {
            List<Autorizaciones> auth = new List<Autorizaciones>();
            DataTable dtAuth = new DataTable();
            Command.CommandText = "exec stp_Autorizacion @idusuario";
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
                a.acceso = bool.Parse(dtAuth.Rows[i]["acceso"].ToString());
                auth.Add(a);
            }
            return auth;
        }

        public List<Autorizacion> obtenerModulos(int idperfil)
        {
            List<Autorizacion> auth = new List<Autorizacion>();
            DataTable dtAuth = new DataTable();
            Command.CommandText = "select idacceso, idperfil, acceso from Autorizaciones where idperfil = @idperfil";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idperfil", idperfil);
            dtAuth = SelectData(Command);
            for (int i = 0; i < dtAuth.Rows.Count; i++)
            {
                Autorizacion a = new Autorizacion();
                a.idperfil = int.Parse(dtAuth.Rows[i]["idperfil"].ToString());
                a.idacceso = int.Parse(dtAuth.Rows[i]["idacceso"].ToString());
                a.acceso = bool.Parse(dtAuth.Rows[i]["acceso"].ToString());
                auth.Add(a);
            }
            return auth;
        }

        public List<Menus> getMenus(string idperfil)
        {
            List<Menus> menu = new List<Menus>();
            DataTable dtMenu = new DataTable();
            Command.CommandText = @"select m.nombre, p.accion
                                    from dbo.Permisos p 
                                    inner join dbo.Menus m on p.idmenu = m.idmenu
                                    inner join dbo.CatalogoPermisos cp on p.idcatpermiso = cp.id
                                    where cp.permiso = 'Ejecutar' and p.idperfil = @idperfil";
            //Command.CommandText = "select m.nombre, ver from menus m left join ediciones e on m.idmenu = e.idmenu where e.idperfil = @idperfil and m.tipomenu = 0;";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idperfil", idperfil);
            dtMenu = SelectData(Command);
            for (int i = 0; i < dtMenu.Rows.Count; i++)
            {
                Menus m = new Menus();
                m.nombre = dtMenu.Rows[i]["nombre"].ToString();
                m.accion = bool.Parse(dtMenu.Rows[i]["accion"].ToString());
                menu.Add(m);
            }
            return menu;
        }

        public List<Ediciones> getEdiciones(int idperfil, string nombre)
        {
            List<Ediciones> edicion = new List<Ediciones>();
            DataTable dtEdicion = new DataTable();
            Command.CommandText = @"select m.nombre, cp.permiso, p.accion
                                    from dbo.Permisos p 
                                    inner join dbo.Menus m on p.idmenu = m.idmenu
                                    inner join dbo.CatalogoPermisos cp on p.idcatpermiso = cp.id
                                    where m.nombre = @nombre and cp.permiso <> 'Ejecutar' and p.idperfil = @idperfil";
            //Command.CommandText = "select m.nombre, e.crear, e.consultar, e.modificar, e.baja, e.eliminar from menus m left join ediciones e on m.idmenu = e.idmenu where e.idperfil = @idperfil and m.tipomenu = 0 and m.nombre = @nombre;";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idperfil", idperfil);
            Command.Parameters.AddWithValue("nombre", nombre);
            dtEdicion = SelectData(Command);
            for (int i = 0; i < dtEdicion.Rows.Count; i++)
            {
                Ediciones e = new Ediciones();
                e.nombre = dtEdicion.Rows[i]["nombre"].ToString();
                e.permiso = dtEdicion.Rows[i]["permiso"].ToString();
                e.accion = bool.Parse(dtEdicion.Rows[i]["accion"].ToString());
                //e.modificar = bool.Parse(dtEdicion.Rows[i]["modificar"].ToString());
                //e.baja = bool.Parse(dtEdicion.Rows[i]["baja"].ToString());
                //e.eliminar = bool.Parse(dtEdicion.Rows[i]["eliminar"].ToString());
                edicion.Add(e);
            }
            return edicion;
        }

        public List<CatalogoMenu> obtenerCatalogoMenu()
        {
            List<CatalogoMenu> lstMenu = new List<CatalogoMenu>();
            DataTable dtMenu = new DataTable();
            Command.CommandText = "select idmenu, nombre from menus";
            Command.Parameters.Clear();
            dtMenu = SelectData(Command);
            for (int i = 0; i < dtMenu.Rows.Count; i++)
            {
                CatalogoMenu menu = new CatalogoMenu();
                menu.idmenu = int.Parse(dtMenu.Rows[i]["idmenu"].ToString());
                menu.nombre = dtMenu.Rows[i]["nombre"].ToString();
                lstMenu.Add(menu);
            }
            return lstMenu;
        }

        public List<CatalogoPermisos> obtenerCatalogoPermisos()
        {
            List<CatalogoPermisos> lstPermiso = new List<CatalogoPermisos>();
            DataTable dtMenu = new DataTable();
            Command.CommandText = "select id, permiso from CatalogoPermisos";
            Command.Parameters.Clear();
            dtMenu = SelectData(Command);
            for (int i = 0; i < dtMenu.Rows.Count; i++)
            {
                CatalogoPermisos permiso = new CatalogoPermisos();
                permiso.id = int.Parse(dtMenu.Rows[i]["id"].ToString());
                permiso.permiso = dtMenu.Rows[i]["permiso"].ToString();
                lstPermiso.Add(permiso);
            }
            return lstPermiso;
        }

        public List<Permisos> obtenerPermisos(int idperfil)
        {
            List<Permisos> lstPermiso = new List<Permisos>();
            DataTable dtMenu = new DataTable();
            Command.CommandText = @"select p.id, p.idperfil, m.nombre, cp.permiso, p.accion
                                    from dbo.Permisos p 
                                    inner join dbo.Menus m on p.idmenu = m.idmenu
                                    inner join dbo.CatalogoPermisos cp on p.idcatpermiso = cp.id
                                    where p.idperfil = @idperfil";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idperfil", idperfil);
            dtMenu = SelectData(Command);
            for (int i = 0; i < dtMenu.Rows.Count; i++)
            {
                Permisos permiso = new Permisos();
                permiso.id = int.Parse(dtMenu.Rows[i]["id"].ToString());
                permiso.idperfil = int.Parse(dtMenu.Rows[i]["idperfil"].ToString());
                permiso.nombre = dtMenu.Rows[i]["nombre"].ToString();
                permiso.permiso = dtMenu.Rows[i]["permiso"].ToString();
                permiso.accion = bool.Parse(dtMenu.Rows[i]["accion"].ToString());
                lstPermiso.Add(permiso);
            }
            return lstPermiso;
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

        public int actualizaAutorizacion(int perfil, List<Autorizacion> lista)
        {
            for (int i = 0; i < lista.Count; i++)
            {
                Command.CommandText = "update autorizaciones set acceso = @acceso where idacceso = @idacceso and idperfil = @idperfil";
                Command.Parameters.Clear();
                Command.Parameters.AddWithValue("idacceso", lista[i].idacceso);
                Command.Parameters.AddWithValue("idperfil", perfil);
                Command.Parameters.AddWithValue("acceso", lista[i].acceso);
                Command.ExecuteNonQuery();
            }
            return 1;
        }

        public int insertarPermiso(PermisosOperaciones p)
        {
            Command.CommandText = "insert into permisos (idmenu, idcatpermiso, idperfil, accion) values (@idmenu, @idcatpermiso, @idperfil, @accion)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idmenu", p.idmenu);
            Command.Parameters.AddWithValue("idcatpermiso", p.idcatpermiso);
            Command.Parameters.AddWithValue("idperfil", p.idperfil);
            Command.Parameters.AddWithValue("accion", p.accion);
            return Command.ExecuteNonQuery();
        }

        public int eliminarPermiso(PermisosOperaciones p)
        {
            Command.CommandText = "delete from permisos where id = @id and idperfil = @idperfil";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", p.id);
            Command.Parameters.AddWithValue("idperfil", p.idperfil);
            return Command.ExecuteNonQuery();
        }
    }
}


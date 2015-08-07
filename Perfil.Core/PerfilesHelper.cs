using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Perfil.Core
{
    public class PerfilesHelper : Data.Obj.DataObj
    {
        public List<Perfiles> obtenerPerfiles()
        {
            DataTable dtPerfiles = new DataTable();
            List<Perfiles> lstPerfiles = new List<Perfiles>();
            Command.CommandText = "select idperfil, nombre from perfiles";
            Command.Parameters.Clear();
            dtPerfiles = SelectData(Command);
            for (int i = 0; i < dtPerfiles.Rows.Count; i++)
            {
                Perfiles p = new Perfiles();
                p.idperfil = int.Parse(dtPerfiles.Rows[i]["idperfil"].ToString());
                p.nombre = dtPerfiles.Rows[i]["nombre"].ToString();
                lstPerfiles.Add(p);
            }
            return lstPerfiles;
        }

        public List<Perfiles> obtenerPerfile(Perfiles p)
        {
            DataTable dtPerfiles = new DataTable();
            List<Perfiles> lstPerfiles = new List<Perfiles>();
            Command.CommandText = "select idperfil, nombre from perfiles where idperfil = @idperfil";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idperfil", p.idperfil);
            dtPerfiles = SelectData(Command);
            for (int i = 0; i < dtPerfiles.Rows.Count; i++)
            {
                Perfiles perfil = new Perfiles();
                perfil.idperfil = int.Parse(dtPerfiles.Rows[i]["idperfil"].ToString());
                perfil.nombre = dtPerfiles.Rows[i]["nombre"].ToString();
                lstPerfiles.Add(perfil);
            }
            return lstPerfiles;
        }

        public object obtenerIdPerfil(Perfiles p)
        {
            Command.CommandText = "select idperfil from perfiles where nombre = @nombre";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("nombre", p.nombre);
            object id = Select(Command);
            return id;
        }

        public int insertaPerfil(Perfiles p)
        {
            Command.CommandText = "insert into perfiles (nombre) values (@nombre)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("nombre", p.nombre);
            return Command.ExecuteNonQuery();
        }

        public int actualizaPerfil(Perfiles p)
        {
            Command.CommandText = "update perfiles set nombre = @nombre where idperfil = @idperfil";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idperfil", p.idperfil);
            Command.Parameters.AddWithValue("nombre", p.nombre);
            return Command.ExecuteNonQuery();
        }

        public int bajaPerfil(Perfiles p)
        {
            Command.CommandText = "delete from perfiles where idperfil = @idperfil";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idperfil", p.idperfil);
            return Command.ExecuteNonQuery();
        }
    }
}

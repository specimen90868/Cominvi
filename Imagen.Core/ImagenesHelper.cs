using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imagen.Core
{
    public class ImagenesHelper : Data.Obj.DataObj
    {
        public List<Imagenes> obtenerImagen(Imagenes img)
        {
            DataTable dtImagen = new DataTable();
            List<Imagenes> lstImagen = new List<Imagenes>();
            Command.CommandText = "select * from imagenes where idpersona = @idpersona and tipopersona = @tipopersona";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idpersona",img.idpersona);
            Command.Parameters.AddWithValue("tipopersona", img.tipopersona);
            dtImagen = SelectData(Command);
            for (int i = 0; i < dtImagen.Rows.Count; i++)
            {
                Imagenes imagen = new Imagenes();
                imagen.idimagen = int.Parse(dtImagen.Rows[i]["idimagen"].ToString());
                imagen.idpersona = int.Parse(dtImagen.Rows[i]["idpersona"].ToString());
                imagen.imagen = (byte[])dtImagen.Rows[i]["imagen"];
                imagen.tipopersona = int.Parse(dtImagen.Rows[i]["tipopersona"].ToString());
                lstImagen.Add(imagen);
            }
            return lstImagen;
        }

        public object ExisteImagen(Imagenes img)
        {
            Command.CommandText = "select count(*) from imagenes where idpersona = @idpersona and tipopersona = @tipopersona";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idpersona",img.idpersona);
            Command.Parameters.AddWithValue("tipopersona", img.tipopersona);
            object existe = Select(Command);
            return existe;
        }

        public int insertaImagen(Imagenes img)
        {
            Command.CommandText = "insert into imagenes (idpersona, imagen, tipopersona) values (@idpersona,@imagen,@tipopersona)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idpersona",img.idpersona);
            Command.Parameters.AddWithValue("imagen", img.imagen);
            Command.Parameters.AddWithValue("tipopersona", img.tipopersona);
            return Command.ExecuteNonQuery();
        }
        
        public int actualizaImagen(Imagenes img)
        {
            Command.CommandText = "update imagenes set imagen = @imagen where idpersona = @idpersona and tipopersona = @tipopersona";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idpersona", img.idpersona);
            Command.Parameters.AddWithValue("tipopersona", img.tipopersona);
            Command.Parameters.AddWithValue("imagen",img.imagen);
            return Command.ExecuteNonQuery();
        }

        public int bajaImagen(Imagenes img)
        {
            Command.CommandText = "delete from imagenes where idpersona = @idpersona and tipopersona = @tipopersona";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idpersona", img.idpersona);
            Command.Parameters.AddWithValue("tipopersona", img.tipopersona);
            return Command.ExecuteNonQuery();
        }
    }
}

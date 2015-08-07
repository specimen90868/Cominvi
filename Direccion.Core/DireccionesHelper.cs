using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Direccion.Core
{
    public class DireccionesHelper : Data.Obj.DataObj
    {
        public List<Direcciones> obtenerDireccion(Direcciones d)
        {
            DataTable dtDireccion = new DataTable();
            List<Direcciones> lstDireccion = new List<Direcciones>();
            Command.CommandText = "select iddireccion, calle, exterior, interior, colonia, cp, ciudad, estado, pais from direcciones where idpersona = @idpersona " +
                "and tipopersona = @tipopersona";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idpersona", d.idpersona);
            Command.Parameters.AddWithValue("tipopersona", d.tipopersona);
            dtDireccion = SelectData(Command);
            for (int i = 0; i < dtDireccion.Rows.Count; i++)
            {
                Direcciones direccion = new Direcciones();
                direccion.iddireccion = int.Parse(dtDireccion.Rows[i]["iddireccion"].ToString());
                direccion.calle = dtDireccion.Rows[i]["calle"].ToString();
                direccion.exterior = dtDireccion.Rows[i]["exterior"].ToString();
                direccion.interior = dtDireccion.Rows[i]["interior"].ToString();
                direccion.colonia = dtDireccion.Rows[i]["colonia"].ToString();
                direccion.cp = dtDireccion.Rows[i]["cp"].ToString();
                direccion.ciudad = dtDireccion.Rows[i]["ciudad"].ToString();
                direccion.estado = dtDireccion.Rows[i]["estado"].ToString();
                direccion.pais = dtDireccion.Rows[i]["pais"].ToString();
                lstDireccion.Add(direccion);
            }

            return lstDireccion;
        }

        public int insertaDireccion(Direcciones d)
        {
            Command.CommandText = "insert into direcciones (idpersona,calle,exterior,interior,colonia,cp,ciudad,estado,pais,tipodireccion,tipopersona) " +
                "values (@idpersona,@calle,@exterior,@interior,@colonia,@cp,@ciudad,@estado,@pais,@tipodireccion,@tipopersona)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("@idpersona", d.idpersona);
            Command.Parameters.AddWithValue("@calle", d.calle);
            Command.Parameters.AddWithValue("@exterior", d.exterior);
            Command.Parameters.AddWithValue("@interior", d.interior);
            Command.Parameters.AddWithValue("@colonia", d.colonia);
            Command.Parameters.AddWithValue("@cp", d.cp);
            Command.Parameters.AddWithValue("@ciudad", d.ciudad);
            Command.Parameters.AddWithValue("@estado", d.estado);
            Command.Parameters.AddWithValue("@pais", d.pais);
            Command.Parameters.AddWithValue("@tipodireccion", d.tipodireccion);
            Command.Parameters.AddWithValue("@tipopersona", d.tipopersona);
            return Command.ExecuteNonQuery();
        }

        public int actualizaDireccion(Direcciones d)
        {
            Command.CommandText = "update direcciones set calle = @calle, exterior = @exterior, interior = @interior, colonia = @colonia, cp = @cp, ciudad = @ciudad, estado = @estado, pais = @pais " + 
                "where iddireccion = @iddireccion and idpersona = @idpersona";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("@iddireccion", d.iddireccion);
            Command.Parameters.AddWithValue("@idpersona", d.idpersona);
            Command.Parameters.AddWithValue("@calle", d.calle);
            Command.Parameters.AddWithValue("@exterior", d.exterior);
            Command.Parameters.AddWithValue("@interior", d.interior);
            Command.Parameters.AddWithValue("@colonia", d.colonia);
            Command.Parameters.AddWithValue("@cp", d.cp);
            Command.Parameters.AddWithValue("@ciudad", d.ciudad);
            Command.Parameters.AddWithValue("@estado", d.estado);
            Command.Parameters.AddWithValue("@pais", d.pais);
            return Command.ExecuteNonQuery();
        }
    }
}

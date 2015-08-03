using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayoutMovimientos.Core
{
    public class LayoutHelper : Data.Obj.DataObj
    {
        public int insertaLayoutMovimiento(LayoutMovimientos l)
        {
            Command.CommandText = "insert into layoutmovimientos (idtrabajador,idempresa,idcliente,movimiento,nombres,paterno,materno,sdi,sdinuevo,nss,digitonss,fecha_ingreso,fecha_sistema,curp,registro,digitoregistro,motivobaja,generado) " +
                "values (@idtrabajador,@idempresa,@idcliente,@movimiento,@nombres,@paterno,@materno,@sdi,@sdinuevo,@nss,@digitonss,@fecha_ingreso,@fecha_sistema,@curp,@registro,@digitoregistro,@motivobaja,@generado)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador",l.idtrabajador);
            Command.Parameters.AddWithValue("idempresa",l.idempresa);
            Command.Parameters.AddWithValue("idcliente",l.idcliente);
            Command.Parameters.AddWithValue("movimiento", l.movimiento);
            Command.Parameters.AddWithValue("nombres", l.nombres);
            Command.Parameters.AddWithValue("paterno", l.paterno);
            Command.Parameters.AddWithValue("materno", l.materno);
            Command.Parameters.AddWithValue("sdi", l.materno);
            Command.Parameters.AddWithValue("sdinuevo", l.materno);
            Command.Parameters.AddWithValue("nss", l.materno);
            Command.Parameters.AddWithValue("digitonss", l.materno);
            Command.Parameters.AddWithValue("fecha_ingreso", l.materno);
            Command.Parameters.AddWithValue("fecha_sistema", l.materno);
            Command.Parameters.AddWithValue("curp", l.materno);
            Command.Parameters.AddWithValue("registro", l.materno);
            Command.Parameters.AddWithValue("digitoregistro", l.materno);
            Command.Parameters.AddWithValue("motivobaja", l.materno);
            Command.Parameters.AddWithValue("generado", l.materno);
            return Command.ExecuteNonQuery();
        }

        public int bajaLayoutMovimiento(LayoutMovimientos l)
        {
            Command.CommandText = "delete from layoutmovimientos where idtrabajador = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", l.idtrabajador);
            return Command.ExecuteNonQuery();
        }
    }
}

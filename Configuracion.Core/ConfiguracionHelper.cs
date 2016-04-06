using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuracion.Core
{
    public class ConfiguracionHelper : Data.Obj.DataObj
    {
        public object obtenerValorConfiguracion(string nombre)
        {
            Command.CommandText = "select valor from configuracion where nombre = @nombre";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("nombre", nombre);
            object dato = Select(Command);
            return dato;
        }

        public int actualizarValorConfiguracion(string nombre, string valor)
        {
            Command.CommandText = "update configuracion set valor = @valor where nombre = @nombre";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("valor", nombre);
            Command.Parameters.AddWithValue("nombre", nombre);
            return Command.ExecuteNonQuery();
        }
    }
}

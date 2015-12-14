using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfonavitProporcional.Core 
{
    public class ProporcionalHelper: Data.Obj.DataObj
    {
        public object obtenerIdSuaInfonavit(InfonavitProporcional ip)
        {
            Command.CommandText = "select idsuainfonavit from InfonavitProporcional where idtrabajador = @idtrabajador and ";
        }

        public int insertaDias(InfonavitProporcional ip)
        {
            Command.CommandText = "insert into InfonavitProporcional (idtrabajador, idempresa, dias, periodoinicio, periodofin, idsuainfonavit, idinfonavit) values (@idtrabajador,@idempresa,@dias,@periodoinicio,@periodofin,@id, @idinfonavit)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", ip.idtrabajador);
            Command.Parameters.AddWithValue("idempresa", ip.idempresa);
            Command.Parameters.AddWithValue("dias", ip.dias);
            Command.Parameters.AddWithValue("periodoinicio", ip.periodoinicio);
            Command.Parameters.AddWithValue("periodofin", ip.periodofin);
            Command.Parameters.AddWithValue("id", ip.idsuainfonavit);
            Command.Parameters.AddWithValue("idinfonavit", ip.idinfonavit);
            return Command.ExecuteNonQuery();
        }

        public int actualizaDias(InfonavitProporcional ip)
        {
            Command.CommandText = "update InfonavitProporcional set dias = @dias, periodoinicio = @periodoinicio, periodofin = @periodofin where idinfonavit = @idinfonavit and idsuainfonavi = @idsuainfonavit";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idinfonavit", ip.idinfonavit);
            Command.Parameters.AddWithValue("idsuainfonavit", ip.idsuainfonavit);
            Command.Parameters.AddWithValue("dias", ip.dias);
            Command.Parameters.AddWithValue("periodoinicio", ip.periodoinicio);
            Command.Parameters.AddWithValue("periodofin", ip.periodofin);
            return Command.ExecuteNonQuery();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Empresas.Core
{
    public class EmpresasHelper : Data.Obj.DataObj
    {
        public List<Empresas> obtenerEmpresas()
        {
            DataTable dtEmpresas = new DataTable();
            Command.CommandText = "select idempresa, nombre, rfc, registro, digitoverificador, representante from empresas where activo = 1";
            Command.Parameters.Clear();
            dtEmpresas = SelectData(Command);
            List<Empresas> lstEmpresa = new List<Empresas>();
            for (int i = 0; i < dtEmpresas.Rows.Count; i++)
            {
                Empresas e = new Empresas();
                e.idempresa = int.Parse(dtEmpresas.Rows[i]["idempresa"].ToString());
                e.nombre = dtEmpresas.Rows[i]["nombre"].ToString();
                e.rfc = dtEmpresas.Rows[i]["rfc"].ToString();
                e.registro = dtEmpresas.Rows[i]["registro"].ToString();
                e.digitoverificador = int.Parse(dtEmpresas.Rows[i]["digitoverificador"].ToString());
                e.representante = dtEmpresas.Rows[i]["representante"].ToString();
                lstEmpresa.Add(e);
            }
            return lstEmpresa;
        }

        public List<Empresas> obtenerEmpresa(int idempresa)
        {
            DataTable dtEmpresas = new DataTable();
            Command.CommandText = "select idempresa, nombre, rfc, registro, digitoverificador, sindicato, representante from empresas where idempresa = @idempresa";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", idempresa);
            dtEmpresas = SelectData(Command);
            List<Empresas> lstEmpresa = new List<Empresas>();
            for (int i = 0; i < dtEmpresas.Rows.Count; i++)
            {
                Empresas e = new Empresas();
                e.idempresa = int.Parse(dtEmpresas.Rows[i]["idempresa"].ToString());
                e.nombre = dtEmpresas.Rows[i]["nombre"].ToString();
                e.rfc = dtEmpresas.Rows[i]["rfc"].ToString();
                e.registro = dtEmpresas.Rows[i]["registro"].ToString();
                e.digitoverificador = int.Parse(dtEmpresas.Rows[i]["digitoverificador"].ToString());
                e.sindicato = int.Parse(dtEmpresas.Rows[i]["sindicato"].ToString());
                e.representante = dtEmpresas.Rows[i]["representante"].ToString();
                lstEmpresa.Add(e);
            }
            return lstEmpresa;
        }
        
        public List<Empresas> InicioEmpresa()
        {
            List<Empresas> lstEmpresa = new List<Empresas>();
            Command.CommandText = "select idempresa, nombre, registro, digitoverificador from empresas where activo = 1";
            Command.Parameters.Clear();
            DataTable dtEmpresa = new DataTable();
            dtEmpresa = SelectData(Command);
            for(int i = 0; i < dtEmpresa.Rows.Count; i++)
            {
                Empresas e = new Empresas();
                e.idempresa = int.Parse(dtEmpresa.Rows[i]["idempresa"].ToString());
                e.nombre = dtEmpresa.Rows[i]["nombre"].ToString();
                e.registro = dtEmpresa.Rows[i]["registro"].ToString();
                e.digitoverificador = int.Parse(dtEmpresa.Rows[i]["digitoverificador"].ToString());
                lstEmpresa.Add(e);
            }
            return lstEmpresa;
        }

        public object obtenerIdEmpresa(Empresas e)
        {
            Command.CommandText = "select idempresa from empresas where rfc = @rfc and registro = @registro";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("rfc", e.rfc);
            Command.Parameters.AddWithValue("registro", e.registro);
            object id = Select(Command);
            return id;
        }

        public MySqlCommand insertaEmpresa(Empresas e)
        {
            Command.CommandText = "insert into empresas (nombre, rfc, registro, digitoverificador, sindicato, representante, activo) " + 
                "values (@nombre, @rfc, @registro, @digitoverificador, @sindicato, @representante, @activo)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("nombre",e.nombre);
            Command.Parameters.AddWithValue("rfc", e.rfc);
            Command.Parameters.AddWithValue("registro", e.registro);
            Command.Parameters.AddWithValue("digitoverificador", e.digitoverificador);
            Command.Parameters.AddWithValue("sindicato", e.sindicato);
            Command.Parameters.AddWithValue("representante", e.representante);
            Command.Parameters.AddWithValue("activo", e.activo);
            Command.ExecuteNonQuery();
            return Command;
        }

        public int actualizaEmpresa(Empresas e)
        {
            Command.CommandText = "update empresas set nombre = @nombre, rfc = @rfc, registro = @registro, digitoverificador = @digitoverificador, sindicato = @sindicato, representante = @representante " +
                "where idempresa = @idempresa";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", e.idempresa);
            Command.Parameters.AddWithValue("nombre", e.nombre);
            Command.Parameters.AddWithValue("rfc", e.rfc);
            Command.Parameters.AddWithValue("registro", e.registro);
            Command.Parameters.AddWithValue("digitoverificador", e.digitoverificador);
            Command.Parameters.AddWithValue("sindicato", e.sindicato);
            Command.Parameters.AddWithValue("representante", e.representante);
            return Command.ExecuteNonQuery();
        }

        public int bajaEmpresa(Empresas e)
        {
            Command.CommandText = "update empresas set activo = 0 where idempresa = @idempresa";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", e.idempresa);
            return Command.ExecuteNonQuery();
        }
    }
}


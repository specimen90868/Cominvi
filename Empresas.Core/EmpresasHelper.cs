using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace Empresas.Core
{
    public class EmpresasHelper : Data.Obj.DataObj
    {
        public List<Empresas> obtenerEmpresas()
        {
            DataTable dtEmpresas = new DataTable();
            Command.CommandText = "select idempresa, nombre, rfc, registro, digitoverificador, representante from empresas where estatus = 1";
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
            Command.CommandText = @"select idempresa, nombre, rfc, registro, digitoverificador, representante, regimen, certificado, llave, password, nocertificado, vigenciacertificado, observacion, obracivil, 
                                    idregimenfiscal, codigopostal from empresas where idempresa = @idempresa";
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
                e.representante = dtEmpresas.Rows[i]["representante"].ToString();
                e.regimen = dtEmpresas.Rows[i]["regimen"].ToString();
                e.certificado = dtEmpresas.Rows[i]["certificado"].ToString();
                e.llave = dtEmpresas.Rows[i]["llave"].ToString();
                e.password = dtEmpresas.Rows[i]["password"].ToString();
                e.nocertificado = dtEmpresas.Rows[i]["nocertificado"].ToString();
                e.vigenciacertificado = DateTime.Parse(dtEmpresas.Rows[i]["vigenciacertificado"].ToString());
                e.observacion = dtEmpresas.Rows[i]["observacion"].ToString();
                e.obracivil = bool.Parse(dtEmpresas.Rows[i]["obracivil"].ToString());
                e.idregimenfiscal = int.Parse(dtEmpresas.Rows[i]["idregimenfiscal"].ToString());
                e.codigopostal = dtEmpresas.Rows[i]["codigopostal"].ToString();
                lstEmpresa.Add(e);
            }
            return lstEmpresa;
        }
        
        public List<Empresas> InicioEmpresa()
        {
            List<Empresas> lstEmpresa = new List<Empresas>();
            Command.CommandText = "select idempresa, nombre, registro, digitoverificador, observacion from empresas where estatus = 1";
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
                e.observacion = dtEmpresa.Rows[i]["observacion"].ToString();
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

        public int obtenerIdEmpresa(string nombre)
        {
            Command.CommandText = "select idempresa from empresas where nombre = @nombre";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("nombre", nombre);          
            object id = Select(Command);
            return (int)id;
        }

        public object obtenerRegistroPatronal(Empresas e)
        {
            Command.CommandText = "select registro + convert(char(1),digitoverificador) as registropatronal from dbo.Empresas where idempresa = @idempresa";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", e.idempresa);
            object id = Select(Command);
            return id;
        }

        public bool obtenerObraCivilEmpresa(Empresas e)
        {
            Command.CommandText = "select obracivil from dbo.Empresas where idempresa = @idempresa";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", e.idempresa);
            object dato = Select(Command);
            return (bool)dato;
        }

        public int insertaEmpresa(Empresas e)
        {
            Command.CommandText = "insert into empresas (nombre, rfc, registro, digitoverificador, representante, estatus, certificado, llave, password, regimen, nocertificado, vigenciacertificado, observacion, obracivil, idregimenfiscal, codigopostal) " +
                "values (@nombre, @rfc, @registro, @digitoverificador, @representante, @estatus, @certificado, @llave, @password, @regimen, @nocertificado, @vigenciacertificado, @observacion, @obracivil, @idregimenfiscal, @codigopostal)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("nombre",e.nombre);
            Command.Parameters.AddWithValue("rfc", e.rfc);
            Command.Parameters.AddWithValue("registro", e.registro);
            Command.Parameters.AddWithValue("digitoverificador", e.digitoverificador);
            Command.Parameters.AddWithValue("representante", e.representante);
            Command.Parameters.AddWithValue("estatus", e.estatus);
            Command.Parameters.AddWithValue("certificado", e.certificado);
            Command.Parameters.AddWithValue("llave", e.llave);
            Command.Parameters.AddWithValue("password", e.password);
            Command.Parameters.AddWithValue("regimen", e.regimen);
            Command.Parameters.AddWithValue("nocertificado", e.certificado);
            Command.Parameters.AddWithValue("vigenciacertificado", e.vigenciacertificado);
            Command.Parameters.AddWithValue("observacion", e.observacion);
            Command.Parameters.AddWithValue("obracivil", e.obracivil);
            Command.Parameters.AddWithValue("idregimenfiscal", e.idregimenfiscal);
            Command.Parameters.AddWithValue("codigopostal", e.codigopostal);
            return Command.ExecuteNonQuery();
        }

        public int actualizaEmpresa(Empresas e)
        {
            Command.CommandText = @"update empresas set nombre = @nombre, rfc = @rfc, registro = @registro, digitoverificador = @digitoverificador, representante = @representante, 
                certificado = @certificado, llave = @llave, password = @password, regimen = @regimen, nocertificado = @nocertificado, vigenciacertificado = @vigencia, observacion = @observacion, 
                obracivil = @obracivil, idregimenfiscal = @idregimenfiscal, codigopostal = @codigopostal where idempresa = @idempresa";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", e.idempresa);
            Command.Parameters.AddWithValue("nombre", e.nombre);
            Command.Parameters.AddWithValue("rfc", e.rfc);
            Command.Parameters.AddWithValue("registro", e.registro);
            Command.Parameters.AddWithValue("digitoverificador", e.digitoverificador);
            Command.Parameters.AddWithValue("representante", e.representante);
            Command.Parameters.AddWithValue("certificado", e.certificado);
            Command.Parameters.AddWithValue("llave", e.llave);
            Command.Parameters.AddWithValue("password", e.password);
            Command.Parameters.AddWithValue("regimen", e.regimen);
            Command.Parameters.AddWithValue("nocertificado", e.nocertificado);
            Command.Parameters.AddWithValue("vigencia", e.vigenciacertificado);
            Command.Parameters.AddWithValue("observacion", e.observacion);
            Command.Parameters.AddWithValue("obracivil", e.obracivil);
            Command.Parameters.AddWithValue("idregimenfiscal", e.idregimenfiscal);
            Command.Parameters.AddWithValue("codigopostal", e.codigopostal);
            return Command.ExecuteNonQuery();
        }

        public int bajaEmpresa(Empresas e)
        {
            Command.CommandText = "update empresas set estatus = 0 where idempresa = @idempresa";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idempresa", e.idempresa);
            return Command.ExecuteNonQuery();
        }
    }
}


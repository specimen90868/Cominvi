using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complementos.Core
{
    public class ComplementoHelper : Data.Obj.DataObj
    {
        public List<Complemento> obtenerComplemento(Complemento c)
        {
            DataTable dtComplemento = new DataTable();
            List<Complemento> lstComplemento = new List<Complemento>();
            Command.CommandText = "select * from complementos where idtrabajador = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador",c.idtrabajador);
            dtComplemento = SelectData(Command);
            for (int i = 0; i < dtComplemento.Rows.Count; i++)
            {
                Complemento complemento = new Complemento();
                complemento.id = int.Parse(dtComplemento.Rows[i]["id"].ToString());
                complemento.idtrabajador = int.Parse(dtComplemento.Rows[i]["idtrabajador"].ToString());
                complemento.contrato = dtComplemento.Rows[i]["contrato"].ToString();
                complemento.jornada = dtComplemento.Rows[i]["jornada"].ToString();
                complemento.iddepartamento = int.Parse(dtComplemento.Rows[i]["iddepartamento"].ToString());
                complemento.idpuesto = int.Parse(dtComplemento.Rows[i]["idpuesto"].ToString());
                complemento.estadocivil = dtComplemento.Rows[i]["estadocivil"].ToString();
                complemento.sexo = dtComplemento.Rows[i]["sexo"].ToString();
                complemento.escolaridad = dtComplemento.Rows[i]["escolaridad"].ToString();
                complemento.horario = dtComplemento.Rows[i]["horario"].ToString();
                complemento.nocontrol = dtComplemento.Rows[i]["nocontrol"].ToString();
                complemento.clinica = dtComplemento.Rows[i]["clinica"].ToString();
                complemento.nacionalidad = dtComplemento.Rows[i]["nacionalidad"].ToString();
                complemento.funciones = dtComplemento.Rows[i]["funciones"].ToString();
                lstComplemento.Add(complemento);
            }
            return lstComplemento;
        }

        public object existeComplemento(Complemento c)
        {
            Command.CommandText = "select count(idtrabajador) from complementos where idtrabajador = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador",c.idtrabajador);
            object dato = Select(Command);
            return dato;
        }

        public int insertaComplemento(Complemento c)
        {
            Command.CommandText = "insert into complementos (idtrabajador,contrato,jornada,iddepartamento,idpuesto,estadocivil,sexo,escolaridad,horario,nocontrol,clinica,nacionalidad,funciones) values " +
                "(@idtrabajador,@contrato,@jornada,@iddepartamento,@idpuesto,@estadocivil,@sexo,@escolaridad,@horario,@nocontrol,@clinica,@nacionalidad,@funciones)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", c.idtrabajador);
            Command.Parameters.AddWithValue("contrato", c.contrato);
            Command.Parameters.AddWithValue("jornada", c.jornada);
            Command.Parameters.AddWithValue("iddepartamento", c.iddepartamento);
            Command.Parameters.AddWithValue("idpuesto", c.idpuesto);
            Command.Parameters.AddWithValue("estadocivil", c.estadocivil);
            Command.Parameters.AddWithValue("sexo", c.sexo);
            Command.Parameters.AddWithValue("escolaridad", c.escolaridad);
            Command.Parameters.AddWithValue("horario", c.horario);
            Command.Parameters.AddWithValue("nocontrol", c.nocontrol);
            Command.Parameters.AddWithValue("clinica", c.clinica);
            Command.Parameters.AddWithValue("nacionalidad", c.nacionalidad);
            Command.Parameters.AddWithValue("funciones", c.funciones);
            return Command.ExecuteNonQuery();
        }

        public int actualizaComplemento(Complemento c)
        {
            Command.CommandText = "update complementos set contrato = @contrato, jornada = @jornada,iddepartamento = @iddepartamento,idpuesto = @idpuesto,estadocivil = @estadocivil," + 
                "sexo = @sexo,escolaridad = @escolaridad,horario = @horario,nocontrol = @nocontrol,clinica = @clinica,nacionalidad = @nacionalidad,funciones= @funciones where id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", c.id);
            Command.Parameters.AddWithValue("contrato", c.contrato);
            Command.Parameters.AddWithValue("jornada", c.jornada);
            Command.Parameters.AddWithValue("iddepartamento", c.iddepartamento);
            Command.Parameters.AddWithValue("idpuesto", c.idpuesto);
            Command.Parameters.AddWithValue("estadocivil", c.estadocivil);
            Command.Parameters.AddWithValue("sexo", c.sexo);
            Command.Parameters.AddWithValue("escolaridad", c.escolaridad);
            Command.Parameters.AddWithValue("horario", c.horario);
            Command.Parameters.AddWithValue("nocontrol", c.nocontrol);
            Command.Parameters.AddWithValue("clinica", c.clinica);
            Command.Parameters.AddWithValue("nacionalidad", c.nacionalidad);
            Command.Parameters.AddWithValue("funciones", c.funciones);
            return Command.ExecuteNonQuery();
        }
    }
}

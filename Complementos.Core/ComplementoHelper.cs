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
                complemento.contrato = int.Parse(dtComplemento.Rows[i]["contrato"].ToString());
                complemento.jornada = int.Parse(dtComplemento.Rows[i]["jornada"].ToString());
                complemento.estadocivil = int.Parse(dtComplemento.Rows[i]["estadocivil"].ToString());
                complemento.sexo = int.Parse(dtComplemento.Rows[i]["sexo"].ToString());
                complemento.escolaridad = int.Parse(dtComplemento.Rows[i]["escolaridad"].ToString());
                complemento.clinica = dtComplemento.Rows[i]["clinica"].ToString();
                complemento.nacionalidad = dtComplemento.Rows[i]["nacionalidad"].ToString();
                complemento.observaciones = dtComplemento.Rows[i]["observaciones"].ToString();
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
            Command.CommandText = "insert into complementos (idtrabajador,contrato,jornada,estadocivil,sexo,escolaridad,clinica,nacionalidad,observaciones) values " +
                "(@idtrabajador,@contrato,@jornada,@estadocivil,@sexo,@escolaridad,@clinica,@nacionalidad,@observaciones)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", c.idtrabajador);
            Command.Parameters.AddWithValue("contrato", c.contrato);
            Command.Parameters.AddWithValue("jornada", c.jornada);
            Command.Parameters.AddWithValue("estadocivil", c.estadocivil);
            Command.Parameters.AddWithValue("sexo", c.sexo);
            Command.Parameters.AddWithValue("escolaridad", c.escolaridad);
            Command.Parameters.AddWithValue("clinica", c.clinica);
            Command.Parameters.AddWithValue("nacionalidad", c.nacionalidad);
            Command.Parameters.AddWithValue("observaciones", c.observaciones);
            return Command.ExecuteNonQuery();
        }

        public int actualizaComplemento(Complemento c)
        {
            Command.CommandText = "update complementos set contrato = @contrato, jornada = @jornada, estadocivil = @estadocivil," + 
                "sexo = @sexo, escolaridad = @escolaridad, clinica = @clinica,nacionalidad = @nacionalidad, observaciones= @observaciones where id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", c.id);
            Command.Parameters.AddWithValue("contrato", c.contrato);
            Command.Parameters.AddWithValue("jornada", c.jornada);
            Command.Parameters.AddWithValue("estadocivil", c.estadocivil);
            Command.Parameters.AddWithValue("sexo", c.sexo);
            Command.Parameters.AddWithValue("escolaridad", c.escolaridad);
            Command.Parameters.AddWithValue("clinica", c.clinica);
            Command.Parameters.AddWithValue("nacionalidad", c.nacionalidad);
            Command.Parameters.AddWithValue("observaciones", c.observaciones);
            return Command.ExecuteNonQuery();
        }
    }
}

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
                complemento.tempresa1 = dtComplemento.Rows[i]["tempresa1"].ToString();
                complemento.tempresa2 = dtComplemento.Rows[i]["tempresa2"].ToString();
                complemento.tempresa3 = dtComplemento.Rows[i]["tempresa3"].ToString();
                complemento.extension = dtComplemento.Rows[i]["extension"].ToString();
                complemento.cempresa = dtComplemento.Rows[i]["cempresa"].ToString();
                complemento.cpersonal = dtComplemento.Rows[i]["cpersonal"].ToString();
                complemento.email = dtComplemento.Rows[i]["email"].ToString();
                complemento.observaciones = dtComplemento.Rows[i]["observaciones"].ToString();
                lstComplemento.Add(complemento);
            }
            return lstComplemento;
        }

        public object obtenerObservacionesTrabajador(Complemento c)
        {
            Command.CommandText = "select observaciones from complementos where idtrabajador = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", c.idtrabajador);
            object dato = Select(Command);
            return dato;
        }

        public object actualizaObservacionesTrabajador(Complemento c)
        {
            Command.CommandText = "update complementos set observaciones = @observaciones where idtrabajador = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", c.idtrabajador);
            Command.Parameters.AddWithValue("observaciones", c.observaciones);
            object dato = Select(Command);
            return dato;
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
            Command.CommandText = "insert into complementos (idtrabajador,contrato,jornada,estadocivil,sexo,escolaridad,clinica,nacionalidad,tempresa1,tempresa2,tempresa3,extension,cempresa,cpersonal,email,observaciones) values " +
                "(@idtrabajador,@contrato,@jornada,@estadocivil,@sexo,@escolaridad,@clinica,@nacionalidad,@tempresa1,@tempresa2,@tempresa3,@extension,@cempresa,@cpersonal,@email,@observaciones)";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", c.idtrabajador);
            Command.Parameters.AddWithValue("contrato", c.contrato);
            Command.Parameters.AddWithValue("jornada", c.jornada);
            Command.Parameters.AddWithValue("estadocivil", c.estadocivil);
            Command.Parameters.AddWithValue("sexo", c.sexo);
            Command.Parameters.AddWithValue("escolaridad", c.escolaridad);
            Command.Parameters.AddWithValue("clinica", c.clinica);
            Command.Parameters.AddWithValue("nacionalidad", c.nacionalidad);
            Command.Parameters.AddWithValue("tempresa1", c.tempresa1);
            Command.Parameters.AddWithValue("tempresa2", c.tempresa2);
            Command.Parameters.AddWithValue("tempresa3", c.tempresa3);
            Command.Parameters.AddWithValue("extension", c.extension);
            Command.Parameters.AddWithValue("tpersonal", c.cempresa);
            Command.Parameters.AddWithValue("tpersonal", c.cpersonal);
            Command.Parameters.AddWithValue("email", c.email);
            Command.Parameters.AddWithValue("observaciones", c.observaciones);
            return Command.ExecuteNonQuery();
        }

        public int actualizaComplemento(Complemento c)
        {
            Command.CommandText = @"update complementos set contrato = @contrato, jornada = @jornada, estadocivil = @estadocivil,
                sexo = @sexo, escolaridad = @escolaridad, clinica = @clinica,nacionalidad = @nacionalidad, tempresa1 = @tempresa1, tempresa2 = @tempresa2, tempresa3 = @tempresa3, extension = @extension, 
                cempresa = @cempresa, cpersonal = @cpersonal, email = @email, observaciones= @observaciones where id = @id";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("id", c.id);
            Command.Parameters.AddWithValue("contrato", c.contrato);
            Command.Parameters.AddWithValue("jornada", c.jornada);
            Command.Parameters.AddWithValue("estadocivil", c.estadocivil);
            Command.Parameters.AddWithValue("sexo", c.sexo);
            Command.Parameters.AddWithValue("escolaridad", c.escolaridad);
            Command.Parameters.AddWithValue("clinica", c.clinica);
            Command.Parameters.AddWithValue("nacionalidad", c.nacionalidad);
            Command.Parameters.AddWithValue("tempresa1", c.tempresa1);
            Command.Parameters.AddWithValue("tempresa2", c.tempresa2);
            Command.Parameters.AddWithValue("tempresa3", c.tempresa3);
            Command.Parameters.AddWithValue("extension", c.extension);
            Command.Parameters.AddWithValue("cempresa", c.cempresa);
            Command.Parameters.AddWithValue("cpersonal", c.cpersonal);
            Command.Parameters.AddWithValue("email", c.email);
            Command.Parameters.AddWithValue("observaciones", c.observaciones);
            return Command.ExecuteNonQuery();
        }

        public int eliminaComplemento(int idtrabajador)
        {
            Command.CommandText = "delete from complementos where idtrabajador = @idtrabajador";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("idtrabajador", idtrabajador);
            return Command.ExecuteNonQuery();
        }
    }
}

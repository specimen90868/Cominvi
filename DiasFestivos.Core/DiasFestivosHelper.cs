using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiasFestivos.Core
{
    public class DiasFestivosHelper : Data.Obj.DataObj
    {
        public List<DiasFestivos> obtenerDiasFestivos()
        {
            Command.CommandText = "select * from DiasFestivos where anio = @anio";
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("anio",DateTime.Now.Year);
            DataTable dtDias = new DataTable();
            List<DiasFestivos> lstDias = new List<DiasFestivos>();
            dtDias = SelectData(Command);
            for (int i = 0; i < dtDias.Rows.Count; i++)
            {
                DiasFestivos df = new DiasFestivos();
                df.id = int.Parse(dtDias.Rows[i]["id"].ToString());
                df.diafestivo = DateTime.Parse(dtDias.Rows[i]["diafestivo"].ToString());
                df.anio = int.Parse(dtDias.Rows[i]["anio"].ToString());
                lstDias.Add(df);
            }
            return lstDias;
        }
    }
}

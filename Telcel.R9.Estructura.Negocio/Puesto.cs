using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telcel.R9.Estructura.Negocio
{
    public class Puesto
    {
        public int IdPuesto { get; set; }

        public string Descripcion { get; set; }

        public List<Object> Puestos { get; set; }

        public static Telcel.R9.Estructura.Negocio.Result GetAllEF()
        {
            Telcel.R9.Estructura.Negocio.Result result = new Telcel.R9.Estructura.Negocio.Result();
            try
            {
                using (Telcel.R9.Estructura.AccesoDatos.OTorresEstructuraEntities context = new Telcel.R9.Estructura.AccesoDatos.OTorresEstructuraEntities())
                {
                    var resultQuery = context.PuestoGetAll().ToList();


                    if (resultQuery != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in resultQuery)
                        {
                            Telcel.R9.Estructura.Negocio.Puesto materia = new Telcel.R9.Estructura.Negocio.Puesto();
                            materia.IdPuesto = obj.IdPuesto;
                            materia.Descripcion = obj.Descripcion;

                            result.Objects.Add(materia);
                            result.Correct = true;
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error la tabla no contiene informacion ";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telcel.R9.Estructura.Negocio
{
    public class Empleado
    {
        public int IdEmpleado { get; set; }

        public string Nombre { get; set; }

        public int IdPuesto { get; set; }

        public int IdDepartamento { get; set; }

        public List<Object> Empleados { get; set; }

        public Telcel.R9.Estructura.Negocio.Departamento Departamento { get; set; }
        public Telcel.R9.Estructura.Negocio.Puesto Puesto { get; set; }



        public static Telcel.R9.Estructura.Negocio.Result GetAllEF()
        {
            Telcel.R9.Estructura.Negocio.Result result = new Telcel.R9.Estructura.Negocio.Result();
            try
            {
                using (Telcel.R9.Estructura.AccesoDatos.OTorresEstructuraEntities context = new Telcel.R9.Estructura.AccesoDatos.OTorresEstructuraEntities())
                {
                    var resultQuery = context.EmpleadoGetAll().ToList();


                    if (resultQuery != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in resultQuery)
                        {
                            Telcel.R9.Estructura.Negocio.Empleado empleado = new Telcel.R9.Estructura.Negocio.Empleado();
                            empleado.IdEmpleado = obj.IdEmpleado;
                            empleado.Nombre = obj.Nombre;
                            empleado.IdPuesto = obj.IdPuesto;
                            empleado.Puesto = new Telcel.R9.Estructura.Negocio.Puesto();
                            empleado.Puesto.IdPuesto = obj.IdPuesto;
                            empleado.Puesto.Descripcion = obj.DescripcionPuesto;
                            empleado.Departamento = new Telcel.R9.Estructura.Negocio.Departamento();
                            empleado.Departamento.IdDepartamento = obj.IdDepatarmento;
                            empleado.Departamento.Descripcion = obj.DescripcionDepartamento;


                            result.Objects.Add(empleado);
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

        public static Telcel.R9.Estructura.Negocio.Result GetAllEF(Telcel.R9.Estructura.Negocio.Empleado empleadoBusqueda)
        {
            Telcel.R9.Estructura.Negocio.Result result = new Telcel.R9.Estructura.Negocio.Result();
            empleadoBusqueda.Nombre = (empleadoBusqueda.Nombre != null) ? empleadoBusqueda.Nombre : "";
            try
            {
                using (Telcel.R9.Estructura.AccesoDatos.OTorresEstructuraEntities context = new Telcel.R9.Estructura.AccesoDatos.OTorresEstructuraEntities())
                {
                    var resultQuery = context.EmpeladoGetBy(empleadoBusqueda.Nombre).ToList();


                    if (resultQuery != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in resultQuery)
                        {
                            Telcel.R9.Estructura.Negocio.Empleado empleado = new Telcel.R9.Estructura.Negocio.Empleado();
                            empleado.IdEmpleado = obj.IdEmpleado;
                            empleado.Nombre = obj.Nombre;
                            empleado.IdPuesto = obj.IdPuesto;
                            empleado.Puesto = new Telcel.R9.Estructura.Negocio.Puesto();
                            empleado.Puesto.IdPuesto = obj.IdPuesto;
                            empleado.Puesto.Descripcion = obj.DescripcionPuesto;
                            empleado.Departamento = new Telcel.R9.Estructura.Negocio.Departamento();
                            empleado.Departamento.IdDepartamento = obj.IdDepatarmento;
                            empleado.Departamento.Descripcion = obj.DescripcionDepartamento;

                            result.Objects.Add(empleado);
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

        public static Telcel.R9.Estructura.Negocio.Result GetByIdEF(int Idempleado)
        {
            Telcel.R9.Estructura.Negocio.Result result = new Telcel.R9.Estructura.Negocio.Result();

            try
            {
                using (Telcel.R9.Estructura.AccesoDatos.OTorresEstructuraEntities context = new Telcel.R9.Estructura.AccesoDatos.OTorresEstructuraEntities())
                {
                    var objEmpleado = context.EmpleadoGetById(Idempleado).FirstOrDefault();

                    result.Objects = new List<object>();

                    if (objEmpleado != null)
                    {
                        Telcel.R9.Estructura.Negocio.Empleado empleado = new Telcel.R9.Estructura.Negocio.Empleado();
                        empleado.IdEmpleado = objEmpleado.IdEmpleado;
                        empleado.Nombre = objEmpleado.Nombre;
                        empleado.Puesto = new Telcel.R9.Estructura.Negocio.Puesto();
                        empleado.Puesto.IdPuesto = objEmpleado.IdPuesto.Value;
                        empleado.Departamento = new Telcel.R9.Estructura.Negocio.Departamento();
                        empleado.Departamento.IdDepartamento = objEmpleado.IdDepartamento.Value;


                        result.Object = empleado;
                        result.Correct = true;

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

        public static Telcel.R9.Estructura.Negocio.Result AddEF(Telcel.R9.Estructura.Negocio.Empleado empleado)
        {
            Telcel.R9.Estructura.Negocio.Result result = new Telcel.R9.Estructura.Negocio.Result();
            try
            {
                using (Telcel.R9.Estructura.AccesoDatos.OTorresEstructuraEntities context = new Telcel.R9.Estructura.AccesoDatos.OTorresEstructuraEntities())
                {
                    var resultQuery = context.EmpleadoAdd(empleado.Nombre, empleado.Puesto.IdPuesto, empleado.Departamento.IdDepartamento);

                    if (resultQuery > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se puede añadir la direccion " + empleado.Nombre;
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
        public static Telcel.R9.Estructura.Negocio.Result UpdateEF(Telcel.R9.Estructura.Negocio.Empleado empleado)
        {
            Telcel.R9.Estructura.Negocio.Result result = new Telcel.R9.Estructura.Negocio.Result();

            try
            {
                using (Telcel.R9.Estructura.AccesoDatos.OTorresEstructuraEntities context = new Telcel.R9.Estructura.AccesoDatos.OTorresEstructuraEntities())
                {
                    var updateResult = context.EmpleadoUpdate(empleado.IdEmpleado, empleado.Nombre, empleado.Puesto.IdPuesto, empleado.Departamento.IdDepartamento);

                    if (updateResult >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se actualizó el empleado" + empleado.Nombre;
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


        public static Telcel.R9.Estructura.Negocio.Result DeleteEF(Telcel.R9.Estructura.Negocio.Empleado empleado)
        {
            Telcel.R9.Estructura.Negocio.Result result = new Telcel.R9.Estructura.Negocio.Result();

            try
            {
                using (Telcel.R9.Estructura.AccesoDatos.OTorresEstructuraEntities context = new Telcel.R9.Estructura.AccesoDatos.OTorresEstructuraEntities())
                {
                    var updateResult = context.EmpleadoDelete(empleado.IdEmpleado);
                    if (updateResult >= 1)
                    {
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al elimino el empleado " + empleado.Nombre;
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

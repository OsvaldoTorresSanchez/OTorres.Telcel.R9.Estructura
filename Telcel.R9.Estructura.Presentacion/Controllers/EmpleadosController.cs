using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Telcel.R9.Estructura.Negocio;

namespace Telcel.R9.Estructura.Presentacion.Controllers
{
    public class EmpleadosController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            Telcel.R9.Estructura.Negocio.Empleado empleado = new Telcel.R9.Estructura.Negocio.Empleado();
            empleado.Empleados = new List<object>();

            Telcel.R9.Estructura.Negocio.Result result = Telcel.R9.Estructura.Negocio.Empleado.GetAllEF();

            if (result.Correct)
            {
                empleado.Empleados = result.Objects.ToList();
                //alumno.Alumnos = result.Objects;
            }
            else
            {
                ViewBag.Message = result.ErrorMessage; //Mandar datos a Controller hacia la vista
            }
            return View(empleado);
        }

        [HttpPost]
        public ActionResult GetAll(Telcel.R9.Estructura.Negocio.Empleado empleado)
        {

            empleado.Empleados = new List<object>();


            Telcel.R9.Estructura.Negocio.Result result = Telcel.R9.Estructura.Negocio.Empleado.GetAllEF(empleado);

            if (result.Correct)
            {
                empleado.Empleados = result.Objects.ToList();
                //empleado.Empleados = result.Objects;
            }
            else
            {
                ViewBag.Message = result.ErrorMessage; //Mandar datos a Controller hacia la vista
            }
            return View(empleado);
        }

        [HttpGet]
        public ActionResult Form(int? IdEmpleado)
        {
            Telcel.R9.Estructura.Negocio.Empleado empleado = new Telcel.R9.Estructura.Negocio.Empleado();
            //get de Rol

            Telcel.R9.Estructura.Negocio.Result resultPuesto = Telcel.R9.Estructura.Negocio.Puesto.GetAllEF();
            empleado.Puesto = new Telcel.R9.Estructura.Negocio.Puesto();
            empleado.Puesto.Puestos = resultPuesto.Objects;

            Telcel.R9.Estructura.Negocio.Result resultDepartamento = Telcel.R9.Estructura.Negocio.Departamento.GetAllEF();
            empleado.Departamento = new Telcel.R9.Estructura.Negocio.Departamento();
            empleado.Departamento.Departamentos = resultDepartamento.Objects;

            if (IdEmpleado == null) // Add
            {
                return View(empleado);
            }
            else //Update
            {
                Telcel.R9.Estructura.Negocio.Result result = Telcel.R9.Estructura.Negocio.Empleado.GetByIdEF((int)IdEmpleado);

                if (result.Correct)
                {
                    empleado = (Telcel.R9.Estructura.Negocio.Empleado)result.Object;
                      empleado.Puesto.Puestos = resultPuesto.Objects;
                      empleado.Departamento.Departamentos = resultDepartamento.Objects;
                }

                return View(empleado); // Vacio
            }
        }
        [HttpPost]
        public ActionResult Form(Telcel.R9.Estructura.Negocio.Empleado empleado)
        {

            if (empleado.IdEmpleado == 0)
            {
                Telcel.R9.Estructura.Negocio.Result result = Telcel.R9.Estructura.Negocio.Empleado.AddEF(empleado);

                if (result.Correct)
                {
                    ViewBag.Message = "Se ha ingresado correctamente el alumno";

                }
                else
                {
                    ViewBag.Message = "no se ingresado correctemnte el alumno , Error: " + result.ErrorMessage;
                }
            }
            else
            {
                Telcel.R9.Estructura.Negocio.Result result = Telcel.R9.Estructura.Negocio.Empleado.UpdateEF(empleado);
                if (result.Correct)
                {

                    ViewBag.Message = "Se ha actualizado correctamente el alumno";

                }
                else
                {
                    ViewBag.Message = "no se actualizado correctemnte el alumno , Error: " + result.ErrorMessage;
                }
            }
            return PartialView("Modal");

            //return View(empleado);

        }

        public ActionResult Delete(Telcel.R9.Estructura.Negocio.Empleado empleado)
        {
            Telcel.R9.Estructura.Negocio.Result result = Telcel.R9.Estructura.Negocio.Empleado.DeleteEF(empleado);

            if (result.Correct)
            {
                ViewBag.Message = "Se ha eliminado correctamente el usuario";
            }

            else
            {
                ViewBag.Message = "no se puede eliminar correctemnte el usuario , Error: " + result.ErrorMessage;
            }

            return PartialView("Modal");
        }

        [HttpGet]
        public ActionResult UploadData()
        {
            Telcel.R9.Estructura.Negocio.Empleado empleado = new Telcel.R9.Estructura.Negocio.Empleado();

            return View(empleado);
        }

        [HttpPost]
        public ActionResult UploadData(Telcel.R9.Estructura.Negocio.Empleado empleado, HttpPostedFileBase cargaMasiva)
        {
            // Obtén la extensión del archivo
            Telcel.R9.Estructura.Negocio.Result result = new Telcel.R9.Estructura.Negocio.Result();
            // if (cargaMasiva == null)
            //{
            //if (cargaMasiva == null)
            // {

            //  }
            //string fileExtension = Path.GetExtension(cargaMasiva.FileName);
            if (cargaMasiva != null)
            {
                if (Session["ListEmpleados"] == null)
                {
                    string direccionExcel = Server.MapPath("~/CargaMasiva/TXT/") + Path.GetFileNameWithoutExtension(cargaMasiva.FileName) + '-' + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";

                    cargaMasiva.SaveAs(direccionExcel);

                    empleado = PrevisualizarTxt(empleado, cargaMasiva);
                    Session["ListEmpleados"] = empleado.Empleados;


                }
                else
                {
                    var lista = (List<Object>)Session["ListEmpleados"];
                    empleado.Empleados = lista;
                    CargaDatos(empleado);
                    Session.Remove("ListEmpleados");
                    ViewBag.Message = "Datos cargados ";
                    return PartialView("Modal");

                    // ViewBag.Message = "No se puede cargar los datos ";
                    // return PartialView("Modal");

                }

            }
            else
            {

            }
            //}
            return View(empleado);
        }

        public static Telcel.R9.Estructura.Negocio.Empleado PrevisualizarTxt(Telcel.R9.Estructura.Negocio.Empleado empleado, HttpPostedFileBase cargaMasiva)
        {
            empleado.Empleados = new List<object>();
            using (StreamReader file = new StreamReader(cargaMasiva.InputStream))
            {
                try
                {
                    string line;
                    line = file.ReadLine();
                    while ((line = file.ReadLine()) != null)
                    {
                        var campos = line.Split('|'); // Dividir la línea en campos usando '|'

                        Telcel.R9.Estructura.Negocio.Empleado empleadoItem = new Telcel.R9.Estructura.Negocio.Empleado();
                        empleadoItem.Nombre = campos[0];
                        empleadoItem.IdPuesto = int.Parse(campos[1]);
                        empleadoItem.IdDepartamento = int.Parse(campos[2]);

                        empleado.Empleados.Add(empleadoItem);

                    }
                }
                catch (IOException ex)
                {

                }
            }
            return empleado;

        }
        public void CargaDatos(Telcel.R9.Estructura.Negocio.Empleado empleado)
        {

            List<string> resultLines = new List<string>();

            foreach (Telcel.R9.Estructura.Negocio.Empleado empleadoItem in empleado.Empleados)
            {
                Telcel.R9.Estructura.Negocio.Result result = Telcel.R9.Estructura.Negocio.Empleado.AddEF(empleadoItem);
                if (result.Correct)
                {
                    resultLines.Add("La inserccion del Empleado" + empleado.Nombre + "Fue exitoso");
                    result.Correct = true;
                }
                else
                {
                    resultLines.Add("Hubo un erro al actualizar de Empleado" + empleado.Nombre + "Error" + result.ErrorMessage);
                    result.Correct = false;

                }
            }

            var fecha = DateTime.Now.ToString("dd-MM-yyyy HHmmss");
            string path = Server.MapPath("~/CargaMasiva/TXT/" + "CargaMasivaTxt_" + fecha + ".txt");

            using (StreamWriter archvioError = System.IO.File.CreateText(path))
            {
                foreach (string line in resultLines)
                {
                    archvioError.WriteLine(line);
                }
            }
        }
    }
}
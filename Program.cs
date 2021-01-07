using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.Entidades;
using CoreEscuela.Util;
using static System.Console;

namespace CoreEscuela
{
    class Program
    {
        static void Main(string[] args)
        { 
            var engine = new EscuelaEngine(); 
            engine.Inicializar();
            Printer.DibujarTitulo("Bienvenidos a la escuela");
            //ImprimirCursosEscuela(engine.Escuela);

            var diccTemp = engine.GetDiccionarioObjetos();

            engine.ImprimirDiccionario(diccTemp, true);

            // Para qeu funciones debo colocar los parametros de salida y asignar que valores deseo que traiga.
            // var listaObjetos = engine.GetObjetosEscuela(
            //     out int conteoEvaluaciones,
            //     out int conteoAlumnos,
            //     out int conteoAsignaturas,
            //     out int conteoCursos,
            //     traeEvaluaciones: true,
            //     traeAlumnos: true,
            //     traeAsignaturas: true,
            //     traeCursos: true
            //     );

            // --- ESTA INTERFAZ TRAE LISTADO DE ESCUELA, ALUMNOS, CURSOS, ASIGNATURAS, EVALUACIONES --- Pasando los parametros como flase no los traera.
            // var listaObjetos = engine.GetObjetosEscuela();

            // --- INTERFAZ PARA LISTAR ALUMNOS ---
            // var listaAlumno =   from obj in listaObjetos
            //                     where obj is Alumno 
            //                     select (Alumno) obj;

            // ---   EJECUTA EL METODO PARA LIMPIAR ESCUELA   ---
            // engine.Escuela.LimpiarLugar();
        }

        private static void ImprimirCursosEscuela(Escuela escuela)
        {

            Printer.DibujarTitulo("Cursos de la escuela");

            if (escuela != null && escuela.Cursos != null)
            {
                foreach (var curso in escuela.Cursos)
                {
                    WriteLine($"Nombre {curso.Nombre} , Id {curso.UniqueId}");
                }
            }
        }
        // private static void ImprimirCursos(Curso[] arregloCursos)
        // {
        //     int contador = 0;
        //     while (contador < arregloCursos.Length)
        //     {
        //         Console.WriteLine($"Nombre {arregloCursos[contador].Nombre}, Id {arregloCursos[contador].UniqueId}");
        //         contador += 1;
        //     }
        // }
    }
}

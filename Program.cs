using System;
using System.Collections.Generic;
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

            ImprimirCursosEscuela(engine.Escuela);
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

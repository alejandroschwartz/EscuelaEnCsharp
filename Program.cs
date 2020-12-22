using System;
using CoreEscuela.Entidades;
using static System.Console;

namespace Etapa1
{
    class Program
    {
        static void Main(string[] args)
        {
            var escuela = new Escuela("Industrial", 1924, TiposEscuela.Secundaria,
                                        ciudad: "Posadas", pais: "Argentina"
                                        );

            escuela.Cursos = new Curso[]
            {
                new Curso() { Nombre = "101" },
                new Curso() { Nombre = "201" },
                new Curso() { Nombre = "301" }
            };

            ImprimirCursosEscuela(escuela);
        }

        private static void ImprimirCursosEscuela(Escuela escuela)
        {
            WriteLine("====================");
            WriteLine("Cursos de la escuela");
            WriteLine("====================");

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

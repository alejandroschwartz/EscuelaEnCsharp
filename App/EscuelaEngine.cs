using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.Entidades;

namespace CoreEscuela
{
    public class EscuelaEngine
    {
        public Escuela Escuela { get; set; }
        public EscuelaEngine()
        {

        }
        public void Inicializar()
        {
            Escuela = new Escuela("Industrial", 1924,
                            TiposEscuela.Secundaria, ciudad: "Posadas",
                            pais: "Argentina");

            CargarCursos();

            foreach (var curso in Escuela.Cursos)
            {
                curso.Alumnos.AddRange(CargarAlumnos());
            }

            CargarAsignaturas();
            
            CargarEvaluaciones();
            
        }

        private void CargarEvaluaciones()
        {
            throw new NotImplementedException();
        }

        private void CargarAsignaturas()
        {
            foreach (var Curso in Escuela.Cursos)
            {
                List<Asignatura> listaAsignaturas = new List<Asignatura>()
                {
                    new Asignatura { Nombre="Matemática" },
                    new Asignatura { Nombre="Física" },
                    new Asignatura { Nombre="Ingles" },
                    new Asignatura { Nombre="Química" }
                }; 
                curso.Asignaturas.AddRange(listaAsignaturas);
            }
        }

        private IEnumerable<Alumno> CargarAlumnos()
        {
            string[] nombre1 = {"Alejandro", "Matías", "Nicolás", "Martín"};
            string[] nombre2 = {"Juan", "José", "Leonardo", "Joaquín"};
            string[] apellido1 = {"Lopez", "Perez", "Molina", "Gonzalez"};

            var listaAlumnos =  from n1 in nombre1
                                from n2 in nombre2
                                from a1 in nombre2
                                select new Alumno { Nombre=$"{n1} {n2} {a1}" };
            return listaAlumnos;
        }

        private void CargarCursos()
        {
            Escuela.Cursos = new List<Curso>()
            {
                new Curso() { Nombre = "101", Jornada = TiposJornada.Mañana },
                new Curso() { Nombre = "201", Jornada = TiposJornada.Mañana },
                new Curso() { Nombre = "301", Jornada = TiposJornada.Mañana },
                new Curso() { Nombre = "102", Jornada = TiposJornada.Tarde },
                new Curso() { Nombre = "202", Jornada = TiposJornada.Tarde },
                new Curso() { Nombre = "302", Jornada = TiposJornada.Tarde }
            };
        }
    }
}
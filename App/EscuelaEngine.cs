using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.Entidades;
using CoreEscuela.Util;


namespace CoreEscuela
{
    public sealed class EscuelaEngine
    {
        public EscuelaEngine(Escuela escuela)
        {
            this.Escuela = escuela;
        }

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
            CargarAsignaturas();
            CargarEvaluaciones();
        }

        #region DICCIONARIO - String, IEnumerable Objetos. Con estructura de constantes. Adicionamos la escuela al diccionario, por cada curso tenemos asignaturas y alumnos, y por cada alumno su conjunto completo de Evaluaciones. 
        public void ImprimirDiccionario(Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> dic,
                                        bool imprEval = false)
        {
            foreach (var objDic in dic)
            {
                Printer.DibujarTitulo(objDic.Key.ToString());

                foreach (var val in objDic.Value)
                {

                    switch ( objDic.Key )
                    {
                        case LlaveDiccionario.Evaluaciones:
                            if( imprEval )
                                Console.WriteLine("Evaluacion: " + val);
                        break;
                        case LlaveDiccionario.Escuela:
                            Console.WriteLine("Escuela: " + val);
                        break;
                        case LlaveDiccionario.Alumnos:
                            Console.WriteLine("Alummo: " + val.Nombre);
                        break;
                        case LlaveDiccionario.Cursos:
                            var cursoTemp = val as Curso;
                            if( cursoTemp != null )
                            {
                                int count = cursoTemp.Alumnos.Count;
                                Console.WriteLine($"Curso: {val.Nombre}, Cantidad alumnos: {count} ");
                            }
                        break;
                        default:
                            Console.WriteLine(val);
                        break;
                    }
                }
            }
        }

        public Dictionary< LlaveDiccionario, IEnumerable< ObjetoEscuelaBase > > GetDiccionarioObjetos()
        {
            var diccionario = new Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>>();

            diccionario.Add(LlaveDiccionario.Escuela, new [] {Escuela});
            diccionario.Add(LlaveDiccionario.Cursos, Escuela.Cursos.Cast<ObjetoEscuelaBase>());

            var listaTempAlum = new List<Alumno>();
            var listaTempAsig = new List<Asignatura>();
            var listaTempEval = new List<Evaluación>();

            foreach (var cur in Escuela.Cursos)
            {
                listaTempAlum.AddRange(cur.Alumnos);
                listaTempAsig.AddRange(cur.Asignaturas);

                foreach (var alum in cur.Alumnos)
                {
                    listaTempEval.AddRange(alum.Evaluaciones);
                }
            }
            diccionario.Add(LlaveDiccionario.Evaluaciones, listaTempEval);
            diccionario.Add(LlaveDiccionario.Alumnos, listaTempAlum);
            diccionario.Add(LlaveDiccionario.Asignaturas, listaTempAsig);
            return diccionario;
        }
        #endregion

        #region DICCIONARIO - String, IEnumerable Objetos. Con estructura de constantes
        // public Dictionary< string, IEnumerable< ObjetoEscuelaBase > > GetDiccionarioObjetos()
        // {
        //     var diccionario = new Dictionary<string, IEnumerable<ObjetoEscuelaBase>>();

        //     diccionario.Add(LlaveDiccionario.ESCUELA, new [] {Escuela});
        //     diccionario.Add(LlaveDiccionario.CURSOS, Escuela.Cursos.Cast<ObjetoEscuelaBase>());

        //     return diccionario;
        // }
        #endregion

        #region MÉTODOS DE CARGA - Cursos, Asignaturas, Evaluaciones
        private void CargarAsignaturas()
        {
            foreach (var curso in Escuela.Cursos)
            {
                var listaAsignaturas = new List<Asignatura>()
                {
                            new Asignatura{Nombre="Matemáticas"} ,
                            new Asignatura{Nombre="Educación Física"},
                            new Asignatura{Nombre="Castellano"},
                            new Asignatura{Nombre="Ciencias Naturales"}
                };
                curso.Asignaturas = listaAsignaturas;
            }
        }

        private void CargarEvaluaciones()
        {
            foreach (var curso in Escuela.Cursos)
            {                
                foreach (var asignatura in curso.Asignaturas)
                {
                    foreach (var alumno in curso.Alumnos)
                    {
                        Random rnd = new Random();

                        for (int i = 0; i < 5; i++)
                        {
                            var ev = new Evaluación
                            {
                                Asignatura = asignatura,
                                Nombre = $"{asignatura.Nombre} Eval# {i + 1}",
                                Nota = MathF.Round( 5 * (float)rnd.NextDouble(), 2),
                                Alumno = alumno
                            };
                            alumno.Evaluaciones.Add(ev);
                        }
                    }
                }
            }

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

            Random rnd = new Random();
            foreach (var c in Escuela.Cursos)
            {
                int cantRnd = rnd.Next(5, 20);
                c.Alumnos = GenerarAlumnosAzar(cantRnd);
            }
        }
        #endregion

        #region LISTA SOLO LECTURA - de ObjetoEscuelaBase con el metodo GetObjetosEscuela con true y conteo.
        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
            out int conteoEvaluaciones,
            out int conteoAlumnos,
            out int conteoAsignaturas,
            out int conteoCursos,
            bool traeEvaluaciones = true,
            bool traeAlumnos = true,
            bool traeAsignaturas = true,
            bool traeCursos = true
        )
        {
            // La lista debe devolver unos parametros de salida. Para que pueda requereilos luego, primero debo inicializarlos.
            conteoEvaluaciones = 0;
            conteoAlumnos = 0;
            conteoAsignaturas = 0;

            var listaObj = new List<ObjetoEscuelaBase>();
                listaObj.Add(Escuela);
                
            if(traeCursos)
                listaObj.AddRange(Escuela.Cursos);

            conteoCursos = Escuela.Cursos.Count;
            foreach (var curso in Escuela.Cursos)
            {
                conteoAsignaturas += curso.Asignaturas.Count; 
                if(traeAsignaturas)
                    listaObj.AddRange(curso.Asignaturas);
                
                conteoAlumnos += curso.Alumnos.Count;
                if(traeAlumnos)
                    listaObj.AddRange(curso.Alumnos);
                    
                if(traeEvaluaciones)
                {
                    foreach (var alumno in curso.Alumnos)
                    {
                        listaObj.AddRange(alumno.Evaluaciones);
                        conteoEvaluaciones += alumno.Evaluaciones.Count;
                    }
                }
            }
            return listaObj.AsReadOnly();
        }
        #endregion

        #region LISTA - de ObjetoEscuelaBase con el metodo GetObjetosEscuela
        // ---   METODO QUE TRAE OBJETOS DE LA ESCUELA   --- No necesito sobrecarga de metodos porque es absorvida por la anterios al cargar los bool con true.
        // public List<ObjetoEscuelaBase> GetObjetosEscuela()
        // {
        //     var listaObj = new List<ObjetoEscuelaBase>();
        //         listaObj.Add(Escuela);
        //         listaObj.AddRange(Escuela.Cursos);
        //         foreach (var curso in Escuela.Cursos)
        //         {
        //             listaObj.AddRange(curso.Asignaturas);
        //             listaObj.AddRange(curso.Alumnos);
        //             foreach (var alumno in curso.Alumnos)
        //             {
        //                 listaObj.AddRange(alumno.Evaluaciones);
        //             }
        //         }
        //     return listaObj;
        // }
        #endregion

        #region LISTA - GenerarAlumnosAzar

        private List<Alumno> GenerarAlumnosAzar(int cantidad)
        {
            string[] nombre1 = { "Alejandro", "Matías", "Nicolás", "Martín" };
            string[] nombre2 = { "Juan", "José", "Leonardo", "Joaquín" };
            string[] apellido1 = { "Lopez", "Perez", "Molina", "Gonzalez" };

            var listaAlumnos = from n1 in nombre1
                               from n2 in nombre2
                               from a1 in apellido1
                               select new Alumno { Nombre = $"{n1} {n2} {a1}" };
            return listaAlumnos.OrderBy( (al) => al.UniqueId ).Take(cantidad).ToList();
        }
        #endregion

    }
}
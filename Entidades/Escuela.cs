using System;
using System.Collections.Generic;
using CoreEscuela.Util;

namespace CoreEscuela.Entidades
{
    public class Escuela: ObjetoEscuelaBase, ILugar
    {
        public int AñoCreacion {get; set;} 

        public string Pais { get; set; }
        
        public string Ciudad { get; set; }

        public string Dirección { get; set; }
        
        public TiposEscuela TipoEscuela { get; set; }
        
        public List<Curso> Cursos { get; set; }
        
        public Escuela (string nombre, int año) => (Nombre, AñoCreacion) = (nombre, año);
        
        public Escuela (string nombre, int año, TiposEscuela tipos, 
                        string pais = "", string ciudad = "") 
        {
            (Nombre, AñoCreacion) = (nombre, año);
            Pais = pais;
            Ciudad = ciudad;
        }


        public override string ToString()
        {
            return $"Nombre: {Nombre}, Tipo: {TipoEscuela} {System.Environment.NewLine}Ciudad: {Ciudad}, Pais: {Pais}";
        }

        public void LimpiarLugar()
        {
            Printer.DibujarLinea();
            Console.WriteLine("Limpiando escuela...");
            foreach (var curso in Cursos)
            {
                curso.LimpiarLugar();
            }
            Printer.DibujarTitulo($"Escuela {Nombre} limpia.");
        }
    }
}
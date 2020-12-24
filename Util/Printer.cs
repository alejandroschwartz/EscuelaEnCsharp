using static System.Console;

namespace CoreEscuela.Util
{
    public static class Printer
    {
        public static void DibujarLinea(int tam = 10)
        {
            WriteLine("".PadLeft(tam, '='));
        }
        public static void DibujarTitulo(string titulo)
        {
            DibujarLinea(titulo.Length);
            WriteLine(titulo);
            DibujarLinea(titulo.Length);
        }
    }
}
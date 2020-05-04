using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Analizador_Lenguaje
{
    public static class LecturaEntradas
    {
        //Leer la información de los archivos 
        public static void ImportarArchivos(ref Dictionary<string, string> Sets, ref Dictionary<string, string> Tokens, ref Dictionary<string, string> Actions, ref List<int> EstadosAceptación, ref List<string> NombreSimbolos)
        {
            string Ubicación = Directory.GetCurrentDirectory();
            Ubicación += "\\Archivos";
            using (StreamReader LeerSets = new StreamReader(Ubicación + "\\Sets.txt"))
            {
                string Linea = string.Empty;
                while ((Linea = LeerSets.ReadLine()) != null)
                {
                    ClasificarSets(Linea, ref Sets);
                }
            }
            using (StreamReader LeerTokens = new StreamReader(Ubicación + "\\Tokens.txt"))
            {
                string Linea = string.Empty;
                while ((Linea = LeerTokens.ReadLine()) != null)
                {
                    ClasificarEntrada(Linea, ref Tokens);
                }
            }
            using (StreamReader LeerAcciones = new StreamReader(Ubicación + "\\Actions.txt"))
            {
                string Linea = string.Empty;
                while ((Linea = LeerAcciones.ReadLine()) != null)
                {
                    ClasificarAcciones(Linea, ref Actions);
                }
            }
            using (StreamReader LeerEstadosAceptación = new StreamReader(Ubicación + "\\EstadosAceptación.txt"))
            {
                string Linea = string.Empty;
                while ((Linea = LeerEstadosAceptación.ReadLine()) != null)
                {
                    EstadosAceptación.Add(Convert.ToInt32(Linea));
                }
            }
            using (StreamReader LeerSímbolosT = new StreamReader(Ubicación + "\\SímbolosTerminales.txt"))
            {
                string Linea = string.Empty;
                while ((Linea = LeerSímbolosT.ReadLine()) != null)
                {
                    if (Linea != "#")
                    {
                        NombreSimbolos.Add(Linea);
                    }
                }
            }
        }
        //Clasificar la información de los sets
        public static void ClasificarSets(string Linea, ref Dictionary<string, string> Sets)
        {
            int PosiciónIgual = 0;
            string[] Secciones = new string[2];
            char[] Letras = Linea.ToCharArray();
            foreach (char Letra in Letras)
            {
                if (Letra != '=')
                {
                    PosiciónIgual++;
                }
                if (Letra == '=')
                {
                    break;
                }
            }
            for (int i = 0; i < PosiciónIgual; i++)
            {
                Secciones[0] += Letras[i];
            }
            for (int i = PosiciónIgual + 1; i < Letras.Length; i++)
            {
                Secciones[1] += Letras[i];
            }
            List<int> PosiciónPuntos = new List<int>();
            List<int> PosiciónComillas = new List<int>();
            List<int> PosiciónMás = new List<int>();
            List<int> PosiciónSímbolosCaracter = new List<int>();
            List<int> PosiciónParentesis = new List<int>();
            List<int> ValorCHR = new List<int>();
            bool ComillaSímbolo = true;
            char[] Valor = Secciones[1].ToCharArray();
            for (int i = 0; i < Valor.Length; i++)
            {
                if (Valor[i] == '.')
                {
                    PosiciónPuntos.Add(i);
                }
                else if (Valor[i] == '\'')
                {
                    if (PosiciónComillas.Count > 0 && (PosiciónComillas[PosiciónComillas.Count - 1]) + 1 == i && ComillaSímbolo == false)
                    {
                        PosiciónSímbolosCaracter.Add(i);
                        ComillaSímbolo = true;
                    }
                    else
                    {
                        PosiciónComillas.Add(i);
                        ComillaSímbolo = false;
                    }
                }
                else if (Valor[i] == '+')
                {
                    PosiciónMás.Add(i);
                }
                else if (Secciones[1].Contains("CHR"))
                {
                    if (Valor[i] == '(' || Valor[i] == ')')
                    {
                        PosiciónParentesis.Add(i);
                    }
                }
                else
                {
                    PosiciónSímbolosCaracter.Add(i);
                }
            }
            string CaracteresSet = string.Empty;
            int PosiciónCaracter1 = 0;
            int PosiciónCaracter2 = 0;
            int ValorCaracter1 = 0;
            int ValorCaracter2 = 0;
            if (PosiciónSímbolosCaracter.Count > 0)
            {
                while (PosiciónSímbolosCaracter.Count > 0)
                {
                    PosiciónCaracter1 = PosiciónSímbolosCaracter.First();
                    ValorCaracter1 = Convert.ToInt32(Valor[PosiciónCaracter1]);
                    if (PosiciónPuntos.Count > 0 && PosiciónMás.Count > 0 && PosiciónPuntos.First() < PosiciónMás.First())
                    {
                        PosiciónSímbolosCaracter.RemoveAt(0);
                        PosiciónPuntos.RemoveAt(0);
                        PosiciónPuntos.RemoveAt(0);
                        PosiciónCaracter2 = PosiciónSímbolosCaracter.First();
                        PosiciónSímbolosCaracter.RemoveAt(0);
                        ValorCaracter2 = Convert.ToInt32(Valor[PosiciónCaracter2]);
                        if (ValorCaracter1 < ValorCaracter2)
                        {
                            for (int i = ValorCaracter1; i <= ValorCaracter2; i++)
                            {
                                CaracteresSet += Convert.ToChar(i);
                            }
                        }
                        else
                        {
                            for (int i = ValorCaracter1; i <= 255; i++)
                            {
                                CaracteresSet += Convert.ToChar(i);
                            }
                            for (int i = 0; i <= ValorCaracter2; i++)
                            {
                                CaracteresSet += Convert.ToChar(i);
                            }
                        }
                    }
                    else if (PosiciónPuntos.Count > 0 && PosiciónMás.Count > 0 && PosiciónPuntos.First() > PosiciónMás.First())
                    {
                        if (PosiciónSímbolosCaracter.First() < PosiciónMás.First())
                        {
                            PosiciónSímbolosCaracter.RemoveAt(0);
                            CaracteresSet += Convert.ToChar(ValorCaracter1);
                        }
                        PosiciónMás.RemoveAt(0);
                    }
                    else if (PosiciónMás.Count > 0)
                    {
                        if (PosiciónSímbolosCaracter.First() < PosiciónMás.First())
                        {
                            PosiciónSímbolosCaracter.RemoveAt(0);
                            CaracteresSet += Convert.ToChar(ValorCaracter1);
                        }
                        PosiciónMás.RemoveAt(0);
                    }
                    else if (PosiciónPuntos.Count > 0)
                    {
                        PosiciónSímbolosCaracter.RemoveAt(0);
                        PosiciónPuntos.RemoveAt(0);
                        PosiciónPuntos.RemoveAt(0);
                        PosiciónCaracter2 = PosiciónSímbolosCaracter.First();
                        PosiciónSímbolosCaracter.RemoveAt(0);
                        ValorCaracter2 = Convert.ToInt32(Valor[PosiciónCaracter2]);
                        if (ValorCaracter1 < ValorCaracter2)
                        {
                            for (int i = ValorCaracter1; i <= ValorCaracter2; i++)
                            {
                                CaracteresSet += Convert.ToChar(i);
                            }
                        }
                        else
                        {
                            for (int i = ValorCaracter1; i <= 255; i++)
                            {
                                CaracteresSet += Convert.ToChar(i);
                            }
                            for (int i = 0; i <= ValorCaracter2; i++)
                            {
                                CaracteresSet += Convert.ToChar(i);
                            }
                        }
                    }
                    else
                    {
                        PosiciónSímbolosCaracter.RemoveAt(0);
                        CaracteresSet += Convert.ToChar(ValorCaracter1);
                    }
                }
            }
            if (PosiciónParentesis.Count > 0)
            {
                while (PosiciónParentesis.Count > 0)
                {
                    string ValorNumero = string.Empty;
                    int Parentesisabre = PosiciónParentesis.First();
                    PosiciónParentesis.RemoveAt(0);
                    int ParentesisCierra = PosiciónParentesis.First();
                    PosiciónParentesis.RemoveAt(0);
                    for (int i = Parentesisabre + 1; i < ParentesisCierra; i++)
                    {
                        ValorNumero += Valor[i];
                    }
                    ValorCHR.Add(Convert.ToInt32(ValorNumero));
                }
                while (ValorCHR.Count > 0)
                {
                    if (PosiciónPuntos.Count > 0 && PosiciónMás.Count > 0 && PosiciónPuntos.First() < PosiciónMás.First())
                    {
                        ValorCaracter1 = ValorCHR.First();
                        ValorCHR.RemoveAt(0);
                        ValorCaracter2 = ValorCHR.First();
                        ValorCHR.RemoveAt(0);
                        if (ValorCaracter1 < ValorCaracter2)
                        {
                            for (int i = ValorCaracter1; i <= ValorCaracter2; i++)
                            {
                                CaracteresSet += Convert.ToChar(i);
                            }
                        }
                        else
                        {
                            for (int i = ValorCaracter1; i <= 255; i++)
                            {
                                CaracteresSet += Convert.ToChar(i);
                            }
                            for (int i = 0; i <= ValorCaracter2; i++)
                            {
                                CaracteresSet += Convert.ToChar(i);
                            }
                        }
                    }
                    else if (PosiciónPuntos.Count > 0 && PosiciónMás.Count > 0 && PosiciónPuntos.First() > PosiciónMás.First())
                    {
                        if (ValorCHR.First() < PosiciónMás.First())
                        {
                            CaracteresSet += Convert.ToChar(ValorCHR.First());
                            ValorCHR.RemoveAt(0);
                        }
                        PosiciónMás.RemoveAt(0);
                    }
                    else if (PosiciónMás.Count > 0)
                    {
                        if (ValorCHR.First() < PosiciónMás.First())
                        {
                            CaracteresSet += Convert.ToChar(ValorCHR.First());
                            ValorCHR.RemoveAt(0);
                        }
                        PosiciónMás.RemoveAt(0);
                    }
                    else if (PosiciónPuntos.Count > 0)
                    {
                        ValorCaracter1 = ValorCHR.First();
                        ValorCHR.RemoveAt(0);
                        ValorCaracter2 = ValorCHR.First();
                        ValorCHR.RemoveAt(0);
                        if (ValorCaracter1 < ValorCaracter2)
                        {
                            for (int i = ValorCaracter1; i <= ValorCaracter2; i++)
                            {
                                CaracteresSet += Convert.ToChar(i);
                            }
                        }
                        else
                        {
                            for (int i = ValorCaracter1; i <= 255; i++)
                            {
                                CaracteresSet += Convert.ToChar(i);
                            }
                            for (int i = 0; i <= ValorCaracter2; i++)
                            {
                                CaracteresSet += Convert.ToChar(i);
                            }
                        }
                    }
                    else
                    {
                        CaracteresSet += Convert.ToChar(ValorCHR.First());
                        ValorCHR.RemoveAt(0);
                    }
                }
            }
            Sets.Add(Secciones[0], CaracteresSet);
        }
        //Clasificar la información del token leído
        public static void ClasificarEntrada(string Linea, ref Dictionary<string, string> Diccionario)
        {
            Linea = Linea.TrimStart("TOKEN".ToCharArray());
            int PosiciónIgual = 0;
            foreach (char Letra in Linea)
            {
                if (Letra != '=')
                {
                    PosiciónIgual++;
                }
                else
                {
                    break;
                }
            }
            string[] Secciones = new string[2];
            char[] Letras = Linea.ToCharArray();
            for (int i = 0; i < PosiciónIgual; i++)
            {
                Secciones[0] += Letras[i];
            }
            for (int i = PosiciónIgual + 1; i < Linea.Length; i++)
            {
                Secciones[1] += Letras[i];
            }
            Letras = Secciones[1].ToCharArray();
            string Valor = string.Empty;
            int PosiciónLinea = 0;
            for (int i = 0; i < Letras.Length; i++)
            {
                if (PosiciónLinea < (Letras.Length) / 2)
                {
                    if (Letras[i] != '\'')
                    {
                        if (!Valor.EndsWith(Letras[i]))
                        {
                            Valor += Letras[i];
                        }
                    }
                    else
                    {
                        if (Letras[i] == '\'' && Letras[i + 2] == '\'')
                        {
                            if (!Valor.EndsWith(Letras[i + 1]))
                            {
                                Valor += Letras[i + 1];
                            }
                        }
                    }
                    PosiciónLinea++;
                }
                else
                {
                    if (Letras[i] != '\'')
                    {
                        if (!Valor.EndsWith(Letras[i]))
                        {
                            Valor += Letras[i];
                        }
                    }
                    else
                    {
                        if (Letras[i - 2] == '\'' && Letras[i] == '\'')
                        {
                            if (!Valor.EndsWith(Letras[i - 1]))
                            {
                                Valor += Letras[i - 1];
                            }
                        }
                    }
                }
            }
            Diccionario.Add(Secciones[0], Valor);
        }
        //Clasificar el contenido de las acciones
        public static void ClasificarAcciones(string Linea, ref Dictionary<string, string> Actions)
        {
            int Contador = 0;
            char[] Letra = Linea.ToCharArray();
            foreach (char Caracter in Letra)
            {
                if (Caracter != '=')
                {
                    Contador++;
                }
                else
                {
                    break;
                }
            }
            string[] Secciones = new string[2];
            for (int i = 0; i < Contador; i++)
            {
                Secciones[0] += Letra[i];
            }
            for (int i = Contador + 1; i < Letra.Length; i++)
            {
                Secciones[1] += Letra[i];
            }
            string Valor = Secciones[1];
            Valor = Valor.Trim('\'');
            Actions.Add(Secciones[0], Valor);
        }
        //Definir los símbolos que se pueden usar
        public static void DefinirSímbolos(ref List<string> Simbolos, ref Dictionary<string, string> Sets, ref Dictionary<string, string> SímbolosOrdenados)
        {
            foreach (string Llave in Simbolos)
            {
                if (Llave != "#")
                {
                    if (!Sets.ContainsKey(Llave))
                    {
                        Sets.Add(Llave, Llave);
                    }
                }
            }
            int Contador = Simbolos.Count - 1;
            for (int i = Contador; i >= 0; i--)
            {
                SímbolosOrdenados.Add(Simbolos[i], Sets[Simbolos[i]]);
            }
            Sets = null;
            Simbolos = null;
        }
        //Lista de número y token
        public static void ObtenerLista(ref Dictionary<string, string> Tokens, ref Dictionary<string, string> Actions, ref SortedList<int, string> NúmerosToken)
        {
            bool AgregarActions = false;
            foreach (KeyValuePair<string, string> Token in Tokens)
            {
                int Número = Convert.ToInt32(Token.Key);
                NúmerosToken.Add(Número, Token.Value);
                if (Token.Value.Contains("{RESERVADAS()}"))
                {
                    AgregarActions = true;
                }
            }
            if (AgregarActions)
            {
                foreach (KeyValuePair<string, string> Action in Actions)
                {
                    int Número = Convert.ToInt32(Action.Key);
                    NúmerosToken.Add(Número, Action.Value);
                }
                Tokens = null;
                Actions = null;
            }
        }
        //Menú de inicio
        public static void Menú(ref int Opción, ref string Cadena)
        {

            Console.WriteLine("Elija una opción");
            Opción = Convert.ToInt32(Console.ReadLine());
            switch (Opción)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("Recuerde que sólo se leerá el texto de la primera fila del archivo" + Environment.NewLine + "Arrastre el archivo sobre a leer sobre esta ventana");
                    string UbicaciónArchivo = Console.ReadLine();
                    UbicaciónArchivo = UbicaciónArchivo.Trim('"');
                    Cadena = LecturaEntradas.Leerarchivo(UbicaciónArchivo);
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Escriba la cadena de texto que desea verificar, cuando termine presione \"Enter\"");
                    Cadena = Console.ReadLine();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("La opción no es válida");
                    Menú(ref Opción, ref Cadena);
                    break;
            }
        }
        //Leer cadena de archivo
        public static string Leerarchivo(string Dirección)
        {
            string Linea = string.Empty;
            while (!Dirección.EndsWith(".txt"))
            {
                Console.WriteLine("El archivo ingresado no es válido." + Environment.NewLine + "Por favor ingrese un archivo .txt");
                Dirección = Console.ReadLine();
            }
            using (StreamReader ArchivoEntrada = new StreamReader(Dirección))
            {

                Linea = ArchivoEntrada.ReadLine();
            }
            return Linea;
        }
    }
}

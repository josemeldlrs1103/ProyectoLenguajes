using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fase2LFA
{
    public static class Fase3
    {
        //Copiar carpeta solución genérica a ubicación carpeta programa generado
        public static void CopiarContenido(string DirecciónFuente, string DirecciónDestino)
        {
            //Buscar las subdirecciones en el folder indicado
            DirectoryInfo DirectorioFuente = new DirectoryInfo(DirecciónFuente);
            if (!DirectorioFuente.Exists)
            {
                throw new DirectoryNotFoundException("No se puede encontrar el directorio fuente: " + DirecciónFuente);
            }
            DirectoryInfo[] Directorios = DirectorioFuente.GetDirectories();
            if (!Directory.Exists(DirecciónDestino))
            {
                Directory.CreateDirectory(DirecciónDestino);
            }
            FileInfo[] Archivos = DirectorioFuente.GetFiles();
            foreach (FileInfo Archivo in Archivos)
            {
                string DirecciónTemporal1 = Path.Combine(DirecciónDestino, Archivo.Name);
                Archivo.CopyTo(DirecciónTemporal1, false);
            }
            foreach (DirectoryInfo SubDirectorio in Directorios)
            {
                string DirecciónTemporal2 = Path.Combine(DirecciónDestino, SubDirectorio.Name);
                CopiarContenido(SubDirectorio.FullName, DirecciónTemporal2);
            }

        }
        //Sustituir las listas de enteros en el diccionario del autómata, por los nombres de los estados
        public static void SimplificarAutomata(ref Queue<List<int>> EstadosVisitados, ref Dictionary<int, List<int>> NombresEstado, ref Dictionary<List<int>, Dictionary<string, List<int>>> EstadosAnalizados, ref List<string> SimbolosUsados, ref Dictionary<int, Dictionary<string, string>> EstadosSimplificados)
        {
            int Contador = 0;
            foreach (List<int> Listado in EstadosVisitados)
            {
                NombresEstado.Add(Contador, Listado);
                Contador++;
            }
            foreach (KeyValuePair<int, List<int>> NumeroEstado in NombresEstado)
            {
                EstadosSimplificados.Add(NumeroEstado.Key, CrearDiccionarioEstados(SimbolosUsados));
            }
            string EstadoN = string.Empty;
            foreach (List<int> Listado in EstadosVisitados)
            {
                foreach (KeyValuePair<int, List<int>> NEstado in NombresEstado)
                {
                    if (Listado.SequenceEqual(NEstado.Value))
                    {
                        EstadoN = (NEstado.Key).ToString();
                        break;
                    }
                }
                foreach (string Símbolo in SimbolosUsados)
                {
                    List<int> NúmerosTransición = EstadosAnalizados[Listado][Símbolo];
                    string auxiliar = string.Empty;
                    foreach (KeyValuePair<int, List<int>> NEstado in NombresEstado)
                    {
                        if (NúmerosTransición.SequenceEqual(NEstado.Value) && NEstado.Value.Count > 0)
                        {
                            auxiliar = (NEstado.Key).ToString();
                            break;
                        }
                    }
                    EstadosSimplificados[Convert.ToInt32(EstadoN)][Símbolo] = auxiliar;
                }
            }
        }
        //Definición de diccionario usado para los símbolos de cada estado
        public static Dictionary<string, string> CrearDiccionarioEstados(List<string> SimbolosUsados)
        {
            Dictionary<string, string> SimbolosEstado = new Dictionary<string, string>();
            string NumeroEstado = string.Empty;
            foreach (string Simbolo in SimbolosUsados)
            {
                SimbolosEstado.Add(Simbolo, NumeroEstado);
            }
            return SimbolosEstado;
        }
        //Definir estados de aceptación
        public static void BuscarEstadosAceptacion(Queue<Nodo> NodosHoja, Dictionary<int, List<int>> NombresEstado, ref List<int> EstadosFinales)
        {
            int NumeroSímboloFinal = 0;
            foreach (Nodo Hoja in NodosHoja)
            {
                if (Hoja.Caracter == "#")
                {
                    NumeroSímboloFinal = Hoja.First.First();
                }
            }
            foreach (KeyValuePair<int, List<int>> Estado in NombresEstado)
            {
                if (Estado.Value.Contains(NumeroSímboloFinal))
                {
                    EstadosFinales.Add(Estado.Key);
                }
            }
        }
        //Exportar arreglos con la información requerida para analisis de entrada de texto
        public static void ExportarArreglos(ArrayList SetsEx, ArrayList TokensEx, ArrayList ActionsEx, List<int> EstadosFinales, ArrayList SimbolosT, string Dirección)
        {
            Dirección += "\\Analizador_Lenguaje\\bin\\Debug\\netcoreapp3.1\\Archivos";
            using (StreamWriter EscribirSets = new StreamWriter(Dirección + "\\Sets.txt"))
            {
                foreach (string Linea in SetsEx)
                {
                    EscribirSets.WriteLine(Linea);
                }
            }
            using (StreamWriter EscribirTokens = new StreamWriter(Dirección + "\\Tokens.txt"))
            {
                foreach (string Linea in TokensEx)
                {
                    EscribirTokens.WriteLine(Linea);
                }
            }
            using (StreamWriter EscribirActions = new StreamWriter(Dirección + "\\Actions.txt"))
            {
                foreach (string Linea in ActionsEx)
                {
                    EscribirActions.WriteLine(Linea);
                }
            }
            using (StreamWriter EscribirEstados = new StreamWriter(Dirección + "\\EstadosAceptación.txt"))
            {
                foreach (int Linea in EstadosFinales)
                {
                    EscribirEstados.WriteLine(Linea);
                }
            }
            using (StreamWriter EscribirSímbolos = new StreamWriter(Dirección + "\\SímbolosTerminales.txt"))
            {
                foreach (string Linea in SimbolosT)
                {
                    EscribirSímbolos.WriteLine(Linea);
                }
            }
        }
        public static void GenerarCodigo(ref Queue<string> LineasEscribir, List<string> SimbolosUsados, Dictionary<int, Dictionary<string, string>> EstadosSimplificados)
        {
            string[] SimbolosT = SimbolosUsados.ToArray();
            LineasEscribir.Enqueue("using System;");
            LineasEscribir.Enqueue("using System.Collections;");
            LineasEscribir.Enqueue("using System.Collections.Generic;");
            LineasEscribir.Enqueue("namespace Analizador_Lenguaje");
            LineasEscribir.Enqueue("{");
            LineasEscribir.Enqueue("    class Program");
            LineasEscribir.Enqueue("    {");
            LineasEscribir.Enqueue("        //Listas que contienen la información importada desde las fases anteriores del proyecto");
            LineasEscribir.Enqueue("        public static Dictionary<string, string> Sets = new Dictionary<string, string>();");
            LineasEscribir.Enqueue("        public static List<string> NombresSets = new List<string>();");
            LineasEscribir.Enqueue("        public static Dictionary<string, string> Tokens = new Dictionary<string, string>();");
            LineasEscribir.Enqueue("        public static Dictionary<string, string> TokensVariados = new Dictionary<string, string>();");  
            LineasEscribir.Enqueue("        public static Dictionary<string, Dictionary<string, string>> Actions = new Dictionary<string, Dictionary<string, string>>();");
            LineasEscribir.Enqueue("        public static List<int> EstadosAceptación = new List<int>();");
            LineasEscribir.Enqueue("        public static List<string> NombreSimbolos = new List<string>();");
            LineasEscribir.Enqueue("        public static Dictionary<string, string> SimbolosPermitidos= new Dictionary<string, string>();");
            LineasEscribir.Enqueue("        static void Main(string[] args)");
            LineasEscribir.Enqueue("        {");
            LineasEscribir.Enqueue("            LecturaEntradas.ImportarArchivos(ref Sets, ref Tokens, ref Actions, ref EstadosAceptación, ref NombreSimbolos, ref NombresSets);");
            LineasEscribir.Enqueue("            LecturaEntradas.DefinirSímbolos(ref NombreSimbolos, ref Sets, ref SimbolosPermitidos);");
            LineasEscribir.Enqueue("            Console.WriteLine(\"Escoja un método para ingresar su cadena de texto\" + Environment.NewLine + \"1 - Archivo de texto\" + Environment.NewLine + \"2 - Escribir cadena en consola\");");
            LineasEscribir.Enqueue("            string Cadena = string.Empty;");
            LineasEscribir.Enqueue("            int Opción = 0;");
            LineasEscribir.Enqueue("            LecturaEntradas.Menú(ref Opción, ref Cadena);");
            LineasEscribir.Enqueue("            string[] EntradasCadena = Cadena.Split(' ');");
            LineasEscribir.Enqueue("            string PalabraRechazada = string.Empty;");
            LineasEscribir.Enqueue("            bool PalabraAceptada = true;");
            LineasEscribir.Enqueue("            foreach(string Palabra in EntradasCadena)");
            LineasEscribir.Enqueue("            {");
            LineasEscribir.Enqueue("                PalabraAceptada = true;");
            LineasEscribir.Enqueue("                int Estado = 0;");
            LineasEscribir.Enqueue("                char[] CaracteresCadena = Palabra.ToCharArray();");
            LineasEscribir.Enqueue("                foreach (char Caracter in CaracteresCadena)");
            LineasEscribir.Enqueue("                {");
            LineasEscribir.Enqueue("                    switch(Estado)");
            LineasEscribir.Enqueue("                    {");
            foreach (KeyValuePair<int, Dictionary<string, string>> Estado in EstadosSimplificados)
            {
                bool PrimeraCondición = true;
                LineasEscribir.Enqueue("                        case " + Estado.Key + ":");


                for (int i = SimbolosUsados.Count - 1; i >= 0; i--)
                {
                    if (Estado.Value[SimbolosT[i]] != "")
                    {
                        if (PrimeraCondición)
                        {
                            if (SimbolosT[i].StartsWith("\\") && SimbolosT[i].EndsWith("\\"))
                            {
                                string auxiliar = SimbolosT[i];
                                auxiliar = auxiliar.Trim('\\');
                                LineasEscribir.Enqueue("                            if(SimbolosPermitidos[\"" + auxiliar + "\"].Contains(Caracter))");
                                LineasEscribir.Enqueue("                            {");
                                LineasEscribir.Enqueue("                                Estado=" + Estado.Value[SimbolosT[i]] + ";");
                                LineasEscribir.Enqueue("                            }");
                                PrimeraCondición = false;
                            }
                            if (SimbolosT[i] == "\"")
                            {
                                LineasEscribir.Enqueue("                            if(SimbolosPermitidos[\"\\" + SimbolosT[i] + "\"].Contains(Caracter))");
                                LineasEscribir.Enqueue("                            {");
                                LineasEscribir.Enqueue("                                Estado=" + Estado.Value[SimbolosT[i]] + ";");
                                LineasEscribir.Enqueue("                            }");
                                PrimeraCondición = false;
                            }
                            else if (SimbolosT[i] != "\"" &&(!SimbolosT[i].StartsWith("\\") && !SimbolosT[i].EndsWith("\\")))
                            {
                                LineasEscribir.Enqueue("                            if(SimbolosPermitidos[\"" + SimbolosT[i] + "\"].Contains(Caracter))");
                                LineasEscribir.Enqueue("                            {");
                                LineasEscribir.Enqueue("                                Estado=" + Estado.Value[SimbolosT[i]] + ";");
                                LineasEscribir.Enqueue("                            }");
                                PrimeraCondición = false;
                            }
                        }
                        else
                        {
                            if (SimbolosT[i].StartsWith("\\") && SimbolosT[i].EndsWith("\\"))
                            {
                                string auxiliar = SimbolosT[i];
                                auxiliar = auxiliar.Trim('\\');
                                LineasEscribir.Enqueue("                            else if(SimbolosPermitidos[\"" + auxiliar + "\"].Contains(Caracter))");
                                LineasEscribir.Enqueue("                            {");
                                LineasEscribir.Enqueue("                                Estado=" + Estado.Value[SimbolosT[i]] + ";");
                                LineasEscribir.Enqueue("                            }");
                                PrimeraCondición = false;
                            }
                            else if (SimbolosT[i] == "\"")
                            {
                                LineasEscribir.Enqueue("                            else if(SimbolosPermitidos[\"\\" + SimbolosT[i] + "\"].Contains(Caracter))");
                                LineasEscribir.Enqueue("                            {");
                                LineasEscribir.Enqueue("                                Estado=" + Estado.Value[SimbolosT[i]] + ";");
                                LineasEscribir.Enqueue("                            }");
                                PrimeraCondición = false;
                            }
                            else if (SimbolosT[i] != "\"" && (!SimbolosT[i].StartsWith("\\") && !SimbolosT[i].EndsWith("\\")))
                            {
                                LineasEscribir.Enqueue("                            else if(SimbolosPermitidos[\"" + SimbolosT[i] + "\"].Contains(Caracter))");
                                LineasEscribir.Enqueue("                            {");
                                LineasEscribir.Enqueue("                                Estado=" + Estado.Value[SimbolosT[i]] + ";");
                                LineasEscribir.Enqueue("                            }");
                                PrimeraCondición = false;
                            }
                        }
                    }
                }
                LineasEscribir.Enqueue("                            break;");
            }
            LineasEscribir.Enqueue("                        default:");
            LineasEscribir.Enqueue("                            Console.WriteLine(\"Uno de los caracteres en la cadena no es correcto\");");
            LineasEscribir.Enqueue("                            break;");
            LineasEscribir.Enqueue("                    }");
            LineasEscribir.Enqueue("                }");
            LineasEscribir.Enqueue("                if(!EstadosAceptación.Contains(Estado))");
            LineasEscribir.Enqueue("                {");
            LineasEscribir.Enqueue("                    PalabraAceptada = false;");
            LineasEscribir.Enqueue("                    PalabraRechazada = Palabra;");
            LineasEscribir.Enqueue("                    break;");
            LineasEscribir.Enqueue("                }");
            LineasEscribir.Enqueue("            }");
            LineasEscribir.Enqueue("            if(!PalabraAceptada)");
            LineasEscribir.Enqueue("            {");
            LineasEscribir.Enqueue("                Console.Clear();");
            LineasEscribir.Enqueue("                Console.WriteLine(\"La cadena que usted ingresó ha sido rechazada, se encontró un error cerca del simbolo\");");
            LineasEscribir.Enqueue("                Console.ForegroundColor = ConsoleColor.Red;");
            LineasEscribir.Enqueue("                Console.ForegroundColor = ConsoleColor.White;");
            LineasEscribir.Enqueue("                Console.WriteLine(\"Asegurese que no hayan espacios entre el símbolo actual y el siguiente símbolo\");");
            LineasEscribir.Enqueue("                Console.ReadKey();");
            LineasEscribir.Enqueue("            }");
            LineasEscribir.Enqueue("            else");
            LineasEscribir.Enqueue("            {");
            LineasEscribir.Enqueue("                Console.Clear();");
            LineasEscribir.Enqueue("                Console.ForegroundColor = ConsoleColor.Green;");
            LineasEscribir.Enqueue("                Console.WriteLine(\"La cadena ha sido aceptada\");");
            LineasEscribir.Enqueue("                Console.ForegroundColor = ConsoleColor.DarkGreen;");
            LineasEscribir.Enqueue("                Console.WriteLine(\"Las palabra ingresadas pertenecen a los siguientes tokens:\");");
            LineasEscribir.Enqueue("                LecturaEntradas.SepararTokensVariables(NombresSets, ref Tokens, ref TokensVariados);");
            LineasEscribir.Enqueue("                LecturaEntradas.BuscarNúmeroToken(EntradasCadena, Sets, Tokens,  Actions, SimbolosPermitidos, TokensVariados);");
            LineasEscribir.Enqueue("                Console.ReadKey();");
            LineasEscribir.Enqueue("            }");
            LineasEscribir.Enqueue("        }");
            LineasEscribir.Enqueue("    }");
            LineasEscribir.Enqueue("}");
        }
        //Escribir Main programa generado
        public static void EscribirMain(Queue<string> LineasEscribir, string Direccion)
        {
            Direccion += "\\Analizador_Lenguaje\\Program.cs";
            using (FileStream Archivo = new FileStream(Direccion, FileMode.Create))
            {
            }
            using (StreamWriter Main = new StreamWriter(Direccion, true))
            {
                while (LineasEscribir.Count > 0)
                {
                    Main.WriteLine(LineasEscribir.Dequeue());
                }
            }
        }


    }
}

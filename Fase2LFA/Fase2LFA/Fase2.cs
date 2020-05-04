using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fase2LFA
{
    public static class Fase2
    {
        //Obtener el nombre de los sets que se encontraron y guardarlos como símbolos terminales
        public static void ObtenerSímbolosSets(ref ArrayList SetsF2, ref ArrayList SimbolosT)
        {
            foreach (string Linea in SetsF2)
            {
                string NombreSet = "";
                char[] Letras = Linea.ToCharArray();
                foreach (char Letra in Letras)
                {
                    if (Letra != '=')
                    {
                        NombreSet += Letra;
                    }
                    else
                    {
                        break;
                    }
                }
                SimbolosT.Add(NombreSet);
            }
        }
        //Obtener valores Tokens
        public static void LimpiarTokens(ref ArrayList Tokens, ref ArrayList TokensF2)
        {
            for (int i = 0; i < Tokens.Count; i++)
            {
                char[] Letras = (Tokens[i].ToString()).ToCharArray();
                int IndiceIgual = 0;
                foreach (char Letra in Letras)
                {
                    if (Letra != '=')
                    {
                        IndiceIgual++;
                    }
                    else
                    {
                        break;
                    }
                }
                string ValorToken = "";
                for (int j = IndiceIgual + 1; j < Letras.Length; j++)
                {
                    ValorToken += Letras[j];
                }
                //Eliminar el contenido entre llaves de la línea 
                if (ValorToken.Contains("{") && ValorToken.Contains("}"))
                {
                    bool LlaveAbierta = false;
                    char[] Caracteres = ValorToken.ToCharArray();
                    ValorToken = string.Empty;
                    foreach (char Caracter in Caracteres)
                    {
                        if (Caracter != '{' && Caracter != '}' && !LlaveAbierta)
                        {
                            ValorToken += Caracter;
                        }
                        if (Caracter == '{')
                        {
                            LlaveAbierta = true;
                        }
                        if (Caracter == '}')
                        {
                            LlaveAbierta = false;
                        }
                    }
                }
                TokensF2.Add(ValorToken);
            }
        }
        //Verificar que se Tokens use sets definidos en la sección de Sets
        public static bool BuscarSets(ref ArrayList Tokens, ref ArrayList SimbolosT)
        {
            ArrayList IndicesMayusculas = new ArrayList();
            foreach(string Linea in Tokens)
            {
                IndicesMayusculas.Clear();
                char[] Letras = Linea.ToCharArray();
                for(int i=0;i<Letras.Length;i++)
                {
                    if(char.IsUpper(Letras[i])&&char.IsLetter(Letras[i]))
                    {
                        IndicesMayusculas.Add(i);
                    }
                }
                if(IndicesMayusculas.Count>0)
                {
                    string PalabraBuscada = "";
                    int ValorEsperado = 0;
                    for(int i=0; i<IndicesMayusculas.Count-1;i++)
                    {
                        if((Convert.ToInt32(IndicesMayusculas[i]))+1 == (Convert.ToInt32(IndicesMayusculas[i+1])))
                        {
                            ValorEsperado = Convert.ToInt32(IndicesMayusculas[i + 1]);
                            PalabraBuscada += Letras[Convert.ToInt32(IndicesMayusculas[i])];
                            if(SimbolosT.Contains(PalabraBuscada))
                            {
                                PalabraBuscada = string.Empty;
                            }
                        }
                        else
                        {
                            if(ValorEsperado!=0)
                            {
                                PalabraBuscada += Letras[ValorEsperado];
                                if(SimbolosT.Contains(PalabraBuscada))
                                {
                                    PalabraBuscada = string.Empty;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }
                    }
                    if (ValorEsperado != 0)
                    {
                        PalabraBuscada += Letras[ValorEsperado];
                        if (SimbolosT.Contains(PalabraBuscada))
                        {
                            PalabraBuscada = string.Empty;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        //Agregar Símbolos terminales de la sección Tokens
        public static bool ObtenerSimbolosTokens(ref ArrayList Tokens, ref ArrayList SimbolosT)
        {
            foreach(string Linea in Tokens)
            {
                ArrayList IndicesApostrofes = new ArrayList();
                char[] Letras = Linea.ToCharArray();
                for(int i =0; i<Letras.Length;i++)
                {
                    if(Letras[i]=='\'')
                    {
                        IndicesApostrofes.Add(i);
                    }
                }
                if (IndicesApostrofes.Count >= 2)
                {
                    while(IndicesApostrofes.Count>0)
                    {
                        if ((Convert.ToInt32(IndicesApostrofes[0]) + 2 == Convert.ToInt32(IndicesApostrofes[1]))|| (Convert.ToInt32(IndicesApostrofes[0]) + 2 == Convert.ToInt32(IndicesApostrofes[2])&& Convert.ToInt32(IndicesApostrofes[0])+1==Convert.ToInt32(IndicesApostrofes[1])))
                        {
                            int Indice = Convert.ToInt32(IndicesApostrofes[0]) + 1;
                            if (Letras[Indice] != '(' && Letras[Indice] != '+' && Letras[Indice] != '?' && Letras[Indice] != '*' && Letras[Indice] != ')' && Letras[Indice] != '|' && Letras[Indice] != '.' && Letras[Indice] != '\'' && Letras[Indice] != '#')
                            {
                                if (!SimbolosT.Contains(Letras[Indice].ToString()))
                                {
                                    SimbolosT.Add(Letras[Indice].ToString());
                                }
                                IndicesApostrofes.RemoveRange(0, 2);
                            }
                            else if(Letras[Indice] == '\'')
                            {
                                if (!SimbolosT.Contains(Letras[Indice].ToString()))
                                {
                                    SimbolosT.Add(Letras[Indice].ToString());    
                                }
                                IndicesApostrofes.RemoveRange(0, 3);
                            }
                            else
                            {
                                if (!SimbolosT.Contains("\\"+Letras[Indice].ToString()+"\\"))
                                {
                                    string SímboloEscapado = "\\" + Letras[Indice] + "\\";
                                    SimbolosT.Add(SímboloEscapado);
                                }
                                IndicesApostrofes.RemoveRange(0, 2);
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            if (!SimbolosT.Contains("#"))
            {
                SimbolosT.Add("#");
            }
            return true;
        }
        //Adaptar el valor de cada token para la expresión regular
        public static void ParsearTokens(ref ArrayList Tokens, ref ArrayList SimbolosT)
        {
            for (int i = 0; i < Tokens.Count; i++)
            {
                string LineaAuxiliar = Tokens[i].ToString();
                char[] Letras = LineaAuxiliar.ToCharArray();
                string LineaAdaptada = "";
                while (LineaAuxiliar.Length > 0)
                {
                    if (Letras[0] != '|' && Letras[0] != ')' && Letras[0] != '+' && Letras[0] != '*' && Letras[0] != '?')
                    {
                        if (LineaAdaptada != string.Empty)
                        {
                            LineaAdaptada += ".";
                        }
                    }
                    else
                    {
                        LineaAdaptada += Letras[0];
                        LineaAuxiliar = LineaAuxiliar.Remove(0, 1);
                        Letras = LineaAuxiliar.ToCharArray();
                    }
                    if (Letras.Length > 0)
                    {
                        if (Letras[0] == '(')
                        {
                            if (!LineaAdaptada.EndsWith("(")&& !LineaAdaptada.EndsWith("|")&&!LineaAdaptada.EndsWith("."))
                            {
                                if (LineaAdaptada != string.Empty)
                                {
                                    LineaAdaptada += ".";
                                }
                            }
                            LineaAdaptada += Letras[0];
                            LineaAuxiliar = LineaAuxiliar.Remove(0, 1);
                            Letras = LineaAuxiliar.ToCharArray();
                        }
                        if (Letras[0] == '\'')
                        {
                            if (Letras[2] == '\'')
                            {
                                if (Letras[1] != '+' && Letras[1] != '*' && Letras[1] != '?' && Letras[1] != '|' && Letras[1] != ')' && Letras[1] != '(' && Letras[1] != '.' && Letras[1] != '#')
                                {
                                    if (!LineaAdaptada.EndsWith("(") && !LineaAdaptada.EndsWith("|") && !LineaAdaptada.EndsWith("."))
                                    {
                                        if (LineaAdaptada != string.Empty)
                                        {
                                            LineaAdaptada += ".";
                                        }
                                    }
                                    LineaAdaptada += Letras[1];
                                    LineaAuxiliar = LineaAuxiliar.Remove(0, 3);
                                    Letras = LineaAuxiliar.ToCharArray();
                                }
                                else
                                {
                                    if (!LineaAdaptada.EndsWith("(") && !LineaAdaptada.EndsWith("|") && !LineaAdaptada.EndsWith("."))
                                    {
                                        if (LineaAdaptada != string.Empty)
                                        {
                                            LineaAdaptada += ".";
                                        }
                                    }
                                    LineaAdaptada += "\\" + Letras[1] + "\\";
                                    LineaAuxiliar = LineaAuxiliar.Remove(0, 3);
                                    Letras = LineaAuxiliar.ToCharArray();
                                }
                            }
                        }
                        else if (char.IsLetter(Letras[0]) && char.IsUpper(Letras[0]))
                        {
                            foreach (string Palabra in SimbolosT)
                            {
                                if (LineaAuxiliar.StartsWith(Palabra))
                                {
                                    if (!LineaAdaptada.EndsWith("(") && !LineaAdaptada.EndsWith("|") && !LineaAdaptada.EndsWith("."))
                                    {
                                        if (LineaAdaptada != string.Empty)
                                        {
                                            LineaAdaptada += ".";
                                        }
                                    }
                                    LineaAdaptada += Palabra;
                                    LineaAuxiliar = LineaAuxiliar.Remove(0, Palabra.Length);
                                    Letras = LineaAuxiliar.ToCharArray();
                                    break;
                                }
                            }
                        }
                    }
                }
                Tokens[i] = LineaAdaptada;
            }
        }
        //Cargar los tokens a la cola para armar arbol de expresión
        public static void CargarColaExpresion(ref ArrayList Tokens,ref ArrayList SimbolosT, ref Queue<string> ColaEx)
        {
            ColaEx.Clear();
            List<char> Letras = new List<char>();
            ColaEx.Enqueue("(");
            int CantidadO = Tokens.Count - 1;
            foreach (string Linea in Tokens)
            {
                string Simbolo = string.Empty;
                Letras = Linea.ToList<char>();
                while (Letras.Count > 0)
                {
                    if (Letras[0] == '\\' && Letras[2] == '\\')
                    { 
                        if (SimbolosT.Contains("\\" + Letras[1].ToString() + "\\"))
                        {
                            ColaEx.Enqueue("\\" + Letras[1].ToString() + "\\");
                            Simbolo = string.Empty;
                        }
                        Letras.RemoveRange(0, 3);
                    }
                    else
                    {
                        if (Letras[0] == '(' || Letras[0] == '+' || Letras[0] == '*' || Letras[0] == '?' || Letras[0] == ')' || Letras[0] == '.' || Letras[0] == '|')
                        {
                            ColaEx.Enqueue(Letras[0].ToString());
                            Letras.RemoveAt(0);
                        }
                        else if (Letras.Count > 0)
                        {
                            Simbolo += Letras[0];
                            Letras.RemoveAt(0);
                            if (Letras.Count > 0)
                            {
                                if (!char.IsLetter(Letras[0]))
                                {
                                    if (SimbolosT.Contains(Simbolo))
                                    {
                                        ColaEx.Enqueue(Simbolo);
                                        Simbolo = string.Empty;
                                    }
                                }
                            }
                            else
                            {
                                if (SimbolosT.Contains(Simbolo))
                                {
                                    ColaEx.Enqueue(Simbolo);
                                    Simbolo = string.Empty;
                                }
                            }
                        }
                    }
                }
                if (CantidadO > 0)
                {
                    ColaEx.Enqueue("|");
                    CantidadO--;
                }
            }
            ColaEx.Enqueue(")");
            ColaEx.Enqueue(".");
            ColaEx.Enqueue("#");
        }
        //Buscar las cadenas de los simbolos terminales usados
        public static void DescartarSimbolosRepetidos(ref List<string> SimbolosUsados, ref Queue<Nodo> NodosHoja)
        {
            foreach(Nodo Hoja in NodosHoja)
            {
                if(!SimbolosUsados.Contains(Hoja.Caracter)&& Hoja.Caracter!="#")
                {
                    SimbolosUsados.Add(Hoja.Caracter);
                }
            }
        }
        //Calcular transiciones
        public static void BuscarTransiciones(ref List<int> EstadoInicial , ref Queue<Nodo> NodosHoja, ref List<string> SimbolosUsados, ref Queue<List<int>> EstadosNuevos, ref Queue<List<int>> EstadosVisitados, ref Dictionary<List<int>, Dictionary<string,List<int>>> EstadosAnalizados)
        {
            EstadosNuevos.Enqueue(EstadoInicial);
            while (EstadosNuevos.Count > 0)
            {
                List<int> NumerosNodo = EstadosNuevos.Dequeue();
                EstadosAnalizados.Add(NumerosNodo, CrearDiccionarioSimbolos(ref SimbolosUsados));
                EstadosVisitados.Enqueue(NumerosNodo);
                foreach (int NumNodo in NumerosNodo)
                {
                    foreach (Nodo Hoja in NodosHoja)
                    {
                        if (Hoja.First.First() == NumNodo)
                        {
                            string CaracterHoja = Hoja.Caracter;
                            if (CaracterHoja != "#")
                            {
                                EstadosAnalizados[NumerosNodo][CaracterHoja] = Hoja.Follow;
                                bool EstadoNuevo = false;
                                bool EstadoVisitado = false;
                                foreach (List<int> Numeros in EstadosNuevos)
                                {
                                    if (Numeros.SequenceEqual(Hoja.Follow))
                                    {
                                        EstadoNuevo = true;
                                    }
                                }
                                foreach (List<int> Numeros1 in EstadosVisitados)
                                {
                                    if (Numeros1.SequenceEqual(Hoja.Follow))
                                    {
                                        EstadoVisitado = true;
                                    }
                                }
                                if (EstadoNuevo == false && EstadoVisitado == false)
                                {
                                    EstadosNuevos.Enqueue(Hoja.Follow);
                                }
                            }
                            break;
                        }
                    }
                }
                
            }
        }
        //Definición de diccionario usado para los símbolos de cada estado
        public static Dictionary<string, List<int>> CrearDiccionarioSimbolos(ref List<string> SimbolosUsados)
        {
            Dictionary<string, List<int>> SimbolosEstado = new Dictionary<string, List<int>>();
            List<int> NodosSimbolo = new List<int>();
            foreach (string Simbolo in SimbolosUsados)
            {
                SimbolosEstado.Add(Simbolo, NodosSimbolo);
            }
            return SimbolosEstado;
        }
        //Copiar carpeta solución genérica a ubicación carpeta programa generado
        public static void CopiarContenido (string DirecciónFuente, string DirecciónDestino)
        {
            //Buscar las subdirecciones en el folder indicado
            DirectoryInfo DirectorioFuente = new DirectoryInfo(DirecciónFuente);
            if(!DirectorioFuente.Exists)
            {
                throw new DirectoryNotFoundException("No se puede encontrar el directorio fuente: " + DirecciónFuente);
            }
            DirectoryInfo[] Directorios = DirectorioFuente.GetDirectories();
            if(!Directory.Exists(DirecciónDestino))
            {
                Directory.CreateDirectory(DirecciónDestino);
            }
            FileInfo[] Archivos = DirectorioFuente.GetFiles();
            foreach(FileInfo Archivo in Archivos)
            {
                string DirecciónTemporal1 = Path.Combine(DirecciónDestino, Archivo.Name);
                Archivo.CopyTo(DirecciónTemporal1, false);
            }
            foreach(DirectoryInfo SubDirectorio in Directorios)
            {
                string DirecciónTemporal2 = Path.Combine(DirecciónDestino, SubDirectorio.Name);
                CopiarContenido(SubDirectorio.FullName, DirecciónTemporal2);
            }

        }
        //Sustituir las listas de enteros en el diccionario del autómata, por los nombres de los estados
        public static void SimplificarAutomata(ref Queue<List<int>> EstadosVisitados, ref Dictionary<int, List<int>> NombresEstado, ref Dictionary<List<int>, Dictionary<string, List<int>>> EstadosAnalizados, ref List<string> SimbolosUsados, ref Dictionary<int, Dictionary<string,string>> EstadosSimplificados)
        {
            int Contador = 0;
            foreach (List<int> Listado in EstadosVisitados)
            {
                NombresEstado.Add(Contador, Listado);
                Contador++;
            } 
            foreach(KeyValuePair<int, List<int>> NumeroEstado in NombresEstado)
            {
                EstadosSimplificados.Add(NumeroEstado.Key,CrearDiccionarioEstados(SimbolosUsados));
            }
            string EstadoN = string.Empty;
            foreach(List<int> Listado in EstadosVisitados)
            {
                foreach (KeyValuePair<int, List<int>> NEstado in NombresEstado)
                {
                    if(Listado.SequenceEqual(NEstado.Value))
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
                        if (NúmerosTransición.SequenceEqual(NEstado.Value)&&NEstado.Value.Count>0)
                        {
                            auxiliar= (NEstado.Key).ToString();
                            break;
                        }
                    }
                    EstadosSimplificados[Convert.ToInt32(EstadoN)][Símbolo] = auxiliar; 
                }
            }
        }
        //Definición de diccionario usado para los símbolos de cada estado
        public static Dictionary<string,string> CrearDiccionarioEstados(List<string> SimbolosUsados)
        {
            Dictionary<string,string> SimbolosEstado = new Dictionary<string,string>();
            string NumeroEstado= string.Empty;
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
            foreach(Nodo Hoja in NodosHoja)
            {
                if(Hoja.Caracter == "#")
                {
                    NumeroSímboloFinal = Hoja.First.First();
                }
            }
            foreach(KeyValuePair<int, List<int>> Estado in NombresEstado)
            {
                if(Estado.Value.Contains(NumeroSímboloFinal))
                {
                    EstadosFinales.Add(Estado.Key);
                }
            }
        }
        //Exportar arreglos con la información requerida para analisis de entrada de texto
        public static void ExportarArreglos(ArrayList SetsEx, ArrayList TokensEx, ArrayList ActionsEx, List<int> EstadosFinales, ArrayList SimbolosT , string Dirección)
        {
            Dirección += "\\Analizador_Lenguaje\\bin\\Debug\\netcoreapp3.1\\Archivos";
            using (StreamWriter EscribirSets = new StreamWriter(Dirección+"\\Sets.txt"))
            {
                foreach(string Linea in SetsEx)
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
                    if (Linea != "{" && Linea != "}" && Linea != "RESERVADAS()")
                    {
                        EscribirActions.WriteLine(Linea);
                    }
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
        //Arreglo que contiene las líneas de código a escribir
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
            LineasEscribir.Enqueue("        public static Dictionary<string, string> Tokens = new Dictionary<string, string>();");
            LineasEscribir.Enqueue("        public static Dictionary<string, string> Actions = new Dictionary<string, string>();");
            LineasEscribir.Enqueue("        public static List<int> EstadosAceptación = new List<int>();");
            LineasEscribir.Enqueue("        public static List<string> NombreSimbolos = new List<string>();");
            LineasEscribir.Enqueue("        public static Dictionary<string, string> SimbolosPermitidos= new Dictionary<string, string>();");
            LineasEscribir.Enqueue("        public static SortedList<int, string> NúmerosToken = new SortedList<int, string>();");
            LineasEscribir.Enqueue("        static void Main(string[] args)");
            LineasEscribir.Enqueue("        {");
            LineasEscribir.Enqueue("            LecturaEntradas.ImportarArchivos(ref Sets, ref Tokens, ref Actions, ref EstadosAceptación, ref NombreSimbolos);");
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
                        if(PrimeraCondición)
                        {
                            LineasEscribir.Enqueue("                            if(SimbolosPermitidos[\"" + SimbolosT[i] + "\"].Contains(Caracter))");
                            LineasEscribir.Enqueue("                            {");
                            LineasEscribir.Enqueue("                                Estado=" + Estado.Value[SimbolosT[i]] + ";");
                            LineasEscribir.Enqueue("                            }");
                            PrimeraCondición = false;
                        }
                        else
                        {
                            LineasEscribir.Enqueue("                            else if(SimbolosPermitidos[\"" + SimbolosT[i] + "\"].Contains(Caracter))");
                            LineasEscribir.Enqueue("                            {");
                            LineasEscribir.Enqueue("                                Estado=" + Estado.Value[SimbolosT[i]] + ";");
                            LineasEscribir.Enqueue("                            }");
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
            using( FileStream Archivo = new FileStream(Direccion, FileMode.Create))
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


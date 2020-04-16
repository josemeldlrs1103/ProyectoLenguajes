using System;
using System.Collections;
using System.Collections.Generic;
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
        public static void LimpiarTokens(ref ArrayList Tokens)
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
                Tokens[i] = ValorToken;
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
    }
}

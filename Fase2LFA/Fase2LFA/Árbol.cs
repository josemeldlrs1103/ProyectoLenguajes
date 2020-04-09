using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fase2LFA
{
    class Árbol
    {
        //Diccionario de Precedencia de los operadores
        Dictionary<string, int> Precedencias= new Dictionary<string,int>();
        //Generar arbol de expresión
        public Nodo CrearArbol(ref Queue<string> ColaExpresion, ref ArrayList SimbolosT)
        {
            Precedencias.Clear();
            Precedencias.Add("==",8);
            Precedencias.Add("::", 8);
            Precedencias.Add("..", 8);
            Precedencias.Add("\\", 7);
            Precedencias.Add("[", 6);
            Precedencias.Add("]", 6);
            Precedencias.Add("(", 5);
            Precedencias.Add(")", 5);
            Precedencias.Add("*", 4);
            Precedencias.Add("+", 4);
            Precedencias.Add("?", 4);
            Precedencias.Add(".", 3);
            Precedencias.Add("$", 2);
            Precedencias.Add("^", 2);
            Precedencias.Add("|", 1);
            Stack<string> PilaT = new Stack<string>();
            Stack<Nodo> PilaS = new Stack<Nodo>();
            string Token = "";
            while(ColaExpresion.Count>0)
            {
                Token = ColaExpresion.Dequeue();
                if(SimbolosT.Contains(Token))
                {
                    Nodo Nuevo = new Nodo(Token.ToString());
                    PilaS.Push(Nuevo);
                }
                else
                {
                    if(Token=="(")
                    {
                        PilaT.Push(Token);
                    }
                    if (Token == ")")
                    {
                        while(PilaT.Count>=1&&(PilaT.First())!="(")
                        {
                            if(PilaT.Count==0)
                            {
                                MessageBox.Show("Existe un error, faltan operandos");
                            }
                            if (PilaS.Count < 2)
                            {
                                MessageBox.Show("Existe un error, faltan operandos");
                            }
                            else
                            {
                                Nodo Temp = new Nodo((PilaT.Pop()).ToString());
                                Temp.HijoDerecho = PilaS.Pop();
                                Temp.HijoIzquierdo = PilaS.Pop();
                                PilaS.Push(Temp);
                            }
                        }
                        PilaT.Pop();
                    }
                    if (Token == "+" || Token == "?" || Token == "*")
                    {
                        Nodo Nuevo = new Nodo(Token.ToString());
                        if (PilaS.Count < 0)
                        {
                            MessageBox.Show("Hubo un error durante la creación de los árboles.");
                        }
                        else
                        {
                            Nuevo.HijoIzquierdo = PilaS.Pop();
                            PilaS.Push(Nuevo);
                        }
                    }
                    else if (Token == "." || Token == "|")
                    {
                        while ((PilaT.Count > 0) && (PilaT.First() != "(") && (Precedencias[Token.ToString()] <= Precedencias[(PilaT.First()).ToString()]))
                        {
                            if (PilaS.Count < 2)
                            {
                                MessageBox.Show("Surgió un error faltan operandos");
                            }
                            else
                            {
                                Nodo Temp = new Nodo((PilaT.Pop()).ToString());
                                Temp.HijoDerecho = PilaS.Pop();
                                Temp.HijoIzquierdo = PilaS.Pop();
                                PilaS.Push(Temp);
                            }
                        }
                        PilaT.Push(Token);
                    }
                }
            }
            while(PilaT.Count>0)
            {
                Nodo Temp = new Nodo(PilaT.Pop().ToString());
                Temp.HijoDerecho = PilaS.Pop();
                Temp.HijoIzquierdo = PilaS.Pop();
                PilaS.Push(Temp);
            }
            //Aqui se retorna el árbol
            return PilaS.Pop();
        }
        //Contador símbolos terminales
        int NumeroHoja = 1;
        //Añadir First y Last
        public void DefinirNumeroSimbolo (ref Nodo ArbolExpresión, ref Queue<Nodo> NodosHoja)
        {
            if(ArbolExpresión.HijoIzquierdo != null)
            {
                DefinirNumeroSimbolo(ref ArbolExpresión.HijoIzquierdo,ref NodosHoja);
            }
            if (ArbolExpresión.HijoDerecho != null)
            {
                DefinirNumeroSimbolo(ref ArbolExpresión.HijoDerecho, ref NodosHoja);
            }
            if(ArbolExpresión.HijoIzquierdo == null &&ArbolExpresión.HijoDerecho==null)
            {
                ArbolExpresión.First.Add(NumeroHoja);
                ArbolExpresión.Last.Add(NumeroHoja);
                NodosHoja.Enqueue(ArbolExpresión);
                NumeroHoja++;
            }
        }
        //Calcular First Y Last de todos los nodos
        public void NodosFyL (ref Nodo ArbolExpresion, ref Queue<Nodo> NodosArbol)
        {
            if(ArbolExpresion.HijoIzquierdo != null)
            {
                NodosFyL(ref ArbolExpresion.HijoIzquierdo, ref NodosArbol);
            }
            if(ArbolExpresion.HijoDerecho != null)
            {
                NodosFyL(ref ArbolExpresion.HijoDerecho, ref NodosArbol);
            }
            if (ArbolExpresion.Caracter == "+" )
            {
                ArbolExpresion.First = ArbolExpresion.HijoIzquierdo.First;
                ArbolExpresion.Last = ArbolExpresion.HijoIzquierdo.Last;
                if (ArbolExpresion.HijoIzquierdo.Nullable == true)
                {
                    ArbolExpresion.Nullable = true;
                }
                else
                {
                    ArbolExpresion.Nullable = false;
                }
            }
            if (ArbolExpresion.Caracter == "*")
            {
                ArbolExpresion.First = ArbolExpresion.HijoIzquierdo.First;
                ArbolExpresion.Last = ArbolExpresion.HijoIzquierdo.Last;
                ArbolExpresion.Nullable = true;
            }
            if (ArbolExpresion.Caracter == "?")
            {
                ArbolExpresion.First = ArbolExpresion.HijoIzquierdo.First;
                ArbolExpresion.Last = ArbolExpresion.HijoIzquierdo.Last;
                ArbolExpresion.Nullable = true;
            }
            if (ArbolExpresion.Caracter == ".")
            {
                //Definición valores First del Nodo
                if(ArbolExpresion.HijoIzquierdo.Nullable==true)
                {
                    foreach(int FirstI in ArbolExpresion.HijoIzquierdo.First)
                    {
                        if(!ArbolExpresion.First.Contains(FirstI))
                        {
                            ArbolExpresion.First.Add(FirstI);
                        }
                    }
                    foreach(int FirstD in ArbolExpresion.HijoDerecho.First)
                    {
                        if(!ArbolExpresion.First.Contains(FirstD))
                        {
                            ArbolExpresion.First.Add(FirstD);
                        }
                    }
                }
                else
                {
                    foreach (int FirstI in ArbolExpresion.HijoIzquierdo.First)
                    {
                        if (!ArbolExpresion.First.Contains(FirstI))
                        {
                            ArbolExpresion.First.Add(FirstI);
                        }
                    }
                }
                //Definición valores Last Del Nodo
                if(ArbolExpresion.HijoDerecho.Nullable==true)
                {
                    foreach(int LastI in ArbolExpresion.HijoIzquierdo.Last)
                    {
                        if(!ArbolExpresion.Last.Contains(LastI))
                        {
                            ArbolExpresion.Last.Add(LastI);
                        }
                    }
                    foreach(int LastD in ArbolExpresion.HijoDerecho.Last)
                    {
                        if(!ArbolExpresion.Last.Contains(LastD))
                        {
                            ArbolExpresion.Last.Add(LastD);
                        }
                    }
                }
                else
                {
                    foreach (int LastD in ArbolExpresion.HijoDerecho.Last)
                    {
                        if (!ArbolExpresion.Last.Contains(LastD))
                        {
                            ArbolExpresion.Last.Add(LastD);
                        }
                    }
                }
                //Definición Nullable del Nodo
                if(ArbolExpresion.HijoIzquierdo.Nullable== true && ArbolExpresion.HijoDerecho.Nullable==true)
                {
                    ArbolExpresion.Nullable = true;
                }
                else
                {
                    ArbolExpresion.Nullable = false;
                }
            }
            if (ArbolExpresion.Caracter == "|")
            {
                //Definición valores First del Nodo
                foreach (int FirstI in ArbolExpresion.HijoIzquierdo.First)
                {
                    if (!ArbolExpresion.First.Contains(FirstI))
                    {
                        ArbolExpresion.First.Add(FirstI);
                    }
                }
                foreach (int FirstD in ArbolExpresion.HijoDerecho.First)
                {
                    if (!ArbolExpresion.First.Contains(FirstD))
                    {
                        ArbolExpresion.First.Add(FirstD);
                    }
                }
                //Definición valores Last Del Nodo
                foreach (int LastI in ArbolExpresion.HijoIzquierdo.Last)
                {
                    if (!ArbolExpresion.Last.Contains(LastI))
                    {
                        ArbolExpresion.Last.Add(LastI);
                    }
                }
                foreach (int LastD in ArbolExpresion.HijoDerecho.Last)
                {
                    if (!ArbolExpresion.Last.Contains(LastD))
                    {
                        ArbolExpresion.Last.Add(LastD);
                    }
                }
                //Definición Nullable del Nodo
                if (ArbolExpresion.HijoIzquierdo.Nullable == true || ArbolExpresion.HijoDerecho.Nullable == true)
                {
                    ArbolExpresion.Nullable = true;
                }
                else
                {
                    ArbolExpresion.Nullable = false;
                }
            }
            Nodo Auxiliar = new Nodo(ArbolExpresion.Caracter);
            Auxiliar.First = ArbolExpresion.First;
            Auxiliar.Last = ArbolExpresion.Last;
            Auxiliar.Nullable = ArbolExpresion.Nullable;
            NodosArbol.Enqueue(Auxiliar);
        }
        //Calcular Follow de los Nodos
        public void NodosFollow(ref Nodo ArbolExpresion, ref Queue<Nodo> NodosHoja)
        {
            if(ArbolExpresion.HijoIzquierdo!=null)
            {
                NodosFollow(ref ArbolExpresion.HijoIzquierdo, ref NodosHoja);
            }
            if (ArbolExpresion.HijoDerecho != null)
            {
                NodosFollow(ref ArbolExpresion.HijoDerecho, ref NodosHoja);
            }
            if(ArbolExpresion.Caracter=="+"||ArbolExpresion.Caracter=="*")
            {
                foreach(int NumeroNodo in ArbolExpresion.HijoIzquierdo.Last)
                {
                    foreach(Nodo Hoja in NodosHoja)
                    {
                        if (Hoja.First.First() == NumeroNodo)
                        {
                            foreach(int First in ArbolExpresion.HijoIzquierdo.First)
                            {
                                if(!Hoja.Follow.Contains(First))
                                {
                                    Hoja.Follow.Add(First);
                                }
                            }
                            break;
                        }
                    }
                }
            }
            if (ArbolExpresion.Caracter == ".")
            {
                foreach (int NumeroNodo in ArbolExpresion.HijoIzquierdo.Last)
                {
                    foreach (Nodo Hoja in NodosHoja)
                    {
                        if (Hoja.First.First() == NumeroNodo)
                        {
                            foreach (int First in ArbolExpresion.HijoDerecho.First)
                            {
                                if (!Hoja.Follow.Contains(First))
                                {
                                    Hoja.Follow.Add(First);
                                }
                            }
                            break;
                        }
                    }
                }
            }
        }
    }
    public class Nodo
    {
        public string Caracter;
        public Nodo HijoIzquierdo;
        public Nodo HijoDerecho;
        public List<int> First = new List<int>();
        public List<int> Last = new List<int>();
        public List<int> Follow = new List<int>();
        public bool Nullable;
        public Nodo(string Carac)
        {
            Caracter = Carac;
            HijoIzquierdo = null;
            HijoDerecho = null;
            if (Carac == "?" || Carac == "*")
            {
                Nullable = true;
            }
            else
            {
                Nullable = false;
            }
        }
    }
}

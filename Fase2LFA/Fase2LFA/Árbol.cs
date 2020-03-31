using System;
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
        public Nodo CrearArbol(string Expresión)
        {
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
            var Operadores = "(.|+?*)";
            Stack<char> PilaT = new Stack<char>();
            Stack<Nodo> PilaS = new Stack<Nodo>();
            foreach(char Token in Expresión)
            {
                if(!Operadores.Contains(Token))
                {
                    Nodo Nuevo = new Nodo(Token.ToString());
                    PilaS.Push(Nuevo);
                }
                else
                {
                    if(Token=='(')
                    {
                        PilaT.Push(Token);
                    }
                    if (Token == ')')
                    {
                        while(PilaT.Count>=1&&(PilaT.First())!='(')
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
                    if (Operadores.Contains(Token))
                    {
                        if(Token=='+'|| Token == '?'|| Token == '*')
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
                        if(Token == '.' || Token == '|')
                        {
                            if ((PilaT.Count > 0) && (PilaT.First() != '(') && (Precedencias[Token.ToString()] <= Precedencias[(PilaT.First()).ToString()]))
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
    }
    public class Nodo
    {
        public string Caracter;
        public Nodo HijoIzquierdo;
        public Nodo HijoDerecho;
        public Nodo(string Carac)
        {
            Caracter = Carac;
            HijoIzquierdo = null;
            HijoDerecho = null;
        }
    }
}

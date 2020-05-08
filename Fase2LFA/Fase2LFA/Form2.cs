using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fase2LFA
{
    public partial class Form2 : Form
    {
        //Ventana para ubicación de carpeta Analizador_Lenguajes
        FolderBrowserDialog DirecciónPrograma = new FolderBrowserDialog();
        //Listas que contienen la información que se va a exportar a la nueva solución
        public static ArrayList SetsExportar = new ArrayList();
        public static ArrayList TokensExportar = new ArrayList();
        public static ArrayList ActionsExportar = new ArrayList();
        //Arreglos a comprobar
        public static ArrayList SetsF2 = new ArrayList();
        public static ArrayList TokensF2 = new ArrayList();
        //Arreglo que contiene las cadenas que se consideran como símbolos terminales
        public static ArrayList SimbolosT = new ArrayList();
        //Cola que se usa para generar la expresión regular de la que se generará el árbol de expresión
        public static Queue<string> ColaSímbolos = new Queue<string>();
        //Cola que contiene todos los nodos del árbol por separado, se usa para mostrar los valores first, last y nullable de cada nodo
        public static Queue<Nodo> NodosÁrbol = new Queue<Nodo>();
        //Cola que contiene únicamente los nodos hoja, se usa para mostrar los follow de los símbolos terminales
        public static Queue<Nodo> NodosHoja = new Queue<Nodo>();
        //Lista que contiene las cadenas de los símbolos terminales usados
        public static List<string> SimbolosUsados = new List<string>();
        //Cola que contiene los estados descubiertos que han sido analizados
        public static Queue<List<int>> EstadosVisitados = new Queue<List<int>>();
        //Cola que contiene los estados descubiertos que no han sido analizados
        public static Queue<List<int>> EstadosNuevos = new Queue<List<int>>();
        //Diccionarios que contienen los estados y los follows de cada símbolo
        public static Dictionary<List<int>, Dictionary<string, List<int>>> EstadosAnalizados = new Dictionary<List<int>, Dictionary<string, List<int>>>();
        public static Dictionary<int, Dictionary<string,string>> EstadosSimplificados = new Dictionary<int, Dictionary<string,string>>();
        //Diccionario de los nombres de cada estado
        public static Dictionary<int, List<int>> NombresEstado = new Dictionary<int, List<int>>();
        //Lista que contiene el nombre de los estados que se consideran válidos
        public static List<int> EstadosFinales = new List<int>();
        //Cola que contiene las líneas de código a escribir sobre el ejecutable generado
        public static Queue<string> LineasEscribir = new Queue<string>();
        public static void ObtenerListas(ArrayList lista1, ArrayList lista2, ArrayList lista3)
        {
            SetsF2 = lista1;
            TokensExportar = lista2;
            ActionsExportar = lista3;
        }
       
        public Form2()
        {
            InitializeComponent();
        }

        private void btFase2_MouseClick(object sender, MouseEventArgs e)
        {
            Fase2.ObtenerSímbolosSets(ref SetsF2, ref SimbolosT);
            Fase2.LimpiarTokens(ref TokensExportar, ref TokensF2);
            bool SetsUsados = Fase2.BuscarSets(ref TokensF2, ref SimbolosT);
            if(SetsUsados)
            {
                bool SimbolosExtraídos = Fase2.ObtenerSimbolosTokens(ref TokensF2, ref SimbolosT);
                if(SimbolosExtraídos)
                {
                    Fase2.ParsearTokens(ref TokensF2, ref SimbolosT);
                    Fase2.CargarColaExpresion(ref TokensF2, ref SimbolosT, ref ColaSímbolos);
                    Árbol ArExpresión = new Árbol();
                    Nodo Raiz = ArExpresión.CrearArbol(ref ColaSímbolos, ref SimbolosT);
                    ArExpresión.DefinirNumeroSimbolo(ref Raiz, ref NodosHoja);
                    ArExpresión.NodosFyL(ref Raiz, ref NodosÁrbol);
                    ArExpresión.NodosFollow(ref Raiz, ref NodosHoja);
                    Fase2.DescartarSimbolosRepetidos(ref SimbolosUsados, ref NodosHoja);
                    Fase2.BuscarTransiciones(ref Raiz.First, ref NodosHoja, ref SimbolosUsados, ref EstadosNuevos,ref EstadosVisitados, ref EstadosAnalizados);
                    foreach(Nodo Elemento in NodosÁrbol )
                    {
                        string[] ValoresNodo = new string[4];
                        string First = "";
                        if(Elemento.First.Count==1)
                        {
                            First += Elemento.First.First().ToString();
                        }
                        else
                        {
                            foreach(int NumNodo in Elemento.First)
                            {
                                if(First=="")
                                {
                                    First += NumNodo.ToString();
                                }
                                else
                                {
                                    First += "," + NumNodo.ToString();
                                }
                            }
                        }
                        string Last = "";
                        if(Elemento.Last.Count==1)
                        {
                            Last += Elemento.Last.First().ToString();
                        }
                        else
                        {
                            foreach(int NumNodo in Elemento.Last)
                            {
                                if(Last== "")
                                {
                                    Last += NumNodo;
                                }
                                else
                                {
                                    Last += "," + NumNodo;
                                }
                            }
                        }
                        ValoresNodo[0] = Elemento.Caracter;
                        ValoresNodo[1] = First;
                        ValoresNodo[2] = Last;
                        if(Elemento.Nullable == false)
                        {
                            ValoresNodo[3] = "False";
                        }
                        else
                        {
                            ValoresNodo[3] = "True";
                        }
                        dgNodoFLN.Rows.Add(ValoresNodo);
                    }
                    foreach (Nodo Hoja in NodosHoja)
                    {
                        string[] ValoresNodo = new string[2];
                        string NumeroNodo = Hoja.First.First().ToString();
                        string FollowsHoja = "";
                        if(Hoja.Follow.Count==1)
                        {
                            FollowsHoja += Hoja.Follow.First().ToString();
                        }
                        else if(Hoja.Follow.Count==0)
                        {
                            FollowsHoja = "------------------";
                        }
                        else
                        {
                            foreach (int NumeroHoja in Hoja.Follow)
                            {
                                if (FollowsHoja == "")
                                {
                                    FollowsHoja += NumeroHoja;
                                }
                                else
                                {
                                    
                                    FollowsHoja += "," + NumeroHoja;
                                }
                            }
                        }
                        ValoresNodo[0] = NumeroNodo;
                        ValoresNodo[1] = FollowsHoja;
                        dgNodoFollow.Rows.Add(ValoresNodo);
                    }
                    foreach(string Simbolo in SimbolosUsados)
                    {
                        dgEstados.Columns.Add(Simbolo, Simbolo);
                    }
                    foreach(List<int> Estado in EstadosVisitados)
                    {
                        string[] ValoresEstado = new string[SimbolosUsados.Count + 1];
                        string NumerosEstado = "";
                        if(Estado.Count==1)
                        {
                            NumerosEstado += Estado.First().ToString();
                        }
                        else
                        {
                            foreach(int Numero in Estado)
                            {
                                if(NumerosEstado=="")
                                {
                                    NumerosEstado += Numero;
                                }
                                else
                                {
                                    NumerosEstado += "," + Numero;
                                }
                            }
                        }
                        ValoresEstado[0] = NumerosEstado;
                        for (int i = 1; i < SimbolosUsados.Count + 1; i++)
                        {
                            List<int> NumerosFollow = EstadosAnalizados[Estado][SimbolosUsados[i - 1]];
                            string NumerosString = "";
                            if (NumerosFollow.Count == 1)
                            {
                                NumerosString += NumerosFollow.First();
                            }
                            else if (NumerosFollow.Count==0)
                            {
                                NumerosString = "--------------";
                            }
                            else
                            {
                                foreach (int NumeroF in NumerosFollow)
                                {
                                    if (NumerosString == "")
                                    {
                                        NumerosString += NumeroF;
                                    }
                                    else
                                    {
                                        NumerosString += "," + NumeroF;
                                    }
                                }
                            }
                            ValoresEstado[i] = NumerosString;
                        }
                        dgEstados.Rows.Add(ValoresEstado);
                    }
                    btExportación.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Se encontró que se está intentando definir dos símbolos terminales dentro de una pareja de apostrofes en uno de los tokens");
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Se encontró que se está intentando usar un set que no ha sido definido");
                this.Close();
            }
        }

        private void btExportación_Click(object sender, EventArgs e)
        {
            DirecciónPrograma.ShowDialog();
            string Dirección = DirecciónPrograma.SelectedPath;
            string Nombre = Microsoft.VisualBasic.Interaction.InputBox("Ingrese un nombre para guardar", "Generar Programa", "");
            Nombre = Nombre.Trim(' ');
            while (Nombre == string.Empty)
            {
                Nombre = Microsoft.VisualBasic.Interaction.InputBox("Ingrese un nombre para guardar", "Generar Programa", "");
                Nombre = Nombre.Trim(' ');
            }
            Dirección += "\\"+ Nombre;
            if(!Directory.Exists(Dirección))
            {
                Directory.CreateDirectory(Dirección);
                string DirecciónActual = Directory.GetCurrentDirectory();
                DirecciónActual = DirecciónActual.TrimEnd("\\Fase2LFA\\Fase2LFA\\bin\\Debug".ToCharArray());
                DirecciónActual += "es\\Analizador_Lenguaje";
                Fase3.CopiarContenido(DirecciónActual, Dirección);
            }
            Fase3.SimplificarAutomata(ref EstadosVisitados, ref NombresEstado, ref EstadosAnalizados, ref SimbolosUsados, ref EstadosSimplificados);
            Fase3.BuscarEstadosAceptacion(NodosHoja,NombresEstado, ref EstadosFinales);
            Fase3.ExportarArreglos(SetsF2, TokensExportar, ActionsExportar,EstadosFinales, SimbolosT , Dirección);
            Fase3.GenerarCodigo(ref LineasEscribir, SimbolosUsados,EstadosSimplificados);
            Fase3.EscribirMain(LineasEscribir, Dirección);
            MessageBox.Show("Cierre las ventanas de este ejecutable y dirijase a la ubicación en la que generó el programa");
        }
    }
}

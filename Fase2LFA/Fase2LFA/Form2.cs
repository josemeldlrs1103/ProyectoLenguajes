using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fase2LFA
{
    public partial class Form2 : Form
    {
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
        public static Queue<string> SimbolosUsados = new Queue<string>();
        //Cola que contiene los estados descubiertos que han sido analizados
        public static Queue<List<int>> EstadosVisitados = new Queue<List<int>>();
        //Cola que contiene los estados descubiertos que no han sido analizados
        public static Queue<List<int>> EstadosNuevos = new Queue<List<int>>();
        //Diccionario que contiene los estados y los follows de cada símbolo
        public static Dictionary<List<int>, Dictionary<string, List<int>>> EstadosAnalizados = new Dictionary<List<int>, Dictionary<string, List<int>>>();
        public static void ObtenerListas(ArrayList lista1, ArrayList lista2)
        {
            SetsF2 = lista1;
            TokensF2 = lista2;
        }
       
        public Form2()
        {
            InitializeComponent();
        }

        private void btFase2_MouseClick(object sender, MouseEventArgs e)
        {
            Fase2.ObtenerSímbolosSets(ref SetsF2, ref SimbolosT);
            Fase2.LimpiarTokens(ref TokensF2);
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
    }
}

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
    public partial class Form1 : Form
    {
        //Creación de Árboles de Expresión
        void DefinirÁrboles()
        {
            Árbol Auxiliar = new Árbol();
            Nodo ArbolSets = Auxiliar.CrearArbol("(0.1*|1.0*).#");
            int pausa = 0;
        }
        //Arreglo que contiene todas las líneas del archivo
        ArrayList Lineas = new ArrayList();
        //Arreglo usado para verficar el orden de las secciones
        ArrayList Secciones = new ArrayList();
        //Índice de Secciones
        int IndSets = 0, IndTokens = 0, IndActions = 0, IndError = 0;
        //Arreglos para separar las secciones del archivo
        ArrayList Sets= new ArrayList();
        ArrayList Tokens= new ArrayList();
        ArrayList Actions = new ArrayList();
        ArrayList Errors = new ArrayList();
        //Método de Lectura
        void Lectura()
        {
            Lineas.Clear();
            string Ruta = "";
            using (OpenFileDialog Selección = new OpenFileDialog())
            {
                Selección.Filter = "txt files (*.txt)|*.txt";
                if (Selección.ShowDialog() == DialogResult.OK)
                {
                    //Obtener la dirección de archivo seleccionado
                    Ruta = Selección.FileName;
                    tbDirección.Text = Ruta;
                    var LeerArchivo = Selección.OpenFile();
                    using (StreamReader Lector = new StreamReader(LeerArchivo))
                    {
                        string LineaLeida = "";
                        while ((LineaLeida = Lector.ReadLine()) != null)
                        {
                            if (LineaLeida != "")
                            {
                                string LineaLimpia = LineaLeida.Trim(' ','\t');
                                Lineas.Add(LineaLimpia);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Para continuar debe seleccionar algún archivo \".txt\", por favor intente de nuevo");
                    Lectura();
                }
            }
        }
        //Verificar que no existan definiciones fuera de las secciones de SETS  o TOKENS
        bool ExpresionSuelta()
        {
            bool Bandera;
            if (((Lineas[0].ToString()) == "SETS") || ((Lineas[0].ToString()) == "TOKENS"))
            {
                Bandera = false;
            }
            else
            {
                Bandera = true;
            }
            return Bandera;
        }
        //Comprobar Orden de las secciones del Archivo
        bool OrdenSecciones()
        {
            bool Bandera;
            foreach (string Linea in Lineas)
            {
                if ((Linea == "SETS") || (Linea == "TOKENS") || (Linea == "ACTIONS") || (Linea.Contains("ERROR")))
                {
                    Secciones.Add(Linea);
                }
            }
            //OrdenEsperado si se encuentra la sección "SETS"
            if (((Secciones[0].ToString())=="SETS")&& ((Secciones[1].ToString()) == "TOKENS")&&((Secciones[2].ToString()) == "ACTIONS")&& ((Secciones[3].ToString()).Contains("ERROR")))
            {
                Bandera = true;
            }
            else if (((Secciones[0].ToString()) == "TOKENS") && ((Secciones[1].ToString()) == "ACTIONS") && ((Secciones[2].ToString()).Contains("ERROR")))
            {
                Bandera = true;
            }
            else
            {
                Bandera = false;
            }
            return Bandera;
        }
        //Calcular índices
        void Índices()
        {
            bool BanderaTokens=false, BanderaActions = false, BanderaError = false;
            foreach (string Linea in Lineas)
            {
                if(Linea == "SETS")
                {
                    IndTokens++;
                    IndActions++;
                    IndError++;
                }
                else if (Linea == "TOKENS")
                {
                    BanderaTokens = true;
                    IndActions++;
                    IndError++;
                }
                else if (Linea == "ACTIONS")
                {
                    BanderaActions = true;
                    IndError++;
                }
                else if (Linea.Contains("ERROR"))
                {
                    BanderaError = true;
                }
                else
                {
                    if(!BanderaTokens)
                    {
                        IndTokens++;
                    }
                    if (!BanderaActions)
                    {
                        IndActions++;
                    }
                    if (!BanderaError)
                    {
                        IndError++;
                    }
                }
            }
        }


        //Separar el contenido de las secciones del archivo
        void SepararSecciones()
        {
            if(IndSets!=IndTokens)
            {
                for(int i= IndSets+1; i<IndTokens; i++)
                {
                    Sets.Add(Lineas[i].ToString());
                }
                for (int i = IndTokens + 1; i < IndActions; i++)
                {
                    Tokens.Add(Lineas[i].ToString());
                }
                for (int i = IndActions + 1; i < IndError; i++)
                {
                    Actions.Add(Lineas[i].ToString());
                }
                for (int i = IndError; i < Lineas.Count; i++)
                {
                    Errors.Add(Lineas[i].ToString());
                }
            }
        }
        public Form1()
        {
            InitializeComponent();
        }
        private void btCrear_MouseClick(object sender, MouseEventArgs e)
        {
            DefinirÁrboles();
            int pasu = 0;
        }
        private void btCargar_MouseClick(object sender, MouseEventArgs e)
        {
            lbProceso.Visible = true;
            Lectura();
            bool ExpSuelta = ExpresionSuelta();
            if (!ExpSuelta)
            {
                bool SeccionesOrdenadas = OrdenSecciones();
                if(SeccionesOrdenadas)
                {
                    Índices();
                    SepararSecciones();
                    int pausa = 0;
                }
                else
                {
                    MessageBox.Show("Las secciones no están ordenadas correctamente");
                    lbProceso.Visible = false;
                    lbIncorrecto.Visible = true;
                }
            }
            else
            {
                MessageBox.Show("Se encontró una definición fuera de las secciones de \"SETS\" o \"TOKENS\"");
                lbProceso.Visible = false;
                lbIncorrecto.Visible = true;
            }
        }
    }
}

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
                        string LineaLimpia = "";
                        while ((LineaLeida = Lector.ReadLine()) != null)
                        {
                            LineaLimpia = string.Empty;
                            if (LineaLeida != "")
                            {
                                char[] Caracteres = LineaLeida.ToCharArray();
                                for (int i = 0; i < Caracteres.Length; i++)
                                {
                                    if (Caracteres[i] != ' ' && Caracteres[i] != '\t'&&Caracteres[i]!='\n')
                                    {
                                        LineaLimpia += (Caracteres[i]).ToString();
                                    }
                                }
                            }
                            if (LineaLimpia != string.Empty)
                            {
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
        public Form1()
        {
            InitializeComponent();
        }
        private void btCargar_MouseClick(object sender, MouseEventArgs e)
        {
            lbProceso.Visible = true;
            Lectura();
            bool ExpSuelta = Fase1.ExpresionSuelta(ref Lineas);
            if (!ExpSuelta)
            {
                bool SeccionesOrdenadas = Fase1.OrdenSecciones(ref Lineas, ref Secciones);
                if(SeccionesOrdenadas)
                {
                    Fase1.Índices(ref Lineas, ref IndSets, ref IndTokens, ref IndActions, ref IndError);
                    Fase1.SepararSecciones(ref Lineas, ref Sets, ref Tokens, ref Actions, ref Errors, ref IndSets, ref IndTokens, ref IndActions, ref IndError);
                    bool SetsAprovados = Fase1.AnalizarSets(ref Sets, ref IndSets);
                    if(SetsAprovados)
                    {
                        bool TokensAprovados = Fase1.AnalizarTokens(ref Tokens, ref IndTokens);
                        if(TokensAprovados)
                        {
                            bool ActionsAprovado = Fase1.AnalizarActions(ref Actions, ref IndActions);
                            if (ActionsAprovado)
                            {
                                bool ErrorsAprovado = Fase1.AnalizarErrors(ref Errors, ref IndError);
                                if(ErrorsAprovado)
                                {
                                    lbProceso.Visible = false;
                                    lbCorrecto.Visible = true;
                                }
                                else
                                {
                                    MessageBox.Show("Se encontró un error en la sección de errors, en la línea " + IndError);
                                    lbProceso.Visible = false;
                                    lbIncorrecto.Visible = true;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Se encontró un error en la sección de actions, en la línea " + IndActions);
                                lbProceso.Visible = false;
                                lbIncorrecto.Visible = true;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Se encontró un error en la sección de tokens, en la línea " + IndTokens);
                            lbProceso.Visible = false;
                            lbIncorrecto.Visible = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Se encontró un error en la sección de sets, en la línea " + IndSets);
                        lbProceso.Visible = false;
                        lbIncorrecto.Visible = true;
                    }
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

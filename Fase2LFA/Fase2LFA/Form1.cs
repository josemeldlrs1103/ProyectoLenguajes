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
                                Lineas.Add(LineaLeida);
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
        public Form1()
        {
            InitializeComponent();
        }

        private void btCargar_MouseClick(object sender, MouseEventArgs e)
        {
            lbProceso.Visible = true;
            Lectura();
            bool ExpSuelta = ExpresionSuelta();
            if (!ExpSuelta)
            {

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

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
        public static ArrayList SimbolosT = new ArrayList();
        public static Queue<string> ColaSímbolos = new Queue<string>();
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
                    int pausa2 = 0;
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

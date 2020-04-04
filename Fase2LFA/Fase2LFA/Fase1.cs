using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fase2LFA
{
    public static class Fase1
    {
        //Verificar que no existan definiciones fuera de las secciones de SETS  o TOKENS
        public static bool ExpresionSuelta(ref ArrayList Lineas)
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
        public static bool OrdenSecciones(ref ArrayList Lineas, ref ArrayList Secciones)
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
            if (((Secciones[0].ToString()) == "SETS") && ((Secciones[1].ToString()) == "TOKENS") && ((Secciones[2].ToString()) == "ACTIONS") && ((Secciones[3].ToString()).Contains("ERROR")))
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
        public static void Índices(ref ArrayList Lineas,ref  int IndSets,ref int IndTokens, ref int IndActions, ref int IndError)
        {
            IndSets = 0;
            IndTokens = 0;
            IndActions = 0;
            IndError = 0;
            bool BanderaTokens = false, BanderaActions = false, BanderaError = false;
            foreach (string Linea in Lineas)
            {
                if (Linea == "SETS")
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
                    if (!BanderaTokens)
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
        public static void SepararSecciones(ref ArrayList Lineas, ref ArrayList Sets, ref ArrayList Tokens, ref ArrayList Actions, ref ArrayList Errors, ref int IndSets, ref int IndTokens, ref int IndActions, ref int IndError)
        {
            if (IndSets != IndTokens)
            {
                for (int i = IndSets + 1; i < IndTokens; i++)
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
            else
            {
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
        //Analizar Sets
        public static bool AnalizarSets(ref ArrayList Sets, ref int IndSets)
        {
            if(Sets.Count>0)
            {
                foreach(string Linea in Sets)
                {
                    IndSets++;
                    if(Linea.Contains("="))
                    {
                        char[] Letras = Linea.ToCharArray();
                        int IndiceSímboloIgual = 0;
                        foreach (char Letra in Linea)
                        {
                            if (Letra == '=')
                            {
                                break;
                            }
                            else
                            {
                                IndiceSímboloIgual++;
                            }
                        }
                        //Nombre set en mayúsculas
                        for(int i=0; i<IndiceSímboloIgual; i++)
                        {
                            if (!char.IsUpper(Letras[i]))
                            {
                                return false;
                            }
                        }
                        if (Linea.Contains("'"))
                        {
                            ArrayList IndiceApostrofes = new ArrayList();
                            for (int i = IndiceSímboloIgual+1; i < Letras.Length; i++)
                            {
                                if (Letras[i] == '\'')
                                {
                                    IndiceApostrofes.Add(i);
                                }
                            }
                            if (IndiceApostrofes.Count >= 3)
                            {
                                bool ApostrofeenSet = false;
                                int ElementosLista = IndiceApostrofes.Count - 2;
                                for (int i = 0; i < ElementosLista; i++)
                                {
                                    int Posición = Convert.ToInt32(IndiceApostrofes[i]);
                                    int Posición1 = Convert.ToInt32(IndiceApostrofes[i + 1]);
                                    if ((Posición + 1) == Posición1)
                                    {
                                        int Posición2 = Convert.ToInt32(IndiceApostrofes[i + 2]);
                                        if ((Posición + 2) == Posición2)
                                        {
                                            if (!ApostrofeenSet)
                                            {
                                                ApostrofeenSet = true;
                                                IndiceApostrofes.RemoveAt(i + 1);
                                            }
                                            else
                                            {
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            return false;
                                        }
                                    }
                                }
                            }
                            if (IndiceApostrofes.Count % 2 != 0)
                            {
                                return false;
                            }
                        }
                        if (Linea.Contains("CHR"))
                        {
                            ArrayList IndiceCHR = new ArrayList();
                            ArrayList IndiceParentesisAbre = new ArrayList();
                            ArrayList IndiceParentesisCierra = new ArrayList();
                            for (int i = IndiceSímboloIgual+1; i < Letras.Length; i++)
                            {
                                if (Letras[i] == 'R' && Letras[i - 1] == 'H' && Letras[i - 2] == 'C')
                                {
                                    IndiceCHR.Add(i - 2);
                                }
                                if (Letras[i] == '(')
                                {
                                    IndiceParentesisAbre.Add(i);
                                }
                                if (Letras[i] == ')')
                                {
                                    IndiceParentesisCierra.Add(i);
                                }
                            }
                            if(IndiceCHR.Count==IndiceParentesisAbre.Count&&IndiceCHR.Count==IndiceParentesisCierra.Count)
                            {
                                int ContadorLlaves = IndiceCHR.Count;
                                while(ContadorLlaves !=0)
                                {
                                    ContadorLlaves--;
                                    if ((Convert.ToInt32(IndiceCHR[0]))< (Convert.ToInt32(IndiceParentesisAbre[0]))&& (Convert.ToInt32(IndiceParentesisAbre[0]))< (Convert.ToInt32(IndiceParentesisCierra[0])))
                                    {
                                        IndiceCHR.RemoveAt(0);
                                        for (int i = (Convert.ToInt32(IndiceParentesisAbre[0]) + 1); i < (Convert.ToInt32(IndiceParentesisCierra[0])); i++)
                                        {
                                           if(!char.IsNumber(Letras[i]))
                                           {
                                                return false;
                                           }
                                        }
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                    IndiceParentesisAbre.RemoveAt(0);
                                    IndiceParentesisCierra.RemoveAt(0);
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                        if (Linea.Contains("+") || Linea.Contains("."))
                        {
                            ArrayList IndiceMás = new ArrayList();
                            ArrayList IndicePunto = new ArrayList();
                            int Ultimaposcición = Letras.Length - 1;
                            if (Letras[Ultimaposcición] != '+' && Letras[Ultimaposcición] != '.')
                            {

                                for (int i = 0; i < Letras.Length; i++)
                                {
                                    if (Letras[i] == '+')
                                    {
                                        IndiceMás.Add(i);
                                    }
                                    if (Letras[i] == '.')
                                    {
                                        IndicePunto.Add(i);
                                    }
                                }
                                if (IndiceMás.Count > 0)
                                {
                                    int Cantidadind = IndiceMás.Count;
                                    for (int i = 0; i < Cantidadind; i++)
                                    {
                                        int Indice = Convert.ToInt32(IndiceMás[i]);
                                        if ((Letras[Indice - 1] != '=') || ((Letras[Indice - 1] == '\'') && (Letras[Indice + 1] == '\'')) || ((Letras[Indice - 1] == ')') && (Letras[Indice + 1] == 'C')))
                                        {
                                            
                                        }
                                        else
                                        {
                                            return false;
                                        }
                                    }
                                }
                                if (IndicePunto.Count > 0)
                                {
                                    int Cantidadind = IndicePunto.Count;
                                    for (int i = 0; i < Cantidadind; i++)
                                    {
                                        int Indice = Convert.ToInt32(IndicePunto[i]);
                                        if ((Letras[Indice - 1] == '\'' && Letras[Indice + 1] == '.') || (Letras[Indice - 1] == ')' && Letras[Indice + 1] == '.') || (Letras[Indice - 1] == '.' && Letras[Indice + 1] == '\'') || (Letras[Indice - 1] == '.' && Letras[Indice + 1] == 'C'))
                                        {
                                        }
                                        else
                                        {
                                            return false;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        //Analizar Tokens
        public static bool AnalizarTokens(ref ArrayList Tokens, ref int IndTokens)
        {
            foreach(string Linea in Tokens)
            {
                IndTokens++;
                char[] Letras = Linea.ToCharArray();
                if(Linea.StartsWith("TOKEN"))
                {
                    if (Linea.Contains("="))
                    {
                        int IndiceIgual = 0;
                        foreach (char Letra in Letras)
                        {
                            if (Letra == '=')
                            {
                                break;
                            }
                            else
                            {
                                IndiceIgual++;
                            }
                        }
                        //Comprobar que exista número de Token
                        for (int i=5;i < IndiceIgual;i++ )
                        {
                            if(!char.IsNumber(Letras[i]))
                            {
                                return false;
                            }
                        }
                        if ((Letras[IndiceIgual] == '=' && Letras[IndiceIgual + 1] != '+') || (Letras[IndiceIgual] == '=' && Letras[IndiceIgual + 1] != '*') || (Letras[IndiceIgual] == '=' && Letras[IndiceIgual + 1] == '?'))
                        {
                            if (Linea.Contains("'"))
                            {
                                int posición = IndiceIgual;
                                int ContadorAp = 0;
                                while (posición < Letras.Length)
                                {
                                    if (Letras[posición] == '\'')
                                    {
                                        ContadorAp++;
                                    }
                                    posición++;
                                }
                                if (ContadorAp % 2 != 0)
                                {
                                    return false;
                                }
                            }
                            int ContadorParentesisAbre = 0;
                            int ContadorParentesisCierra = 0;
                            if (Linea.Contains("("))
                            {
                                if (Linea.Contains("'('"))
                                {
                                    ContadorParentesisAbre--;
                                }
                                for (int i = IndiceIgual; i < Letras.Length; i++)
                                {
                                    if (Letras[i] == '(')
                                    {
                                        ContadorParentesisAbre++;
                                    }
                                }
                            }
                            if (Linea.Contains(")"))
                            {
                                if (Linea.Contains("')'"))
                                {
                                    ContadorParentesisCierra--;
                                }
                                for (int i = IndiceIgual; i < Letras.Length; i++)
                                {
                                    if (Letras[i] == ')')
                                    {
                                        ContadorParentesisCierra++;
                                    }
                                }
                            }
                            if (Linea.Contains("|"))
                            {
                                int UltimaPosición = Letras.Length - 1;
                                if (Letras[UltimaPosición] != '|')
                                {
                                    ArrayList IndiceO = new ArrayList();
                                    for(int i=IndiceIgual+1; i<Letras.Length;i++)
                                    {
                                        if(Letras[i]=='|')
                                        {
                                            IndiceO.Add(i);
                                        }
                                    }
                                    foreach(int Indice in IndiceO)
                                    {
                                        if((Letras[Indice+1]=='+')|| (Letras[Indice + 1] == '?')|| (Letras[Indice + 1] == '*'))
                                        {
                                            return false;
                                        }
                                    }
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        //Analizar Actions
        public static bool AnalizarActions(ref ArrayList Actions, ref int IndActions)
        {
            bool LlaveAbierta = false;
            int ContadorFunciones = 0;
            int ContadorLlaveAbre = 0;
            int ContadorLlaveCierra = 0;
            for (int i = 0; i < Actions.Count; i++)
            {
                if ((Actions[i].ToString()).Contains("()"))
                {
                    ContadorFunciones++;
                }
                if ((Actions[i].ToString()).Contains("{"))
                {
                    ContadorLlaveAbre++;
                }
                if ((Actions[i].ToString()).Contains("}"))
                {
                    ContadorLlaveCierra++;
                }
            }
            if (ContadorFunciones == ContadorLlaveAbre && ContadorFunciones == ContadorLlaveCierra)
            {
                foreach (string lineaAction in Actions)
                {
                    IndActions++;
                    char[] Letras = lineaAction.ToCharArray();
                    if (lineaAction.Contains("()"))
                    {
                        if (LlaveAbierta)
                        {
                            return false;
                        }
                    }
                    if (lineaAction.Contains("{"))
                    {
                        if (LlaveAbierta)
                        {
                            return false;
                        }
                        else
                        {
                            LlaveAbierta = true;
                        }
                    }
                    if (lineaAction.Contains("}"))
                    {
                        if (LlaveAbierta)
                        {
                            LlaveAbierta = false;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    if (lineaAction.Contains("="))
                    {
                        int IndiceIgual = 0;
                        for (int i = 0; i < Letras.Length; i++)
                        {
                            if (Letras[i] == '=')
                            {
                                break;
                            }
                            else
                            {
                                IndiceIgual++;
                            }
                        }
                        //Buscar el número de la acción
                        for (int i = 0; i < IndiceIgual; i++)
                        {
                            if (!char.IsNumber(Letras[i]))
                            {
                                return false;
                            }
                        }
                        if (lineaAction.Contains("'"))
                        {
                            int ContadorApostrofes = 0;
                            for (int i = IndiceIgual + 1; i < Letras.Length; i++)
                            {
                                if (Letras[i] == '\'')
                                {
                                    ContadorApostrofes++;
                                }
                            }
                            if (ContadorApostrofes != 2)
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            else
            {
                return false;
            }
            return true;
        }
        //Analizar Errors
        public static bool AnalizarErrors(ref ArrayList Errors, ref int IndError)
        {
            foreach(string Linea in Errors)
            {
                IndError++;
                char[] Letras = Linea.ToCharArray();
                if (Linea.Contains("ERROR"))
                {
                    if (Linea.Contains("="))
                    {
                        int IndiceIgual = 0;
                        for (int i = 0; i < Letras.Length; i++)
                        {
                            if (Letras[i] == '=')
                            {
                                break;
                            }
                            else
                            {
                                IndiceIgual++;
                            }
                        }
                        for (int i = 0; i < IndiceIgual; i++)
                        {
                            if (!char.IsLetter(Letras[i]))
                            {
                                return false;
                            }
                        }
                        for (int i = IndiceIgual + 1; i < Letras.Length; i++)
                        {
                            if (!char.IsNumber(Letras[i]))
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
}

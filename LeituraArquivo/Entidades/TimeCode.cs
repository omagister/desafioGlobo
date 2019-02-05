using System;
using System.Collections.Generic;
using System.Text;

namespace LeituraArquivo.Entidades
{
    public class TimeCode
    {
        public int Horas { get; set; }
        public int Minutos { get; set; }
        public int Segundos { get; set; }
        public int Frames { get; set; }
        
        public string Extenso()
        {
            string extenso = string.Empty;

            string horas = AcrescentaZero(Horas.ToString());
            string minutos = AcrescentaZero(Minutos.ToString());
            string segundos = AcrescentaZero(Segundos.ToString());
            string frames = AcrescentaZero(Frames.ToString());

            extenso = horas + ":" + minutos + ":" + segundos + ";" + frames;

            return extenso;
        }

        public string AcrescentaZero(string numero)
        {
            if (numero.Length < 2)
            {
                numero = "0" + numero;
            }

            return numero;
        }

       
    }
}

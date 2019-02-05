using System;
using LeituraArquivo.Entidades;
using System.Collections.Generic;

namespace consoleLeitura
{
    class Program
    {
        static void Main(string[] args)
        {
            Leitura leitura = new Leitura();

            List<InfoCorteDado> dadosCorte = leitura.GeraDadosParaCorte(@"c:\teste");

            string dadosJson = leitura.ConverteParaJson(dadosCorte);

            Console.WriteLine(dadosJson);

            Console.ReadLine();
        }
    }
}

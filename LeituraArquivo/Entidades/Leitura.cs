using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;

namespace LeituraArquivo.Entidades
{
    public class Leitura
    {
        public string ConverteParaJson(List<InfoCorteDado> dados)
        {
            string dadosJson = string.Empty;

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<InfoCorteDado>));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, dados);
            dadosJson = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();

            return dadosJson;
        }

        public List<InfoCorteDado> GeraDadosParaCorte(string diretorio)
        {
            List<Arquivo> arquivos = ListarArquivos(diretorio);

            List<InfoCorteDado> dadosCorte = new List<InfoCorteDado>();

            foreach (var item in arquivos)
            {
                var linhas = Ler(item.caminho);

                foreach (var linha in linhas)
                {
                    if (!String.IsNullOrEmpty(linha.valor.ToString()))
                    {
                        InfoCorteDado infoCorte = SepararDados(linha.valor);
                        if (infoCorte.StartTime != null)
                        {
                            if ((infoCorte.Duration.Minutos > 0) || (infoCorte.Duration.Minutos == 0 && infoCorte.Duration.Segundos > 30))
                            {
                                if ((dadosCorte.Find(d => d.Title == infoCorte.Title && d.StartTime.Extenso() == infoCorte.StartTime.Extenso() && d.EndTime.Extenso() == infoCorte.EndTime.Extenso())) == null)
                                {
                                    dadosCorte.Add(infoCorte);
                                }
                                    
                                    //Console.WriteLine(infoCorte.StartTime.Extenso() + " " + infoCorte.EndTime.Extenso() + " " + infoCorte.Duration.Extenso() + " " + infoCorte.Title + " " + infoCorte.ReconcileKey.ToString());
                                
                            }
                        }

                    }

                }

            }

            return dadosCorte;
        }

        public IEnumerable<Linha> Ler (string caminho)
        {
            List<Linha> linhas = new List<Linha>();

            if (!String.IsNullOrEmpty(caminho))
            {
                StreamReader arquivo = new StreamReader(caminho);
                

                int contador = 0;
                string linha = string.Empty;
                

                while ((linha = arquivo.ReadLine()) != null)
                {
                    Linha novaLinha = new Linha();
                    novaLinha.valor = linha;
                    linhas.Add(novaLinha);
                    contador++;
                }

                arquivo.Close();

            }

            return linhas;
        }

        public List<Arquivo> ListarArquivos(string diretorio)
        {
            string[] files = Directory.GetFiles(diretorio, "*.txt");
            List<Arquivo> arquivos = new List<Arquivo>();

            foreach (var arquivo in files)
            {
                Arquivo arq = new Arquivo();
                arq.caminho = arquivo;
                arquivos.Add(arq);
            }

            return arquivos;
        }

        
        public InfoCorteDado SepararDados(string linha)
        {
            InfoCorteDado infoCorte = new InfoCorteDado();

            if (!String.IsNullOrEmpty(linha) && linha[1].ToString() == "P")
            {
                string start = linha.Substring(17, 11);
                string end = linha.Substring(40, 11);
                string titulo = linha.Substring(106, 32);
                string duracao = linha.Substring(184, 11);
                string reconcileKey = linha.Substring(279, 32);

                TimeCode StartTime = new TimeCode();
                StartTime.Horas = int.Parse(start.Substring(0, 2));
                StartTime.Minutos = int.Parse(start.Substring(3, 2));
                StartTime.Segundos = int.Parse(start.Substring(6, 2));
                StartTime.Frames = int.Parse(start.Substring(9, 2));

                TimeCode EndTime = new TimeCode();
                EndTime.Horas = int.Parse(end.Substring(0, 2));
                EndTime.Minutos = int.Parse(end.Substring(3, 2));
                EndTime.Segundos = int.Parse(end.Substring(6, 2));
                EndTime.Frames = int.Parse(end.Substring(9, 2));

                TimeCode Duration = new TimeCode();
                Duration.Horas = int.Parse(duracao.Substring(0, 2));
                Duration.Minutos = int.Parse(duracao.Substring(3, 2));
                Duration.Segundos = int.Parse(duracao.Substring(6, 2));
                Duration.Frames = int.Parse(duracao.Substring(9, 2));

                infoCorte.StartTime = StartTime;
                infoCorte.EndTime = EndTime;
                infoCorte.Duration = Duration;
                infoCorte.Title = titulo.Trim();
                if (!String.IsNullOrEmpty(reconcileKey))
                {
                    infoCorte.ReconcileKey = long.Parse(reconcileKey);
                }
                infoCorte.NomeArquivo = titulo.Trim();
                infoCorte.Path = "//SERVER_PLAY";
            }

            return infoCorte;
        }
    }

    public class Linha
    {
        public string valor { get; set; }
    }

    
}

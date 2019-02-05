using System;
using System.Collections.Generic;
using System.Text;
using LeituraArquivo.Entidades;

namespace LeituraArquivo.Entidades
{
    public class InfoCorteDado
    {
        public TimeCode StartTime { get; set; }
        public TimeCode EndTime { get; set; }
        public string Title { get; set; }
        public TimeCode Duration { get; set; }
        public long ReconcileKey { get; set; }
        public string NomeArquivo { get; set; }
        public string Path { get; set; }
    }
}

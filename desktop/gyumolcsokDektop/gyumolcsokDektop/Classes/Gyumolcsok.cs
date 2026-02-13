using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace gyumolcsokDektop.Classes
{
    //SELECT `gyumolcsid`, `nev`, `megjegyzes`, `nev_eng`, `alt_szoveg`, `src` FROM `gyumolcs` WHERE 1
    internal class Gyumolcsok
    {
        [JsonProperty("gyumolcsid")]
        public int Gyumolcsid { get; set; }

        [JsonProperty("nev")]
        public string Nev { get; set; }

        [JsonProperty("megjegyzes")]
        public string Megjegyzes { get; set; }

        [JsonProperty("nev_eng")]
        public string Nev_eng { get; set; }

        [JsonProperty("alt_szoveg")]
        public string Alt_szoveg { get; set; }

        [JsonProperty("src")]
        public string Src { get; set; }

        public Gyumolcsok() { }
    }
}

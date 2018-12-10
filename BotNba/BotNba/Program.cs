using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using HtmlAgilityPack;
using System.IO;

namespace BotNba
{
    class Program
    {
        /*
        1. Ver como obtener la info de jugadores
        2. Guardar en carpeta un fichero por jugador
        3. Guardar info en BD
        4. Crear una web con los datos
        */
        static void Main(string[] args)
        {
            WebClient client = new WebClient();
            HtmlDocument doc = new HtmlDocument();
            HtmlNode.ElementsFlags["br"] = HtmlElementFlag.Empty;
            doc.OptionWriteEmptyNodes = true;

            var web = HttpWebRequest.Create("http://www.espn.com/nba/player/_/id/800");
            Stream stream = web.GetResponse().GetResponseStream();
            doc.Load(stream);

            foreach(HtmlNode node in doc.DocumentNode.SelectNodes("//div[@class='player-bio']"))
            {
                Console.Write(node.InnerHtml);
            }

            Console.ReadLine();
        }
    }
}

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
            int contador = 1;

            while(contador<50)
            {

                HtmlDocument doc = new HtmlDocument();
                HtmlNode.ElementsFlags["br"] = HtmlElementFlag.Empty;
                doc.OptionWriteEmptyNodes = true;

                var web = HttpWebRequest.Create("http://www.espn.com/nba/player/_/id/"+contador.ToString());
                Stream stream = web.GetResponse().GetResponseStream();
                doc.Load(stream);

                HtmlNode nombreJugador = doc.DocumentNode.SelectSingleNode("//body//h1");
                StreamWriter sw = new StreamWriter("C:\\JugadoresNBA\\" + nombreJugador.InnerHtml + ".html", false);

                string selector = "//div[@class='player-bio']";

                foreach (HtmlNode node in doc.DocumentNode.SelectNodes(selector))
                {
                    sw.Write(node.InnerHtml);
                }

                selector = "//div[@class='mod-container mod-table mod-player-stats']";

                HtmlNodeCollection htmlNodeCollection = doc.DocumentNode.SelectNodes(selector);
                if(htmlNodeCollection != null && htmlNodeCollection.Count > 0)
                {
                    foreach (HtmlNode node in htmlNodeCollection)
                    {
                        sw.Write("ESTADISTICAS: ");
                        sw.Write(node.InnerHtml);
                    }
                }
                sw.Close();
                sw.Dispose();
                contador++;
                Console.Write("#" + contador + "..");
            }

            Console.Write("Carga finalizada!");
            Console.ReadLine();
        }
    }
}

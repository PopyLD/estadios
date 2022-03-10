using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;


namespace testMundiales
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("API estadios");

            Program programa = new Program();
            programa.consumo();
        }

        public void consumo() {
           
            string url = "https://v3.football.api-sports.io/teams?country=Mexico";

            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(url);
            List<respuesta> equipos = new List<respuesta>();

            Request.Method = "GET";
            Request.Headers.Add("x-rapidapi-host", "v3.football.api-sports.io");
            Request.Headers.Add("x-rapidapi-key", "cb2401781c4b1a2a0e812bd86fd6014f");



            try
            {
                HttpWebResponse response = (HttpWebResponse)Request.GetResponse();
                string strResponse = string.Empty;

                string e1 = string.Empty;
                string e2 = string.Empty;

                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    strResponse = sr.ReadToEnd();
                }

                Informacion info = (Informacion)JsonConvert.DeserializeObject(strResponse, typeof(Informacion));

                equipos = info.response.OrderBy(x => x.team.name).ToList();

                foreach (var item in equipos)
                {
                    Console.WriteLine($"Equipo: {item?.team?.name??"N/A"} Estadio:  {item?.venue?.name??"N/A"}");
                }
               


            }
            catch (WebException ex)
            {
                Console.WriteLine("Ocurrio un error: " + ex.Message);
            }
        }
    }

    class Informacion
    {
        public string get { get; set; }

        public  parametro parameters { get; set; }

        public int results { get; set; }

        public pagina paging  { get; set; }

        public List <respuesta>  response { get; set; }
    }

    class parametro
    { 
        
        public string country { get; set; }
    }

    class pagina
    { 
        
        public int current { get; set; }

        public int total { get; set; }
    }

    class respuesta
    {
        public  equipo team { get; set; }

        public estadio venue  { get; set; }
    }

    class equipo { 
        public int? id { get; set; }

        public string? name { get; set; }

        public string? code { get; set; }

        public string? country { get; set; }

        public int? founded { get; set; }

        public string? national { get; set; }

        public string? logo {get; set; }
    }

    class estadio {
        public int? id { get; set; }

        public string? name { get; set; }

        public string? address { get; set; }

        public string? city { get; set; }

        public int? capacity { get; set; }

        public string? surface { get; set; }

        public string? image { get; set; }

    }

    
}

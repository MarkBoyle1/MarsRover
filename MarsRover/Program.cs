using System;
using System.IO;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MarsRover
{
    class Program
    {
        static void Main(string[] args)
        {
            InputProcessor _inputProcessor = new InputProcessor();

            try
            {
                args = _inputProcessor.GetInputFromFile(args);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Thread.Sleep(3000);
                Console.WriteLine("File Not Found!");
                Thread.Sleep(5000);
            }
            
            RoverSettings roverSettings = _inputProcessor.GetRoverSettings(args);
            PlanetSettings planetSettings = _inputProcessor.GetPlanetSettings(args);
            Engine engine = new Engine(roverSettings, planetSettings);
            
            engine.RunProgram();
            

            // Console.WriteLine(Path.GetExtension(defaultFilePath));
            // var myJsonString = File.ReadAllText(defaultFilePath);
            // var myJObject = JObject.Parse(myJsonString);
            // var properties = myJObject.Properties();
            //
            // foreach (var p in properties)
            // {
            //     Console.WriteLine(p.Name + p.Value);
            // }

            // SelectToken("location:").Value<string>());

            // string json = @"
            //     {
            //        'CPU': 'Intel',
            //        'PSU': '500W',
            //        'Drives': 
            //         [
            //              'DVD read/writer'
            //              /*(broken)*/,
            //              '500 gigabyte hard drive',
            //              '200 gigabyte hard drive'
            //          ]
            //      }";
            
            // JsonTextReader reader = new JsonTextReader(new StringReader(json));
            // while (reader.Read())
            // {
            //     if (reader.Value != null)
            //     {
            //         Console.WriteLine("Token: {0}, Value: {1}", reader.TokenType, reader.Value);
            //     }
            //     else
            //     {
            //         Console.WriteLine("Token: {0}", reader.);
            //     }
            // }





        }
    }
}
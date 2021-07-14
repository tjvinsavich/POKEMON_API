using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;

namespace PokemonAPI
{
    class Program
    {       
        static void Main(string[] args)
        {
           string pokeURL = "https://pokeapi.co/api/v2/pokemon?limit=9&offset=251";


            //TODO create a new instance of HttpClient called client.
            var client = new HttpClient();
            //TODO use your client instance to get a response from the poke URL.
            var response = client.GetStringAsync(pokeURL).Result;

            //TODO go to https://json2csharp.com and convert your json reponse to classes. Create a new class file in visual studio and paste the classes you created on the website.
            //TODO create a variable that = JsonConvert.DeserializeObject<YourRootClassGoesHere>(yourStringResponseGoesHere);
            var root = JsonConvert.DeserializeObject<Root>(response);
            //TODO print the results from your

            foreach (var r in root.results)
            {
                Console.WriteLine(r.name + " => " + r.url);
            }

            //TODO use the pokemon url above and change it to try and call your favorite pokemon.
            pokeURL = "https://pokeapi.co/api/v2/pokemon/257/";
            response = client.GetStringAsync(pokeURL).Result;

            //TODO Use select token to try and grab a couple values from your pokemon and display them.

            var pokeName = JObject.Parse(response).GetValue("name").ToString();

            var pokeMoniker = JObject.Parse(response).SelectToken("abilities[0].ability.name").ToString();

            var pokeType1 = JObject.Parse(response).SelectToken("types[0].type.name").ToString();
            var pokeType2 = JObject.Parse(response).SelectToken("types[1].type.name").ToString();

            Console.WriteLine($"\n{pokeName.ToUpper()}: The {pokeMoniker.ToUpper()} Pokémon");
            Console.WriteLine($"It is a {pokeType1.ToUpper()} and {pokeType2.ToUpper()} type.");
        }
    }
}

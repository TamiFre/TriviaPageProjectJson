using LoginPage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LoginPage.Service
{
    public class JsonServiceLogin
    {
        HttpClient httpClient;//אובייקט לשליחת בקשות וקבלת תשובות מהשרת

        JsonSerializerOptions options;//פרמטרים שישמשו אותנו להגדרות הjson

        const string URL = $@"https://qsc714b9-7128.euw.devtunnels.ms/";

        public JsonServiceLogin()
        {
            //http client
            httpClient = new HttpClient();

            //options when doing serialization/deserialization
            options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
        }

        public async Task <Player> Login(Player ps)
        {
            Player playerEmailPass = new Player();
            { playerEmailPass.PlayerMail = ps.PlayerMail; playerEmailPass.PlayerPass = ps.PlayerPass; }

            string jsonString = JsonSerializer.Serialize(playerEmailPass, options);//הפכתי לגייסון

            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{URL}/TriviaApi/Login", content);

            if (response.IsSuccessStatusCode)
            {

                return ps;
            }
            return null;
        }


        public async Task<bool> Register(Player ps)
        {

            string jsonString = JsonSerializer.Serialize(ps, options);//הפכתי לגייסון

            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync($"{URL}/TriviaApi/RegisterPlayer", content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;

        }

        public async Task<Q> GetRandomQuestion()
        {
            Q question = null;
            var response = await httpClient.GetAsync($"{URL}/TriviaApi/GetRandomQuestion");

            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();

                question = JsonSerializer.Deserialize<Q>(jsonString, options);

            }
        }


    }
}

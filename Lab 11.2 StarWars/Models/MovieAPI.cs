namespace Lab_11._2_StarWars.Models
{
    // Class for Film
    // Class for Character
    // Class for StarShip
    // Class for DAL
    public class MovieAPI
    {
        public static HttpClient _web = null;
        public static HttpClient GetHttpClient()
        {
            if (_web == null)
            {
                _web = new HttpClient();
                _web.BaseAddress = new Uri("https://swapi.dev/api/");
            }
            return _web;
        }

        async public static Task<Movie> FindMovie(int filmnum)
        {
            HttpClient web = GetHttpClient();
            var connection = await web.GetAsync($"films/{filmnum}");
            FilmResponse resp = await connection.Content.ReadAsAsync<FilmResponse>();

            Movie mymovie = new Movie();
            mymovie.Characters = new List<string>();
            mymovie.StarShips = new List<string>();
            mymovie.Title = resp.title;
            mymovie.Release = resp.release_date;

            foreach (string url in resp.characters)
            {
                connection = await web.GetAsync(url);
                CharacterResponse presp = await connection.Content.ReadAsAsync<CharacterResponse>();
                mymovie.Characters.Add(presp.name);
            }

            foreach (string url in resp.starships)
            {
                connection = await web.GetAsync(url);
                StarShipResponse presp = await connection.Content.ReadAsAsync<StarShipResponse>();
                mymovie.StarShips.Add(presp.name);
            }

            return mymovie;
        }





    }



    public class FilmResponse
    {
        public string title { get; set; }
        public int episode_id { get; set; }
        public string release_date { get; set; }
        public List<string> characters { get; set; }
        public List<string> starships { get; set; }
    }



    public class CharacterResponse
    {
        public string name { get; set; }
       

    }
    public class StarShipResponse
    {
        public string name { get; set; }
        public string model { get; set; }
        public string manufacturer { get; set; }
        public string cost_in_credits { get; set; }
    }

   
}

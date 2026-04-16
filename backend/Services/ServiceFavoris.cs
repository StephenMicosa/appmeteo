using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace WeatherApp.Services
{
    public class FavoritesService
    {
        private readonly string _filePath;

        public FavoritesService()
        {
            // On sauvegarde les favoris dans le dossier de l'application
            string appFolder = AppDomain.CurrentDomain.BaseDirectory;
            _filePath = Path.Combine(appFolder, "favorites.json");
        }

        public List<string> GetFavorites()
        {
            if (!File.Exists(_filePath))
                return new List<string>();

            string json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<string>>(json) ?? new List<string>();
        }

        public void AddFavorite(string city)
        {
            List<string> favorites = GetFavorites();

            // Pas de doublons (insensible à la casse)
            if (favorites.Exists(f => f.ToLower() == city.ToLower()))
                return;

            favorites.Add(city);
            SaveFavorites(favorites);
        }

        public void RemoveFavorite(string city)
        {
            List<string> favorites = GetFavorites();
            favorites.RemoveAll(f => f.ToLower() == city.ToLower());
            SaveFavorites(favorites);
        }

        public bool IsFavorite(string city)
        {
            return GetFavorites().Exists(f => f.ToLower() == city.ToLower());
        }

        private void SaveFavorites(List<string> favorites)
        {
            string json = JsonConvert.SerializeObject(favorites, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }
    }
}

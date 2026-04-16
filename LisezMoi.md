# Application Meteo - WinForms C#

Application meteo developpee en C# avec WinForms.
Elle utilise l'API OpenWeatherMap pour afficher la meteo actuelle, les previsions sur 5 jours et les previsions horaires (24h).

## Fonctionnalites

- Recherche meteo par ville (bouton ou touche Entree)
- Affichage de la meteo actuelle: temperature, ressenti, humidite, vent, nuages, icone
- Previsions sur 5 jours
- Previsions horaires sur 24h (pas de 3h)
- Gestion des favoris (fichier JSON local)
- Gestion des erreurs (ville introuvable, champ vide, etc.)

## Prerequis

- Windows
- .NET SDK 8.0+
- Visual Studio 2022 ou VS Code
- Une cle API OpenWeatherMap

## Configuration

L'application lit les variables d'environnement depuis un fichier `.env` a la racine du repo (charge automatiquement au demarrage).

Exemple de contenu:

```env
OPENWEATHER_API_KEY=your_api_key_here
OPENWEATHER_GEO_URL=https://api.openweathermap.org
OPENWEATHER_WEATHER_URL=https://api.openweathermap.org
```

Notes:
- `OPENWEATHER_API_KEY` est obligatoire
- `.env` doit rester ignore par Git

## Installation et execution

```bash
dotnet restore
dotnet build
dotnet run --project WeatherApp.csproj
```

Vous pouvez aussi ouvrir `appM-t-o.sln` dans Visual Studio et lancer avec F5.

## Structure du projet

```text
.
|- Program.cs
|- WeatherApp.csproj
|- appM-t-o.sln
|- backend/
|  |- Configuration/
|  |  |- EnvLoader.cs
|  |- Forms/
|  |  |- MainForm.cs
|  |  |- MainForm.Designer.cs
|  |- Models/
|  |  |- WeatherData.cs
|  |  |- ForecastDay.cs
|  |  |- HourlyForecast.cs
|  |- Services/
|  |  |- GeocodingService.cs
|  |  |- WeatherService.cs
|  |  |- FavoritesService.cs
|- docs/
```

## Dependances

- Newtonsoft.Json (parsing JSON des reponses OpenWeather)

## API utilisee

- https://openweathermap.org/api
- Endpoints:
	- `/geo/1.0/direct`
	- `/data/2.5/weather`
	- `/data/2.5/forecast`
	- `/img/wn/{icon}@2x.png`

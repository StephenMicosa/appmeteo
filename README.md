# 🌤 Application Météo - WinForms C#

Application météo développée en C# avec WinForms dans le cadre du projet scolaire.
Elle utilise l'API OpenWeatherMap pour afficher la météo en temps réel.

## Fonctionnalités

- 🔍 Recherche météo par ville (bouton ou touche Entrée)
- 🌡️ Affichage de la météo actuelle : température, ressenti, humidité, vent, nuages, icône
- 📅 Prévisions sur 5 jours
- ⭐ Gestion des favoris (sauvegardés en JSON)
- ✅ Gestion des erreurs (ville introuvable, champ vide, etc.)

## Prérequis

- Visual Studio 2022 (ou VS Code avec le SDK .NET)
- .NET 6.0 SDK (Windows)
- Une clé API OpenWeatherMap (gratuite)

## Installation

### 1. Cloner le dépôt

```bash
git clone https://github.com/votre-nom/weather-app.git
cd weather-app
```

### 2. Obtenir une clé API OpenWeatherMap

1. Créer un compte sur [https://openweathermap.org/api](https://openweathermap.org/api)
2. Aller dans "My API keys"
3. Copier votre clé API

### 3. Ajouter la clé API dans le code

Dans le fichier `Services/WeatherService.cs`, remplacer :

```csharp
private const string API_KEY = "VOTRE_CLE_API_ICI";
```

par votre vraie clé API.

> ⚠️ **Attention** : Ne commitez pas votre clé API sur GitHub ! Ajoutez `favorites.json` dans votre `.gitignore`.

### 4. Restaurer les packages NuGet

```bash
dotnet restore
```

### 5. Lancer l'application

```bash
dotnet run
```

Ou ouvrir le fichier `.sln` dans Visual Studio et appuyer sur F5.

## Structure du projet

```
WeatherApp/
│
├── Models/
│   └── WeatherData.cs        # Modèles de données (météo actuelle + prévisions)
│
├── Services/
│   ├── WeatherService.cs     # Appels à l'API OpenWeatherMap
│   └── FavoritesService.cs   # Gestion des favoris (lecture/écriture JSON)
│
├── Forms/
│   ├── MainForm.cs           # Logique de l'interface principale
│   └── MainForm.Designer.cs  # Définition des contrôles WinForms
│
├── Program.cs                # Point d'entrée
├── WeatherApp.csproj         # Fichier projet
└── README.md
```

## Packages utilisés

- **Newtonsoft.Json** : pour parser les réponses JSON de l'API

## API utilisée

[OpenWeatherMap](https://openweathermap.org/api) - Plan gratuit suffisant pour ce projet (60 requêtes/minute).

Endpoints utilisés :
- `/data/2.5/weather` — météo actuelle
- `/data/2.5/forecast` — prévisions 5 jours
- `https://openweathermap.org/img/wn/{icon}@2x.png` — icônes météo

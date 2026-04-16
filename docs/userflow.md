# User Flow - Application Meteo WinForms

## 1. Objectif
L'application permet a un utilisateur de:
- rechercher la meteo d'une ville,
- consulter la meteo actuelle,
- consulter les previsions sur 5 jours,
- consulter les previsions horaires sur 24h (pas de 3h),
- gerer une liste de villes favorites.

## 2. Parcours utilisateur principal

### Etape 0 - Demarrage de l'application
1. L'utilisateur lance l'application.
2. L'application charge les variables de configuration depuis le fichier .env.
3. La fenetre principale s'ouvre avec:
   - zone de recherche de ville,
   - bouton Rechercher,
   - bloc favoris,
   - zones de resultats meteo (masquees tant qu'il n'y a pas de recherche).

### Etape 1 - Recherche d'une ville
1. L'utilisateur saisit une ville (ex: Paris).
2. Il clique sur Rechercher (ou appuie sur Entree).
3. L'application verifie que le champ n'est pas vide.
4. Si vide: message d'avertissement.

### Etape 2 - Geocodage (ville vers coordonnees)
1. L'application envoie la ville au service de geocodage.
2. Le service appelle OpenWeather Geocoding API.
3. L'API retourne latitude/longitude.
4. Si la ville est introuvable: message d'erreur.

### Etape 3 - Recuperation meteo
1. Avec lat/lon, l'application appelle:
   - meteo actuelle,
   - previsions 5 jours,
   - previsions 24h (3h).
2. Les donnees sont transformees en modeles internes.

### Etape 4 - Affichage des resultats
1. Bloc meteo actuelle:
   - ville, pays,
   - temperature, ressenti,
   - humidite, vent, nuages,
   - description + icone.
2. Bloc previsions 5 jours:
   - cartes journalieres (min/max + description + icone).
3. Bloc previsions 24h:
   - cartes horaires (heure, temperature, humidite + icone).

### Etape 5 - Gestion des favoris
1. L'utilisateur clique sur Ajouter aux favoris.
2. La ville est enregistree dans favorites.json.
3. Il peut:
   - double-cliquer un favori pour relancer une recherche,
   - supprimer un favori.

## 3. Parcours alternatif et erreurs
- Champ ville vide: avertissement utilisateur.
- Ville invalide: erreur Ville introuvable.
- API indisponible/erreur reseau: message d'erreur.
- Erreur chargement icone: l'application continue sans bloquer.

## 4. Sequence simplifiee
1. UI -> WeatherService: Search(city)
2. WeatherService -> GeocodingService: GetCoordinates(city)
3. GeocodingService -> OpenWeather Geocoding API
4. WeatherService -> OpenWeather Weather/Forecast APIs (lat/lon)
5. WeatherService -> UI: objets meteo
6. UI -> FavoritesService: add/remove/list favoris

## 5. Valeur utilisateur
- Interface simple et rapide.
- Donnees meteo completes (actuel + 5 jours + 24h).
- Experience personnalisable via favoris.

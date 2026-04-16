# Presentation - Application Meteo (2m30)

## Slide 1 - Titre
- Application Meteo WinForms en C#
- Projet scolaire
- Auteur: [Ton nom]

Message cle:
Application desktop simple, utile, et evolutive pour consulter la meteo.

## Slide 2 - Besoin
- Besoin: obtenir rapidement la meteo d'une ville
- Limites d'une simple recherche web:
  - pas personnalisee,
  - pas centralisee,
  - pas de favoris persistants

Message cle:
L'application regroupe recherche, previsions et favoris dans un seul outil.

## Slide 3 - Fonctionnalites principales
- Recherche par ville
- Meteo actuelle detaillee
- Previsions sur 5 jours
- Previsions horaires sur 24h (pas de 3h)
- Gestion des favoris (JSON)

Message cle:
Le scope couvre un vrai besoin utilisateur avec une UX claire.

## Slide 4 - Architecture en bref
- Frontend desktop: WinForms
- Services:
  - GeocodingService (ville -> coordonnees)
  - WeatherService (meteo + previsions)
  - FavoritesService (stockage local)
- Configuration: .env

Message cle:
Separation des responsabilites pour un code plus maintenable.

## Slide 5 - User Flow
1. Saisie ville
2. Geocodage via API
3. Appels meteo via lat/lon
4. Affichage cartes meteo
5. Ajout/suppression favoris

Message cle:
Le parcours est simple: l'utilisateur saisit une ville et obtient directement les donnees utiles.

## Slide 6 - Demo rapide
- Rechercher Paris
- Afficher meteo actuelle
- Montrer previsions 5 jours
- Montrer previsions 24h
- Ajouter puis supprimer un favori

Message cle:
Validation visuelle immediate de la valeur produit.

## Slide 7 - Defis et solutions
- Gestion erreurs API
- Null safety sur parsing JSON
- API gratuite limitee a 5 jours / 3h

Message cle:
Les contraintes techniques ont ete gerees proprement.

## Slide 8 - Conclusion et perspectives
- Projet fonctionnel et proprement structure
- Perspectives:
  - cartes meteo geographiques,
  - multi-langue,
  - graphiques temperature,
  - notifications meteo

Message cle:
Base simple, claire, et facile a faire evoluer.

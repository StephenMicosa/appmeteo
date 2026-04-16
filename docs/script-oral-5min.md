# Script Oral - 2m30

## 0:00 - 0:20 | Introduction
Bonjour, je vais vous presenter mon application Meteo developpee en C# avec WinForms.
L'objectif etait de creer une application desktop simple, claire et utile pour consulter la meteo d'une ville.

## 0:20 - 0:45 | Le besoin
Le besoin de depart est simple: obtenir rapidement des informations meteo fiables pour une ville.
Plutot que d'ouvrir un navigateur a chaque fois, j'ai centralise les fonctions principales dans une interface unique.
L'utilisateur peut rechercher une ville, voir les donnees importantes, et garder ses villes frequentes en favoris.

## 0:45 - 1:20 | Fonctionnalites principales
L'application propose quatre blocs utiles.
Premier bloc: la recherche par ville.
Deuxieme bloc: l'affichage de la meteo actuelle avec temperature, ressenti, humidite, vent, nuages et icone.
Troisieme bloc: les previsions sur 5 jours.
Quatrieme bloc: les previsions sur 24 heures, plus les favoris sauvegardes localement en JSON.

## 1:20 - 1:55 | Comment ca marche techniquement
Au lancement, l'application charge les variables d'environnement depuis le fichier .env, notamment la cle API.
Quand l'utilisateur saisit une ville, un service de geocodage convertit le nom en latitude et longitude.
Ensuite, le service meteo appelle OpenWeather avec ces coordonnees pour recuperer la meteo actuelle et les previsions.
Le code est separe en services pour garder une architecture lisible et evolutive.

## 1:55 - 2:15 | Demo guidee
Ici je saisis une ville, par exemple Paris, puis je lance la recherche.
On voit d'abord la meteo actuelle, puis les previsions sur 5 jours.
En dessous, on voit les previsions sur les prochaines 24 heures.
Je peux ensuite ajouter la ville en favori.
Le favori est sauvegarde localement, et je peux le relancer par double-clic ou le supprimer.

## 2:15 - 2:30 | Conclusion
Pour conclure, cette application repond au besoin initial avec une interface simple et des fonctionnalites completes pour un usage quotidien.
Le projet est proprement structure, avec une separation entre geocodage, meteo, et gestion des favoris.
Comme evolutions possibles, je pourrais ajouter des graphiques de temperature, des notifications meteo, et une interface encore plus moderne.
Merci pour votre attention.


using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.Forms
{
    public partial class MainForm : Form
    {
        private readonly WeatherService _weatherService;
        private readonly FavoritesService _favoritesService;
        private WeatherData? _currentWeather;
        private Label? _lblHourlyForecastTitle;
        private FlowLayoutPanel? _panelHourlyForecast;

        public MainForm()
        {
            InitializeComponent();
            _weatherService = new WeatherService();
            _favoritesService = new FavoritesService();
            InitializeHourlyForecastControls();

            LoadFavorites();
        }

        // Recherche déclenchée par le bouton ou la touche Entrée
        private async void BtnSearch_Click(object sender, EventArgs e)
        {
            await SearchWeather();
        }

        private async void TxtCity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                await SearchWeather();
        }

        private async Task SearchWeather()
        {
            string city = txtCity.Text.Trim();

            if (string.IsNullOrEmpty(city))
            {
                MessageBox.Show("Veuillez entrer le nom d'une ville.", "Champ vide",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // On désactive les contrôles pendant le chargement
            SetLoading(true);

            try
            {
                // Appel API météo actuelle
                _currentWeather = await _weatherService.GetCurrentWeather(city);
                DisplayCurrentWeather(_currentWeather);

                // Appel API prévisions
                List<ForecastDay> forecast = await _weatherService.GetForecast(city);
                DisplayForecast(forecast);

                // Appel API prévisions horaires (24h, pas de 3h)
                List<HourlyForecast> hourlyForecast = await _weatherService.GetHourlyForecast24h(city);
                DisplayHourlyForecast(hourlyForecast);

                // Met à jour le bouton favoris
                UpdateFavoriteButton();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur: {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetLoading(false);
            }
        }

        private async void DisplayCurrentWeather(WeatherData weather)
        {
            lblCityName.Text = $"{weather.CityName}, {weather.Country}";
            lblTemperature.Text = $"{weather.Temperature:F1}°C";
            lblFeelsLike.Text = $"Ressenti : {weather.FeelsLike:F1}°C";
            lblDescription.Text = weather.Description;
            lblHumidity.Text = $"💧 Humidité : {weather.Humidity}%";
            lblWind.Text = $"💨 Vent : {weather.WindSpeed} m/s";
            lblClouds.Text = $"☁️ Nuages : {weather.CloudCoverage}%";
            lblDate.Text = weather.Date.ToString("dddd dd MMMM yyyy");

            // Chargement de l'icône météo
            try
            {
                Image icon = await _weatherService.GetWeatherIcon(weather.Icon);
                picWeatherIcon.Image = icon;
            }
            catch
            {
                // Pas grave si l'icône ne charge pas
                picWeatherIcon.Image = null;
            }

            panelMain.Visible = true;
        }

        private async void DisplayForecast(List<ForecastDay> forecast)
        {
            // On efface les anciennes prévisions
            panelForecast.Controls.Clear();

            foreach (ForecastDay day in forecast)
            {
                Panel dayPanel = CreateForecastPanel(day);
                panelForecast.Controls.Add(dayPanel);
            }
        }

        private void InitializeHourlyForecastControls()
        {
            _lblHourlyForecastTitle = new Label
            {
                Text = "Prévisions prochaines 24h (toutes les 3h)",
                ForeColor = Color.FromArgb(149, 165, 166),
                Font = new Font("Segoe UI", 8),
                Location = new Point(20, 585),
                AutoSize = true
            };

            _panelHourlyForecast = new FlowLayoutPanel
            {
                Location = new Point(20, 605),
                Size = new Size(680, 140),
                BackColor = Color.Transparent,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoScroll = true
            };

            Controls.Add(_lblHourlyForecastTitle);
            Controls.Add(_panelHourlyForecast);
        }

        private void DisplayHourlyForecast(List<HourlyForecast> hourlyForecast)
        {
            if (_panelHourlyForecast == null)
            {
                return;
            }

            _panelHourlyForecast.Controls.Clear();

            foreach (HourlyForecast hour in hourlyForecast)
            {
                Panel hourPanel = CreateHourlyForecastPanel(hour);
                _panelHourlyForecast.Controls.Add(hourPanel);
            }
        }

        private Panel CreateHourlyForecastPanel(HourlyForecast hour)
        {
            Panel panel = new Panel
            {
                Width = 100,
                Height = 120,
                Margin = new Padding(5),
                BackColor = Color.FromArgb(52, 73, 94)
            };

            Label lblTime = new Label
            {
                Text = hour.DateTime.ToString("HH:mm"),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                AutoSize = false,
                Width = 90,
                TextAlign = ContentAlignment.MiddleCenter,
                Top = 6,
                Left = 5
            };

            PictureBox pic = new PictureBox
            {
                Width = 36,
                Height = 36,
                Top = 28,
                Left = 32,
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            Label lblTemp = new Label
            {
                Text = $"{hour.Temperature:F0}°C",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9),
                AutoSize = false,
                Width = 90,
                TextAlign = ContentAlignment.MiddleCenter,
                Top = 66,
                Left = 5
            };

            Label lblInfo = new Label
            {
                Text = $"💧{hour.Humidity}%",
                ForeColor = Color.FromArgb(189, 195, 199),
                Font = new Font("Segoe UI", 7),
                AutoSize = false,
                Width = 90,
                TextAlign = ContentAlignment.MiddleCenter,
                Top = 90,
                Left = 5
            };

            panel.Controls.AddRange(new Control[] { lblTime, pic, lblTemp, lblInfo });
            LoadForecastIcon(pic, hour.Icon);

            return panel;
        }

        private Panel CreateForecastPanel(ForecastDay day)
        {
            Panel panel = new Panel
            {
                Width = 120,
                Height = 160,
                Margin = new Padding(5),
                BackColor = Color.FromArgb(52, 73, 94),
                Cursor = Cursors.Default
            };

            // Jour de la semaine
            Label lblDay = new Label
            {
                Text = day.Date.ToString("dddd"),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                AutoSize = false,
                Width = 110,
                TextAlign = ContentAlignment.MiddleCenter,
                Top = 5,
                Left = 5
            };

            // PictureBox pour l'icône
            PictureBox pic = new PictureBox
            {
                Width = 50,
                Height = 50,
                Top = 25,
                Left = 35,
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            // Température min/max
            Label lblTemp = new Label
            {
                Text = $"{day.TempMax:F0}° / {day.TempMin:F0}°",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9),
                AutoSize = false,
                Width = 110,
                TextAlign = ContentAlignment.MiddleCenter,
                Top = 80,
                Left = 5
            };

            // Description
            Label lblDesc = new Label
            {
                Text = day.Description,
                ForeColor = Color.FromArgb(189, 195, 199),
                Font = new Font("Segoe UI", 7),
                AutoSize = false,
                Width = 110,
                Height = 35,
                TextAlign = ContentAlignment.MiddleCenter,
                Top = 100,
                Left = 5
            };

            panel.Controls.AddRange(new Control[] { lblDay, pic, lblTemp, lblDesc });

            // Chargement async de l'icône (sans bloquer l'UI)
            LoadForecastIcon(pic, day.Icon);

            return panel;
        }

        private async void LoadForecastIcon(PictureBox pic, string iconCode)
        {
            try
            {
                Image icon = await _weatherService.GetWeatherIcon(iconCode);
                if (!pic.IsDisposed)
                    pic.Image = icon;
            }
            catch { /* Si l'icone ne charge pas c'est pas grave */ }
        }

        private void BtnFavorite_Click(object sender, EventArgs e)
        {
            if (_currentWeather == null) return;

            string city = _currentWeather.CityName;

            if (_favoritesService.IsFavorite(city))
            {
                _favoritesService.RemoveFavorite(city);
                MessageBox.Show($"{city} retiré des favoris.", "Favoris",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _favoritesService.AddFavorite(city);
                MessageBox.Show($"{city} ajouté aux favoris !", "Favoris",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            UpdateFavoriteButton();
            LoadFavorites();
        }

        private void UpdateFavoriteButton()
        {
            if (_currentWeather == null) return;

            bool isFav = _favoritesService.IsFavorite(_currentWeather.CityName);
            btnFavorite.Text = isFav ? "★ Retirer des favoris" : "☆ Ajouter aux favoris";
            btnFavorite.BackColor = isFav ? Color.FromArgb(231, 76, 60) : Color.FromArgb(39, 174, 96);
        }

        private void LoadFavorites()
        {
            listFavorites.Items.Clear();
            List<string> favorites = _favoritesService.GetFavorites();

            foreach (string city in favorites)
                listFavorites.Items.Add(city);
        }

        // Double-clic sur un favori => recherche la météo
        private async void ListFavorites_DoubleClick(object sender, EventArgs e)
        {
            if (listFavorites.SelectedItem == null) return;

            txtCity.Text = listFavorites.SelectedItem.ToString();
            await SearchWeather();
        }

        // Bouton supprimer favori
        private void BtnRemoveFavorite_Click(object sender, EventArgs e)
        {
            if (listFavorites.SelectedItem == null)
            {
                MessageBox.Show("Sélectionnez d'abord une ville à supprimer.", "Attention",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string? city = listFavorites.SelectedItem as string;
            if (string.IsNullOrWhiteSpace(city))
            {
                return;
            }

            _favoritesService.RemoveFavorite(city);
            LoadFavorites();
            UpdateFavoriteButton();
        }

        private void SetLoading(bool loading)
        {
            btnSearch.Enabled = !loading;
            txtCity.Enabled = !loading;
            btnSearch.Text = loading ? "Chargement..." : "Rechercher";
            progressBar.Visible = loading;
        }
    }
}

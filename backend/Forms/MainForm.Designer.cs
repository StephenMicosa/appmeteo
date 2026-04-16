namespace WeatherApp.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtCity = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblCityName = new System.Windows.Forms.Label();
            this.lblTemperature = new System.Windows.Forms.Label();
            this.lblFeelsLike = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblHumidity = new System.Windows.Forms.Label();
            this.lblWind = new System.Windows.Forms.Label();
            this.lblClouds = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.picWeatherIcon = new System.Windows.Forms.PictureBox();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelForecast = new System.Windows.Forms.FlowLayoutPanel();
            this.btnFavorite = new System.Windows.Forms.Button();
            this.listFavorites = new System.Windows.Forms.ListBox();
            this.btnRemoveFavorite = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblFavTitle = new System.Windows.Forms.Label();
            this.lblSearchTitle = new System.Windows.Forms.Label();
            this.lblForecastTitle = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.picWeatherIcon)).BeginInit();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();

            // --- Form ---
            this.Text = "🌤 Application Météo";
            this.Size = new System.Drawing.Size(1000, 840);
            this.MinimumSize = new System.Drawing.Size(760, 620);
            this.BackColor = System.Drawing.Color.FromArgb(30, 39, 46);
            this.Font = new System.Drawing.Font("Segoe UI", 9);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.AutoScroll = true;

            // --- lblSearchTitle ---
            this.lblSearchTitle.Text = "Recherche";
            this.lblSearchTitle.ForeColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.lblSearchTitle.Font = new System.Drawing.Font("Segoe UI", 8);
            this.lblSearchTitle.Location = new System.Drawing.Point(20, 15);
            this.lblSearchTitle.AutoSize = true;

            // --- txtCity ---
            this.txtCity.Location = new System.Drawing.Point(20, 35);
            this.txtCity.Size = new System.Drawing.Size(250, 30);
            this.txtCity.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.txtCity.ForeColor = System.Drawing.Color.White;
            this.txtCity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCity.Font = new System.Drawing.Font("Segoe UI", 11);
            this.txtCity.PlaceholderText = "Entrez une ville...";
            this.txtCity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtCity_KeyPress);

            // --- btnSearch ---
            this.btnSearch.Text = "Rechercher";
            this.btnSearch.Location = new System.Drawing.Point(280, 35);
            this.btnSearch.Size = new System.Drawing.Size(110, 30);
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold);
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);

            // --- progressBar ---
            this.progressBar.Location = new System.Drawing.Point(20, 72);
            this.progressBar.Size = new System.Drawing.Size(370, 4);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.Visible = false;

            // --- panelMain (current weather) ---
            this.panelMain.Location = new System.Drawing.Point(20, 90);
            this.panelMain.Size = new System.Drawing.Size(580, 220);
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.panelMain.Visible = false;

            // --- picWeatherIcon (inside panelMain) ---
            this.picWeatherIcon.Location = new System.Drawing.Point(10, 10);
            this.picWeatherIcon.Size = new System.Drawing.Size(100, 100);
            this.picWeatherIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;

            // --- lblCityName ---
            this.lblCityName.Text = "";
            this.lblCityName.ForeColor = System.Drawing.Color.White;
            this.lblCityName.Font = new System.Drawing.Font("Segoe UI", 18, System.Drawing.FontStyle.Bold);
            this.lblCityName.Location = new System.Drawing.Point(120, 10);
            this.lblCityName.AutoSize = true;

            // --- lblDate ---
            this.lblDate.Text = "";
            this.lblDate.ForeColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.lblDate.Font = new System.Drawing.Font("Segoe UI", 9);
            this.lblDate.Location = new System.Drawing.Point(120, 50);
            this.lblDate.AutoSize = true;

            // --- lblTemperature ---
            this.lblTemperature.Text = "";
            this.lblTemperature.ForeColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.lblTemperature.Font = new System.Drawing.Font("Segoe UI", 28, System.Drawing.FontStyle.Bold);
            this.lblTemperature.Location = new System.Drawing.Point(120, 65);
            this.lblTemperature.AutoSize = true;

            // --- lblFeelsLike ---
            this.lblFeelsLike.Text = "";
            this.lblFeelsLike.ForeColor = System.Drawing.Color.FromArgb(189, 195, 199);
            this.lblFeelsLike.Font = new System.Drawing.Font("Segoe UI", 9);
            this.lblFeelsLike.Location = new System.Drawing.Point(120, 115);
            this.lblFeelsLike.AutoSize = true;

            // --- lblDescription ---
            this.lblDescription.Text = "";
            this.lblDescription.ForeColor = System.Drawing.Color.White;
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Italic);
            this.lblDescription.Location = new System.Drawing.Point(10, 115);
            this.lblDescription.AutoSize = true;

            // --- lblHumidity ---
            this.lblHumidity.Text = "";
            this.lblHumidity.ForeColor = System.Drawing.Color.White;
            this.lblHumidity.Font = new System.Drawing.Font("Segoe UI", 9);
            this.lblHumidity.Location = new System.Drawing.Point(10, 150);
            this.lblHumidity.AutoSize = true;

            // --- lblWind ---
            this.lblWind.Text = "";
            this.lblWind.ForeColor = System.Drawing.Color.White;
            this.lblWind.Font = new System.Drawing.Font("Segoe UI", 9);
            this.lblWind.Location = new System.Drawing.Point(10, 175);
            this.lblWind.AutoSize = true;

            // --- lblClouds ---
            this.lblClouds.Text = "";
            this.lblClouds.ForeColor = System.Drawing.Color.White;
            this.lblClouds.Font = new System.Drawing.Font("Segoe UI", 9);
            this.lblClouds.Location = new System.Drawing.Point(10, 200);
            this.lblClouds.AutoSize = true;

            // --- btnFavorite ---
            this.btnFavorite.Text = "☆ Ajouter aux favoris";
            this.btnFavorite.Location = new System.Drawing.Point(20, 320);
            this.btnFavorite.Size = new System.Drawing.Size(180, 32);
            this.btnFavorite.BackColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.btnFavorite.ForeColor = System.Drawing.Color.White;
            this.btnFavorite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFavorite.FlatAppearance.BorderSize = 0;
            this.btnFavorite.Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold);
            this.btnFavorite.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFavorite.Click += new System.EventHandler(this.BtnFavorite_Click);

            // --- lblForecastTitle ---
            this.lblForecastTitle.Text = "Prévisions sur 4 jours";
            this.lblForecastTitle.ForeColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.lblForecastTitle.Font = new System.Drawing.Font("Segoe UI", 8);
            this.lblForecastTitle.Location = new System.Drawing.Point(20, 365);
            this.lblForecastTitle.AutoSize = true;

            // --- panelForecast ---
            this.panelForecast.Location = new System.Drawing.Point(20, 385);
            this.panelForecast.Size = new System.Drawing.Size(680, 185);
            this.panelForecast.BackColor = System.Drawing.Color.Transparent;
            this.panelForecast.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.panelForecast.WrapContents = false;
            this.panelForecast.AutoScroll = true;

            // --- Favoris panel (droite) ---
            this.lblFavTitle.Text = "⭐ Mes favoris";
            this.lblFavTitle.ForeColor = System.Drawing.Color.FromArgb(241, 196, 15);
            this.lblFavTitle.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            this.lblFavTitle.Location = new System.Drawing.Point(720, 15);
            this.lblFavTitle.AutoSize = true;

            // --- listFavorites ---
            this.listFavorites.Location = new System.Drawing.Point(720, 40);
            this.listFavorites.Size = new System.Drawing.Size(240, 250);
            this.listFavorites.BackColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.listFavorites.ForeColor = System.Drawing.Color.White;
            this.listFavorites.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listFavorites.Font = new System.Drawing.Font("Segoe UI", 10);
            this.listFavorites.IntegralHeight = false;
            this.listFavorites.DoubleClick += new System.EventHandler(this.ListFavorites_DoubleClick);

            // --- btnRemoveFavorite ---
            this.btnRemoveFavorite.Text = "🗑 Supprimer";
            this.btnRemoveFavorite.Location = new System.Drawing.Point(720, 300);
            this.btnRemoveFavorite.Size = new System.Drawing.Size(120, 30);
            this.btnRemoveFavorite.BackColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.btnRemoveFavorite.ForeColor = System.Drawing.Color.White;
            this.btnRemoveFavorite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveFavorite.FlatAppearance.BorderSize = 0;
            this.btnRemoveFavorite.Font = new System.Drawing.Font("Segoe UI", 9);
            this.btnRemoveFavorite.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRemoveFavorite.Click += new System.EventHandler(this.BtnRemoveFavorite_Click);

            // Ajouter les contrôles dans panelMain
            this.panelMain.Controls.Add(this.picWeatherIcon);
            this.panelMain.Controls.Add(this.lblCityName);
            this.panelMain.Controls.Add(this.lblDate);
            this.panelMain.Controls.Add(this.lblTemperature);
            this.panelMain.Controls.Add(this.lblFeelsLike);
            this.panelMain.Controls.Add(this.lblDescription);
            this.panelMain.Controls.Add(this.lblHumidity);
            this.panelMain.Controls.Add(this.lblWind);
            this.panelMain.Controls.Add(this.lblClouds);

            // Ajouter tous les contrôles au Form
            this.Controls.Add(this.lblSearchTitle);
            this.Controls.Add(this.txtCity);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.btnFavorite);
            this.Controls.Add(this.lblForecastTitle);
            this.Controls.Add(this.panelForecast);
            this.Controls.Add(this.lblFavTitle);
            this.Controls.Add(this.listFavorites);
            this.Controls.Add(this.btnRemoveFavorite);

            ((System.ComponentModel.ISupportInitialize)(this.picWeatherIcon)).EndInit();
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // Déclaration des contrôles
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblCityName;
        private System.Windows.Forms.Label lblTemperature;
        private System.Windows.Forms.Label lblFeelsLike;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblHumidity;
        private System.Windows.Forms.Label lblWind;
        private System.Windows.Forms.Label lblClouds;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.PictureBox picWeatherIcon;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.FlowLayoutPanel panelForecast;
        private System.Windows.Forms.Button btnFavorite;
        private System.Windows.Forms.ListBox listFavorites;
        private System.Windows.Forms.Button btnRemoveFavorite;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblFavTitle;
        private System.Windows.Forms.Label lblSearchTitle;
        private System.Windows.Forms.Label lblForecastTitle;
    }
}

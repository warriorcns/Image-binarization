namespace Binaryzacja
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.buttonMain = new System.Windows.Forms.Button();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.thresholdLabel = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pixtureBoxOriginalImage = new System.Windows.Forms.PictureBox();
            this.pictureBoxOtsu = new System.Windows.Forms.PictureBox();
            this.pictureBoxBernsen = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pixtureBoxOriginalImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOtsu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBernsen)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonMain
            // 
            this.buttonMain.Location = new System.Drawing.Point(13, 13);
            this.buttonMain.Name = "buttonMain";
            this.buttonMain.Size = new System.Drawing.Size(75, 23);
            this.buttonMain.TabIndex = 0;
            this.buttonMain.Text = "Run";
            this.buttonMain.UseVisualStyleBackColor = true;
            this.buttonMain.Click += new System.EventHandler(this.buttonMain_Click);
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(13, 42);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(947, 151);
            this.chart1.TabIndex = 1;
            this.chart1.Text = "chart1";
            // 
            // thresholdLabel
            // 
            this.thresholdLabel.AutoSize = true;
            this.thresholdLabel.Location = new System.Drawing.Point(231, 18);
            this.thresholdLabel.Name = "thresholdLabel";
            this.thresholdLabel.Size = new System.Drawing.Size(14, 13);
            this.thresholdLabel.TabIndex = 2;
            this.thresholdLabel.Text = "T";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(95, 13);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.pixtureBoxOriginalImage);
            this.panel1.Controls.Add(this.pictureBoxOtsu);
            this.panel1.Controls.Add(this.pictureBoxBernsen);
            this.panel1.Location = new System.Drawing.Point(13, 199);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(947, 318);
            this.panel1.TabIndex = 4;
            // 
            // pixtureBoxOriginalImage
            // 
            this.pixtureBoxOriginalImage.Location = new System.Drawing.Point(70, 81);
            this.pixtureBoxOriginalImage.Name = "pixtureBoxOriginalImage";
            this.pixtureBoxOriginalImage.Size = new System.Drawing.Size(162, 124);
            this.pixtureBoxOriginalImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pixtureBoxOriginalImage.TabIndex = 8;
            this.pixtureBoxOriginalImage.TabStop = false;
            // 
            // pictureBoxOtsu
            // 
            this.pictureBoxOtsu.Location = new System.Drawing.Point(375, 81);
            this.pictureBoxOtsu.Name = "pictureBoxOtsu";
            this.pictureBoxOtsu.Size = new System.Drawing.Size(151, 124);
            this.pictureBoxOtsu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxOtsu.TabIndex = 7;
            this.pictureBoxOtsu.TabStop = false;
            // 
            // pictureBoxBernsen
            // 
            this.pictureBoxBernsen.Location = new System.Drawing.Point(683, 69);
            this.pictureBoxBernsen.Name = "pictureBoxBernsen";
            this.pictureBoxBernsen.Size = new System.Drawing.Size(176, 136);
            this.pictureBoxBernsen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxBernsen.TabIndex = 6;
            this.pictureBoxBernsen.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(972, 529);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.thresholdLabel);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.buttonMain);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pixtureBoxOriginalImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOtsu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBernsen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonMain;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label thresholdLabel;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pixtureBoxOriginalImage;
        private System.Windows.Forms.PictureBox pictureBoxOtsu;
        private System.Windows.Forms.PictureBox pictureBoxBernsen;
    }
}


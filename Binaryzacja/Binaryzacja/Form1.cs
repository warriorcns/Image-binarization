using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MoreLinq;
//using System.Reactive.Linq;

namespace Binaryzacja
{
    public partial class Form1 : Form
    {
        private static int _maxPixelValue = 256;
        private static Image _sampleImage = Image.FromFile(@"image.jpg");
        private static int _imageSize = _sampleImage.Height * _sampleImage.Width;
        private static GrayPixel[] _originalPixels = new GrayPixel[_imageSize];

        //example method
        public static Bitmap MakeGrayscale(Bitmap original)
        {
            //make an empty bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            for (int i = 0; i < original.Width; i++)
            {
                for (int j = 0; j < original.Height; j++)
                {
                    //get the pixel from the original image
                    Color originalColor = original.GetPixel(i, j);

                    //create the grayscale version of the pixel
                    int grayScale = (int)((originalColor.R * .3) + (originalColor.G * .59)
                        + (originalColor.B * .11));

                    //create the color object
                    Color newColor = Color.FromArgb(grayScale, grayScale, grayScale);

                    //set the new image's pixel to the grayscale version
                    newBitmap.SetPixel(i, j, newColor);
                }
            }

            return newBitmap;
        }

        public static GrayPixelHistogram[] getAllPossibleConfigurationsGrayPixels()
        {
            GrayPixelHistogram[] hst = new GrayPixelHistogram[_maxPixelValue];
            for (int i = 0; i < _maxPixelValue; i++)
                hst[i] = new GrayPixelHistogram();

            for (int i = 0; i < _maxPixelValue; i++)
            {
                hst[i].Value = i;
                hst[i].Count = 0;
            }

            return hst;
        }

        public static GrayPixelHistogram[] getGrayHistogramFromImage(Image image)
        {
            int matrixSize = image.Height * image.Width;
            int j = 0;
            using (Bitmap bmp = new Bitmap(image))
            {
                Color originalColor;
                GrayPixelHistogram[] pixels = new GrayPixelHistogram[matrixSize];
                GrayPixelHistogram[] allHstValues = new GrayPixelHistogram[_maxPixelValue];
                GrayPixelHistogram[] sortedPixels = new GrayPixelHistogram[_maxPixelValue];
                
                for (int i = 0; i < matrixSize; i++)
                {
                    pixels[i] = new GrayPixelHistogram();
                    _originalPixels[i] = new GrayPixel();
                }

                for (int i = 0; i < _maxPixelValue; i++)
                {
                    allHstValues[i] = new GrayPixelHistogram();
                    sortedPixels[i] = new GrayPixelHistogram();
                }
                for (int x = 0; x < image.Width; x++)
                {
                    for (int y = 0; y < image.Height; y++)
                    {
                        originalColor = bmp.GetPixel(x, y);
                        //create the grayscale version of the pixel
                        pixels[j].Value = (int)((originalColor.R * .3) + (originalColor.G * .59) + (originalColor.B * .11));
                        _originalPixels[j].value = pixels[j].Value;
                        _originalPixels[j].R = originalColor.R;
                        _originalPixels[j].G = originalColor.G;
                        _originalPixels[j].B = originalColor.B;
                        _originalPixels[j].x = x;
                        _originalPixels[j].y = y;
                        
                        j++;
                    }
                }

                var listOfUniqueVectors = pixels.GroupBy(l => l.Value ).Select(g => new
                {
                    Value = g.Key,
                    Count = g.Select(l => l.Value).Count()
                });

                var sortedList = listOfUniqueVectors.OrderBy(r => r.Value);
                allHstValues = getAllPossibleConfigurationsGrayPixels();

                //int _counter = 0;
                //foreach (var item in sortedList)
                //{
                //    sortedPixels[_counter].Value = item.Value;
                //    sortedPixels[_counter].Count = item.Count;
                //    _counter++;
                //}
                
                //rewrite vectors into empty matrix
                for (int i = 0; i < _maxPixelValue; i++)
                {
                    foreach (var item in sortedList)
                    {
                        if (item.Value == allHstValues[i].Value)
                        {
                            allHstValues[i].Value = Convert.ToInt32(item.Value);
                            allHstValues[i].Count = item.Count;
                        }
                    }
                }
                return allHstValues;
            }
        }

        public void generateHistogram(GrayPixelHistogram[] histogram)
        {
            chart1.Series[0].IsVisibleInLegend = false;
            chart1.Series["Series1"].Points.Clear();
            foreach (var item in histogram)
                chart1.Series["Series1"].Points.AddXY(item.Value.ToString(), item.Count);       
        }

        public int Otsu(GrayPixelHistogram[] histogram)
        { 
            //def
            Probability[] _pixelProbability = new Probability[_maxPixelValue];
            Variance[] _thresholdVariance = new Variance[_maxPixelValue];
            Variance _threshold = new Variance();
            Bitmap myBitmap = new Bitmap(_sampleImage.Width, _sampleImage.Height);

            for (int i = 0; i < _maxPixelValue; i++)
            {
                _pixelProbability[i] = new Probability();
                _thresholdVariance[i] = new Variance();
            }

            int _counter = 0;
            double _objectProbability = 0;
            double _groundProbability = 0;
            double _objectAverage = 0;
            double _groundAverage = 0;
            //int _threshold = 0;

            //calculate probability for each pixel and store data
            foreach (var item in histogram)
            {
                _pixelProbability[_counter].pixel = item.Value;
                _pixelProbability[_counter].probability = ((double)item.Count / (double)_imageSize);
                //Console.WriteLine((float)item.Count / (float)_imageSize);
                _counter++;
            }
            
            //for each threshold -> t
            for (int t = 0; t < _maxPixelValue; t++)
            {
                _objectProbability = 0;
                _groundProbability = 0;
                _objectAverage = 0;
                _groundAverage = 0;

                //calculate probability of object
                for (int i = 0; i <= t; i++)
                {
                    _objectProbability += _pixelProbability[i].probability;
                }

                //calculate probability of ground
                for (int i = t + 1; i < _maxPixelValue; i++)
                {
                    _groundProbability += _pixelProbability[i].probability;
                }

                //calculate average of object
                for (int i = 0; i <= t; i++)
                {
                    _objectAverage += (i * _pixelProbability[i].probability / _objectProbability);
                }

                //calculate average of ground
                for (int i = t + 1; i < _maxPixelValue; i++)
                {
                    _groundAverage += (i * _pixelProbability[i].probability / _groundProbability);
                }

                //calculate viariance for each threshold -> t
                _thresholdVariance[t].threshold = t;
                _thresholdVariance[t].interViariance = _objectProbability * _groundProbability * (_objectAverage - _groundAverage) * (_objectAverage - _groundAverage);
            }
            //_threshold = _thresholdVariance.GroupBy(x => x.interViariance).Select(group => group.Where(x => x.interViariance == group.Max(y => y.interViariance))).First();
            _threshold = _thresholdVariance.MaxBy(x => x.interViariance);
            //GroupBy(x => x.Title).Select(group => group.Where(x => x.Price == group.Max(y => y.Price)).First());
            ;

            int j = 0;
            for (int Xcount = 0; Xcount < myBitmap.Width; Xcount++)
            {
                for (int Ycount = 0; Ycount < myBitmap.Height; Ycount++)
                {

                    if (_originalPixels[j].value > _threshold.threshold)
                    {
                        myBitmap.SetPixel(Xcount, Ycount, Color.White);
                    }
                    else if (_originalPixels[j].value < _threshold.threshold)
                    {
                        myBitmap.SetPixel(Xcount, Ycount, Color.Black);
                    }

                    j++;

                }
            }
            pictureBoxOtsu.Image = myBitmap;
            return _threshold.threshold;
        }

        //liczy prog dla kazdego pixela w obrazie (nie rysuje histogramu, tylko obraz)
        public Image Bernsen(Image sourceImage, int windowSize, int eps)
        {
            int matrixSize = sourceImage.Height * sourceImage.Width;
            Image im = Image.FromFile(@"image.jpg");
            Bitmap myBitmap = new Bitmap(sourceImage.Width, sourceImage.Height);
            try
            {
                if (windowSize % 2 != 0)
                {
                    //odczyt obrazu
                    using (Bitmap bmp = new Bitmap(sourceImage))
                    {
                        Color originalColor;
                        Color framePixelColor;
                        GrayPixel[] _pixels = new GrayPixel[matrixSize];
                        GrayPixel _framePixelTemp = new GrayPixel();
                        GrayPixel[] _framePixels = new GrayPixel[(windowSize - 1) * (windowSize - 1)];

                        int j = 0;
                        int counter = 0;
                        int distance = Convert.ToInt32((windowSize - 1) * 0.5);
                        int _maxTempValue = 0;
                        int _minTempValue = 0;
                        for (int i = 0; i < matrixSize; i++)
                        {
                            _pixels[i] = new GrayPixel();
                        }

                        for (int i = 0; i < (windowSize - 1) * (windowSize - 1); i++)
                        {
                            _framePixels[i] = new GrayPixel();
                        }

                        #region
                        for (int x = 0; x < sourceImage.Width; x++)
                        {
                            for (int y = 0; y < sourceImage.Height; y++)
                            {
                                originalColor = bmp.GetPixel(x, y);
                                //create the grayscale version of the pixel
                                _pixels[j].value = (int)((originalColor.R * .3) + (originalColor.G * .59) + (originalColor.B * .11));
                                _pixels[j].R = originalColor.R;
                                _pixels[j].G = originalColor.G;
                                _pixels[j].B = originalColor.B;
                                _pixels[j].x = x;
                                _pixels[j].y = y;

                                //tworzymy liste o wielkosci okna naokolo pixela 
                                if (x >= distance && x < sourceImage.Width - distance)
                                {
                                    if (y >= distance && y < sourceImage.Height - distance)
                                    {
                                        ;
                                        //_framePixels.Clear();
                                        counter = 0;
                                        //i,k - obramowanie okna po ktorym sie poruszam naokolo danego pixela
                                        //pobieram pixele naokolo pixela i licze prog za pomoca ich wartosci
                                        for (int i = x - distance; i < x + distance; i++)
                                        {
                                            for (int k = y - distance; k < y + distance; k++)
                                            {
                                                ;
                                                //pobieram wartosci dla kazdego pixela z okna i odrazu licze prog lokalny dla obecnego pixela(centralnego z okna)
                                                framePixelColor = bmp.GetPixel(i, k);
                                                _framePixels[counter].value = (int)((framePixelColor.R * .3) + (framePixelColor.G * .59) + (framePixelColor.B * .11));
                                                _framePixels[counter].x = i;
                                                _framePixels[counter].y = k;
                                                //lista pixeli w oknie - zle dodaje do listy? WTF - do poprawy
                                                //_framePixels.Add(_framePixelTemp);
                                                counter++;
                                            }
                                        }
                                        //obliczenia na kazdym pixelu w liscie w danej chwili by obliczyc prog dla pixela na pozycji globalnej x,y

                                        _maxTempValue = _framePixels.Max(max => max.value); //max

                                        // min
                                        _minTempValue = _framePixels.Min(min => min.value);
                                        // oblicz prog dla danego globalnego pixela
                                        if (_maxTempValue - _minTempValue <= eps)
                                        {
                                            _pixels[j].threshold = 121;//Convert.ToInt32((_maxTempValue + _minTempValue) * 0.5);
                                        }
                                        else
                                        {
                                            _pixels[j].threshold = Convert.ToInt32((_maxTempValue + _minTempValue) * 0.5);
                                        }
                                    }
                                }
                                else
                                { 
                                    //tu trafiaja przypadki, kiedy pixel jest za blisko brzegu obrazka - nie ruszac pixeli i wziac je z oryginalnego obrazka

                                }


                                j++;
                            }
                        }
                        #endregion
                        j = 0;
                        for (int Xcount = 0; Xcount < myBitmap.Width; Xcount++)
                        {
                            for (int Ycount = 0; Ycount < myBitmap.Height; Ycount++)
                            {
                                if (Xcount >= windowSize && Ycount >= windowSize && Xcount <= myBitmap.Width - windowSize && Ycount <= myBitmap.Height - windowSize)
                                {
                                    if (_pixels[j].value > _pixels[j].threshold)
                                    {
                                        myBitmap.SetPixel(Xcount, Ycount, Color.White);
                                    }
                                    else if (_pixels[j].value <= _pixels[j].threshold)
                                    {
                                        myBitmap.SetPixel(Xcount, Ycount, Color.Black);
                                    }
                                }
                                else
                                {
                                    myBitmap.SetPixel(Xcount, Ycount, Color.FromArgb(_pixels[j].R,_pixels[j].G,_pixels[j].B));
                                }
                                j++;
                                
                            }
                        }

                        im = myBitmap;
                    }
                }
                else { return im; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return im;
        }


        //public static GrayPixelHistogram[]
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Otsu");
            comboBox1.Items.Add("Bernsen");
            comboBox1.SelectedItem = "Bernsen";

        }

        private void buttonMain_Click(object sender, EventArgs e)
        {
            try
            {
                
                pixtureBoxOriginalImage.Image = Image.FromFile(@"image.jpg");
                switch (comboBox1.SelectedItem.ToString())
                {
                    case "Otsu":
                        var histogram = getGrayHistogramFromImage(_sampleImage);
                        thresholdLabel.Text = Otsu(histogram).ToString();
                        generateHistogram(histogram);
                        break;
                    case "Bernsen":
                        pictureBoxBernsen.Image = Bernsen(_sampleImage, 11, 100);
                        break;
                    default:
                        break;
                }
               
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }

    public class GrayPixelHistogram
    {
        public int Value { get; set; } //from 0 to 255
        public int Count { get; set; }
    }

    public class Variance
    {
        public int threshold { get; set; }
        public double interViariance { get; set; }
    }

    public class Probability
    {
        public int pixel { get; set; }
        public double probability { get; set; }
    }

    public class GrayPixel
    {
        public int value { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }
        public int threshold { get; set; } //prog lokalny dla danego pixela
    }
}


using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;


namespace Draw_Hypotrochoid
{
    public partial class Form_Main : Form
    {
        private readonly int _default_A =  80;
        private readonly int _default_B =  14;
        private readonly int _default_C = 100;

        private FloatTrackBar TrackBar_Radius      = new FloatTrackBar();
        private FloatTrackBar TrackBar_InnerCircle = new FloatTrackBar();
        private FloatTrackBar TrackBar_OuterCircle = new FloatTrackBar();

        private Color[] _color_Pereset_1;
        private Color[] _color_Pereset_2;

        private int    _iterations;
        private bool   _isSetup;
        private bool   _isComplete;
        private Color  _penColor;
        private Random _random;


        public Form_Main()
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            Setup();

            //DrawSpirograph();
            Invalidator();

        }   // Form_Main()


        private void Invalidator()
        {
            _isComplete = false;

            Invalidate();

        }   // Invalidator()


        protected override void OnPaint(PaintEventArgs e)
        {
            Debug.Print("OnPaint");

            base.OnPaint(e);

            if (_isComplete) return;

            Debug.Print("_iterations");

            _iterations = 0;

            int A = TrackBar_Radius     .Value;
            int B = TrackBar_InnerCircle.Value;
            int C = TrackBar_OuterCircle.Value;

            int iterations = int.Parse(txtIter.Text);
            int width      = pictureBox.ClientSize.Width;
            int height     = pictureBox.ClientSize.Height;

            Bitmap bitmap = new Bitmap(width, height);

            using (Graphics gr = Graphics.FromImage(bitmap))
            {
                gr.SmoothingMode = SmoothingMode.AntiAlias;

                int    cx    = width  / 2;
                int    cy    = height / 2;
                double t     = 0;
                double dt    = Math.PI / iterations;
                double max_t = 2 * Math.PI * B / GCD(A, B);
                double x1    = cx + X(t, A, B, C);
                double y1    = cy + Y(t, A, B, C);

                List<PointF> pointsList = new List<PointF>
                {
                    new PointF((float)x1, (float)y1)
                };

                while (t <= max_t)
                {
                    //_iterations++;
                    //lblIterationCount.Text = _iterations.ToString();

                    t += dt;
                    x1 = cx + X(t, A, B, C);
                    y1 = cy + Y(t, A, B, C);
                    pointsList.Add(new PointF((float)x1, (float)y1));
                }

                //// Draw the polygon.
                //using (Pen pen = new Pen(_penColor))
                //{
                //    gr.DrawPolygon(pen, pointsList.ToArray());
                //}

                Brush[] colors = { Brushes.Red, Brushes.Green, Brushes.Blue };

                // Draw the polygon.
                for (int i = 0; i < pointsList.Count - 1; i++)
                {
                    lblIterationCount.Text = i.ToString();

                    //var pen = new Pen(colors[i % colors.Length], 1);
                    //var pen = new Pen(_color_Pereset_1[i % _color_Pereset_1.Length], 1);
                    var pen = new Pen(_color_Pereset_2[i % _color_Pereset_2.Length], 1);
                    gr.DrawLine(pen, pointsList[i], pointsList[i + 1]);
                }
            }

            pictureBox.Image = bitmap;

            _isComplete = true;

        }   // OnPaint()


        private void BtnColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog
            {
                AllowFullOpen = true,
                AnyColor      = true,
                FullOpen      = true,
                Color         = GetRandomColor()
            };

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                // Use the selected color.
                _penColor = colorDialog.Color;

                //DrawSpirograph();
                Invalidator();
            }

        }   // BtnColor_Click()


        private void BtnQuit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to quit?", "Confirm Quit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }

        }   // BtnQuit_Click()


        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveSpirographImage();

        }   // BtnSave_Click()


        // Draw the hypotrochoid.
        private void DrawSpirograph()
        {
            //_iterations = 0;

            //int A = TrackBar_Radius     .Value;
            //int B = TrackBar_InnerCircle.Value;
            //int C = TrackBar_OuterCircle.Value;

            //int iterations = int.Parse(txtIter.Text);
            //int width      = pictureBox.ClientSize.Width;
            //int height     = pictureBox.ClientSize.Height;

            //Bitmap bitmap = new Bitmap(width, height);

            //using (Graphics gr = Graphics.FromImage(bitmap))
            //{
            //    gr.SmoothingMode = SmoothingMode.AntiAlias;

            //    int    cx    = width  / 2;
            //    int    cy    = height / 2;
            //    double t     = 0;
            //    double dt    = Math.PI / iterations;
            //    double max_t = 2 * Math.PI * B / GCD(A, B);
            //    double x1    = cx + X(t, A, B, C);
            //    double y1    = cy + Y(t, A, B, C);

            //    List<PointF> pointsList = new List<PointF>
            //    {
            //        new PointF((float)x1, (float)y1)
            //    };

            //    while (t <= max_t)
            //    {
            //        _iterations++;
            //        lblIterationCount.Text = _iterations.ToString();

            //        t += dt;
            //        x1 = cx + X(t, A, B, C);
            //        y1 = cy + Y(t, A, B, C);
            //        pointsList.Add(new PointF((float)x1, (float)y1));
            //    }

            //    //// Draw the polygon.
            //    //using (Pen pen = new Pen(_penColor))
            //    //{
            //    //    gr.DrawPolygon(pen, pointsList.ToArray());
            //    //}

            //    Brush[] colors = { Brushes.Red, Brushes.Green, Brushes.Blue };

            //    // Draw the polygon.
            //    for (int i = 0; i < pointsList.Count - 1; i++)
            //    {
            //        //var pen = new Pen(colors[i % colors.Length], 1);
            //        //var pen = new Pen(_color_Pereset_1[i % _color_Pereset_1.Length], 1);
            //        var pen = new Pen(_color_Pereset_2[i % _color_Pereset_2.Length], 1);
            //        gr.DrawLine(pen, pointsList[i], pointsList[i + 1]);
            //    }
            //}

            //pictureBox.Image = bitmap;

        }   // DrawSpirograph()


        private Color GetRandomColor()
        {
            Color _color = Color.FromArgb(_random.Next(256), _random.Next(256),_random.Next(256));

            while (_color == DefaultBackColor)
            {
                _color = Color.FromArgb(_random.Next(256), _random.Next(256), _random.Next(256));
            }

            return _color;

        }   // GetRandomColor()


        // Use Euclid's algorithm to calculate the greatest common divisor (GCD) of two numbers.
        private long GCD(long a, long b)
        {
            // Make a >= b.
            a = Math.Abs(a);
            b = Math.Abs(b);

            if (a < b)
            {
                (b, a) = (a, b);
            }

            // Pull out remainders.
            for (; ; )
            {
                long remainder = a % b;

                if (remainder == 0)
                    return b;

                a = b;
                b = remainder;
            }

        }   // GCD()


        // Return the least common multiple (LCM) of two numbers.
        private long LCM(long a, long b)
        {
            return a * b / GCD(a, b);

        }   // LCM()


        private void SaveSpirographImage()
        {
            int width  = pictureBox.Size.Width;
            int height = pictureBox.Size.Height;

            using (Bitmap bitmap = new Bitmap(width, height))
            {
                // Read the control into the bitmap container.
                pictureBox.DrawToBitmap(bitmap, new Rectangle(0, 0, width, height));

                // Displays a SaveFileDialog so the user can save the Image.
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Title            = "Save Spirograph Image File",
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Favorites),
                    FileName         = "SpirographImage",
                    AddExtension     = true,
                    Filter           = "Bitmap Image|*.bmp|"
                                     + "Exchangeable Image Format|*.exif|"
                                     + "Graphics Interchange Format Image|*.gif|"
                                     + "Joint Photographics Experts Group Image|*.jpg;*.jpeg|"
                                     + "Portable Network Graphics Image|*.png|"
                                     + "Tagged Image File Format|*.tiff"
                };

                try
                {
                    if ((saveFileDialog.ShowDialog() == DialogResult.OK) && (saveFileDialog.FileName != ""))
                    {
                        // Selected file path.
                        string filePath = saveFileDialog.FileName;

                        // Saves the Image via a FileStream created by the OpenFile method.
                        FileStream fs = (FileStream)saveFileDialog.OpenFile();

                        // Saves the Image in the appropriate ImageFormat selected in the dialog box.
                        // NOTE:  The FilterIndex property is one-based.
                        switch (saveFileDialog.FilterIndex)
                        {
                            case 1: bitmap.Save(fs, System.Drawing.Imaging.ImageFormat.Bmp ); break;
                            case 2: bitmap.Save(fs, System.Drawing.Imaging.ImageFormat.Exif); break;
                            case 3: bitmap.Save(fs, System.Drawing.Imaging.ImageFormat.Gif ); break;
                            case 4: bitmap.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg); break;
                            case 5: bitmap.Save(fs, System.Drawing.Imaging.ImageFormat.Png ); break;
                            case 6: bitmap.Save(fs, System.Drawing.Imaging.ImageFormat.Tiff); break;
                        }

                        fs.Close();

                        MessageBox.Show($"Image saved successfully as:\n\r\n\r{saveFileDialog.FileName}", "Success",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving Image:\n\r{saveFileDialog.FileName}\n\r\n\r{ex.Message}", "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }

        }   // SaveControlPicture()


        private void Setup()
        {
            _isSetup = true;

            // Initialize Random instance once.
            _random   = new Random();
            _penColor = GetRandomColor();

            _color_Pereset_1 = new Color[] { Color.Red, Color.White, Color.Blue };
            _color_Pereset_2 = new Color[] { Color.Maroon, Color.Orange };
            
            NumericUpDown_Radius     .Value = _default_A;
            NumericUpDown_InnerCircle.Value = _default_B;
            NumericUpDown_OuterCircle.Value = _default_C;

            TrackBar_Radius = new FloatTrackBar
            {
                Location    = new Point(6, 19),
                Maximum     = 100,
                MaximumSize = new Size(300, 30),
                Minimum     = 1,
                MinimumSize = new Size(300, 30),
                Name        = "TrackBar_Radius",
                Size        = new Size(300, 45),
                TickStyle   = TickStyle.None,
                Value       = _default_A
            };
            TrackBar_Radius.ValueChanged += TrackBar_Radius_ValueChanged;

            TrackBar_InnerCircle = new FloatTrackBar
            {
                Location    = new Point(6, 19),
                Maximum     = 200,
                MaximumSize = new Size(300, 30),
                Minimum     = 1,
                MinimumSize = new Size(300, 30),
                Name        = "TrackBar_InnerCircle",
                Size        = new Size(300, 45),
                TickStyle   = TickStyle.None,
                Value       = _default_B
            };
            TrackBar_InnerCircle.ValueChanged += TrackBar_InnerCircle_ValueChanged;

            TrackBar_OuterCircle = new FloatTrackBar
            {
                Location    = new Point(6, 19),
                Maximum     = 200,
                MaximumSize = new Size(300, 30),
                Minimum     = 1,
                MinimumSize = new Size(300, 30),
                Name        = "TrackBar_OuterCircle",
                Size        = new Size(300, 45),
                TickStyle   = TickStyle.None,
                Value       = _default_C
            };
            TrackBar_OuterCircle.ValueChanged += TrackBar_OuterCircle_ValueChanged;

            groupBox_Radius     .Controls.Add(TrackBar_Radius);
            groupBox_InnerCircle.Controls.Add(TrackBar_InnerCircle);
            groupBox_OuterCircle.Controls.Add(TrackBar_OuterCircle);

            _isSetup = false;

        }   // Setup()


        // The parametric function X(t).
        private double X(double t, double A, double B, double C)
        {
            return (A - B) * Math.Cos(t) + C * Math.Cos((A - B) / B * t);

        }   // X()


        // The parametric function Y(t).
        private double Y(double t, double A, double B, double C)
        {
            return (A - B) * Math.Sin(t) - C * Math.Sin((A - B) / B * t);

        }   // Y()


        private void NumericUpDown_InnerCircle_ValueChanged(object sender, EventArgs e)
        {
            if (_isSetup)
                return;

            TrackBar_InnerCircle.Value = (int)NumericUpDown_InnerCircle.Value;

            //DrawSpirograph();
            Invalidator();

        }   // NumericUpDown_InnerCircle_ValueChanged()


        private void NumericUpDown_OuterCircle_ValueChanged(object sender, EventArgs e)
        {
            if (_isSetup)
                return;

            TrackBar_OuterCircle.Value = (int)NumericUpDown_OuterCircle.Value;

            //DrawSpirograph();
            Invalidator();

        }   // NumericUpDown_OuterCircle_ValueChanged()


        private void NumericUpDown_Radius_ValueChanged(object sender, EventArgs e)
        {
            if (_isSetup)
                return;

            TrackBar_Radius.Value = (int)NumericUpDown_Radius.Value;

            //DrawSpirograph();
            Invalidator();

        }   // NumericUpDown_Radius_ValueChanged()


        private void TrackBar_InnerCircle_ValueChanged(object sender, EventArgs e)
        {
            if (_isSetup)
                return;

            NumericUpDown_InnerCircle.Value = TrackBar_InnerCircle.Value;

            //DrawSpirograph();
            Invalidator();

        }   // TrackBar_InnerCircle_ValueChanged()


        private void TrackBar_OuterCircle_ValueChanged(object sender, EventArgs e)
        {
            if (_isSetup)
                return;

            NumericUpDown_OuterCircle.Value = TrackBar_OuterCircle.Value;

            //DrawSpirograph();
            Invalidator();

        }   // TrackBar_OuterCircle_ValueChanged()


        private void TrackBar_Radius_ValueChanged(object sender, EventArgs e)
        {
            if (_isSetup)
                return;

            NumericUpDown_Radius.Value = TrackBar_Radius.Value;

            //DrawSpirograph();
            Invalidator();

        }   // TrackBar_Radius_ValueChanged()


        private void Form_Main_ClientSizeChanged(object sender, EventArgs e)
        {
            Debug.Print("Form_Main_ClientSizeChanged");
        }


        private void Form_Main_Resize(object sender, EventArgs e)
        {
            Debug.Print("Form_Main_Resize");
        }


        private void Form_Main_ResizeBegin(object sender, EventArgs e)
        {
            Debug.Print("Form_Main_ResizeBegin");
        }

        private void Form_Main_ResizeEnd(object sender, EventArgs e)
        {
            Debug.Print("Form_Main_ResizeEnd");

            Invalidator();
        }


    }   // class Form_Main

}   // namespace Draw_Hypotrochoid

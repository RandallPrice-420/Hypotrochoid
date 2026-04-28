
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Spirograph_v1
{
    public partial class Form_Main : Form
    {
        /// --------------------------------------------------------------------
        /// /// Private Properties:
        /// -------------------
        ///   _default_A
        ///   _default_B
        ///   _default_C
        ///   _trackBar_Radius 
        ///   _trackBar_InnerCircle
        ///   _trackBar_OuterCircle
        ///   _color_Pereset_1
        ///   _color_Pereset_2
        ///   _iterations
        ///   _isSetup
        ///   _isComplete
        ///   _penColor
        ///   _random
        /// --------------------------------------------------------------------

        #region .  Private Properties  .

        private readonly int _default_A  =  80;
        private readonly int _default_B  =  14;
        private readonly int _default_C  = 100;
        private readonly int _iterations =   0;

        private FloatTrackBar _trackBar_Radius      = new FloatTrackBar();
        private FloatTrackBar _trackBar_InnerCircle = new FloatTrackBar();
        private FloatTrackBar _trackBar_OuterCircle = new FloatTrackBar();

        private Color[] _color_Pereset_1;
        private Color[] _color_Pereset_2;

        private bool   _isSetup;
        private bool   _isComplete;
        private Color  _penColor;
        private Random _random;

        // Rendering control
        private CancellationTokenSource _renderCts;
        private readonly object         _renderLock = new object();

        // Debounce timer for coalescing rapid Invalidator() calls (UI thread)
        private System.Windows.Forms.Timer _debounceTimer;

        // Regex pattern for a valid Windows filename (no path, just the name)
        // - Disallows \ / : * ? " < > | 
        // - No leading/trailing spaces
        // - At least one character
        private static readonly Regex _fileNameRegex = new Regex(@"^(?!\s)(?!.*\s$)(?!.*[\\/:*?""<>|]).+$",
                                                                 RegexOptions.Compiled);

        #endregion



        /// ------------------------------------------------------------------------
        /// <remarks>
        /// MethodName()
        /// </remarks>
        /// <summary>
        /// This method...
        /// </summary>
        /// <param name="None"></param>
        /// <remarks>
        /// This method ...
        /// </remarks>
        /// <returns>Nothing</returns>
        /// ------------------------------------------------------------------------
        public Form_Main()
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            Setup();

            Invalidator();

        }   // Form_Main()


        private void Form_Main_ResizeEnd(object sender, EventArgs e)
        {
            Debug.Print("Form_Main_ResizeEnd");

            Invalidator();

        }   // Form_Main_ResizeEnd()


        #region .  Button Events  .

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

                Invalidator();
            }

        }   // BtnColor_Click()


        private void BtnQuit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to quit?", "Confirm Quit",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }

        }   // BtnQuit_Click()


        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveSpirographImage();

        }   // BtnSave_Click()

        #endregion


        private string CreateFilename()
        {
            string a = _trackBar_Radius     .Value.ToString();
            string b = _trackBar_InnerCircle.Value.ToString();
            string c = _trackBar_OuterCircle.Value.ToString();

            string fileName = $"Spirograph_{a}-{b}-{c}_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";

            return fileName;

        }   // CreateFilename()


        private Color GetRandomColor()
        {
            Color _color = Color.FromArgb(_random.Next(256), _random.Next(256),_random.Next(256));

            while (_color == DefaultBackColor)
            {
                _color = Color.FromArgb(_random.Next(256), _random.Next(256), _random.Next(256));
            }

            return _color;

        }   // GetRandomColor()


        #region .  GCD()  .
        /// ------------------------------------------------------------------------
        /// <remarks>
        /// CGD()
        /// </remarks>
        /// <summary>
        /// Get the greatest common divisor (GCD) of two numbers.
        /// </summary>
        /// <param name="a">
        /// The first number.
        /// </param>
        /// <param name="b">
        /// The second number.
        /// </param>
        /// <remarks>
        /// Use Euclid's algorithm to calculate the greatest common divisor (GCD) of two numbers.
        /// </remarks>
        /// <returns>long: The greatest common divisor (GCD) of the two numbers.</returns>
        /// ------------------------------------------------------------------------
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
        #endregion


        private void Invalidator()
        {
            _isComplete = false;

            // Debounce rapid calls to avoid cancelling/starting many renders.
            if (_debounceTimer != null)
            {
                _debounceTimer.Stop();
                _debounceTimer.Start();
            }
            else
            {
                // Fallback: immediate render if timer not initialized for some reason
                StartRenderAsync();
            }

            Invalidate();

        }   // Invalidator()


        private bool IsValidFileName(string fileName)
        {
            return fileName.IndexOfAny(Path.GetInvalidFileNameChars()) == -1;

        }   // IsValidFileName()


        // Return the least common multiple (LCM) of two numbers.
        private long LCM(long a, long b)
        {
            return a * b / GCD(a, b);

        }   // LCM()


        protected override void OnPaint(PaintEventArgs e)
        {
            // Keep form paint light — rendering runs in background and updates pictureBox.Image.
            base.OnPaint(e);

        }   // OnPaint()


        /// <summary>
        /// Renders a spirograph to a new Bitmap. Runs on a background thread.
        /// Reports iteration counts via progress and observes cancellation.
        /// </summary>
        private Bitmap RenderSpirographToBitmap(int A, int B, int C, int iterations, int width, int height, IProgress<int> progress, CancellationToken token)
        {
            Bitmap bitmap = new Bitmap(width, height);

            try
            {
                using (Graphics gr = Graphics.FromImage(bitmap))
                {
                    gr.SmoothingMode = SmoothingMode.AntiAlias;
                    gr.Clear(pictureBox.BackColor);

                    int    cx    = width / 2;
                    int    cy    = height / 2;
                    double t     = 0;
                    double dt    = Math.PI / iterations;
                    double max_t = 2 * Math.PI * B / GCD(A, B);
                    double xPrev = cx + X(t, A, B, C);
                    double yPrev = cy + Y(t, A, B, C);

                    // Build points while optionally reporting progress.
                    List<PointF> pointsList = new List<PointF> { new PointF((float)xPrev, (float)yPrev) };

                    int localIter = 0;

                    while (t <= max_t)
                    {
                        token.ThrowIfCancellationRequested();

                        localIter++;
                        progress?.Report(localIter);

                        t += dt;

                        double x = cx + X(t, A, B, C);
                        double y = cy + Y(t, A, B, C);
                        pointsList.Add(new PointF((float)x, (float)y));
                    }

                    // Prepare pens once and reuse
                    Color[]   penColors = { Color.Red, Color.Green, Color.Blue };
                    List<Pen> pens      = new List<Pen>(penColors.Length);

                    try
                    {
                        foreach (var c in penColors)
                        {
                            pens.Add(new Pen(c, 2));
                        }

                        for (int i = 0; i < pointsList.Count - 1; i++)
                        {
                            token.ThrowIfCancellationRequested();

                            var pen = pens[i % pens.Count];
                            gr.DrawLine(pen, pointsList[i], pointsList[i + 1]);
                        }
                    }
                    finally
                    {
                        foreach (var p in pens)
                        {
                            p.Dispose();
                        }
                    }

                }   // using (Graphics...)

                return bitmap;
            }
            catch (OperationCanceledException)
            {
                // Dispose partial bitmap and propagate cancellation
                bitmap.Dispose();
                throw;
            }
            catch (Exception)
            {
                // Dispose partial bitmap and propagate other exceptions
                bitmap.Dispose();
                throw;
            }

        }   // RenderSpirographToBitmap()


        private void SaveSpirographImage()
        {
            int width  = pictureBox.Size.Width;
            int height = pictureBox.Size.Height;

            using (Bitmap bitmap = new Bitmap(width, height))
            {
                // Read the control into the bitmap container.
                pictureBox.DrawToBitmap(bitmap, new Rectangle(0, 0, width, height));

                string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "Spirograph_Images") + "\\";
                string fileName      = CreateFilename();

                // Displays a SaveFileDialog so the user can save the Image.
                using (SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Title              = "Save Spirograph Image File",
                    InitialDirectory   = directoryPath,
                    FileName           = fileName,
                    AddExtension       = true,
                    AutoUpgradeEnabled = true,
                    CheckFileExists    = false,
                    CheckPathExists    = false,
                    OverwritePrompt    = true,
                    RestoreDirectory   = true,
                    FilterIndex        = 5,
                    Filter             = "Bitmap Image|*.bmp|"
                                       + "Exchangeable Image Format|*.exif|"
                                       + "Graphics Interchange Format Image|*.gif|"
                                       + "Joint Photographics Experts Group Image|*.jpg;*.jpeg|"
                                       + "Portable Network Graphics Image|*.png|"
                                       + "Tagged Image File Format|*.tiff"
                })
                {
                    try
                    {
                        DialogResult dialogResult = saveFileDialog.ShowDialog();

                        switch (dialogResult)
                        {
                            case DialogResult.Cancel:
                            case DialogResult.No:
                                MessageBox.Show("The Image was not saved.", "Save Image Cancelled",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Information);
                                return;

                            case DialogResult.OK:
                                //if (ValidateFileName(saveFileDialog.FileName))
                                if (IsValidFileName(Path.GetFileName(saveFileDialog.FileName)))
                                {
                                    // Saves the Image via a FileStream created by the OpenFile method.
                                    using (FileStream fs = (FileStream)saveFileDialog.OpenFile())
                                    {
                                        // Saves the Image in the ImageFormat selected in the dialog box.
                                        // NOTE:  The FilterIndex property is one-based.
                                        switch (saveFileDialog.FilterIndex)
                                        {
                                            case 1 : bitmap.Save(fs, ImageFormat.Bmp ); break;
                                            case 2 : bitmap.Save(fs, ImageFormat.Exif); break;
                                            case 3 : bitmap.Save(fs, ImageFormat.Gif ); break;
                                            case 4 : bitmap.Save(fs, ImageFormat.Jpeg); break;
                                            case 5 : bitmap.Save(fs, ImageFormat.Png ); break;
                                            case 6 : bitmap.Save(fs, ImageFormat.Tiff); break;
                                            default: bitmap.Save(fs, ImageFormat.Png );
                                                break;
                                        }

                                    }   // using (FileStream...)

                                    MessageBox.Show($"The Image was saved successfully as:\n\r\n\r{saveFileDialog.FileName}",
                                                     "Image Saved Successfully",
                                                     MessageBoxButtons.OK,
                                                     MessageBoxIcon.Information);
                                }
                                else
                                {
                                    char[] invalidChars = Path.GetInvalidFileNameChars();
                                    MessageBox.Show($"The file name is invalid:\n\r\n\r{Path.GetFileName(saveFileDialog.FileName)}\n\r\n\rIt contains these invalid characters: {string.Join(" ", invalidChars)}\n\r\n\rPlease enter a valid file name without any of the following characters: \\ / : * ? \" < > |",
                                                     "Invalid File Name",
                                                     MessageBoxButtons.OK,
                                                     MessageBoxIcon.Error);
                                    return;
                                }
                                break;

                        }   // switch(dialogResult)
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred while trying to save the Image:\n\r{saveFileDialog.FileName}\n\r\n\r{ex.Message}",
                                         "Error Saving Image",
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Error);
                    }

                }   // using (SaveFileDialog...)

            }   // using (Bitmap...)

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

            _trackBar_Radius = new FloatTrackBar
            {
                Location    = new Point(6, 19),
                Maximum     = 100,
                MaximumSize = new Size(300, 30),
                Minimum     = 1,
                MinimumSize = new Size(300, 30),
                Name        = "_trackBar_Radius",
                Size        = new Size(300, 45),
                TickStyle   = TickStyle.None,
                Value       = _default_A
            };
            _trackBar_Radius.ValueChanged += TrackBar_Radius_ValueChanged;

            _trackBar_InnerCircle = new FloatTrackBar
            {
                Location    = new Point(6, 19),
                Maximum     = 200,
                MaximumSize = new Size(300, 30),
                Minimum     = 1,
                MinimumSize = new Size(300, 30),
                Name        = "_trackBar_InnerCircle",
                Size        = new Size(300, 45),
                TickStyle   = TickStyle.None,
                Value       = _default_B
            };
            _trackBar_InnerCircle.ValueChanged += TrackBar_InnerCircle_ValueChanged;

            _trackBar_OuterCircle = new FloatTrackBar
            {
                Location    = new Point(6, 19),
                Maximum     = 200,
                MaximumSize = new Size(300, 30),
                Minimum     = 1,
                MinimumSize = new Size(300, 30),
                Name        = "_trackBar_OuterCircle",
                Size        = new Size(300, 45),
                TickStyle   = TickStyle.None,
                Value       = _default_C
            };
            _trackBar_OuterCircle.ValueChanged += TrackBar_OuterCircle_ValueChanged;

            groupBox_Radius     .Controls.Add(_trackBar_Radius);
            groupBox_InnerCircle.Controls.Add(_trackBar_InnerCircle);
            groupBox_OuterCircle.Controls.Add(_trackBar_OuterCircle);

            // Initialize debounce timer for Invalidator()
            _debounceTimer = new System.Windows.Forms.Timer
            {
                Interval = 200 // ms - adjust to taste
            };
            _debounceTimer.Tick += DebounceTimer_Tick;

            _isSetup = false;

        }   // Setup()


        /// <summary>
        /// Start a background render of the spirograph. Cancels any prior render.
        /// Reports iteration progress to update the UI label.
        /// </summary>
        private void StartRenderAsync()
        {
            // Capture current parameters
            int A = _trackBar_Radius     .Value;
            int B = _trackBar_InnerCircle.Value;
            int C = _trackBar_OuterCircle.Value;

            if (!int.TryParse(txtIter.Text, out int iterations) || iterations <= 0)
            {
                iterations = 100;
                txtIter.Text = iterations.ToString();
            }

            int width  = Math.Max(1, pictureBox.ClientSize.Width);
            int height = Math.Max(1, pictureBox.ClientSize.Height);

            lock (_renderLock)
            {
                // Cancel previous render
                _renderCts?.Cancel();
                _renderCts?.Dispose();
                _renderCts = new CancellationTokenSource();

                _isComplete = false;

                var cts      = _renderCts;
                var        progress = new Progress<int>(i =>
                {
                    // Update UI iteration label (Progress posts to UI thread)
                    try
                    {
                        lblIterationCount.Text = i.ToString();
                    }
                    catch
                    { /* swallow in case control disposed */ }
                });

                // Start background render
                Task.Run(() => RenderSpirographToBitmap(A, B, C, iterations, width, height, progress, cts.Token), cts.Token)
                    .ContinueWith(t =>
                    {
                        // Continuation runs on UI thread
                        if (t.IsCanceled)
                        {
                            // no-op; canceled by a new render request
                            return;
                        }
                        if (t.IsFaulted)
                        {
                            var ex = t.Exception?.GetBaseException();
                            MessageBox.Show($"An error occurred while rendering:\n\n{ex?.Message}",
                                            "Render Error",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error);
                            return;
                        }

                        // Successful render; assign bitmap to pictureBox.Image, disposing previous image.
                        try
                        {
                            var bitmap = t.Result;
                            var oldImg  = pictureBox.Image;
                            pictureBox.Image = bitmap;

                            oldImg?.Dispose();

                            _isComplete = true;
                            Invalidate();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"An error occurred while updating the image:\n\n{ex.Message}",
                                             "Render Update Error",
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Error);
                        }

                    },
                    TaskScheduler.FromCurrentSynchronizationContext());

            }   // lock (...)

        }   // StartRenderAsync()


        private bool ValidateFileName(string fileName)
        {
            // Regex pattern to allow alphanumeric characters, spaces, dots, hyphens, and underscores
            string pattern = @"^[a-zA-Z0-9 _.-]+$";
            Regex regex = new Regex(pattern);

            return regex.IsMatch(fileName);

        }   // ValidateFileName()


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
            if (_isSetup) return;

            _trackBar_InnerCircle.Value = (int)NumericUpDown_InnerCircle.Value;

            Invalidator();

        }   // NumericUpDown_InnerCircle_ValueChanged()


        private void NumericUpDown_OuterCircle_ValueChanged(object sender, EventArgs e)
        {
            if (_isSetup) return;

            _trackBar_OuterCircle.Value = (int)NumericUpDown_OuterCircle.Value;

            Invalidator();

        }   // NumericUpDown_OuterCircle_ValueChanged()


        private void NumericUpDown_Radius_ValueChanged(object sender, EventArgs e)
        {
            if (_isSetup) return;

            _trackBar_Radius.Value = (int)NumericUpDown_Radius.Value;

            Invalidator();

        }   // NumericUpDown_Radius_ValueChanged()


        private void TrackBar_InnerCircle_ValueChanged(object sender, EventArgs e)
        {
            if (_isSetup) return;

            NumericUpDown_InnerCircle.Value = _trackBar_InnerCircle.Value;

            Invalidator();

        }   // TrackBar_InnerCircle_ValueChanged()


        private void TrackBar_OuterCircle_ValueChanged(object sender, EventArgs e)
        {
            if (_isSetup) return;

            NumericUpDown_OuterCircle.Value = _trackBar_OuterCircle.Value;

            Invalidator();

        }   // TrackBar_OuterCircle_ValueChanged()


        private void TrackBar_Radius_ValueChanged(object sender, EventArgs e)
        {
            if (_isSetup) return;

            NumericUpDown_Radius.Value = _trackBar_Radius.Value;

            Invalidator();

        }   // TrackBar_Radius_ValueChanged()


        #region .  Form Resize Events  --  NOT USED  .

        //private void Form_Main_ClientSizeChanged(object sender, EventArgs e)
        //{
        //    Debug.Print("Form_Main_ClientSizeChanged");
        //}


        //private void Form_Main_Resize(object sender, EventArgs e)
        //{
        //    Debug.Print("Form_Main_Resize");
        //}


        //private void Form_Main_ResizeBegin(object sender, EventArgs e)
        //{
        //    Debug.Print("Form_Main_ResizeBegin");
        //}

        #endregion

        // Debounce timer tick handler - runs on UI thread
        private void DebounceTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                _debounceTimer.Stop();

                if (IsDisposed || Disposing)
                    return;

                StartRenderAsync();
            }
            catch
            {
                // Swallow exceptions from timer handler to avoid crashing message loop.
            }
        }

        private void Form_Main_Load(object sender, EventArgs e)
        {

        }
    }   // class Form_Main

}   // Spirograph_v1
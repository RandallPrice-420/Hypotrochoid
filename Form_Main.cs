using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Spirograph_v1
{
    public partial class FormMain : Form
    {
        // ---------------------------------------------------------------------
        // Private Properties:
        // -------------------
        //   _default_A
        //   _pens
        //   _isSetup
        //   _penColors
        //   _debounceTimer
        //   _isClosing
        //   _latestIteration
        //   _renderCts
        //   _renderLock
        //   _renderTask
        //   _uiProgressTimer
        // ---------------------------------------------------------------------

        #region .  Private Properties  .

        private readonly int               _default_A  =  80;
        private readonly List<Pen>         _pens = [];

        private bool                       _isSetup;
        private Color[]                    _penColors;

        // Debounce timer for coalescing rapid Invalidator() calls (UI thread).
        private System.Windows.Forms.Timer _debounceTimer;

        // Flag to mark we are already performing the closing handshake.
        private bool                       _isClosing;

        // Latest iteration value supplied by background render (atomic).
        private volatile int               _latestIteration;

        // Rendering controls.
        // Track currently running render Task so shutdown can wait/cancel deterministically.
        private CancellationTokenSource    _renderCts;
        private readonly object            _renderLock = new();
        private Task                       _renderTask;

        // UI timer to poll latest iteration reported by background render.
        private System.Windows.Forms.Timer _uiProgressTimer;

        #endregion



        // ---------------------------------------------------------------------
        // Constructor:
        // ------------
        //   Form_Main()
        // ---------------------------------------------------------------------

        #region .  Form_Main()  .
        // ---------------------------------------------------------------------
        //   Method.......:  Form_Main()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        public FormMain()
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            Setup();
            SetupPen();
            SetupPenColors();

            Invalidator();

        }   // Form_Main()
        #endregion



        // ---------------------------------------------------------------------
        // Private Methods:
        // ----------------
        //   CreateCarbonFiberTexture()
        //   CreateFilename()
        //   DebounceTimer_Tick()
        //   GetRandomColor()
        //   GCD()
        //   Invalidator()
        //   IsValidFileName()
        //   LCM()
        //   RenderSpirographToBitmap()
        //   SaveSpirographImage()
        //   Setup()
        //   SetupPen()
        //   SetupPenColors()
        //   StartRenderAsync()
        //   UiProgressTimer_Tick()
        //   X()
        //   Y()
        // ---------------------------------------------------------------------

        #region .  CreateCarbonFiberTexture()  .
        // ---------------------------------------------------------------------
        //   Method.......:  CreateCarbonFiberTexture()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  
        // ---------------------------------------------------------------------
        private static Bitmap CreateCarbonFiberTexture()
        {
            Bitmap bmp = new(8, 8);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.FromArgb(25, 25, 30));

                using Pen p = new(Color.FromArgb(40, 40, 50), 2);
                g.DrawLine(p, 0, 0, 8, 8);
                g.DrawLine(p, 0, 8, 8, 0);
            }

            return bmp;

        }   // CreateCarbonFiberTexture()
        #endregion


        #region .  CreateFilename()  .
        // ---------------------------------------------------------------------
        //   Method.......:  CreateFilename()
        //   Description..:  Creates a filename for the spirograph image.
        //   Parameters...:  None
        //   Returns......:  A string representing the filename.
        // ---------------------------------------------------------------------
        private string CreateFilename()
        {
            string a = _default_A.ToString();
            string b = RPSlider_InnerCircle.SliderValue.ToString();
            string c = RPSlider_OuterCircle.SliderValue.ToString();

            string fileName = $"Spirograph_{a}-{b}-{c}_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";

            return fileName;

        }   // CreateFilename()
        #endregion


        #region .  DebounceTimer_Tick()  .
        // ---------------------------------------------------------------------
        //   Method.......:  DebounceTimer_Tick()
        //   Description..:  Handles the Tick event of the DebounceTimer control.
        //                   Used to debounce rapid calls to Invalidator() and
        //                   coalesce them into a single render. Runs on UI thread.
        //   Parameters...:  sender - The event source.
        //                   e      - The event data.
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
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

        }   // DebounceTimer_Tick()
        #endregion


        #region .  GetRandomColor()  --  CAN DELETE  .
        //// ---------------------------------------------------------------------
        ////   Method.......:  GetRandomColor()
        ////   Description..:  Generates a random color, but not one that matches the
        ////                   default background color.
        ////   Parameters...:  None
        ////   Returns......:  A Color object representing the random color.
        //// ---------------------------------------------------------------------
        //private Color GetRandomColor()
        //{
        //    Color _color = DefaultBackColor;

        //    while (_color == DefaultBackColor)
        //    {
        //        _color = Color.FromArgb(_random.Next(256), _random.Next(256), _random.Next(256));
        //    }

        //    return _color;

        //}   // GetRandomColor()
        #endregion


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
        private static long GCD(long a, long b)
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


        #region .  Invalidator()  .
        // ---------------------------------------------------------------------
        //   Method.......:  Invalidator()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  
        // ---------------------------------------------------------------------
        private void Invalidator()
        {
            // Debounce rapid calls to avoid cancelling/starting many renders.
            if (_debounceTimer != null)
            {
                _debounceTimer.Stop();
                _debounceTimer.Start();
            }
            else
            {
                // Fallback:  immediate render if timer not initialized for some reason.
                StartRenderAsync();
            }

            Invalidate();

        }   // Invalidator()
        #endregion


        #region .  IsValidFileName()  .
        // ---------------------------------------------------------------------
        //   Method.......:  IsValidFileName()
        //   Description..:  Checks if a given string is a valid file name.
        //   Parameters...:  string fileName - The file name to validate.
        //   Returns......:  bool - True if the file name is valid, false otherwise.
        // ---------------------------------------------------------------------
        private static bool IsValidFileName(string fileName)
        {
            return fileName.IndexOfAny(Path.GetInvalidFileNameChars()) == -1;

        }   // IsValidFileName()
        #endregion


        #region .  LCM()  .
        // ---------------------------------------------------------------------
        //   Method.......:  LCM()
        //   Description..:  Returns the least common multiple (LCM) of 2 numbers.
        //   Parameters...:  long a - The first number.
        //                   long b - The second number.
        //   Returns......:  long   - The least common multiple (LCM) of the 2 numbers.
        // ---------------------------------------------------------------------
        private static long LCM(long a, long b)
        {
            return a * b / GCD(a, b);

        }   // LCM()
        #endregion


        #region .  RenderSpirographToBitmap()  .
        // ---------------------------------------------------------------------
        //   Method.......:  RenderSpirographToBitmap()
        //   Description..:  Renders a spirograph to a new Bitmap. This runs on a
        //                   background thread, and it reports iteration counts via
        //                   progress and observes cancellation.
        //   Parameters...:  A          - Inner circle radius
        //                   B          - Outer circle radius
        //                   C          - Pen offset
        //                   iterations - Number of iterations
        //                   width      - Bitmap width
        //                   height     - Bitmap height
        //                   progress   - Progress reporter
        //                   token      - Cancellation token
        //   Returns......:  Bitmap containing the rendered spirograph
        // ---------------------------------------------------------------------
        private Bitmap RenderSpirographToBitmap(int A, int B, int C, int iterations, int width, int height, IProgress<int> progress, CancellationToken token)
        {
            Bitmap bitmap = new(width, height);

            // Clone so we own the disposable handle for this render.
            List<Pen> localPens = new(_pens.Count);
            foreach (var p in _pens)
            {
                localPens.Add((Pen)p.Clone());
            }

            try
            {
                using (Graphics gr = Graphics.FromImage(bitmap))
                {
                    gr.SmoothingMode = SmoothingMode.AntiAlias;
                    gr.Clear(PicSpirograph.BackColor);

                    int    cx    = width  / 2;
                    int    cy    = height / 2;
                    double t     = 0;
                    double dt    = Math.PI / iterations;
                    double max_t = 2 * Math.PI * B / GCD(A, B);
                    double xPrev = cx + X(t, A, B, C);
                    double yPrev = cy + Y(t, A, B, C);

                    // Build points while optionally reporting progress.
                    List<PointF> pointsList = [new PointF((float)xPrev, (float)yPrev)];

                    int localIter = 0;

                    // Report progress less frequently to reduce cross-thread marshals.
                    int reportEvery = Math.Max(1, iterations / 200);    // ~200 updates max

                    while (t <= max_t)
                    {
                        token.ThrowIfCancellationRequested();

                        localIter++;

                        if ((localIter % reportEvery) == 0)
                        {
                            try
                            {
                                progress?.Report(localIter);
                            }
                            catch
                            {
                                // Swallow: progress targets may be tearing down.
                            }
                        }

                        t += dt;

                        double x = cx + X(t, A, B, C);
                        double y = cy + Y(t, A, B, C);

                        pointsList.Add(new PointF((float)x, (float)y));
                    }

                    try
                    {
                        for (int i = 0; i < pointsList.Count - 1; i++)
                        {
                            token.ThrowIfCancellationRequested();

                            var pen = localPens[i % _pens.Count];
                            gr.DrawLine(pen, pointsList[i], pointsList[i + 1]);
                        }
                    }
                    finally
                    {
                        foreach (var p in localPens)
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
        #endregion


        #region .  SaveSpirographImage()  .
        // ---------------------------------------------------------------------
        //   Method.......:  SaveSpirographImage()
        //   Description..:  Saves the current spirograph image to a file.
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void SaveSpirographImage()
        {
            int width  = PicSpirograph.Size.Width;
            int height = PicSpirograph.Size.Height;

            using Bitmap bitmap = new(width, height);

            // Read the control into the bitmap container.
            PicSpirograph.DrawToBitmap(bitmap, new Rectangle(0, 0, width, height));

            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "Spirograph_Images") + "\\";
            string fileName      = CreateFilename();

            // Displays a SaveFileDialog so the user can save the Image.
            using SaveFileDialog saveFileDialog = new()
            {
                Title              = "Save Spirograph Image File",
                InitialDirectory   = directoryPath,
                FileName           = fileName,
                AddExtension       = true,
                AutoUpgradeEnabled = true,
                CheckFileExists    = false,
                CheckPathExists    = false,
                OverwritePrompt    = false,
                RestoreDirectory   = true,
                FilterIndex        = 5,
                Filter             = "Bitmap Image|*.bmp|"
                                   + "Exchangeable Image Format|*.exif|"
                                   + "Graphics Interchange Format Image|*.gif|"
                                   + "Joint Photographics Experts Group Image|*.jpg;*.jpeg|"
                                   + "Portable Network Graphics Image|*.png|"
                                   + "Tagged Image File Format|*.tiff"
            };
            try
            {
                DialogResult dialogResult = saveFileDialog.ShowDialog();

                switch (dialogResult)
                {
                    case DialogResult.No:
                    case DialogResult.Cancel:
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
                                    case 1:
                                        bitmap.Save(fs, ImageFormat.Bmp);
                                        break;
                                    case 2:
                                        bitmap.Save(fs, ImageFormat.Exif);
                                        break;
                                    case 3:
                                        bitmap.Save(fs, ImageFormat.Gif);
                                        break;
                                    case 4:
                                        bitmap.Save(fs, ImageFormat.Jpeg);
                                        break;
                                    case 5:
                                        bitmap.Save(fs, ImageFormat.Png);
                                        break;
                                    case 6:
                                        bitmap.Save(fs, ImageFormat.Tiff);
                                        break;
                                    default:
                                        bitmap.Save(fs, ImageFormat.Png);
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
            // using (SaveFileDialog...)
            // using (Bitmap...)

        }   // SaveControlPicture()
        #endregion


        #region .  Setup()  .
        // ---------------------------------------------------------------------
        //   Method.......:  Setup()
        //   Description..:  Initializes the UI components and settings.
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void Setup()
        {
            // Set the Slider's Title and subscribe to the event.
            RPSlider_InnerCircle.TitleLabel = "Inner Circle Radius";
            RPSlider_InnerCircle.SliderValueChanged += InnerCircleValueChanged;

            RPSlider_OuterCircle.TitleLabel = "Outer Circle Radius";
            RPSlider_OuterCircle.SliderValueChanged += OuterCircleValueChanged;

            RPSlider_Iterations.TitleLabel = "Iterations";
            RPSlider_Iterations.SliderValueChanged += IterationsValueChanged;

            _isSetup = true;

            // Initialize debounce timer for Invalidator()
            _debounceTimer = new System.Windows.Forms.Timer
            {
                Interval = 200 // ms - adjust to taste
            };
            _debounceTimer.Tick += DebounceTimer_Tick;

            // Initialize UI progress poller (reads latest iteration from background without cross-thread marshals)
            _uiProgressTimer = new System.Windows.Forms.Timer
            {
                Interval = 100 // ms
            };
            _uiProgressTimer.Tick += UiProgressTimer_Tick;
            _uiProgressTimer.Start();

            _isSetup = false;

            BtnColor .Paint += Button_Paint;
            BtnQuit  .Paint += Button_Paint;
            BtnRedraw.Paint += Button_Paint;
            BtnSave  .Paint += Button_Paint;

        }   // Setup()
        #endregion


        #region .  SetupPen()  .
        // ---------------------------------------------------------------------
        //   Method.......:  SetupPen()
        //   Description..:  Initializes the UI components and settings.
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void SetupPen()
        {
            string[] enumNames = ColorPresets.GetEnumNames();

            // Load the color preset names into the ComboBox..
            for (int i = 0; i < enumNames.Length; i++)
            {
                CboColorPresets.Items.Add(enumNames[i]);
            }
            CboColorPresets.SelectedIndex = 0;

            _penColors = new ColorPresets().GetColorPresets()[CboColorPresets.SelectedIndex].Colors;

            foreach (var c in _penColors)
            {
                _pens.Add(new Pen(c, 2));
            }

        }   // SetupPen()
        #endregion


        #region .  SetupPenColors()  .
        // ---------------------------------------------------------------------
        //   Method.......:  SetupPenColors()
        //   Description..:  
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void SetupPenColors()
        {
            _penColors = new ColorPresets().GetColorPresets()[CboColorPresets.SelectedIndex].Colors;

            _pens.Clear();
            foreach (var c in _penColors)
            {
                _pens.Add(new Pen(c, 2));
            }

        }   // SetupPenColors()
        #endregion


        #region .  StartRenderAsync()  .
        // ---------------------------------------------------------------------
        //   Method.......:  StartRenderAsync()
        //   Description..:  Starts a background render of the spirograph. Cancels
        //                   any prior render.
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void StartRenderAsync()
        {
            // Capture current parameters.
            int A = _default_A;
            int B = RPSlider_InnerCircle.SliderValue;
            int C = RPSlider_OuterCircle.SliderValue;

            int iterations = RPSlider_Iterations.SliderValue;

            //if (!int.TryParse(TextIterations.Text, out int iterations) || iterations <= 0)
            //{
            //    iterations = 100;
            //    TextIterations.Text = iterations.ToString();
            //}

            int width  = Math.Max(1, PicSpirograph.ClientSize.Width);
            int height = Math.Max(1, PicSpirograph.ClientSize.Height);

            lock (_renderLock)
            {
                // Cancel previous render.
                _renderCts?.Cancel();
                _renderCts?.Dispose();
                _renderCts = new CancellationTokenSource();

                var cts = _renderCts;

                // Create a SafeProgress that simply stores the iteration value atomically.
                // The UI timer will poll _latestIteration and update the label on the UI thread.
                var safeProgress = new SafeProgress(v => Interlocked.Exchange(ref _latestIteration, v));

                // Start background render and keep a reference so shutdown can await it.
                var render = Task.Run(() => RenderSpirographToBitmap(A, B, C, iterations, width, height, safeProgress, cts.Token), cts.Token);
                _renderTask = render;

                render.ContinueWith(t =>
                {
                    // Continuation runs on UI thread.
                    try
                    {
                        if (t.IsCanceled)
                        {
                            // no-op; canceled by a new render request.
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
                            var oldImg  = PicSpirograph.Image;
                            PicSpirograph.Image = bitmap;

                            oldImg?.Dispose();

                            Invalidate();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"An error occurred while updating the image:\n\n{ex.Message}",
                                             "Render Update Error",
                                              MessageBoxButtons.OK,
                                              MessageBoxIcon.Error);
                        }
                    }
                    finally
                    {
                        // Dispose the SafeProgress so further background Report calls are no-ops.
                        safeProgress.Dispose();
                    }

                },
                TaskScheduler.FromCurrentSynchronizationContext());

            }   // lock (...)

        }   // StartRenderAsync()
        #endregion


        #region .  UiProgressTimer_Tick()  .
        // ---------------------------------------------------------------------
        //   Method.......:  UiProgressTimer_Tick()
        //   Description..:  Handles the UI timer tick event to update the
        //                   iteration count label.
        //   Parameters...:  sender - The event source.
        //                   e      - The event data.
        //   Returns......:  None
        // ---------------------------------------------------------------------
        private void UiProgressTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (LblIterationsCount == null)
                    return;
                if (LblIterationsCount.IsDisposed)
                    return;
                if (!LblIterationsCount.IsHandleCreated)
                    return;

                // Atomically read and reset the latest iteration value.
                int value = Interlocked.Exchange(ref _latestIteration, 0);

                if (value > 0)
                {
                    LblIterationsCount.Text = value.ToString();
                }
            }
            catch
            {
                // Swallow – UI may be tearing down.
            }

        }   // UiProgressTimer_Tick()
        #endregion


        #region .  X()  .
        // ---------------------------------------------------------------------
        //   Method.......:  X()
        //   Description..:  The parametric function X(t).
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private static double X(double t, double A, double B, double C)
        {
            return (A - B) * Math.Cos(t) + C * Math.Cos((A - B) / B * t);

        }   // X()
        #endregion


        #region .  Y()  .
        // ---------------------------------------------------------------------
        //   Method.......:  Y()
        //   Description..:  The parametric function Y(t).
        //   Parameters...:  None
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private static double Y(double t, double A, double B, double C)
        {
            return (A - B) * Math.Sin(t) - C * Math.Sin((A - B) / B * t);

        }   // Y()
        #endregion



        // ---------------------------------------------------------------------
        // Private Control Events:
        // -----------------------
        //   Form_Main_ResizeEnd()
        //   BtnColor_Click()
        //   BtnControlsForm_Click()
        //   BtnQuit_Click()
        //   BtnRedraw_Click()
        //   BtnSave_Click()
        //   CboColorPresets_SelectedIndexChanged()
        //   ChkMultiColorGradient_CheckedChanged()
        //   InnerCircleValueChanged()
        //   IterationsValueChanged()
        //   OuterCircleValueChanged()
        // ---------------------------------------------------------------------

        #region .  Form_Main_ResizeEnd()  .
        // ---------------------------------------------------------------------
        //   Method.......:  Form_Main_ResizeEnd()
        //   Description..:  Handles the ResizeEnd event of the main form, and
        //                   calls Invalidator() to redraw the Spirograph.
        //   Parameters...:  sender - The event source.
        //                   e      - The event data.
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void Form_Main_ResizeEnd(object sender, EventArgs e)
        {
            Invalidator();

        }   // Form_Main_ResizeEnd()
        #endregion


        #region .  BtnColor_Click()  --  COMMENTED OUT  .
        //// ---------------------------------------------------------------------
        ////   Method.......:  BtnColor_Click()
        ////   Description..:  Handles the Click event of the BtnColor control.
        ////   Parameters...:  sender - The event source.
        ////                   e      - The event data.
        ////   Returns......:  Nothing
        //// ---------------------------------------------------------------------
        //private void BtnColor_Click(object sender, EventArgs e)
        //{
        //    ColorDialog colorDialog = new()
        //    {
        //        AllowFullOpen = true,
        //        AnyColor      = true,
        //        FullOpen      = true,
        //        Color         = GetRandomColor()
        //    };

        //    if (colorDialog.ShowDialog() == DialogResult.OK)
        //    {
        //        // Use the selected color.
        //        _penColor = colorDialog.Color;

        //        Invalidator();
        //    }

        //}   // BtnColor_Click()
        #endregion


        #region .  BtnControlsForm_Click()  .
        // ---------------------------------------------------------------------
        //   Method.......:  BtnControlsForm_Click()
        //   Description..:  Handles the Click event of the BtnControlsForm control.
        //   Parameters...:  sender - The event source.
        //                   e      - The event data.
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void BtnControlsForm_Click(object sender, EventArgs e)
        {
            Form_TestUserControls TestForm = new();
            TestForm.ShowDialog();

        }   // BtnControlsForm_Click()
        #endregion


        #region .  BtnQuit_Click()  .
        // ---------------------------------------------------------------------
        //   Method.......:  BtnQuit_Click()
        //   Description..:  Handles the Click event of the BtnQuit control.
        //   Parameters...:  sender - The event source.
        //                   e      - The event data.
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void BtnQuit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to quit?", "Confirm Quit",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }

        }   // BtnQuit_Click()
        #endregion


        #region .  BtnRedraw_Click()  .
        // ---------------------------------------------------------------------
        //   Method.......:  BtnRedraw_Click()
        //   Description..:  Handles the Click event of the BtnRedraw control.
        //   Parameters...:  sender - The event source.
        //                   e      - The event data.
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void BtnRedraw_Click(object sender, EventArgs e)
        {
            //_penColors = new ColorPresets().GetColorPresets()[CboColorPresets.SelectedIndex].Colors;
            SetupPenColors();
            Invalidator();

        }   // BtnRedraw_Click()
        #endregion


        #region .  BtnSave_Click()  .
        // ---------------------------------------------------------------------
        //   Method.......:  BtnSave_Click()
        //   Description..:  Handles the Click event of the BtnSave control.
        //   Parameters...:  sender - The event source.
        //                   e      - The event data.
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveSpirographImage();

        }   // BtnSave_Click()
        #endregion


        #region .  CboColorPresets_SelectedIndexChanged()  .
        // ---------------------------------------------------------------------
        //   Method.......:  CboColorPresets_SelectedIndexChanged()
        //   Description..:  Handles the Changed event of the CboColorPrsets control.
        //   Parameters...:  sender - The event source.
        //                   e      - The event data.
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void CboColorPresets_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetupPenColors();
            Invalidator();

        }   // CboColorPresets_SelectedIndexChanged()
        #endregion


        #region .  ChkMultiColorGradient_CheckedChanged()  .
        // ---------------------------------------------------------------------
        //   Method.......:  ChkMultiColorGradient_CheckedChanged()
        //   Description..:  
        //   Parameters...:  
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        private void ChkMultiColorGradient_CheckedChanged(object sender, EventArgs e)
        {
            Invalidate();

        }   // ChkMultiColorGradient_CheckedChanged()
        #endregion


        #region .  InnerCircleValueChanged()  .
        // ---------------------------------------------------------------------
        //   Method.......:  InnerCircleValueChanged()
        //   Description..:  Handles the event when the inner circle value changes.
        //   Parameters...:  sender - The event source.
        //                    e      - The event data.
        //   Returns......:  None
        // ---------------------------------------------------------------------
        private void InnerCircleValueChanged(object sender, EventArgs e)
        {
            if (_isSetup)
                return;

            Invalidator();

        }   // InnerCircleValueChanged()
        #endregion


        #region .  IterationsValueChanged()  .
        // ---------------------------------------------------------------------
        //   Method.......:  IterationsValueChanged()
        //   Description..:  Handles the event when the iterations value changes.
        //   Parameters...:  sender - The event source.
        //                    e      - The event data.
        //   Returns......:  None
        // ---------------------------------------------------------------------
        private void IterationsValueChanged(object sender, EventArgs e)
        {
            if (_isSetup)
                return;

            Invalidator();

        }   // IterationsValueChanged()
        #endregion


        // ---------------------------------------------------------------------
        //   Method.......:  OuterCircleValueChanged()
        //   Description..:  Handles the event when the outer circle value changes.
        //   Parameters...:  sender - The event source.
        //                    e      - The event data.
        //   Returns......:  None
        // ---------------------------------------------------------------------
        private void OuterCircleValueChanged(object sender, EventArgs e)
        {
            if (_isSetup)
                return;

            Invalidator();

        }   // OuterCircleValueChanged()


        #region .  Button_Paint()  .
        // ---------------------------------------------------------------------
        //   Method.......:  Button_Paint()
        //   Description..:  Handles the event when the outer circle value changes.
        //   Parameters...:  sender - The event source.
        //                    e      - The event data.
        //   Returns......:  None
        // ---------------------------------------------------------------------
        private void Button_Paint(object sender, PaintEventArgs e)
        {
            Button button = sender as Button;
            
            Graphics g = e.Graphics;
            g.FillRectangle
            (
                new LinearGradientBrush
                (
                    button.ClientRectangle,
                    Color.White,
                    Color.Red,
                    45f,
                    false
                ),
                new RectangleF(PointF.Empty, button.Size)
            );

            // Draw the button text manually
            TextRenderer.DrawText
            (
                e.Graphics,
                button.Text,
                button.Font,
                button.ClientRectangle,
                button.ForeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
            );

        }   // Button_Paint()
        #endregion



        // ---------------------------------------------------------------------
        // Private Override Methods:
        // -------------------------
        //   OnFormClosing()
        //   OnPaint()
        //   OnPaintBackground()
        // ---------------------------------------------------------------------

        #region .  OnFormClosing()  .
        // ---------------------------------------------------------------------
        //   Method.......:  OnFormClosing()
        //   Description..:  Ensure background render is cancelled and given a short
        //                   time to finish when closing.
        //   Parameters...:  
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        protected override async void OnFormClosing(FormClosingEventArgs e)
        {
            // Non-blocking, deterministic shutdown handshake:
            // - On first invocation cancel and wait (bounded) for background render.
            // - Set e.Cancel = true so message loop continues while waiting.
            // - When waiting completes, call Close() again to allow normal teardown.
            if (_isClosing)
            {
                // Second invocation after handshake; allow normal closing.
                base.OnFormClosing(e);
                return;
            }

            _isClosing = true;
            e.Cancel = true;

            // Cancel any running render promptly.
            lock (_renderLock)
            {
                try
                {
                    _renderCts?.Cancel();
                }
                catch
                {
                    // Swallow - shutting down
                }
            }

            // Give the running render a bounded time to complete/observe cancellation.
            Task running = null;

            lock (_renderLock)
            {
                running = _renderTask;
            }

            if (running != null)
            {
                try
                {
                    await Task.WhenAny(running, Task.Delay(2000));
                }
                catch
                {
                    // Swallow - shutting down
                }
            }

            // Stop and dispose UI progress timer so it won't try to touch controls after teardown.
            try
            {
                if (_uiProgressTimer != null)
                {
                    _uiProgressTimer.Stop();
                    _uiProgressTimer.Tick -= UiProgressTimer_Tick;
                    _uiProgressTimer.Dispose();
                    _uiProgressTimer = null;
                }
            }
            catch { }

            // Now re-enter close sequence; this time OnFormClosing sees _isClosing true and will permit closing.
            Close();

        }   // OnFormClosing()
        #endregion


        #region .  OnPaint()  .
        // ---------------------------------------------------------------------
        //   Method.......:  OnPaint()
        //   Description..:  
        //   Parameters...:  
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        protected override void OnPaint(PaintEventArgs e)
        {
            // Keep form paint light — rendering runs in background and updates pictureBox.Image.
            base.OnPaint(e);

        }   // OnPaint()
        #endregion


        #region .  OnPaintBackground()  .
        // ---------------------------------------------------------------------
        //   Method.......:  OnPaintBackground()
        //   Description..:  
        //   Parameters...:  
        //   Returns......:  Nothing
        // ---------------------------------------------------------------------
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            Rectangle rect = this.ClientRectangle;

            if (ChkMultiColorGradient.Checked)
            {
                ColorBlend blend = new()
                {
                    Colors =
                    [
                        Color.Violet,
                        Color.Indigo,
                        Color.Blue,
                        Color.Green,
                        Color.Yellow,
                        Color.Orange,
                        Color.Red,
                        //Color.Blue,
                        //Color.Magenta,
                        //Color.Red,
                        //Color.Yellow,
                        //Color.Green
                    ],
                    Positions =
                    [
                        0f,
                        1/6f,
                        2/6f,
                        3/6f,
                        4/6f,
                        5/6f,
                        1f
                    ]
                };

                LinearGradientBrush brush = new
                (
                    rect,
                    Color.Empty,        // Start color
                    Color.Empty,        // End color
                    45f,                // Angle
                    false               // Is angle scalable?
                )
                {
                    InterpolationColors = blend
                };

                using (brush)
                {
                    e.Graphics.FillRectangle(brush, rect);
                }
            }
            else
            {
                LinearGradientBrush brush = new                (
                    rect,
                    Color.DarkBlue,     // Start color
                    Color.RoyalBlue,    // End color
                    45f,                // Angle
                    false               // Is angle scalable?
                );

                using (brush)
                {
                    e.Graphics.FillRectangle(brush, rect);
                }
            }


        }   //  OnPaintBackground()
        #endregion



        // ---------------------------------------------------------------------
        // Private Sealed Classes:
        // -----------------------
        //   SafeProgress
        // ---------------------------------------------------------------------

        #region .  class SafeProgress Helper  .

        // SafeProgress does not post into SynchronizationContext.
        // It simply calls a provided Action<int> (must be thread-safe)
        // so background threads don't attempt COM context transitions.
        private sealed class SafeProgress(Action<int> action) : IProgress<int>, IDisposable
        {
            private readonly Action<int> _action = action ?? throw new ArgumentNullException(nameof(action));
            private int _disposed; // 0 == false, 1 == true

            public void Report(int value)
            {
                if (Interlocked.CompareExchange(ref _disposed, 0, 0) == 1)
                    return;
                try
                {
                    _action(value);
                }
                catch
                {
                    // Swallow: keep reporting robust during teardown.
                }
            }

            public void Dispose()
            {
                Interlocked.Exchange(ref _disposed, 1);
            }


        }   // class SafeProgress

        #endregion


    }   // class Form_Main

}   // Spirograph_v1
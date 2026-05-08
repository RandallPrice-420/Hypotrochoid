using Spirograph_v1.Controls.RPSciFiButton;
using Spirograph_v1.Controls.RPSciFiCameraFeed;
using Spirograph_v1.Controls.RPSciFiCommsPanel;
using Spirograph_v1.Controls.RPSciFiGroupPanel;
using Spirograph_v1.Controls.RPSciFiHoloTable;
using Spirograph_v1.Controls.RPSciFiInventory;
using Spirograph_v1.Controls.RPSciFiMap;
using Spirograph_v1.Controls.RPSciFiRadar;
using Spirograph_v1.Controls.RPSciFiTerminal;

using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Spirograph_v1
{
    public partial class RPSciFiBridgeForm : Form
    {
        private RPSciFiButton         _btnDashboard;
        private RPSciFiButton         _btnTestControls;

        private RPSciFiDashboardForm  _dashboardForm;
        private Form_TestUserControls _testControlsForm;

        private RPSciFiGroupPanel     _bottomDock;
        private RPSciFiGroupPanel     _centerDock;
        private RPSciFiGroupPanel     _leftDock;
        private RPSciFiGroupPanel     _rightDock;

        private RPSciFiCameraFeed     _camera;
        private RPSciFiCommsPanel     _comms;
        private RPSciFiHoloTable      _holo;
        private RPSciFiInventory      _inventory;
        private RPSciFiMap            _map;
        private RPSciFiRadar          _radar;
        private RPSciFiTerminal       _terminal;

        private readonly RPSciFiControlBus _bus = new();


        public RPSciFiBridgeForm()
        {
            Text           = "RPSciFi Starship Bridge Console";
            BackColor      = Color.FromArgb(5, 5, 12);
            WindowState    = FormWindowState.Maximized;
            DoubleBuffered = true;

            BuildLayout();
            RegisterControls();
            HookEvents();

            // Subscribe to SizeChanged event.
            this.SizeChanged += Form_SizeChanged;

        }   // RPSciFiBridgeForm()()



        private void BuildLayout()
        {
            #region .  Left Dock  .

            // -----------------------------------------------------------------
            // LEFT DOCK — Radar + HoloTable
            // -----------------------------------------------------------------
            _leftDock = new RPSciFiGroupPanel()
            {
                GlowColor = Color.Cyan,
                Dock      = DockStyle.Left,
                Width     = 360,
            };
            Controls.Add(_leftDock);


            // -----------------------------------
            // RADAR
            // -----------------------------------
            _radar = new RPSciFiRadar()
            {
                Location = new Point(40, 40),
                Size     = new Size(260, 260),
            };
            _leftDock.Controls.Add(_radar);


            // -----------------------------------
            // HOLO TABLE
            // -----------------------------------
            _holo = new RPSciFiHoloTable()
            {
                Location = new Point(40, 320),
                Size     = new Size(260, 260),
            };
            _leftDock.Controls.Add(_holo);

            #endregion


            #region .  Right Dock  .

            // -----------------------------------------------------------------
            // RIGHT DOCK — Comms Panel + Camera Feed
            // -----------------------------------------------------------------
            _rightDock = new RPSciFiGroupPanel()
            {
                GlowColor = Color.Magenta,
                Dock      = DockStyle.Right,
                Width     = 380,
            };
            Controls.Add(_rightDock);


            // -----------------------------------
            // COMMS PANEL
            // -----------------------------------
            _comms = new RPSciFiCommsPanel()
            {
                Location = new Point(40, 40),
                Size     = new Size(300, 160),
            };
            _rightDock.Controls.Add(_comms);


            // -----------------------------------
            // CAMERA FEED
            // -----------------------------------
            _camera = new RPSciFiCameraFeed()
            {
                Location = new Point(40, 240),
                Size     = new Size(300, 240),
            };
            _rightDock.Controls.Add(_camera);

            #endregion


            #region .  Bottom Dock  .

            // -----------------------------------------------------------------
            // BOTTOM DOCK — Inventory + Terminal + Dashboard + Test Controls
            // -----------------------------------------------------------------
            _bottomDock = new RPSciFiGroupPanel()
            {
                GlowColor = Color.Cyan,
                Dock      = DockStyle.Bottom,
                Height    = 260,
            };
            Controls.Add(_bottomDock);


            // -----------------------------------
            // INVENTORY
            // -----------------------------------
            _inventory = new RPSciFiInventory()
            {
                Location = new Point(40, 40),
                Size     = new Size(500, 180),
            };
            _bottomDock.Controls.Add(_inventory);


            // -----------------------------------
            // TERMINAL
            // -----------------------------------
            _terminal = new RPSciFiTerminal()
            {
                Location = new Point(580, 40),
                Size     = new Size(500, 180),
            };
            _bottomDock.Controls.Add(_terminal);


            // -----------------------------------
            // DASHBOARD FORM
            // -----------------------------------
             _btnDashboard = new()
            {
                Text     = "DASHBOARD",
                Font     = new Font(Font.FontFamily, 16),
                Location = new Point(1740, 115),
                Size     = new Size(160, 50),
            };
            _bottomDock.Controls.Add(_btnDashboard);

            _btnDashboard.Click += (s, e) =>
            {
                if (!IsFormOpen<RPSciFiDashboardForm>())
                {
                    // Open the form.
                    _dashboardForm = new RPSciFiDashboardForm();
                    _dashboardForm.Show();
                }
                else
                {
                    // Optionally bring the existing form to front
                    RPSciFiDashboardForm dashboardgForm = Application.OpenForms.OfType<RPSciFiDashboardForm>().FirstOrDefault();
                    if (dashboardgForm != null)
                    {
                        dashboardgForm.BringToFront();
                    }
                }
            };


            // -----------------------------------
            // TEST CONTROLS FORM
            // -----------------------------------
            _btnTestControls = new()
            {
                Text     = "Test Controls",
                Font     = new Font(Font.FontFamily, 16),
                Location = new Point(1740, 185),
                Size     = new Size(160, 50),
            };
            _bottomDock.Controls.Add(_btnTestControls);

            _btnTestControls.Click += (s, e) =>
            {
                _testControlsForm = new Form_TestUserControls();
                _testControlsForm.Show();
            };

            #endregion


            #region .  Center Dock  .

            // ---------------------------------------------
            // CENTER DOCK — Tactical Map
            // ---------------------------------------------
            _centerDock = new RPSciFiGroupPanel()
            {
                GlowColor = Color.Cyan,
                Dock      = DockStyle.Fill
            };
            Controls.Add(_centerDock);

            _map = new RPSciFiMap()
            {
                Dock = DockStyle.Fill
            };
            _centerDock.Controls.Add(_map);

            #endregion

        }   // BuildLayout()


        private void Form_SizeChanged(object sender, EventArgs e)
        {
            // Force all controls to repaint when this form size changes.
            foreach (Control c in Controls)
            {
                c.Refresh();
            }

        }   // Form_SizeChanged()


        private void HookEvents()
        {
            _bus.OnEvent += evt =>
            {
                // Debug output.
                Debug.Print($"{evt.ControlType} {evt.ControlId} -> {evt.EventName} = {evt.Value}");

                // Radar contact sweep → highlight on map.
                if ((evt.ControlType == RPSciFiControlType.GroupPanel) &&  (evt.EventName == "ContactSweep"))
                {
                    _map.AddMarker(evt.Value.ToString(), x: 100 + new Random().Next(-50, 50),
                                                         y: 100 + new Random().Next(-50, 50));
                }

                // Terminal commands.
                if ((evt.ControlType == RPSciFiControlType.GroupPanel) && (evt.EventName == "Command"))
                {
                    string cmd = evt.Value.ToString().ToLower();

                    switch (cmd)
                    {
                        case "add contact":
                            _radar.AddRandomContact();
                            break;

                        case "add holo":
                            _holo.AddRandomObject();
                            break;

                        case "add item":
                            _inventory.AddItem(Guid.NewGuid().ToString(), "Item", Color.Cyan); break;

                        case "signaltoggled":
                            //camera.PerformClick();
                            break;
                    }
                }

                // Comms encryption toggled → terminal log.
                if (evt.EventName == "EncryptionToggled")
                {
                    _terminal.WriteLine("Comms encryption: " + evt.Value);
                }

                // Inventory selection → terminal log.
                if (evt.EventName == "ItemSelected")
                {
                    _terminal.WriteLine("Selected item: " + evt.Value);
                }
            };

        }   // HookEvents()


        // Check if a form of a given type is already open.
        private bool IsFormOpen<T>() where T : Form
        {
            bool isOpen = Application.OpenForms.OfType<T>().First().Text != "";

            return isOpen;

        }   // IsFormOpen()


        private void RegisterControls()
        {
            _camera   .Register(_bus);
            _comms    .Register(_bus);
            _holo     .Register(_bus);
            _inventory.Register(_bus);
            _map      .Register(_bus);
            _radar    .Register(_bus);
            _terminal .Register(_bus);

        }   // RegisterControls()


    }   // class RPSciFiBridgeForm

}   // namespace Spirograph_v1

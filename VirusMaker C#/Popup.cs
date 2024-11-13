namespace VirusMaker
{
    public class Popup
    {
        public Form Form;
        public PictureBox PictureBox;
        public Vector2Int Velocity;
        public int Lifetime;

        public Popup(Image Image)
        {
            Form = new();
            Form.Width = Image.Width;
            Form.Height = Image.Height;
            Form.FormBorderStyle = FormBorderStyle.None;
            PictureBox = new();
            PictureBox.Image = Image;
            PictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            Form.Controls.Add(PictureBox);
            Velocity = new Vector2Int(Random.Shared.Next(-5, 5), Random.Shared.Next(-5, 5));
            Form.Show();
        }
    }
}

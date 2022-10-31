namespace PISWF.view;

public class AliveOneSecond : Form
{
    double ms = 4000000;
    ProgressBar pb = new();
    
    public AliveOneSecond()
    {
        Location = new Point(900, 900);
        pb.Maximum = (int)ms;
        pb.Dock = DockStyle.Bottom;
        Controls.Add(pb);
        Size = new Size(200, 200);
        FormBorderStyle = FormBorderStyle.None;
        StartPosition = FormStartPosition.Manual;
        Rectangle screenSize = Screen.PrimaryScreen.Bounds;
        Location = new Point(screenSize.Width - Width, screenSize.Height - Height);
        Show();
        TimerToDie();
    }

    private void TimerToDie()
    {
        double i = 0;
        while (ms > i)
        {
            i += 0.1;
            pb.Value = (int)i;
        }
        Close();
    }
}
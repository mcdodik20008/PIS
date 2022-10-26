namespace PISWF.view;

public class AliveOneSecond : Form
{
    double ms = 40000000;
    ProgressBar pb = new();
    
    public AliveOneSecond()
    {
        // TODO: Как разместить форму слева снизу?
        Location = new Point(900, 900);
        pb.Maximum = (int)ms;
        pb.Dock = DockStyle.Bottom;
        Controls.Add(pb);
        Size = new Size(200, 200);
        FormBorderStyle = FormBorderStyle.None;
        StartPosition = FormStartPosition.Manual;
        Location = new Point(0, 0);
        Show();
        TimerToDie();
    }

    private void TimerToDie()
    {
        double i = 0;
        while (ms > i)
        {
            i += 1;
            pb.Value = (int)i;
        }
        Close();
    }
}
using pis.infrasrtucture.dgvf;
using PISWF.domain.registermc.controller;
using PISWF.infrasrtucture.auth.controller;
using PISWF.infrasrtucture.page;

namespace PISWF.view;

public class DgvFilter : Form
{
    private RegistermcController _registermcController;

    private Page _page = new(0, 25);

    public DgvFilter(RegistermcController registermcController)
    {
        _registermcController = registermcController;
        DoubleBuffered = true;
        InitializeItems();
        AddControls();
        _dg.FillDataGrid(registermcController.Read(_page));
    }

    //Каждый метод обарачиваем в трукачте и при неудачи выкидываем пользователю масаге боксом или чем, то что не будет бесить.
    private void CreateUser(object e, object sender)
    {
        new Thread(() => new AliveOneSecond()).Start();
        /* AuthController.AddUser(new UserBasic(nameBox.Text, passwordBox.Text));
         DG.FillDataGrid(AuthController.ReadUser(page));*/
    }

    private void InitializeItems()
    {
        _nameBox.Location = new Point(650, 100);
        _passwordBox.Location = new Point(650, 150);
        _createUserButton.Location = new Point(650, 200);
        _createUserButton.Click += CreateUser;
        Size = new Size(900, 900);
        _dg.Bounds = new Rectangle(0, 0, 600, 600);
        _dg.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
        _dg.AllowUserToAddRows = false;
    }

    private void AddControls()
    {
        Controls.Add(_dg);
        Controls.Add(_nameBox);
        Controls.Add(_passwordBox);
        Controls.Add(_createUserButton);
    }

    #region компоненты для формы

    private DataGridViewWithFilter _dg = new();
    private TextBox _nameBox = new();
    private TextBox _passwordBox = new();
    private Button _createUserButton = new();

    #endregion
}
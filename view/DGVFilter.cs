using pis.infrasrtucture.dgvf;
using PISWF.domain.registermc.controller;
using PISWF.domain.registermc.model.entity;
using PISWF.domain.registermc.model.view;
using PISWF.infrasrtucture.auth.controller;
using PISWF.infrasrtucture.filter;
using PISWF.infrasrtucture.page;

namespace PISWF.view;

public class DgvFilter : Form
{
    private FilterFactory _factory;
    
    private RegistermcController _registermcController;

    private Page _page = new(0, 25);
    
    public DgvFilter(RegistermcController registermcController, FilterFactory factory)
    {
       
        _registermcController = registermcController;
        _factory = factory;
        InitializeItems();
        AddControls();
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
        _dg = new(_factory);
        _dg.FillDataGrid(_registermcController.Read(_page, _dg.GetFilter<RegisterMC>()));
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

    private DataGridViewWithFilter<RegisterFilter> _dg;
    private TextBox _nameBox = new();
    private TextBox _passwordBox = new();
    private Button _createUserButton = new();

    #endregion
}
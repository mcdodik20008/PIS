using pis.infrasrtucture.dgvf;
using pis.infrasrtucture.filter.impl;
using PISWF.domain.registermc.controller;
using PISWF.domain.registermc.model.entity;
using PISWF.infrasrtucture.auth.controller;
using PISWF.infrasrtucture.auth.model.entity;
using PISWF.infrasrtucture.auth.model.view;
using PISWF.infrasrtucture.filter;
using PISWF.infrasrtucture.page;

namespace PISWF.view;

public class DgvFilter : Form
{
    private RegistermcController _registermcController;

    private AuthController _authController;
    
    private Page _page = new(0, 100);

    public DgvFilter(RegistermcController registermcController, AuthController authController, IFilterFactory factory, FilterMapper filterMapper)
    {
        _authController = authController;
        authController.Authorization(new UserAuth("admin", "1234"));
        _registermcController = registermcController;
        _dg = new(factory, filterMapper);
        InitializeItems();
        AddControls();
    }

    private void FillWithFilter(object e, object sender)
    {
        _dg.DataSource = null;
        _dg.FillDataGrid(_registermcController.Read(_page, _dg.GetFilter<RegisterMC>(), _dg.SortParameters<RegisterMC>()));
        //_registermcController.UpLoadFile(_registermcController.Read(1));
    }

    private void InitializeItems()
    {
        Size = new Size(900, 900);
        _nameBox.Location = new Point(650, 100);
        _passwordBox.Location = new Point(650, 150);
        _createUserButton.Location = new Point(650, 200);
        _createUserButton.Click += FillWithFilter;
        // TODO: почему-то первый раз с фильтром не рабоитает???
        _dg.FillDataGrid(_registermcController.Read(_page));
        _dg.FillDataGrid(_registermcController.Read(_page, _dg.GetFilter<RegisterMC>(), _dg.SortParameters<RegisterMC>()));
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
    private Panel _panel = new();

    #endregion
}
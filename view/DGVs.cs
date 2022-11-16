using System.Data;
using PISWF.infrasrtucture.auth.controller;
using PISWF.infrasrtucture.auth.model.view;
using pis.infrasrtucture.dgvf;
using pis.infrasrtucture.filter.impl;
using PISWF.infrasrtucture.filter;
using PISWF.domain.registermc.controller;
using PISWF.domain.registermc.model.entity;
using PISWF.domain.registermc.model.view;
using PISWF.domain.registermc.service;
using PISWF.infrasrtucture.page;


namespace PISWF.view;

public class DGVs : Form
{
    private AuthController _authController;
    
    private RegistermcController _registermcController;
    
    private DgvLong _dgvLong;
    
    private Page _page = new(0, 25);
    
    public DGVs(AuthController authController, 
        IFilterFactory factory, 
        FilterSorterMapper filterSorterMapper, 
        RegistermcController registermcController, DgvLong dgvLong)
    {
        _dgvLong = dgvLong;
        _registermcController = registermcController;
        _authController = authController;
        dg = new(factory, filterSorterMapper);
        InitializeItems();
        AddControls();
    }
    
    private void OpenLongDgv(object e, object sender)
    {
        var selectedItem = dg.GetSelectedItem(dg.CurrentRow.Index);
        _dgvLong.GetShortRegisterMC(selectedItem);
        _dgvLong.ShowDialog();
    }
    
    private void FillWithFilter(object e, object sender)
    {
        dg.DataSource = null;
        dg.FillDataGrid(_registermcController.Read(_page, dg.GetFilter<RegisterMC>(), dg.GetSortParameters<RegisterMC>()));
    }
    
    private void CreateNew(object e, object sender)
    {
        var registerMCLong = new RegisterMCLong();
        _dgvLong.ClearRegisterMC(registerMCLong);
        _dgvLong.ShowDialog();
    }
    
    private void InitializeItems()
    {
        Size = new Size(800, 600);
        
        dg.FillDataGrid(_registermcController.Read(_page));
        dg.FillDataGrid(_registermcController.Read(_page, dg.GetFilter<RegisterMC>(), dg.GetSortParameters<RegisterMC>()));
        dg.Location = new Point(0, 0);
        dg.Size = new Size(655, 600);
        //dg.Bounds = new Rectangle(0, 0, 655, 460);
        dg.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
        dg.AllowUserToAddRows = false;

        filterButton.Location = new Point(658, 317);
        filterButton.Size = new System.Drawing.Size(120, 28);
        filterButton.Text = "Фильтровать";
        filterButton.Click += FillWithFilter;
        
        openButton.Location = new Point(658, 346);
        openButton.Size = new System.Drawing.Size(120, 28);
        openButton.Text = "Открыть";
        openButton.Click += OpenLongDgv;
        
        addButton.Location = new Point(658, 375);
        addButton.Size = new System.Drawing.Size(120, 28);
        addButton.Text = "Добавить";
        addButton.Click += CreateNew;
        
        deleteButton.Location = new Point(658, 404);
        deleteButton.Size = new System.Drawing.Size(120, 28);
        deleteButton.Text = "Удалить";
      //  deleteButton.Click += Authorization;
        
        exportButton.Location = new Point(658, 433);
        exportButton.Size = new System.Drawing.Size(120, 50);
        exportButton.Text = "Экспорт в Excel";
       // exportButton.Click += Authorization;
       
       userLabel.Location = new System.Drawing.Point(675, 28);
       userLabel.Size = new System.Drawing.Size(120, 28);
       var user = _authController.AutorizedUser.FirstName +" "+  _authController.AutorizedUser.LastName;
       userLabel.Text = user;
        
    }
    
    private void AddControls()
    {
        Controls.Add(dg);
        Controls.Add(openButton);
        Controls.Add(addButton);
        Controls.Add(deleteButton);
        Controls.Add(exportButton);
        Controls.Add(userLabel);
        Controls.Add(filterButton);
    }
    
    #region компоненты для формы
    
    private DataGridViewWithFilter<RegisterMCShort, RegisterFilter> dg;
    private Button openButton = new();
    private Button addButton = new();
    private Button deleteButton = new();
    private Button exportButton = new();
    private Button filterButton = new ();
    private Label userLabel = new ();
    
    #endregion
}

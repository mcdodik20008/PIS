using System.Data;
using PISWF.infrasrtucture.auth.controller;
using PISWF.infrasrtucture.auth.model.view;
using pis.infrasrtucture.dgvf;
using pis.infrasrtucture.filter.impl;
using PISWF.infrasrtucture.filter;
using PISWF.domain.registermc.controller;
using PISWF.domain.registermc.model.entity;
using PISWF.domain.registermc.service;
using PISWF.infrasrtucture.page;


namespace PISWF.view;

public class DGVs : Form
{
    private AuthController _authController;
    
    private RegistermcController _registermcController;
    
    private Page _page = new(0, 25);
    
    public DGVs(AuthController authController, 
        IFilterFactory factory, 
        FilterSorterMapper filterSorterMapper, 
        RegistermcController registermcController)
    {
        _registermcController = registermcController;
        _authController = authController;
        dg = new(factory, filterSorterMapper);
        InitializeItems();
        AddControls();
       // dg.Columns[0].Visible = false; //вот тут затуп!!!
    }
    
    private void OpenLongDgv(object e, object sender)
    {
        var id = dg.CurrentRow.Cells[0].Value.ToString();
        var dgvLongForm = new DgvLong(Convert.ToInt32(id), _registermcController);
        dgvLongForm.ShowDialog();
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

        openButton.Location = new Point(658, 346);
        openButton.Size = new System.Drawing.Size(120, 28);
        openButton.Text = "Открыть";
        openButton.Click += OpenLongDgv;
        
        addButton.Location = new Point(658, 375);
        addButton.Size = new System.Drawing.Size(120, 28);
        addButton.Text = "Добавить";
      //  addButton.Click += Authorization;
        
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
    }
    
    #region компоненты для формы
    
    private DataGridViewWithFilter<RegisterFilter> dg;
    private Button openButton = new();
    private Button addButton = new();
    private Button deleteButton = new();
    private Button exportButton = new();
    private Label userLabel = new ();
    #endregion
}

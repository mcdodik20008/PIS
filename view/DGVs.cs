using PISWF.infrasrtucture.auth.controller;
using pis.infrasrtucture.dgvf;
using pis.infrasrtucture.filter.impl;
using PISWF.infrasrtucture.filter;
using PISWF.domain.registermc.controller;
using PISWF.domain.registermc.model.entity;
using PISWF.domain.registermc.model.view;
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
        StartPosition = FormStartPosition.CenterScreen;
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
        _dgvLong.SetShortRegisterMC(selectedItem);
        _dgvLong.ShowDialog();
    }
    
    private void FillWithFilter(object e, object sender)
    {
        dg.DataSource = null;
        // можно на несколько строк разбить, не читаемо
        //так пойдет? я не знаю как лучше
        dg.FillDataGrid(_registermcController.Read(
            _page, 
            dg.GetFilter<RegisterMC>(), 
            dg.GetSortParameters<RegisterMC>()));
    }
    
    private void CreateNew(object e, object sender)
    {
        _dgvLong.ClearRegisterMC(new RegisterMCLong());
        _dgvLong.ShowDialog();
    }
    
    private void Delete(object e, object sender)
    {
        var selectedItem = dg.GetSelectedItem(dg.CurrentRow.Index);
        _registermcController.Delete(selectedItem.Id);
        //TODO: удалять файлы вместе с записью
        dg.FillDataGrid(_registermcController.Read(_page, dg.GetFilter<RegisterMC>(), dg.GetSortParameters<RegisterMC>()));
    }
    
    private void ExportToExcel(object e, object sender)
    {
        _registermcController.ExportToExcel();
    }
    
    private void UpdatePageSize(object e, object sender)
    {
        _page = new Page(int.Parse(numberPageBox.Text)-1, (int)sizePageNumericUpDown.Value);
        dg.FillDataGrid(_registermcController.Read(_page, dg.GetFilter<RegisterMC>(), dg.GetSortParameters<RegisterMC>()));
    }
    
    private void UpdatePageNumberDown(object e, object sender)
    {
        if (_page.Number == 0)
            MessageBox.Show("Номер страницы не может быть меньше 1");
        else
        {
            _page = new Page(_page.Number-1, (int)sizePageNumericUpDown.Value);
            numberPageBox.Text = (_page.Number + 1).ToString();
            dg.FillDataGrid(_registermcController.Read(_page, dg.GetFilter<RegisterMC>(), dg.GetSortParameters<RegisterMC>()));
        }
    }
    
    private void UpdatePageNumberUp(object e, object sender)
    {
        _page = new Page(_page.Number+ 1, (int)sizePageNumericUpDown.Value);
        numberPageBox.Text = (_page.Number+1).ToString();
        dg.FillDataGrid(_registermcController.Read(_page, dg.GetFilter<RegisterMC>(), dg.GetSortParameters<RegisterMC>()));
    }
    
    private void InitializeItems()
    {
        Size = new Size(800, 600);
        Text = "Реестр";
        
        dg.FillDataGrid(_registermcController.Read(_page));
        dg.FillDataGrid(_registermcController.Read(_page, dg.GetFilter<RegisterMC>(), dg.GetSortParameters<RegisterMC>()));
        dg.Location = new Point(0, 0);
        dg.Size = new Size(655, 510);
        dg.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
        dg.AllowUserToAddRows = false;
        dg.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        filterButton.Location = new Point(658, 317);
        filterButton.Size = new Size(120, 28);
        filterButton.Text = "Фильтровать";
        filterButton.Click -= FillWithFilter;
        filterButton.Click += FillWithFilter;
        
        openButton.Location = new Point(658, 346);
        openButton.Size = new Size(120, 28);
        openButton.Text = "Открыть";
        openButton.Click -= OpenLongDgv;
        openButton.Click += OpenLongDgv;
        
        addButton.Location = new Point(658, 375);
        addButton.Size = new Size(120, 28);
        addButton.Text = "Добавить";
        addButton.Click -= CreateNew;
        addButton.Click += CreateNew;
        
        deleteButton.Location = new Point(658, 404);
        deleteButton.Size = new Size(120, 28);
        deleteButton.Text = "Удалить";
        deleteButton.Click -= Delete;
        deleteButton.Click += Delete;
        
        exportButton.Location = new Point(658, 433);
        exportButton.Size = new Size(120, 50);
        exportButton.Text = "Экспорт в Excel";
        exportButton.Click -= ExportToExcel;
        exportButton.Click += ExportToExcel;
       
       userLabel.Location = new Point(675, 28);
       userLabel.Size = new Size(120, 28);
       var user = _authController.AutorizedUser.FirstName +" "+  _authController.AutorizedUser.LastName;
       userLabel.Text = user;

       numberPageLabel.Location = new Point(12,520);
       numberPageLabel.Size = new Size(132, 18);
       numberPageLabel.Text = "Номер страницы";

       downPageButton.Location = new Point(145,520);
       downPageButton.Size = new Size(44, 27);
       downPageButton.Text = "<";
       downPageButton.Click += UpdatePageNumberDown;
       
       numberPageBox.Location = new Point(190,520);
       numberPageBox.Size = new Size(46, 18);
       numberPageBox.Text = "1";
       numberPageBox.TextChanged += UpdatePageSize;

       upPageButton.Location = new Point(237,520);
       upPageButton.Size = new Size(44, 27);
       upPageButton.Text = ">";
       upPageButton.Click += UpdatePageNumberUp;
       
       sizePageLabel.Location = new Point(292,520);
       sizePageLabel.Size = new Size(132, 18);
       sizePageLabel.Text = "Размер страницы";

       sizePageNumericUpDown.Location = new Point(425,520);
       sizePageNumericUpDown.Size = new Size(44, 23);
       sizePageNumericUpDown.Value = 25;
       sizePageNumericUpDown.ValueChanged += UpdatePageSize;
       sizePageNumericUpDown.TextChanged += UpdatePageSize;
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
        Controls.Add(numberPageLabel);
        Controls.Add(downPageButton);
        Controls.Add(numberPageBox);
        Controls.Add(upPageButton);
        Controls.Add(sizePageLabel);
        Controls.Add(sizePageNumericUpDown);
    }
    
    #region компоненты для формы
    
    private DataGridViewWithFilter<RegisterMCShort, RegisterFilter> dg;
    private Button openButton = new();
    private Button addButton = new();
    private Button deleteButton = new();
    private Button exportButton = new();
    private Button filterButton = new ();
    private Label userLabel = new ();
    private Label numberPageLabel = new();
    private Button downPageButton = new();
    private TextBox numberPageBox = new();
    private Button upPageButton = new();
    private Label sizePageLabel = new ();
    private NumericUpDown sizePageNumericUpDown = new ();
    
    #endregion
}

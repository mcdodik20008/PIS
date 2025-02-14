﻿using DGWF.dgvf;
using DGWF.dgvf.filter;
using DGWF.impl;
using PISWF.infrasrtucture.auth.controller;
using PISWF.domain.registermc.controller;
using PISWF.domain.registermc.model.entity;
using PISWF.domain.registermc.model.view;
using PISWF.infrasrtucture.auth.model.entity;
using PISWF.infrasrtucture.page;


namespace PISWF.view;

public class DGVs : Form
{
    private AuthController _authController;

    private User _user;

    private RegistermcController _registermcController;

    private DgvLong _dgvLong;

    private Page _page = new(0, 25);

    public DGVs(
        AuthController authController,
        Mapper mapper,
        RegistermcController registermcController,
        DgvLong dgvLong)
    {
        StartPosition = FormStartPosition.CenterScreen;
        _dgvLong = dgvLong;
        _registermcController = registermcController;
        _authController = authController;
        _user = authController.AutorizedUser;
        mapper.SetBasicMaps();
        dg = new(new RegisterFilter(), mapper);
        InitializeItems();
        AddControls();
    }

    private async void CheckForm(object e, object sender)
    {
        dg.Reset();
        addButton.Hide();
        deleteButton.Hide();
        _user = _authController.AutorizedUser;
        if (!(_user.Roles.Where(x => x.Possibility.Rate.Equals("Ведения")).Count() == 0))
        {
            addButton.Show();
            deleteButton.Show();
        }

        _page = new(0, 25);
        numberPageBox.Text = "1";
        sizePageNumericUpDown.Value = 25;
        dg.DataSource = null;
        dg.FillDataGrid(await Task.Run(() =>
            _registermcController.Read(_page, dg.GetFilter<RegisterMC>(), dg.GetSortParameters<RegisterMC>())) , new List<string>() {"Id"});
    }

    private void OpenLongDgv(object e, object sender)
    {
        if (dg.CurrentRow is null) return;
        var selectedItem = dg.GetSelectedValue();
        _dgvLong.SetShortRegisterMC(selectedItem);
        _dgvLong.ShowDialog();
        FillWithFilter(e, sender);
    }

    private async void FillWithFilter(object e, object sender)
    {
        dg.DataSource = null;
        dg.FillDataGrid(
            await Task.Run(() => _registermcController.Read(_page, dg.GetFilter<RegisterMC>(), dg.GetSortParameters<RegisterMC>())), new List<string>() {"Id"});
    }

    //TODO Отфильтруй по 'xx' (x - любое число), напирмер, и поменяй на 'xx123' и у тебя в гриде будет 'xx'
    private void CreateNew(object e, object sender)
    {
        _dgvLong.ClearRegisterMC(new RegisterMCLong());
        _dgvLong.ShowDialog();
        FillWithFilter(e, sender);
    }

    private async void Delete(object e, object sender)
    {
        var selectedItem = dg.GetSelectedValue();
        _registermcController.Delete(selectedItem.Id);
        //TODO: удалять файлы вместе с записью
        dg.FillDataGrid(await Task.Run(() =>
            _registermcController.Read(_page, dg.GetFilter<RegisterMC>(), dg.GetSortParameters<RegisterMC>())));
    }

    private void ExportToExcel(object e, object sender)
    {
        _registermcController.ExportToExcel();
    }

    private async void UpdatePageSize(object e, object sender)
    {
        _page = new Page(int.Parse(numberPageBox.Text) - 1, (int)sizePageNumericUpDown.Value);
        dg.FillDataGrid(await Task.Run(() =>
            _registermcController.Read(_page, dg.GetFilter<RegisterMC>(), dg.GetSortParameters<RegisterMC>())));
    }

    private async void UpdatePageNumberDown(object e, object sender)
    {
        if (_page.Number == 0)
        {
            MessageBox.Show("Номер страницы не может быть меньше 1");
        }
        else
        {
            _page = new Page(_page.Number - 1, (int)sizePageNumericUpDown.Value);
            numberPageBox.Text = (_page.Number + 1).ToString();
            dg.FillDataGrid(await Task.Run(() =>
                _registermcController.Read(_page, dg.GetFilter<RegisterMC>(), dg.GetSortParameters<RegisterMC>())));
        }
    }

    private async void UpdatePageNumberUp(object e, object sender)
    {
        if ((_page.Number + 1) * _page.Size > _registermcController.Count())
            return;
        _page = new Page(_page.Number + 1, (int)sizePageNumericUpDown.Value);
        numberPageBox.Text = (_page.Number + 1).ToString();
        dg.FillDataGrid(await Task.Run(() =>
            _registermcController.Read(_page, dg.GetFilter<RegisterMC>(), dg.GetSortParameters<RegisterMC>())));
    }

    private void InitializeItems()
    {
        Size = new Size(800, 600);
        Text = "Реестр";
        dg.Location = new Point(0, 0);
        dg.Size = new Size(655, 510);
        dg.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
        dg.AllowUserToAddRows = false;
        dg.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        filterButton.Location = new Point(658, 317);
        filterButton.Size = new Size(120, 28);
        filterButton.Anchor = AnchorStyles.Right | AnchorStyles.Top;
        filterButton.Text = "Фильтровать";
        filterButton.Click -= FillWithFilter;
        filterButton.Click += FillWithFilter;

        openButton.Location = new Point(658, 346);
        openButton.Size = new Size(120, 28);
        openButton.Anchor = AnchorStyles.Right | AnchorStyles.Top;
        openButton.Text = "Открыть";
        openButton.Click -= OpenLongDgv;
        openButton.Click += OpenLongDgv;

        addButton.Location = new Point(658, 375);
        addButton.Size = new Size(120, 28);
        addButton.Anchor = AnchorStyles.Right | AnchorStyles.Top;
        addButton.Text = "Добавить";
        addButton.Click -= CreateNew;
        addButton.Click += CreateNew;

        deleteButton.Location = new Point(658, 404);
        deleteButton.Size = new Size(120, 28);
        deleteButton.Anchor = AnchorStyles.Right | AnchorStyles.Top;
        deleteButton.Text = "Удалить";
        deleteButton.Click -= Delete;
        deleteButton.Click += Delete;

        exportButton.Location = new Point(658, 267);
        exportButton.Size = new Size(120, 50);
        exportButton.Anchor = AnchorStyles.Right | AnchorStyles.Top;
        exportButton.Text = "Экспорт в Excel";
        exportButton.Click -= ExportToExcel;
        exportButton.Click += ExportToExcel;

        userLabel.Location = new Point(675, 28);
        userLabel.Size = new Size(120, 28);
        //userLabel.Text = _user.Login;

        numberPageLabel.Location = new Point(12, 520);
        numberPageLabel.Size = new Size(132, 18);
        numberPageLabel.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
        numberPageLabel.Text = "Номер страницы";

        downPageButton.Location = new Point(145, 520);
        downPageButton.Size = new Size(44, 27);
        downPageButton.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
        downPageButton.Text = "<";
        downPageButton.Click += UpdatePageNumberDown;

        numberPageBox.Location = new Point(190, 520);
        numberPageBox.Size = new Size(46, 18);
        numberPageBox.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
        numberPageBox.Text = "1";
        numberPageBox.TextChanged += UpdatePageSize;

        upPageButton.Location = new Point(237, 520);
        upPageButton.Size = new Size(44, 27);
        upPageButton.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
        upPageButton.Text = ">";
        upPageButton.Click += UpdatePageNumberUp;

        sizePageLabel.Location = new Point(292, 520);
        sizePageLabel.Size = new Size(132, 18);
        sizePageLabel.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
        sizePageLabel.Text = "Размер страницы";

        sizePageNumericUpDown.Location = new Point(425, 520);
        sizePageNumericUpDown.Size = new Size(44, 23);
        sizePageNumericUpDown.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
        sizePageNumericUpDown.Value = 25;
        sizePageNumericUpDown.ValueChanged += UpdatePageSize;
        sizePageNumericUpDown.TextChanged += UpdatePageSize;

        Shown += CheckForm;
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
    private Button filterButton = new();
    private Label userLabel = new();
    private Label numberPageLabel = new();
    private Button downPageButton = new();
    private TextBox numberPageBox = new();
    private Button upPageButton = new();
    private Label sizePageLabel = new();
    private NumericUpDown sizePageNumericUpDown = new();

    #endregion
}
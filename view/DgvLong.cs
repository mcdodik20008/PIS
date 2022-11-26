using PISWF.domain.registermc.controller;
using PISWF.domain.registermc.model.view;
using PISWF.infrasrtucture.auth.controller;
using PISWF.infrasrtucture.auth.model.entity;
using PISWF.infrasrtucture.logger.controller;
using PISWF.infrasrtucture.muni_org.controller;
using PISWF.infrasrtucture.muni_org.model.view;

namespace PISWF.view;

public class DgvLong : Form
{
    private User _user;

    private RegisterMCLong _registerMcLong;

    private RegistermcController _registermcController;

    private OrganizationController _organizationController;

    private MunicipalityController _municipalityController;

    private AuthController _authController;

    private LogController _logController;

    private List<OrganizationShort> _organizationList;

    private List<MunicipalityShort> _municipalityList;

    public DgvLong(
        RegistermcController registermcController,
        OrganizationController organizationController,
        MunicipalityController municipalityController,
        AuthController authController)
    {
        StartPosition = FormStartPosition.CenterScreen;
        _registermcController = registermcController;
        _organizationController = organizationController;
        _municipalityController = municipalityController;
        _authController = authController;
        _organizationList = _organizationController.Read();
        _municipalityList = _municipalityController.Read();
        InitializeItems();
        AddControls();
    }

    public void SetShortRegisterMC(RegisterMCShort registerMcShort)
    {
        SetLongRegisterMc(registerMcShort.Id);
    }

    private void InitializeItems()
    {
        Size = new Size(830, 570);
        MinimumSize = new Size(830, 570);
        Text = "Контракт";
        var organizationsList = _organizationController.Read();
        var municipalityList = _municipalityController.Read();
        organizationComboBox.DataSource = organizationsList;
        municipalityComboBox.DataSource = municipalityList;

        FF();
        Shown += CheckForm;
    }

    private void FF()
    {
        numberLabel.Location = new Point(218, 10);
        numberLabel.Size = new Size(72, 20);
        numberLabel.Text = "Номер МК";
        validDateLabel.Location = new Point(146, 42);
        validDateLabel.Size = new Size(144, 20);
        validDateLabel.Text = "Дата заключения МК";

        locationLabel.Location = new Point(107, 72);
        locationLabel.Size = new Size(170, 20);
        locationLabel.Text = "Место оказания услуги";

        actionTimeLabel.Location = new Point(167, 102);
        actionTimeLabel.Size = new Size(125, 20);
        actionTimeLabel.Text = "Дата действия МК";

        organizationLabel.Location = new Point(174, 133);
        organizationLabel.Size = new Size(116, 20);
        organizationLabel.Text = "Исполнитель МК";

        municipalityLabel.Location = new Point(55, 162);
        municipalityLabel.Size = new Size(250, 20);
        municipalityLabel.Text = "Муниципальное образование";

        municipalityComboBox.Location = new Point(321, 160);
        municipalityComboBox.Size = new Size(200, 20);

        omsuLabel.Location = new Point(223, 192);
        omsuLabel.Size = new Size(55, 20);
        omsuLabel.Text = "ОМСУ";

        yearLabel.Location = new Point(138, 224);
        yearLabel.Size = new Size(145, 45);
        yearLabel.Text = "Год, на который \r\nвыдана субвенция";

        yearNumericUpDown.Location = new Point(321, 225);
        yearNumericUpDown.Size = new Size(200, 20);
        yearNumericUpDown.Maximum = 9999999;

        priceLabel.Location = new Point(158, 270);
        priceLabel.Size = new Size(120, 20);
        priceLabel.Text = "Цена контракта";

        priceNumericUpDown.Location = new Point(321, 268);
        priceNumericUpDown.Size = new Size(200, 20);
        priceNumericUpDown.Maximum = 9999999;

        subventionShareLabel.Location = new Point(150, 298);
        subventionShareLabel.Size = new Size(140, 45);
        subventionShareLabel.Text = "Доля субвенции \r\nв цене контракта";

        subventionShareNumericUpDown.Location = new Point(321, 302);
        subventionShareNumericUpDown.Size = new Size(200, 20);
        subventionShareNumericUpDown.Maximum = 9999999;

        amountMoneyLabel.Location = new Point(74, 345);
        amountMoneyLabel.Size = new Size(220, 65);
        amountMoneyLabel.Text = "Объём денежных средств, \r\nвыплаченных Исполнителю \r\nпо контракту";

        amountMoneyNumericUpDown.Location = new Point(321, 348);
        amountMoneyNumericUpDown.Size = new Size(200, 20);
        amountMoneyNumericUpDown.Maximum = 9999999;

        partMoneyLabel.Location = new Point(74, 410);
        partMoneyLabel.Size = new Size(220, 65);
        partMoneyLabel.Text = "Доля денежных средств \r\nиз субвенции, выплаченной \r\nпо контракту в %\r\n\r\n";

        shareFundsSubventionNumericUpDown.Location = new Point(321, 410);
        shareFundsSubventionNumericUpDown.Size = new Size(200, 20);
        shareFundsSubventionNumericUpDown.Maximum = 9999999;

        numberBox.Location = new Point(321, 10);
        numberBox.Size = new Size(200, 20);

        validDatePicker.Location = new Point(321, 40);
        validDatePicker.Size = new Size(200, 20);

        locationBox.Location = new Point(321, 70);
        locationBox.Size = new Size(200, 20);

        actionTimePicker.Location = new Point(321, 100);
        actionTimePicker.Size = new Size(200, 20);

        organizationComboBox.Location = new Point(321, 130);
        organizationComboBox.Size = new Size(200, 20);

        omsuBox.Location = new Point(321, 190);
        omsuBox.Size = new Size(200, 20);

        changeButton.Location = new Point(44, 480);
        changeButton.Size = new Size(125, 28);
        changeButton.Text = "Сохранить";
        changeButton.Click += Add;

        uploadFileButton.Location = new Point(169, 480);
        uploadFileButton.Size = new Size(150, 28);
        uploadFileButton.Text = "Загрузить файл";
        uploadFileButton.Click += UploadFile;

        deleteFileButton.Location = new Point(318, 480);
        deleteFileButton.Size = new Size(150, 28);
        deleteFileButton.Text = "Удалить файл";
        deleteFileButton.Click += DeleteFile;

        fielsDataGridView.Location = new Point(550, 22);
        fielsDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        fielsDataGridView.AllowUserToAddRows = false;
        fielsDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        fielsDataGridView.ColumnHeadersVisible = false;
        fielsDataGridView.CellContentClick +=(e, x) => OpenImage(e, x);
        fielsDataGridView.ReadOnly = true;
        
        pictureBox.Location = new Point(550, 200);
        pictureBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
        pictureBox.Size = new Size(250, 200);
        pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

    }

    private void OpenImage(object? sender, DataGridViewCellEventArgs dataGridViewCellEventArgs)
    {
        var zz = _registerMcLong.Documents[fielsDataGridView.CurrentRow.Index];
        pictureBox.Image = _registermcController.GetImage(zz.Id);
    }

    private void CheckForm(object e, object sender)
    {
        changeButton.Show();
        _user = _authController.AutorizedUser;
        if (_registerMcLong.Id == 0)
        {
            uploadFileButton.Hide();
            deleteFileButton.Hide();
        }
        else
        {
            uploadFileButton.Show();
            deleteFileButton.Show();
        }

        if (_user.Roles.Where(x => x.Possibility.Rate.Equals("Ведения")).Count() == 0)
        {
            changeButton.Hide();
            uploadFileButton.Hide();
            deleteFileButton.Hide();
        }
    }

    public void ClearRegisterMC(RegisterMCLong registerMcLong)
    {
        _registerMcLong = registerMcLong;
        validDatePicker.Value = DateTime.Today;
        actionTimePicker.Value = DateTime.Today;
        organizationComboBox.Text = "";
        municipalityComboBox.Text = "";
        FillControls(_registerMcLong);
    }

    private void FillControls(RegisterMCLong registerMcLong)
    {
        numberBox.Text = registerMcLong.Number;
        locationBox.Text = registerMcLong.Location;
        omsuBox.Text = registerMcLong.Omsu;
        yearNumericUpDown.Text = registerMcLong.Year.ToString();
        priceNumericUpDown.Text = registerMcLong.Price.ToString();
        subventionShareNumericUpDown.Text = registerMcLong.SubventionShare.ToString();
        amountMoneyNumericUpDown.Text = registerMcLong.AmountMoney.ToString();
        shareFundsSubventionNumericUpDown.Text = registerMcLong.ShareFundsSubvention.ToString();
        fielsDataGridView.DataSource = _registerMcLong.Documents;
    }

    private void UploadFile(object e, object sender)
    {
        _registermcController.UpLoadFile(_registerMcLong);
        _registerMcLong = _registermcController.Read(_registerMcLong.Id);
        fielsDataGridView.DataSource = _registerMcLong.Documents;
    }

    private void DeleteFile(object e, object sender)
    {
        if (fielsDataGridView.Rows.Count > 0)
        {
            var xxx = fielsDataGridView.CurrentRow.Cells[0].Value.ToString();
            _registermcController.DeleteFile(_registerMcLong.Id, long.Parse(xxx));
            _registerMcLong = _registermcController.Read(_registerMcLong.Id);
            fielsDataGridView.DataSource = _registerMcLong.Documents;
        }
    }

    private void SetLongRegisterMc(long id)
    {
        _registerMcLong = _registermcController.Read(id);
        validDatePicker.Value = _registerMcLong.ValidDate;
        actionTimePicker.Value = _registerMcLong.ActionDate;
        organizationComboBox.Text = _registerMcLong.Organization.Name;
        municipalityComboBox.Text = _registerMcLong.Municipality.Name;
        FillControls(_registerMcLong);
    }

    private RegisterMCLong FillRegisterMc(RegisterMCLong registerMcLong)
    {
        registerMcLong.Number = numberBox.Text;
        registerMcLong.ValidDate = validDatePicker.Value;
        registerMcLong.Location = locationBox.Text;
        registerMcLong.ActionDate = actionTimePicker.Value;
        registerMcLong.Organization = _organizationList[organizationComboBox.SelectedIndex];
        registerMcLong.Municipality = _municipalityList[municipalityComboBox.SelectedIndex];
        registerMcLong.Omsu = omsuBox.Text;
        registerMcLong.Year = Int32.Parse(yearNumericUpDown.Text);
        registerMcLong.Price = double.Parse(priceNumericUpDown.Text);
        registerMcLong.SubventionShare = Double.Parse(subventionShareNumericUpDown.Text);
        registerMcLong.AmountMoney = Double.Parse(amountMoneyNumericUpDown.Text);
        registerMcLong.ShareFundsSubvention = Double.Parse(shareFundsSubventionNumericUpDown.Text);
        return registerMcLong;
    }

    private void Add(object e, object sender)
    {
        string error = "";
        if (numberBox.Text.Length < 1)
            error += "Длина номера должна быть больше 1 \n";
        if (locationBox.Text.Length < 1)
            error += "Заплоните поле \"Место оказания услуги\" \n";
        if (omsuBox.Text.Length < 1)
            error += "Заплоните поле \"ОМСУ\" \n";
        
        if (error.Length != 0)
        {
            MessageBox.Show(error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        if (_registerMcLong.Id == 0)
        {
            var entity = FillRegisterMc(new RegisterMCLong());
            _registerMcLong = _registermcController.Create(entity);
            uploadFileButton.Show();
            deleteFileButton.Show();
        }
        else
        {
            _registerMcLong = FillRegisterMc(_registerMcLong);
            _registermcController.Update(_registerMcLong.Id, _registerMcLong);
        }
    }

    private void AddControls()
    {
        Controls.Add(numberLabel);
        Controls.Add(validDateLabel);
        Controls.Add(locationLabel);
        Controls.Add(actionTimeLabel);
        Controls.Add(organizationLabel);
        Controls.Add(municipalityLabel);
        Controls.Add(omsuLabel);
        Controls.Add(yearLabel);
        Controls.Add(priceLabel);
        Controls.Add(subventionShareLabel);
        Controls.Add(amountMoneyLabel);
        Controls.Add(partMoneyLabel);
        Controls.Add(numberBox);
        Controls.Add(validDatePicker);
        Controls.Add(locationBox);
        Controls.Add(actionTimePicker);
        Controls.Add(organizationComboBox);
        Controls.Add(municipalityComboBox);
        Controls.Add(omsuBox);
        Controls.Add(yearNumericUpDown);
        Controls.Add(priceNumericUpDown);
        Controls.Add(subventionShareNumericUpDown);
        Controls.Add(amountMoneyNumericUpDown);
        Controls.Add(shareFundsSubventionNumericUpDown);
        Controls.Add(changeButton);
        Controls.Add(uploadFileButton);
        Controls.Add(deleteFileButton);
        Controls.Add(fielsDataGridView);
        Controls.Add(pictureBox);
    }

    #region компоненты для формы

    private Label numberLabel = new();
    private Label validDateLabel = new();
    private Label locationLabel = new();
    private Label actionTimeLabel = new();
    private Label organizationLabel = new();
    private Label municipalityLabel = new();
    private Label omsuLabel = new();
    private Label yearLabel = new();
    private Label priceLabel = new();
    private Label subventionShareLabel = new();
    private Label amountMoneyLabel = new();
    private Label partMoneyLabel = new();
    private TextBox numberBox = new();
    private DateTimePicker validDatePicker = new();
    private TextBox locationBox = new();
    private DateTimePicker actionTimePicker = new();
    private ComboBox organizationComboBox = new();
    private ComboBox municipalityComboBox = new();
    private TextBox omsuBox = new();
    private NumericUpDown yearNumericUpDown = new();
    private NumericUpDown priceNumericUpDown = new();
    private NumericUpDown subventionShareNumericUpDown = new();
    private NumericUpDown amountMoneyNumericUpDown = new();
    private NumericUpDown shareFundsSubventionNumericUpDown = new();
    private Button changeButton = new();
    private Button uploadFileButton = new();
    private Button deleteFileButton = new();
    private DataGridView fielsDataGridView = new();
    private PictureBox pictureBox = new();
    
    #endregion
}
using PISWF.domain.registermc.controller;
using PISWF.domain.registermc.model.view;
using PISWF.infrasrtucture.muni_org.controller;
using PISWF.infrasrtucture.muni_org.model.entity;
using PISWF.infrasrtucture.muni_org.service;

namespace PISWF.view;

public class DgvLong : Form
{
    private RegisterMCLong _registerMcLong;
    private RegistermcController _registermcController;
    private OrganizationController _organizationController;
    private MunicipalityController _municipalityController;
    
    public DgvLong(RegistermcController registermcController, 
        OrganizationController organizationController,
        MunicipalityController municipalityController)
    {
        _registermcController = registermcController;
        _organizationController = organizationController;
        _municipalityController = municipalityController;
        InitializeItems();
        AddControls();
    }
    
    private void InitializeItems()
    {
        Size = new Size(590, 550);
        Text = "Контракт";

        var OrganizationsList = _organizationController.Read();
        var municipalityList = _municipalityController.Read();
        foreach (var organization in OrganizationsList)
        {
            organizationComboBox.Items.Add(organization.Name);
        }
        foreach (var municipality in municipalityList)
        {
            municipalityComboBox.Items.Add(municipality.Name);
        }

        numberLabel.Location = new Point(218, 10);
        numberLabel.Size = new Size(72, 20);
        numberLabel.Text = "Номер МК";
        
        validDateLabel.Location = new Point(146, 39);
        validDateLabel.Size = new Size(144, 20);
        validDateLabel.Text = "Дата заключения МК";
        
        locationLabel.Location = new Point(105, 65);
        locationLabel.Size = new Size(170, 20);
        locationLabel.Text = "Место оказания услуги";
        
        actionTimeLabel.Location = new Point(165, 92);
        actionTimeLabel.Size = new Size(125, 20);
        actionTimeLabel.Text = "Дата действия МК";
        
        organizationLabel.Location = new Point(174, 118);
        organizationLabel.Size = new Size(116, 20);
        organizationLabel.Text = "Исполнитель МК";
        
        municipalityLabel.Location = new Point(150, 140);
        municipalityLabel.Size = new Size(130, 45);
        municipalityLabel.Text = "Муниципальное \r\nобразование";
        
        omsuLabel.Location = new Point(223, 181);
        omsuLabel.Size = new Size(55, 20);
        omsuLabel.Text = "ОМСУ";
        
        yearLabel.Location = new Point(138, 204);
        yearLabel.Size = new Size(145, 45);
        yearLabel.Text = "Год, на который \r\nвыдана субвенция";
        
        priceLabel.Location = new Point(160, 247);
        priceLabel.Size = new Size(120, 20);
        priceLabel.Text = "Цена контракта";
        
        subventionShareLabel.Location = new Point(153, 278);
        subventionShareLabel.Size = new Size(140, 45);
        subventionShareLabel.Text = "Доля субвенции \r\nв цене контракта";
        
        amountMoneyLabel.Location = new Point(80, 325);
        amountMoneyLabel.Size = new Size(220, 65);
        amountMoneyLabel.Text = "Объём денежных средств, \r\nвыплаченных Исполнителю \r\nпо контракту";
        
        partMoneyLabel.Location = new Point(80, 392);
        partMoneyLabel.Size = new Size(220, 65);
        partMoneyLabel.Text = "Доля денежных средств \r\nиз субвенции, выплаченной \r\nпо контракту в %\r\n\r\n";
        
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

        municipalityComboBox.Location = new Point(321, 160);
        municipalityComboBox.Size = new Size(200, 20);
        
        omsuBox.Location = new Point(321, 190);
        omsuBox.Size = new Size(200, 20);

        yearNumericUpDown.Location = new Point(321, 220);
        yearNumericUpDown.Size = new Size(200, 20);
        yearNumericUpDown.Maximum = 9999999;

        priceNumericUpDown.Location = new Point(321, 250);
        priceNumericUpDown.Size = new Size(200, 20);
        priceNumericUpDown.Maximum = 9999999;

        subventionShareNumericUpDown.Location = new Point(321, 280);
        subventionShareNumericUpDown.Size = new Size(200, 20);
        subventionShareNumericUpDown.Maximum = 9999999;

        amountMoneyNumericUpDown.Location = new Point(321, 325);
        amountMoneyNumericUpDown.Size = new Size(200, 20);
        amountMoneyNumericUpDown.Maximum = 9999999;

        shareFundsSubventionNumericUpDown.Location = new Point(321, 386);
        shareFundsSubventionNumericUpDown.Size = new Size(200, 20);
        shareFundsSubventionNumericUpDown.Maximum = 9999999;

        changeButton.Location = new Point(24, 460);
        changeButton.Size = new Size(125, 28);
        changeButton.Text = "Сохранить";
        changeButton.Click+= Add; 
           
        uploadFileButton.Location = new Point(149, 460);
        uploadFileButton.Size = new Size(150, 28);
        uploadFileButton.Text = "Загрузить файл";
        uploadFileButton.Click += UploadFile;
           
        deleteFileButton.Location = new Point(298, 460);
        deleteFileButton.Size = new Size(150, 28);
        deleteFileButton.Text = "Удалить файл";
        deleteFileButton.Click += DeleteFile;

    }
    public void GetShortRegisterMC(RegisterMCShort registerMcShort)
    {
        GetLongRegisterMC(registerMcShort.Id);
    }
    
    public void ClearRegisterMC(RegisterMCLong registerMcLong)
    {
        _registerMcLong = registerMcLong;
        validDatePicker.Value = DateTime.Today;
        actionTimePicker.Value = DateTime.Today;
        organizationComboBox.Text = "";
        municipalityComboBox.Text = "";
        FillInformation();
    }

    private void FillInformation()
    {
        numberBox.Text = _registerMcLong.Number;
        locationBox.Text = _registerMcLong.Location;
        omsuBox.Text = _registerMcLong.Omsu;
        yearNumericUpDown.Text = _registerMcLong.Year.ToString(); 
        priceNumericUpDown.Text = _registerMcLong.Price.ToString(); 
        subventionShareNumericUpDown.Text = _registerMcLong.SubventionShare.ToString(); 
        amountMoneyNumericUpDown.Text = _registerMcLong.AmountMoney.ToString();
        shareFundsSubventionNumericUpDown.Text = _registerMcLong.ShareFundsSubvention.ToString();
    }
    
    private void UploadFile(object e, object sender)
    {
        //_registermcController.UpLoadFile(_registerMcLong); почему метод аплоад принимает РегистерМц, а не лонг или шорт
    }
    
    private void DeleteFile(object e, object sender)
    {
        //_registermcController.D(_registerMcLong);  нет метода для удаления файла
    }
    
    private void GetLongRegisterMC(long id)
    {
        _registerMcLong = _registermcController.Read(id);
        validDatePicker.Value = _registerMcLong.ValidDate;
        actionTimePicker.Value = _registerMcLong.ActionDate;
        organizationComboBox.Text = _registerMcLong.Organization.ToString();
        municipalityComboBox.Text = _registerMcLong.Municipality.ToString();
        FillInformation();
    }

    private void Add(object e, object sender)
    {
        _registerMcLong.Number = numberBox.Text; 
        _registerMcLong.ValidDate = validDatePicker.Value;
        _registerMcLong.Location = locationBox.Text;
        _registerMcLong.ActionDate = actionTimePicker.Value;
        var organization = new Organization();
        organization.Name = organizationComboBox.Text;
        _registerMcLong.Organization = organization;
        var municipality = new Municipality();
        municipality.Name = municipalityComboBox.Text;
        _registerMcLong.Municipality = municipality;
        _registerMcLong.Omsu = omsuBox.Text;
        _registerMcLong.Year = Int32.Parse(yearNumericUpDown.Text);
        _registerMcLong.Price = double.Parse(priceNumericUpDown.Text);
        _registerMcLong.SubventionShare = Double.Parse(subventionShareNumericUpDown.Text);
        _registerMcLong.AmountMoney = Double.Parse(amountMoneyNumericUpDown.Text);
        _registerMcLong.ShareFundsSubvention = Double.Parse(shareFundsSubventionNumericUpDown.Text);
        _registermcController.Create(_registerMcLong);
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
    }
    
    #region компоненты для формы
    
    private Label numberLabel = new ();
    private Label validDateLabel = new ();
    private Label locationLabel = new ();
    private Label actionTimeLabel = new ();
    private Label organizationLabel = new ();
    private Label municipalityLabel = new ();
    private Label omsuLabel = new ();
    private Label yearLabel = new ();
    private Label priceLabel = new ();
    private Label subventionShareLabel = new ();
    private Label amountMoneyLabel = new ();
    private Label partMoneyLabel = new ();
    private TextBox numberBox = new ();
    private DateTimePicker validDatePicker = new ();
    private TextBox locationBox = new ();
    private DateTimePicker actionTimePicker = new();
    private ComboBox organizationComboBox = new ();
    private ComboBox municipalityComboBox = new ();
    private TextBox omsuBox = new ();
    private NumericUpDown yearNumericUpDown = new ();
    private NumericUpDown priceNumericUpDown = new ();
    private NumericUpDown subventionShareNumericUpDown = new ();
    private NumericUpDown amountMoneyNumericUpDown = new ();
    private NumericUpDown shareFundsSubventionNumericUpDown = new ();
    private Button changeButton = new ();
    private Button uploadFileButton = new ();
    private Button deleteFileButton = new ();

    #endregion
}
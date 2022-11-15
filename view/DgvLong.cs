using PISWF.domain.registermc.controller;
using PISWF.domain.registermc.model.view;

namespace PISWF.view;

public class DgvLong : Form
{
    private RegisterMCLong _registerMcLong;
    private RegistermcController _registermcController;
    
    public DgvLong(RegisterMCShort registerMcShort, RegistermcController registermcController)
    {
        _registerMcLong = registermcController.Read(registerMcShort.Id);
        _registermcController = registermcController;
        InitializeItems();
        InitializeFormForOpen();
        AddControls();
    }
    
    public DgvLong(RegisterMCLong registerMCLong, RegistermcController registermcController) //для создания новой
    {
        _registerMcLong = registerMCLong;
        _registermcController = registermcController;
        InitializeItems();
        InitializeFormForAdd();
        AddControls();
    }
    
    private void InitializeItems()
    {
        Size = new Size(590, 550);
        
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

        organizationBox.Location = new Point(321, 130);
        organizationBox.Size = new Size(200, 20);

        municipalityBox.Location = new Point(321, 160);
        municipalityBox.Size = new Size(200, 20);
        
        omsuBox.Location = new Point(321, 190);
        omsuBox.Size = new Size(200, 20);

        yearBox.Location = new Point(321, 220);
        yearBox.Size = new Size(200, 20);

        priceBox.Location = new Point(321, 250);
        priceBox.Size = new Size(200, 20);

        subventionShareBox.Location = new Point(321, 280);
        subventionShareBox.Size = new Size(200, 20);

        amountMoneyBox.Location = new Point(321, 325);
        amountMoneyBox.Size = new Size(200, 20);

        partMoneyBox.Location = new Point(321, 386);
        partMoneyBox.Size = new Size(200, 20);

        changeButton.Location = new Point(24, 460);
        changeButton.Size = new Size(125, 28);
        changeButton.Text = "Изменить";
        //changeButton.Click+= ; 
           
        uploadFileButton.Location = new Point(149, 460);
        uploadFileButton.Size = new Size(150, 28);
        uploadFileButton.Text = "Загрузить файл";
        //uploadFileButton.Click +=
           
        deleteFileButton.Location = new Point(298, 460);
        deleteFileButton.Size = new Size(150, 28);
        deleteFileButton.Text = "Удалить файл";
        //deleteFileButton.Click +=
        
        confirmButton.Location = new Point(400, 460);
        confirmButton.Size = new Size(150, 28);
        confirmButton.Text = "Добавить";
        confirmButton.Click += Add;
    }
    
    private void Add(object e, object sender)
    {
        _registerMcLong.Number = numberBox.Text; 
        //TODO запонить контроллер из текстбоксов
        _registermcController.Create(_registerMcLong);
    }
    
    private void InitializeFormForAdd()
    {
        changeButton.Hide();
        uploadFileButton.Hide();
        deleteFileButton.Hide();
    }
    
    private void InitializeFormForOpen()
    {
        confirmButton.Hide();
        numberBox.Text = _registerMcLong.Number;
        validDatePicker.Value = _registerMcLong.ValidDate;
        locationBox.Text = _registerMcLong.Location;
        actionTimePicker.Value = _registerMcLong.ActionDate;
        organizationBox.Text = _registerMcLong.Organization.ToString();
        municipalityBox.Text = _registerMcLong.Municipality.ToString();
        omsuBox.Text = _registerMcLong.Omsu;
        yearBox.Text = _registerMcLong.Year.ToString(); 
        priceBox.Text = _registerMcLong.Price.ToString(); 
        subventionShareBox.Text = _registerMcLong.SubventionShare.ToString(); 
        amountMoneyBox.Text = _registerMcLong.AmountMoney.ToString();
        partMoneyBox.Text = _registerMcLong.SubventionShare.ToString();
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
        Controls.Add(organizationBox);
        Controls.Add(municipalityBox);
        Controls.Add(omsuBox);
        Controls.Add(yearBox);
        Controls.Add(priceBox);
        Controls.Add(subventionShareBox);
        Controls.Add(amountMoneyBox);
        Controls.Add(partMoneyBox);
        Controls.Add(changeButton);
        Controls.Add(uploadFileButton);
        Controls.Add(deleteFileButton);
        Controls.Add(confirmButton);
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
    private TextBox organizationBox = new ();
    private TextBox municipalityBox = new ();
    private TextBox omsuBox = new ();
    private TextBox yearBox = new ();
    private TextBox priceBox = new ();
    private TextBox subventionShareBox = new ();
    private TextBox amountMoneyBox = new ();
    private TextBox partMoneyBox = new ();
    private Button changeButton = new ();
    private Button uploadFileButton = new ();
    private Button deleteFileButton = new ();
    private Button confirmButton = new ();

    #endregion
}
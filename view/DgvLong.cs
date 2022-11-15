using PISWF.domain.registermc.controller;
using PISWF.domain.registermc.model.view;

namespace PISWF.view;

public class DgvLong : Form
{
    // в названии поля short есть, в названии аргумента конструктора - нет
    private RegisterMCShort _registerMcShort;
    private RegistermcController _registermcController;
    // почему id - int? Если оно long.
    private int _id;
    public DgvLong(RegisterMCShort registerMc, RegistermcController registermcController)
    {
        // тогда и тут довнкастить не надо
        _id = (int)registerMc.Id;
        // особо нет смысла передавать и id и registerMcShort, а вот с помощью контроллера и id
        // (полученного из short-а или просто переданного в конструкторе) получить long версию - можно
        _registerMcShort = registerMc;
        _registermcController = registermcController;
        InitializeItems();
        AddControls();

    }
    
    private void InitializeItems()
    {
        Size = new Size(590, 550);
        
        // как будто-бы 'System.Drawing' - можн выпилить
        numberLabel.Location = new System.Drawing.Point(218, 10);
        numberLabel.Size = new System.Drawing.Size(72, 20);
        numberLabel.Text = "Номер МК";
        
        validDateLabel.Location = new System.Drawing.Point(146, 39);
        validDateLabel.Size = new System.Drawing.Size(144, 20);
        validDateLabel.Text = "Дата заключения МК";
        
        locationLabel.Location = new System.Drawing.Point(105, 65);
        locationLabel.Size = new System.Drawing.Size(170, 20);
        locationLabel.Text = "Место оказания услуги";
        
        actionTimeLabel.Location = new System.Drawing.Point(165, 92);
        actionTimeLabel.Size = new System.Drawing.Size(125, 20);
        actionTimeLabel.Text = "Дата действия МК";
        
        organizationLabel.Location = new System.Drawing.Point(174, 118);
        organizationLabel.Size = new System.Drawing.Size(116, 20);
        organizationLabel.Text = "Исполнитель МК";
        
        municipalityLabel.Location = new System.Drawing.Point(150, 140);
        municipalityLabel.Size = new System.Drawing.Size(130, 45);
        municipalityLabel.Text = "Муниципальное \r\nобразование";
        
        omsuLabel.Location = new System.Drawing.Point(223, 181);
        omsuLabel.Size = new System.Drawing.Size(55, 20);
        omsuLabel.Text = "ОМСУ";
        
        yearLabel.Location = new System.Drawing.Point(138, 204);
        yearLabel.Size = new System.Drawing.Size(145, 45);
        yearLabel.Text = "Год, на который \r\nвыдана субвенция";
        
        priceLabel.Location = new System.Drawing.Point(160, 247);
        priceLabel.Size = new System.Drawing.Size(120, 20);
        priceLabel.Text = "Цена контракта";
        
        partLabel.Location = new System.Drawing.Point(153, 278);
        partLabel.Size = new System.Drawing.Size(140, 45);
        partLabel.Text = "Доля субвенции \r\nв цене контракта";
        
        amountMoneyLabel.Location = new System.Drawing.Point(80, 325);
        amountMoneyLabel.Size = new System.Drawing.Size(220, 65);
        amountMoneyLabel.Text = "Объём денежных средств, \r\nвыплаченных Исполнителю \r\nпо контракту";
        
        partMoneyLabel.Location = new System.Drawing.Point(80, 392);
        partMoneyLabel.Size = new System.Drawing.Size(220, 65);
        partMoneyLabel.Text = "Доля денежных средств \r\nиз субвенции, выплаченной \r\nпо контракту в %\r\n\r\n";
        
        numberBox.Location = new System.Drawing.Point(321, 10);
        numberBox.Size = new System.Drawing.Size(200, 20);
        numberBox.Text = _registerMcShort.Number.ToString();

        validDatePicker.Location = new System.Drawing.Point(321, 40);
        validDatePicker.Size = new System.Drawing.Size(200, 20);
        validDatePicker.Value = _registerMcShort.ValidDate;
        
        locationBox.Location = new System.Drawing.Point(321, 70);
        locationBox.Size = new System.Drawing.Size(200, 20);
        //locationBox.Text = _registermcController.Read(_id). нет 
        
        actionTimePicker.Location = new System.Drawing.Point(321, 100);
        actionTimePicker.Size = new System.Drawing.Size(200, 20);
        //actionTimePicker.Text = _registermcController.Read(_id). нет 
        
        organizationBox.Location = new System.Drawing.Point(321, 130);
        organizationBox.Size = new System.Drawing.Size(200, 20);
        organizationBox.Text = _registermcController.Read(_id).Organization.ToString();
        
        municipalityBox.Location = new System.Drawing.Point(321, 160);
        municipalityBox.Size = new System.Drawing.Size(200, 20);
        municipalityBox.Text = _registermcController.Read(_id).Municipality.ToString();
        
        omsuBox.Location = new System.Drawing.Point(321, 190);
        omsuBox.Size = new System.Drawing.Size(200, 20);
        // omsuBox.Text = _registermcController.Read(_id)..ToString(); net 
       
        yearBox.Location = new System.Drawing.Point(321, 220);
        yearBox.Size = new System.Drawing.Size(200, 20);
        yearBox.Text = _registermcController.Read(_id).Year.ToString(); 

        priceBox.Location = new System.Drawing.Point(321, 250);
        priceBox.Size = new System.Drawing.Size(200, 20);
        priceBox.Text = _registermcController.Read(_id).Price.ToString(); 
        
        partBox.Location = new System.Drawing.Point(321, 280);
        partBox.Size = new System.Drawing.Size(200, 20);
        //partBox.Text = _registermcController.Read(_id).Price.ToString(); 
       
        amountMoneyBox.Location = new System.Drawing.Point(321, 325);
        amountMoneyBox.Size = new System.Drawing.Size(200, 20);
        amountMoneyBox.Text = _registermcController.Read(_id).AmountMoney.ToString();
        
        partMoneyBox.Location = new System.Drawing.Point(321, 386);
        partMoneyBox.Size = new System.Drawing.Size(200, 20);
        partMoneyBox.Text = _registermcController.Read(_id).SubventionShare.ToString(); 
           
        changeButton.Location = new System.Drawing.Point(24, 460);
        changeButton.Size = new System.Drawing.Size(125, 28);
        changeButton.Text = "Изменить";
        //changeButton.Click+= ; 
           
        uploadFileButton.Location = new System.Drawing.Point(149, 460);
        uploadFileButton.Size = new System.Drawing.Size(150, 28);
        uploadFileButton.Text = "Загрузить файл";
        //uploadFileButton.Click +=
           
        deleteFileButton.Location = new System.Drawing.Point(298, 460);
        deleteFileButton.Size = new System.Drawing.Size(150, 28);
        deleteFileButton.Text = "Удалить файл";
        //deleteFileButton.Click +=
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
        Controls.Add(partLabel);
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
        Controls.Add(partBox);
        Controls.Add(amountMoneyBox);
        Controls.Add(partMoneyBox);
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
    private Label partLabel = new ();
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
    private TextBox partBox = new ();
    private TextBox amountMoneyBox = new ();
    private TextBox partMoneyBox = new ();
    private Button changeButton = new ();
    private Button uploadFileButton = new ();
    private Button deleteFileButton = new ();
    
    #endregion
}
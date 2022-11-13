using PISWF.domain.registermc.controller;

namespace PISWF.view;

public class DgvLong : Form
{
    private int _id;
    private RegistermcController _registermcController;
    public DgvLong(int id, RegistermcController registermcController)
    {
        _registermcController = registermcController;
        _id = id;
        InitializeItems();
        AddControls();

    }
    
    private void InitializeItems()
    {
        Size = new Size(590, 550);
        
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
        
        locationBox.Location = new System.Drawing.Point(129, 65);
        locationBox.Size = new System.Drawing.Size(161, 20);
        //locationBox.Text = _registermcController.Read(_id). нет места заключения договора в регистерМЦ??????
        
        numberBox.Location = new System.Drawing.Point(318, 10);
        numberBox.Size = new System.Drawing.Size(200, 20);
        numberBox.Text = _registermcController.Read(_id).Number.ToString();
        
        validDatePicker.Location = new System.Drawing.Point(321, 40);
        validDatePicker.Size = new System.Drawing.Size(200, 20);
        validDatePicker.Value = _registermcController.Read(_id).ValidDate;
        
        locationBox.Location = new System.Drawing.Point(321, 62);
        locationBox.Size = new System.Drawing.Size(200, 22);
        //locationBox.Text = _registermcController.Read(_id). нет места заключения договора в регистерМЦ??????

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

    #endregion
}
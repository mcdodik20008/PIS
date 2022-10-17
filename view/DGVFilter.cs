using DGVWF;
using pis.infrasrtucture.dgvf;
using PISWF.infrasrtucture;
using PISWF.infrasrtucture.auth.controller;
using PISWF.infrasrtucture.auth.model.view;
using PISWF.infrasrtucture.page;

namespace PISWF.view;

public class DGVFilter : Form
{
    private AuthController AuthController { get; }

    private Page page = new(0, 25);

    public DGVFilter(AuthController authController)
    {
        AuthController = authController;
        InitializeItems();
        AddControls();
        DG.FillDataGrid(AuthController.ReadUser(page));
    }

    //Каждый метод обарачиваем в трукачте и при неудачи выкидываем пользователю масаге боксом или чем, то что не будет бесить.
    private void createUser(object e, object sender)
    {
        AuthController.AddUser(new UserBasic(nameBox.Text, passwordBox.Text));
        DG.FillDataGrid(AuthController.ReadUser(page));
    }

    private void InitializeItems()
    {
        nameBox.Location = new Point(650, 100);
        passwordBox.Location = new Point(650, 150);
        CreateUserButton.Location = new Point(650, 200);
        CreateUserButton.Click += createUser;
        Size = new Size(900, 900);
        DG.Bounds = new Rectangle(0, 0, 600, 600);
        DG.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
        DG.AllowUserToAddRows = false;
    }

    private void AddControls()
    {
        Controls.Add(DG);
        Controls.Add(nameBox);
        Controls.Add(passwordBox);
        Controls.Add(CreateUserButton);
    }

    #region компоненты для формы

    private DataGridViewWithFilter DG = new DataGridViewWithFilter();
    private TextBox nameBox = new TextBox();
    private TextBox passwordBox = new TextBox();
    private Button CreateUserButton = new Button();

    #endregion
}
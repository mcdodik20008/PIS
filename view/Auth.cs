using PISWF.infrasrtucture.auth.controller;
using PISWF.infrasrtucture.auth.model.view;


namespace PISWF.view;

public class Auth: Form
{
    private AuthController _authController;

    private DGVs _dgVs;
    
    public Auth(AuthController authController, DGVs dgVs)
    {
        StartPosition = FormStartPosition.CenterScreen;
        _dgVs = dgVs;
        _authController = authController;
        InitializeItems();
        AddControls();
        //для тестов
        loginBox.Text = "admin";
        passwordBox.Text = "1234";
    }
    
    private void Authorization(object e, object sender)
    {
        if (loginBox.Text.Length > 0 && passwordBox.Text.Length > 0)
            {
                var userAuth = new UserAuth(loginBox.Text, passwordBox.Text);
                _authController.Authorization(userAuth);
            }

            if (!_authController.AutorizedUser.Login.Equals("guest"))
            {
                _dgVs.ShowDialog();
            }
    }
    
    private void InitializeItems()
    {
        Size = new Size(480, 310);
        Text = "Авторизация";
        
        loginBox.Location = new Point(105, 56);
        loginBox.Size = new Size(294, 27);
        
        passwordBox.Location = new Point(105, 87);
        passwordBox.Size = new Size(294, 27);
        
        authorizationButton.Location = new Point(260, 120);
        authorizationButton.Size = new Size(139, 34);
        authorizationButton.Text = "Вход";
        authorizationButton.Click += Authorization;
        
        loginLabel.Location = new Point(36, 56);
        loginLabel.Size = new Size(53, 20);
        loginLabel.Text = "Login";
        
        passwordLabel.Location = new Point(14, 87);
        passwordLabel.Size = new Size(75, 20);
        passwordLabel.Text = "Password";
    }
    
    private void AddControls()
    {
        Controls.Add(loginBox);
        Controls.Add(loginLabel);
        Controls.Add(passwordLabel);
        Controls.Add(passwordBox);
        Controls.Add(authorizationButton);
    }
    
    #region компоненты для формы
    
    private TextBox loginBox = new();
    private TextBox passwordBox = new();
    private Button authorizationButton = new();
    private Label loginLabel = new ();
    private Label passwordLabel = new ();

    #endregion
}
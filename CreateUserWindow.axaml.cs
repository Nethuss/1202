using Avalonia.Controls;
using Success.Services;

namespace Success;

public partial class CreateUserWindow : Window
{
    private readonly UserService _users;

    private readonly MainWindow _mainWindow;

    public CreateUserWindow(UserService userService, MainWindow mainWindow)
    {
        InitializeComponent();
        _users = userService;
         _mainWindow = mainWindow;
    }

    private void Create_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var name = NameBox.Text ?? "";
        if (name.Length == 0) return;

         _users.CreateUser(name);

        _mainWindow.ReloadUsers();

        Close();
    }
}
using Avalonia.Controls;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Success;

public partial class LoginWindow : Window
{
    private string _captcha = "";
    private int _captchaFails = 0;
    private DateTime _lockUntil = DateTime.MinValue;

    public LoginWindow()
    {
        InitializeComponent();
        GenerateCaptcha();
    }

    private void GenerateCaptcha()
    {
        var rnd = new Random();
        const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";

        _captcha = new string(Enumerable.Range(0, 5)
            .Select(_ => chars[rnd.Next(chars.Length)])
            .ToArray());

        CaptchaText.Text = _captcha;
        CaptchaBox.Text = "";
        ErrorText.Text = "";
    }

    private void RefreshCaptcha_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        GenerateCaptcha();
    }

    private async void Login_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        // Проверка блокировки
        if (DateTime.Now < _lockUntil)
        {
            var secLeft = (int)(_lockUntil - DateTime.Now).TotalSeconds;
            ErrorText.Text = $"Слишком много ошибок. Подождите {secLeft} секунд.";
            return;
        }

        var login = LoginBox.Text;
        var password = PasswordBox.Text;
        var captcha = CaptchaBox.Text;

        if (captcha != _captcha)
        {
            _captchaFails++;

            if (_captchaFails >= 3)
            {
                // блокируем на 30 секунд
                _lockUntil = DateTime.Now.AddSeconds(10);

                ErrorText.Text = "Вы ввели капчу неверно 3 раза. Вход заблокирован на 30 секунд.";
                _captchaFails = 0;

                // делаем кнопку неактивной
                ((Button)sender).IsEnabled = false;

                // таймер восстановления
                await Task.Delay(10000);

                ((Button)sender).IsEnabled = true;
                ErrorText.Text = "";
                GenerateCaptcha();
                return;
            }

            ErrorText.Text = "Капча введена неверно!";
            GenerateCaptcha();
            return;
        }

        // капча верна — сбрасываем счётчик
        _captchaFails = 0;

        // проверка логина/пароля
        if (login == "admin" && password == "1234")
        {
            var main = new MainWindow();
            main.Show();
            this.Close();
        }
        else
        {
            ErrorText.Text = "Неверный логин или пароль!";
        }
    }
}

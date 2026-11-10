using System.Collections.Generic;

namespace GUgunaExxamen.Pages;

public partial class LoginPage : ContentPage
{
    private readonly string[,] credenciales =
    {
        { "estudiante", "moviles" },
        { "suda", "2025" }
    };

    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnIniciarSesionClicked(object? sender, EventArgs e)
    {
        var usuario = UsuarioEntry.Text?.Trim() ?? string.Empty;
        var contrasena = ContrasenaEntry.Text ?? string.Empty;

        for (var fila = 0; fila < credenciales.GetLength(0); fila++)
        {
            if (usuario.Equals(credenciales[fila, 0], StringComparison.OrdinalIgnoreCase) &&
                contrasena == credenciales[fila, 1])
            {
                await Shell.Current.GoToAsync(nameof(RegistroPage), new Dictionary<string, object>
                {
                    ["usuario"] = credenciales[fila, 0]
                });
                return;
            }
        }

        await DisplayAlertAsync("Acceso no válido", "El usuario o la contraseña son incorrectos.", "Aceptar");
    }
}

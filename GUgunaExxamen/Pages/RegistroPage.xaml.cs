using System.Collections.ObjectModel;
using System.Globalization;
using GUgunaExxamen.Models;

namespace GUgunaExxamen.Pages;

public partial class RegistroPage : ContentPage, IQueryAttributable
{
    private const decimal CostoCurso = 1500m;
    private const decimal PorcentajeAdicional = 0.04m;
    private string usuario = string.Empty;

    public ObservableCollection<string> Paises { get; } = new() { "Ecuador", "Colombia", "Perú", "México" };
    public ObservableCollection<string> Ciudades { get; } = new();

    public RegistroPage()
    {
        InitializeComponent();
        BindingContext = this;
        FechaPicker.Date = DateTime.Today;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("usuario", out var valor) && valor is string nombreUsuario)
        {
            usuario = nombreUsuario;
            UsuarioLabel.Text = $"Usuario conectado: {usuario}";
        }
    }

    private void OnPaisChanged(object? sender, EventArgs e)
    {
        Ciudades.Clear();
        var ciudades = PaisPicker.SelectedItem?.ToString() switch
        {
            "Ecuador" => new[] { "Quito", "Guayaquil", "Cuenca" },
            "Colombia" => new[] { "Bogotá", "Medellín", "Cali" },
            "Perú" => new[] { "Lima", "Arequipa", "Cusco" },
            "México" => new[] { "Ciudad de México", "Guadalajara", "Monterrey" },
            _ => Array.Empty<string>()
        };

        foreach (var ciudad in ciudades)
        {
            Ciudades.Add(ciudad);
        }
    }

    private void OnMontoInicialChanged(object? sender, TextChangedEventArgs e)
    {
        if (TryParseDecimal(e.NewTextValue, out var monto) && monto >= 0 && monto <= CostoCurso)
        {
            PagoMensualEntry.Text = CalcularPagoMensual(monto).ToString("C2", CultureInfo.CurrentCulture);
        }
        else
        {
            PagoMensualEntry.Text = string.Empty;
        }
    }

    private async void OnVerResumenClicked(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NombreEntry.Text) || string.IsNullOrWhiteSpace(ApellidoEntry.Text))
        {
            await DisplayAlertAsync("Datos incompletos", "Ingresa el nombre y el apellido.", "Aceptar");
            return;
        }

        if (!int.TryParse(EdadEntry.Text, out var edad) || edad < 1 || edad > 120)
        {
            await DisplayAlertAsync("Edad no válida", "Ingresa una edad entre 1 y 120 años.", "Aceptar");
            return;
        }

        if (PaisPicker.SelectedItem is not string pais || CiudadPicker.SelectedItem is not string ciudad)
        {
            await DisplayAlertAsync("Ubicación incompleta", "Selecciona un país y una ciudad.", "Aceptar");
            return;
        }

        if (!TryParseDecimal(MontoInicialEntry.Text, out var montoInicial) || montoInicial < 0 || montoInicial > CostoCurso)
        {
            await DisplayAlertAsync("Monto no válido", "El monto inicial debe ser numérico, no negativo y no superar $1500.", "Aceptar");
            return;
        }

        var pagoMensual = CalcularPagoMensual(montoInicial);
        var estudiante = new Student
        {
            Nombre = NombreEntry.Text.Trim(),
            Apellido = ApellidoEntry.Text.Trim(),
            Edad = edad,
            Fecha = FechaPicker.Date ?? DateTime.Today,
            Pais = pais,
            Ciudad = ciudad,
            MontoInicial = montoInicial,
            PagoMensual = pagoMensual,
            PagoTotal = montoInicial + pagoMensual * 4
        };

        await Shell.Current.GoToAsync(nameof(ResumenPage), new Dictionary<string, object>
        {
            ["usuario"] = usuario,
            ["student"] = estudiante
        });
    }

    private static decimal CalcularPagoMensual(decimal montoInicial) =>
        ((CostoCurso - montoInicial) / 4) + (CostoCurso * PorcentajeAdicional);

    private static bool TryParseDecimal(string? texto, out decimal valor)
    {
        return decimal.TryParse(texto, NumberStyles.Number, CultureInfo.CurrentCulture, out valor) ||
               decimal.TryParse(texto, NumberStyles.Number, CultureInfo.InvariantCulture, out valor);
    }
}

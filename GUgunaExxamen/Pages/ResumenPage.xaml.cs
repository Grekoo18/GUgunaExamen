using System.Globalization;
using GUgunaExxamen.Models;

namespace GUgunaExxamen.Pages;

public partial class ResumenPage : ContentPage, IQueryAttributable
{
    public ResumenPage()
    {
        InitializeComponent();
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("usuario", out var usuario) && usuario is string nombreUsuario)
        {
            UsuarioLabel.Text = $"Usuario conectado: {nombreUsuario}";
        }

        if (query.TryGetValue("student", out var valor) && valor is Student estudiante)
        {
            NombreLabel.Text = estudiante.Nombre;
            ApellidoLabel.Text = estudiante.Apellido;
            EdadLabel.Text = estudiante.Edad.ToString(CultureInfo.CurrentCulture);
            FechaLabel.Text = estudiante.Fecha.ToString("dd/MM/yyyy", CultureInfo.CurrentCulture);
            CiudadLabel.Text = estudiante.Ciudad;
            PaisLabel.Text = estudiante.Pais;
            MontoInicialLabel.Text = estudiante.MontoInicial.ToString("C2", CultureInfo.CurrentCulture);
            PagoMensualLabel.Text = estudiante.PagoMensual.ToString("C2", CultureInfo.CurrentCulture);
            PagoTotalLabel.Text = estudiante.PagoTotal.ToString("C2", CultureInfo.CurrentCulture);
        }
    }
}

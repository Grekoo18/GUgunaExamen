
namespace GUgunaExxamen.Models;

public class Student
{
    public string Nombre { get; set; } = "";
    public string Apellido { get; set; } = "";
    public int Edad { get; set; }
    public DateTime Fecha { get; set; }
    public string Pais { get; set; } = "";
    public string Ciudad { get; set; } = "";
    public decimal MontoInicial { get; set; }
    public decimal PagoMensual { get; set; }
    public decimal PagoTotal { get; set; }
}

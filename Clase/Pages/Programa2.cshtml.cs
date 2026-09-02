using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Clase.Pages
{
    [BindProperties]
    public class Programa2Model : PageModel
    {
        #region Entidad
        public int TipoServicio { get; set; }
        public int TipoEquipo { get; set; }
        public int AntiguedadEquipo { get; set; }
        public int NumeroEquipos { get; set; }
        public int GarantiaExtendida { get; set; }
        public int IVA { get; set; } = 16;
        public int PrioridadServicio { get; set; }

        public string? ResultadoOK { get; set; }
        public string? ResultadoNoOK { get; set; }
        #endregion

        public void OnGet()
        {
        }

        public void OnPostCalcular()
        {
            try
            {
                List<string> lstErrores = new List<string>();

                if (TipoServicio == 0)
                    lstErrores.Add("Falta seleccionar un tipo de servicio.");
                if (TipoEquipo == 0)
                    lstErrores.Add("Falta seleccionar un tipo de equipo.");
                if (AntiguedadEquipo < 1 || AntiguedadEquipo > 20)
                    lstErrores.Add("Falta ingresar una antigüedad válida entre 1 y 20 años.");
                if (NumeroEquipos < 1 || NumeroEquipos > 50)
                    lstErrores.Add("Falta ingresar un número de equipos entre 1 y 50.");
                if (GarantiaExtendida == 0)
                    lstErrores.Add("Falta seleccionar una opción de garantía extendida.");
                if (PrioridadServicio == 0)
                    lstErrores.Add("Falta seleccionar la prioridad del servicio.");

                if (lstErrores.Count == 0)
                {
                    // Precios base por servicio
                    decimal precioServicio = TipoServicio switch
                    {
                        1 => 350m, // Limpieza interna
                        2 => 600m, // Mantenimiento preventivo
                        3 => 450m, // Cambio de pasta térmica
                        4 => 800m, // Formateo e instalación
                        _ => 0m
                    };

                    // Cargos adicionales por equipo
                    decimal cargoEquipo = TipoEquipo switch
                    {
                        1 => 0m,   // Laptop
                        2 => 100m, // PC de escritorio
                        3 => 150m, // All-in-One
                        4 => 300m, // Mac
                        _ => 0m
                    };

                    // Cargo por garantía extendida
                    decimal cargoGarantia = GarantiaExtendida switch
                    {
                        1 => 0m,   // 30 días
                        2 => 150m, // 90 días
                        3 => 300m, // 180 días
                        _ => 0m
                    };

                    // Cargo por prioridad
                    decimal cargoPrioridad = PrioridadServicio switch
                    {
                        1 => 0m,   // Normal
                        2 => 200m, // Urgente
                        3 => 400m, // Express
                        _ => 0m
                    };

                    // Cálculo del total
                    decimal costoUnitario = precioServicio + cargoEquipo + cargoGarantia + cargoPrioridad;
                    decimal subtotal = costoUnitario * NumeroEquipos;
                    decimal montoIVA = subtotal * (IVA / 100m);
                    decimal total = subtotal + montoIVA;

                    ResultadoOK = $"<b>Cálculo exitoso:</b><br>" +
                                 $"Subtotal: ${subtotal:N2} MXN<br>" +
                                 $"IVA (16%): ${montoIVA:N2} MXN<br>" +
                                 $"<b>Total a pagar: ${total:N2} MXN</b>";
                }
                else
                {
                    ResultadoNoOK = string.Join("<br>", lstErrores);
                }
            }
            catch (Exception e)
            {
                ResultadoNoOK = "Ocurrió un error no esperado: " + e.Message;
            }
        }

        public void OnPostLimpiar()
        {
            ModelState.Clear();
            TipoServicio = 0;
            TipoEquipo = 0;
            AntiguedadEquipo = 0;
            NumeroEquipos = 0;
            GarantiaExtendida = 0;
            PrioridadServicio = 0;
            ResultadoOK = string.Empty;
            ResultadoNoOK = string.Empty;
        }
    }
}


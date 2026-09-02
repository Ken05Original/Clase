using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Clase.Pages
{
    [BindProperties]
    public class Programa3Model : PageModel
    {
        #region Entidad
        public int TipoHabitacion { get; set; }
        public int Noches { get; set; }
        public int TipoHuesped { get; set; }
        public int Huespedes { get; set; }

        // Checkboxes para Servicios Adicionales
        public bool ChkDesayuno { get; set; }
        public bool ChkEstacionamiento { get; set; }
        public bool ChkWifi { get; set; }
        public bool ChkSpa { get; set; }

        public int IVA { get; set; } = 16;
        public string DescuentoTexto { get; set; } = "Según tipo de huésped";

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

                if (TipoHabitacion == 0)
                    lstErrores.Add("Falta seleccionar un tipo de habitación.");
                if (Noches < 1 || Noches > 30)
                    lstErrores.Add("Falta ingresar un número de noches entre 1 y 30.");
                if (TipoHuesped == 0)
                    lstErrores.Add("Falta seleccionar un tipo de huésped.");
                if (Huespedes < 1 || Huespedes > 10)
                    lstErrores.Add("Falta ingresar el número de huéspedes (mínimo 1).");

                if (lstErrores.Count == 0)
                {
                    // Tarifa base por noche según tipo de habitación
                    decimal tarifaNoche = TipoHabitacion switch
                    {
                        1 => 800m,  // Sencilla
                        2 => 1200m, // Doble
                        3 => 1800m, // Suite
                        4 => 2500m, // Master Suite
                        _ => 0m
                    };

                    // Porcentaje de descuento según tipo de huésped
                    decimal porcentajeDescuento = TipoHuesped switch
                    {
                        1 => 0.00m, // Regular (0%)
                        2 => 0.10m, // Frecuente (10%)
                        3 => 0.15m, // VIP / INAPAM (15%)
                        _ => 0.00m
                    };

                    // Costo base hospedaje
                    decimal subtotalHabitacion = tarifaNoche * Noches;

                    // Costo servicios adicionales
                    decimal costoDesayuno = ChkDesayuno ? (150m * Huespedes * Noches) : 0m;
                    decimal costoEstacionamiento = ChkEstacionamiento ? (100m * Noches) : 0m;
                    decimal costoWifi = ChkWifi ? (80m * Noches) : 0m;
                    decimal costoSpa = ChkSpa ? (250m * Noches) : 0m;

                    decimal totalServicios = costoDesayuno + costoEstacionamiento + costoWifi + costoSpa;

                    // Cálculos
                    decimal subtotalBruto = subtotalHabitacion + totalServicios;
                    decimal montoDescuento = subtotalBruto * porcentajeDescuento;
                    decimal subtotalConDescuento = subtotalBruto - montoDescuento;
                    decimal montoIVA = subtotalConDescuento * (IVA / 100m);
                    decimal totalPagar = subtotalConDescuento + montoIVA;

                    ResultadoOK = $"<b>Reservación Procesada:</b><br>" +
                                 $"Subtotal hospedaje y servicios: ${subtotalBruto:N2} MXN<br>" +
                                 $"Descuento aplicado: -${montoDescuento:N2} MXN<br>" +
                                 $"IVA ({IVA}%): ${montoIVA:N2} MXN<br>" +
                                 $"<b>Total a pagar: ${totalPagar:N2} MXN</b>";
                }
                else
                {
                    ResultadoNoOK = string.Join("<br>", lstErrores);
                }
            }
            catch (Exception e)
            {
                ResultadoNoOK = "Ocurrió un error inesperado: " + e.Message;
            }
        }

        public void OnPostLimpiar()
        {
            ModelState.Clear();
            TipoHabitacion = 0;
            Noches = 0;
            TipoHuesped = 0;
            Huespedes = 0;
            ChkDesayuno = false;
            ChkEstacionamiento = false;
            ChkWifi = false;
            ChkSpa = false;
            ResultadoOK = string.Empty;
            ResultadoNoOK = string.Empty;
        }
    }
}
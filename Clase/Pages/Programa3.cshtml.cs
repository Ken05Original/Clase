using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public bool ChkDesayuno { get; set; }
        public bool ChkEstacionamiento { get; set; }
        public bool ChkWifi { get; set; }
        public bool ChkSpa { get; set; }

        public int IVA { get; set; } = 16;

        public string? DescuentoTexto { get; set; }

        public string? ResultadoOK { get; set; }
        public string? ResultadoNoOK { get; set; }

        public List<SelectListItem> ddlTipoHabitacion { get; set; } = new();

        #endregion


        // Cargar habitaciones
        public void CargarDropDownList()
        {
            ddlTipoHabitacion = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = "0",
                    Text = "Seleccione"
                },

                new SelectListItem
                {
                    Value = "1",
                    Text = "Sencilla ($800/noche)"
                },

                new SelectListItem
                {
                    Value = "2",
                    Text = "Doble ($1,200/noche)"
                },

                new SelectListItem
                {
                    Value = "3",
                    Text = "Suite ($2,000/noche)"
                }
            };
        }


        // Se ejecuta al abrir la página
        public void OnGet()
        {
            CargarDropDownList();

            IVA = 16;
            DescuentoTexto = "0%";
        }


        // Acción del botón Calcular
        public void OnPostCalcular()
        {
            try
            {
                // Volver a cargar el DropDownList
                CargarDropDownList();

                // Lista para guardar errores
                List<string> lstErrores = new List<string>();


                // Validación de datos

                if (TipoHabitacion == 0)
                    lstErrores.Add("Falta seleccionar el tipo de habitación.");

                if (TipoHuesped == 0)
                    lstErrores.Add("Falta seleccionar el tipo de huésped.");

                if (Noches <= 0)
                    lstErrores.Add("El número de noches debe ser mayor a 0.");

                if (Huespedes <= 0)
                    lstErrores.Add("El número de huéspedes debe ser mayor a 0.");


                // Validar capacidad de la habitación
                int capacidad_habitacion = TipoHabitacion switch
                {
                    1 => 2,   // Sencilla
                    2 => 4,   // Doble
                    3 => 6,   // Suite
                    _ => 0
                };


                if (Huespedes > capacidad_habitacion && TipoHabitacion != 0)
                    lstErrores.Add(
                        "El número de huéspedes supera la capacidad de la habitación."
                    );


                // Validar que no haya errores
                if (lstErrores.Count == 0)
                {
                    // Precio de la habitación
                    decimal costo_habitacion = TipoHabitacion switch
                    {
                        1 => 800m,    // Sencilla
                        2 => 1200m,   // Doble
                        3 => 2000m,   // Suite
                        _ => 0m
                    };


                    // Costo de la habitación por las noches
                    decimal costo_habitaciones =
                        costo_habitacion * Noches;


                    // Costo de servicios adicionales
                    decimal costo_desayuno = 0m;
                    decimal costo_estacionamiento = 0m;
                    decimal costo_wifi = 0m;
                    decimal costo_spa = 0m;


                    // Desayuno:
                    // $150 por huésped por noche
                    if (ChkDesayuno)
                    {
                        costo_desayuno =
                            150m * Huespedes * Noches;
                    }


                    // Estacionamiento:
                    // $100 por noche
                    if (ChkEstacionamiento)
                    {
                        costo_estacionamiento =
                            100m * Noches;
                    }


                    // WiFi:
                    // $80 por noche
                    if (ChkWifi)
                    {
                        costo_wifi =
                            80m * Noches;
                    }


                    // Spa:
                    // $250 por noche
                    if (ChkSpa)
                    {
                        costo_spa =
                            250m * Noches;
                    }


                    // Total de servicios adicionales
                    decimal costo_servicios =
                        costo_desayuno +
                        costo_estacionamiento +
                        costo_wifi +
                        costo_spa;


                    // Subtotal
                    decimal subtotal =
                        costo_habitaciones +
                        costo_servicios;


                    // Porcentaje de descuento según huésped
                    decimal porcentaje_descuento = TipoHuesped switch
                    {
                        1 => 0m,      // General
                        2 => 0.10m,   // Frecuente
                        3 => 0.20m,   // VIP
                        _ => 0m
                    };


                    // Texto que aparecerá en el campo Descuento
                    DescuentoTexto =
                        (porcentaje_descuento * 100).ToString("0") + "%";


                    // Monto del descuento
                    decimal monto_descuento =
                        subtotal * porcentaje_descuento;


                    // Subtotal después del descuento
                    decimal subtotal_descuento =
                        subtotal - monto_descuento;


                    // IVA
                    decimal monto_iva =
                        subtotal_descuento * 0.16m;


                    // Total final
                    decimal total_pagar =
                        subtotal_descuento + monto_iva;


                    // Mostrar resultados
                    ResultadoOK =
                        "<div class='table-responsive'>" +

                        "<table class='table'>" +

                        "<tr>" +

                        "<td>Costo de habitaciones</td>" +
                        "<td>Costo de servicios adicionales</td>" +
                        "<td>Subtotal</td>" +
                        "<td>Descuento</td>" +
                        "<td>Monto descuento</td>" +
                        "<td>Subtotal con descuento</td>" +
                        "<td>IVA</td>" +
                        "<td>Total a pagar</td>" +

                        "</tr>" +

                        "<tr>" +

                        "<td>$" +
                        costo_habitaciones.ToString("N2") +
                        "</td>" +

                        "<td>$" +
                        costo_servicios.ToString("N2") +
                        "</td>" +

                        "<td>$" +
                        subtotal.ToString("N2") +
                        "</td>" +

                        "<td>" +
                        DescuentoTexto +
                        "</td>" +

                        "<td>$" +
                        monto_descuento.ToString("N2") +
                        "</td>" +

                        "<td>$" +
                        subtotal_descuento.ToString("N2") +
                        "</td>" +

                        "<td>$" +
                        monto_iva.ToString("N2") +
                        "</td>" +

                        "<td>$" +
                        total_pagar.ToString("N2") +
                        "</td>" +

                        "</tr>" +

                        "</table>" +

                        "</div>";
                }
                else
                {
                    ResultadoNoOK =
                        string.Join("<br>", lstErrores);
                }
            }
            catch (Exception e)
            {
                ResultadoNoOK =
                    "Esto es un error, no sé por qué pasó... " +
                    e.Message;
            }
        }


        // Acción del botón Limpiar
        public void OnPostLimpiar()
        {
            ModelState.Clear();

            CargarDropDownList();

            TipoHabitacion = 0;
            Noches = 0;
            TipoHuesped = 0;
            Huespedes = 0;

            ChkDesayuno = false;
            ChkEstacionamiento = false;
            ChkWifi = false;
            ChkSpa = false;

            IVA = 16;

            DescuentoTexto = "0%";

            ResultadoOK = string.Empty;
            ResultadoNoOK = string.Empty;
        }
    }
}
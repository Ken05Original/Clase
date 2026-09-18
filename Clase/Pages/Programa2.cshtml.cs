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
            // Automáticamente se ejecuta al abrir la página
        }


        // Acción del botón Calcular
        public void OnPostCalcular()
        {
            try
            {
                // Lista para guardar los errores de validación
                List<string> lstErrores = new List<string>();


                // Validación de entrada de datos

                if (TipoServicio == 0)
                    lstErrores.Add("Falta seleccionar un tipo de servicio.");

                if (TipoEquipo == 0)
                    lstErrores.Add("Falta seleccionar un tipo de equipo.");

                if (AntiguedadEquipo < 0 || AntiguedadEquipo > 30)
                    lstErrores.Add("Falta ingresar una antigüedad entre 0 y 30 años.");

                if (NumeroEquipos < 1 || NumeroEquipos > 50)
                    lstErrores.Add("Falta ingresar un número de equipos entre 1 y 50.");

                if (GarantiaExtendida == 0)
                    lstErrores.Add("Falta seleccionar una opción de garantía extendida.");

                if (PrioridadServicio == 0)
                    lstErrores.Add("Falta seleccionar la prioridad del servicio.");


                // Validar que no haya errores
                if (lstErrores.Count == 0)
                {
                    // Costo base dependiendo del tipo de servicio
                    decimal costo_servicio = TipoServicio switch
                    {
                        1 => 350m,  // Limpieza interna
                        2 => 600m,  // Mantenimiento preventivo
                        3 => 450m,  // Cambio de pasta térmica
                        4 => 800m,  // Formateo e instalación
                        _ => 0m
                    };


                    // Cargo adicional dependiendo del tipo de equipo
                    decimal costo_equipo = TipoEquipo switch
                    {
                        1 => 0m,    // Laptop
                        2 => 100m,  // PC de escritorio
                        3 => 150m,  // All-in-One
                        4 => 300m,  // Mac
                        _ => 0m
                    };


                    // Cargo adicional dependiendo de la garantía
                    decimal costo_garantia = GarantiaExtendida switch
                    {
                        1 => 0m,    // 30 días
                        2 => 150m,  // 90 días
                        3 => 300m,  // 180 días
                        _ => 0m
                    };


                    // Cargo dependiendo de la prioridad
                    decimal cargo_prioridad = PrioridadServicio switch
                    {
                        1 => 0m,    // Normal
                        2 => 200m,  // Urgente
                        3 => 400m,  // Express
                        _ => 0m
                    };


                    // Recargo por antigüedad
                    decimal recargo_antiguedad = 0m;

                    if (AntiguedadEquipo > 5)
                    {
                        recargo_antiguedad = costo_servicio * 0.10m;
                    }


                    // Costo unitario por equipo
                    decimal costo_unitario =
                        costo_servicio +
                        costo_equipo +
                        costo_garantia +
                        recargo_antiguedad;


                    // Subtotal de todos los equipos
                    decimal subtotal_equipos =
                        costo_unitario * NumeroEquipos;


                    // Subtotal base incluyendo la prioridad
                    decimal subtotal_base =
                        subtotal_equipos + cargo_prioridad;


                    // Cálculo del IVA
                    decimal monto_iva =
                        subtotal_base * 0.16m;


                    // Total final a pagar
                    decimal total_pagar =
                        subtotal_base + monto_iva;


                    // Mostrar resultado
                    ResultadoOK =
                        "<div class='table-responsive'>" +
                        "<table class='table'>" +

                        "<tr>" +
                        "<td>Costo del servicio</td>" +
                        "<td>Cargo por equipo</td>" +
                        "<td>Cargo por garantía</td>" +
                        "<td>Recargo por antigüedad</td>" +
                        "<td>Costo unitario por equipo</td>" +
                        "<td>Número de equipos</td>" +
                        "<td>Subtotal de equipos</td>" +
                        "<td>Cargo por prioridad</td>" +
                        "<td>Subtotal base</td>" +
                        "<td>Monto IVA</td>" +
                        "<td>Total a pagar</td>" +
                        "</tr>" +

                        "<tr>" +
                        "<td>$" + costo_servicio + "</td>" +
                        "<td>$" + costo_equipo + "</td>" +
                        "<td>$" + costo_garantia + "</td>" +
                        "<td>$" + recargo_antiguedad + "</td>" +
                        "<td>$" + costo_unitario + "</td>" +
                        "<td>" + NumeroEquipos + "</td>" +
                        "<td>$" + subtotal_equipos + "</td>" +
                        "<td>$" + cargo_prioridad + "</td>" +
                        "<td>$" + subtotal_base + "</td>" +
                        "<td>$" + monto_iva + "</td>" +
                        "<td>$" + total_pagar + "</td>" +
                        "</tr>" +

                        "</table>" +
                        "</div>";
                }
                else
                {
                    ResultadoNoOK = string.Join("<br>", lstErrores);
                }
            }
            catch (Exception e)
            {
                ResultadoNoOK =
                    "Esto es un error, no sé por qué pasó... " + e.Message;
            }
        }


        // Acción del botón Limpiar
        public void OnPostLimpiar()
        {
            ModelState.Clear();

            TipoServicio = 0;
            TipoEquipo = 0;
            AntiguedadEquipo = 0;
            NumeroEquipos = 0;
            GarantiaExtendida = 0;
            IVA = 16;
            PrioridadServicio = 0;

            ResultadoOK = string.Empty;
            ResultadoNoOK = string.Empty;
        }
    }
}
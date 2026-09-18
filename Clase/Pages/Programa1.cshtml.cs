using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeBehind.Pages
{
    // Obtiene los valores directamente de los controles de HTML
    [BindProperties]
    public class Programa1Model : PageModel
    {
        // Mapear todos los controles para obtener sus propiedades
        #region Entidad
        public int Producto { get; set; }
        public int Cantidad { get; set; }
        public int TipoCliente { get; set; }
        public int MetodoEnvio { get; set; }
        public int Descuento { get; set; }
        public int IVA { get; set; }

        public string? ResultadoOK { get; set; }
        public string? ResultadoNoOK { get; set; }
        #endregion

        public void OnGet()
        {
            // Automaticamente se ejecuta al abrir la página
        }

        // Acción del botón Guardar
        public void OnPostGuardar()
        {
            // Lógica del botón
            try
            {
                // Validación de entrada de datos
                List<string> lstErrores = new List<string>();

                if (Producto == 0)
                    lstErrores.Add("Falta seleccionar un producto.");
                if (Cantidad < 1 || Cantidad > 100)
                    lstErrores.Add("Falta ingresar una cantidad entre 1 y 100.");
                if (TipoCliente == 0)
                    lstErrores.Add("Falta seleccionar un tipo de cliente.");
                if (MetodoEnvio == 0)
                    lstErrores.Add("Falta seleccionar un método de envío.");
                if (Descuento < 1 || Descuento > 50)
                    lstErrores.Add("Falta ingresar un descuento entre 0% y 50%");

                // Validar que no haya errores
                if (lstErrores.Count == 0)
                {
                    decimal precio_producto = Producto switch
                    {
                        1 => 15000m, // este sería el case , tipo case 1
                        2 => 350m, // case 2
                        3 => 1200m, // case 3
                        4 => 3800m,
                        _ => 0 // para el default
                    };

                    decimal tipo_cliente = TipoCliente switch
                    {
                        1 => 0m,
                        2 => 0.05m,
                        3 => 0.15m,
                        _ => 0m

                    };

                    int metodo_envio = MetodoEnvio switch
                    {
                      1 => 0,
                      2 => 150,
                      3 => 300,
                      _ => 0  
                    };


                decimal subtotal_base = precio_producto * Cantidad; // subtotal de los productos por la cantidad
                decimal descuento_admin = subtotal_base * (Descuento / 100m); // calculo el descuento que puso el administrador
                decimal subtotal_descuento_1 = subtotal_base - descuento_admin; // se resta el primer descuento
                decimal monto_descuento_por_cliente = subtotal_base * tipo_cliente; // se calcula el descuento por tipo del cliente TODO LOS DESCUENTOS ES POR EL SUBTOTAL PREDETERMINADO
                decimal subtotal_descuento_2 = subtotal_descuento_1 - monto_descuento_por_cliente; // se resta el segundo descuento
                decimal monto_iva = subtotal_descuento_2 * 0.16m;
                decimal total_pagar = subtotal_descuento_2 + monto_iva;
                
                


                ResultadoOK = "<div class=table-responsive>" + 
                "<table class='table'>" + 
                "<tr>" +
                "<td>Subtotal base $</td>" +
                "<td>Monto Descuento (Que elejiste)</td>" +
                "<td>Subtotal Después del Primer Descuento $</td>" +
                "<td>Monto Descuento (Por Tipo de Cliente)</td>" +
                "<td>Subtotal Después del Segundo Descuento</td>" +
                "<td>Monto IVA</td>" +
                "<td>Total a Pagar $</td>" +
                "</tr>" +
                "<tr>" +
                "<td>" + subtotal_base + "</td>"+
                "<td>" + descuento_admin + "</td>"+
                "<td>" + subtotal_descuento_1 + "</td>"+
                "<td>" + monto_descuento_por_cliente + "</td>"+
                "<td>" + subtotal_descuento_2 + "</td>"+
                "<td>" + monto_iva + "</td>"+
                "<td>" + total_pagar + "</td>"+
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
                ResultadoNoOK = "Esto es un error, no se porque paso..." + e;
            }
        }

        // Acción del botón Limpiar
        public void OnPostLimpiar()
        {
            ModelState.Clear();
            Producto = 0;
            Cantidad = 0;
            TipoCliente = 0;
            MetodoEnvio = 0;
            Descuento = 0;
            ResultadoOK = string.Empty;
            ResultadoNoOK = string.Empty;
        }
    }
}
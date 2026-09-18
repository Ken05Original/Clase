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
                    decimal costo_servicio = TipoServicio switch
                    {
                        1 => 350m,
                        2 => 600m,
                        3 => 450m,
                        4 => 800m,
                        _ => 0m

                    };

                    decimal costo_equipo = TipoEquipo switch
                    {
                        1 => 100m,
                        2 => 150m,
                        3 => 300m,
                        _ => 0m

                    };

                    

                    ResultadoOK = "YUPI SIN ERRORES";
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


using DL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.ComponentModel.DataAnnotations;

namespace BL
{
    public class Emisor
    {
        //PROPIEDADES 
        public string IdEmisor { get; set; }
        public string? RFC { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FechaInicioOperaciones { get; set; }
        public decimal? Capital { get; set; }
        public List<object>? Emisores { get; set; }
        public bool Correct { get; set; }
        //bandera para usar en la validacion en el FORM del controlador en PL
        public string Bandera { get; set; }


        //METODOS CPARA DE NEGOCIOS

        public static string AddEF(Emisor emisor)
        {
            string id = "";

            try
            {
                var fecha = emisor.FechaInicioOperaciones.Value.ToString("MM-dd-yyyy");

                using (DL.BrodriguezMasterediContext contex = new DL.BrodriguezMasterediContext())
                {
                    var query = contex.Database.ExecuteSqlRaw($"EmisorAdd '{emisor.IdEmisor}', '{emisor.RFC}', '{fecha}', {emisor.Capital}");

                    if (query > 0)
                    {
                        id = emisor.IdEmisor;
                    }
                    else
                    {
                        id = "";
                    }
                }
            }
            catch (Exception ex)
            { 
                var exepcion = ex;
                id = "";
            }
            return id;
        }
        public static string UpdateEF(Emisor emisor)
        {
            string id = "";

            try
            {
                using (DL.BrodriguezMasterediContext contex = new DL.BrodriguezMasterediContext())
                {
                    var query = contex.Database.ExecuteSqlRaw($"EmisorUpdate '{emisor.IdEmisor}', '{emisor.RFC}', '{emisor.FechaInicioOperaciones}', {emisor.Capital}");

                    if (query > 0)
                    {
                        id = emisor.IdEmisor;
                    }
                    else
                    {
                        id = "";
                    }
                }
            }
            catch (Exception ex)
            {
                var exepcion = ex;
                id = "";
            }
            return id;
        }
        public static string DeleteEF(string idEmisor)
        {
            string id = "";

            try
            {
                using (DL.BrodriguezMasterediContext contex = new DL.BrodriguezMasterediContext())
                {
                    var query = contex.Database.ExecuteSqlRaw($"EmisorDelete '{idEmisor}'");

                    if (query > 0)
                    {
                        id = idEmisor;
                    }
                    else
                    {
                        id = "";
                    }
                }
            }
            catch (Exception ex)
            {
                var exepcion = ex;
                id = "";
            }
            return id;
        }

        public static Emisor GetAllEF()
        {
            Emisor emisor = new Emisor();
            try
            {
                using (DL.BrodriguezMasterediContext context = new DL.BrodriguezMasterediContext())
                {
                    var query = context.Emisors.FromSqlRaw("EmisorGetAll").ToList();

                    emisor.Emisores = new List<object>();

                    if (query != null)
                    {
                        foreach (var registro in query)
                        {
                            Emisor emisor1 = new Emisor();

                            emisor1.IdEmisor = registro.IdEmisor;
                            emisor1.RFC = registro.Rfc;
                            emisor1.FechaInicioOperaciones = registro.FechaInicioOperacion;
                            emisor1.Capital = registro.Capital;

                            emisor.Emisores.Add(emisor1);    
                        }
                        emisor.Correct = true;
                    }
                    else
                    {
                        emisor.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                var exepcion = ex;
                emisor.Correct = false;
            }
            return emisor;
        }

        public static Emisor GetByIdEF(string idEmisor)
        {
            Emisor emisor = new Emisor();
            try
            {
                using (DL.BrodriguezMasterediContext context = new DL.BrodriguezMasterediContext())
                {
                    var query = context.Emisors.FromSqlRaw($"EmisorGetById '{idEmisor}'").AsEnumerable().SingleOrDefault();

                    if (query != null)
                    {
                        emisor.IdEmisor = query.IdEmisor;
                        emisor.RFC = query.Rfc;
                        emisor.FechaInicioOperaciones = query.FechaInicioOperacion;
                        emisor.Capital = query.Capital;

                        emisor.Correct = true;
                    }
                    else
                    {
                        emisor.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                var exepcion = ex;
                emisor.Correct = false;
            }
            return emisor;
        }
    }
}
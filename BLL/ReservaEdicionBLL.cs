using BLL.DAL;
using BLL.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class ReservaEdicionBLL
    {
        #region Métodos Públicos

        public int ObtenerProximaReserva()
        {
            int loIdReserva = 0;

            try
            {
                using (var rep = new Repository<ReservaEdicion>())
                {
                    var loExiste = rep.FindAll().Any();

                    if (loExiste)
                    {
                        // Devuelve el mayor valor del campo ID_VENTA
                        loIdReserva = rep.FindAll().Max(p => p.ID_RESERVA_EDICION);

                        if (loIdReserva > 0)
                            loIdReserva = loIdReserva + 1;
                    }
                    else
                        loIdReserva = 1;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return loIdReserva;
        }

        public ReservaEdicion ObtenerReservaEdicion(long idReservaEdicion)
        {
            ReservaEdicion oReservaEdicion = null;

            try
            {
                using (var rep = new Repository<ReservaEdicion>())
                {
                    oReservaEdicion = rep.Find(p => p.ID_RESERVA_EDICION == idReservaEdicion);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return oReservaEdicion;
        }

        public List<ReservaEdicion> ObtenerReservaEdicionPorCliente(ClienteFiltro oClienteFiltro)
        {
            List<ReservaEdicion> lstReservaListado = null;
            List<Reserva> lstReserva = null;
            List<ReservaEdicion> lstReservaEdicion = null;
            List<ClienteListado> lstClientes = null;

            try
            {
                using (var loRepReserva = new Repository<Reserva>())
                {
                    lstClientes = new BLL.ClienteBLL().ObtenerClientes(oClienteFiltro);
                    foreach (var loCliente in lstClientes)
                    {
                        lstReserva = loRepReserva.Search(p => p.COD_CLIENTE == loCliente.ID_CLIENTE).OrderByDescending(p => p.ID_RESERVA).ToList();
                    }

                    lstReservaListado = new List<ReservaEdicion>();

                    foreach (var loReserva in lstReserva)
                    {
                        try
                        {
                            using (var loRepReservaEdicion = new Repository<ReservaEdicion>())
                            {
                                lstReservaEdicion = loRepReservaEdicion.Search(p => p.COD_ESTADO == 15 && p.COD_RESERVA == loReserva.ID_RESERVA);

                                if (lstReservaEdicion.Count > 0)
                                {
                                    foreach (var loReservaEdicion in lstReservaEdicion)
                                    {
                                        lstReservaListado.Add(loReservaEdicion);
                                    }
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstReservaListado;
        }

        public bool AltaReservaEdicion(ReservaEdicion oReservaEdicion)
        {
            var bRes = false;

            try
            {
                using (var rep = new Repository<ReservaEdicion>())
                {
                    bRes = rep.Create(oReservaEdicion) != null;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bRes;
        }

        public bool ModificarReservaEdidion(ReservaEdicion oReservaEdicion)
        {
            var bRes = false;
            try
            {
                using (var rep = new Repository<ReservaEdicion>())
                {
                    bRes = rep.Update(oReservaEdicion);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bRes;
        }

        #endregion
    }

    #region Clases

    public class ReservaEdicionListado
    {
        public int ID_RESERVA_EDICION { get; set; }
        public int COD_RESERVA { get; set; }
        public DateTime FECHA { get; set; }
        public String NOMBRE_CLIENTE { get; set; }
        public string NOMBRE_PRODUCTO { get; set; }
        public int? COD_CLIENTE { get; set; }
        public string TIPO_RESERVA { get; set; }
        public string FORMA_ENTREGA { get; set; }
        public DateTime? FECHA_INICIO { get; set; }
        public DateTime? FECHA_FIN { get; set; }
        public string ESTADO { get; set; }
        public string COD_EDICION { get; set; }
        public string EDICION { get; set; }
        public string NOMBRE_EDICION { get; set; }
        public string DESC_EDICION { get; set; }
        public string PRECIO_EDICION { get; set; }
    }



    #endregion
}

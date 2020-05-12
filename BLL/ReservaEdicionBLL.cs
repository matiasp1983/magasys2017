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

        public List<ReservaEdicionListado> ObtenerReservaEdicionPorCliente(ClienteFiltro oClienteFiltro)
        {
            List<ReservaEdicionListado> lstReservaListado = null;
            List<Reserva> lstReserva = null;
            List<ReservaEdicion> lstReservaEdicion = null;

            try
            {
                using (var loRepReserva = new Repository<Reserva>())
                {
                    lstReserva = loRepReserva.Search(p => p.COD_CLIENTE == oClienteFiltro.Id_cliente).OrderByDescending(p => p.ID_RESERVA).ToList();
                    lstReservaListado = new List<ReservaEdicionListado>();

                    if (lstReserva != null)
                    {
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
                                            ReservaEdicionListado oReservaListado = new ReservaEdicionListado
                                            {
                                                ID_RESERVA_EDICION = loReservaEdicion.ID_RESERVA_EDICION,
                                                COD_RESERVA = loReservaEdicion.COD_RESERVA,
                                                FECHA = loReservaEdicion.FECHA,
                                                NOMBRE_CLIENTE = loReservaEdicion.Reserva.Cliente.APELLIDO + ", " + loReservaEdicion.Reserva.Cliente.NOMBRE,
                                                COD_CLIENTE = loReservaEdicion.Reserva.COD_CLIENTE,
                                                FECHA_INICIO = loReservaEdicion.Reserva.FECHA_INICIO,
                                                FECHA_FIN = loReservaEdicion.Reserva.FECHA_FIN,
                                                ESTADO = loReservaEdicion.Estado.NOMBRE,
                                                COD_EDICION = loReservaEdicion.COD_PROD_EDICION.ToString(),
                                                EDICION = loReservaEdicion.ProductoEdicion.EDICION,
                                                NOMBRE_EDICION = loReservaEdicion.ProductoEdicion.NOMBRE,
                                                DESC_EDICION = loReservaEdicion.ProductoEdicion.DESCRIPCION,
                                                PRECIO_EDICION = loReservaEdicion.ProductoEdicion.PRECIO.ToString()
                                            };
                                            //ProductoEdicion oProductoEdicion = new BLL.ProductoEdicionBLL().ObtenerEdicion(Convert.ToInt32(oReservaListado.COD_EDICION));
                                            oReservaListado.NOMBRE_PRODUCTO = loReservaEdicion.ProductoEdicion.Producto.NOMBRE;
                                            if (loReservaEdicion.Reserva.ENVIO_DOMICILIO == null)
                                                oReservaListado.FORMA_ENTREGA = "Retira en Local";
                                            else
                                                oReservaListado.FORMA_ENTREGA = "Envío a Domicilio";
                                            lstReservaListado.Add(oReservaListado);
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
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstReservaListado;
        }

        public List<ReservaEdicionListado> ObtenerReservaEdicionPorProdEdicion(long codProductoEdicion)
        {
            List<ReservaEdicionListado> lstReservaListado = null;
            List<ReservaEdicion> lstReservaEdicion = null;

            lstReservaListado = new List<ReservaEdicionListado>();

            try
            {
                using (var loRepReservaEdicion = new Repository<ReservaEdicion>())
                {
                    lstReservaEdicion = loRepReservaEdicion.Search(p => p.COD_PROD_EDICION == codProductoEdicion && p.COD_ESTADO == 15).ToList();

                    if (lstReservaEdicion.Count > 0)
                    {
                        foreach (var loReservaEdicion in lstReservaEdicion)
                        {
                            ReservaEdicionListado oReservaListado = new ReservaEdicionListado
                            {
                                ID_RESERVA_EDICION = loReservaEdicion.ID_RESERVA_EDICION,
                                COD_RESERVA = loReservaEdicion.COD_RESERVA,
                                FECHA = loReservaEdicion.FECHA,
                                NOMBRE_CLIENTE = loReservaEdicion.Reserva.Cliente.APELLIDO + ", " + loReservaEdicion.Reserva.Cliente.NOMBRE,
                                COD_CLIENTE = loReservaEdicion.Reserva.COD_CLIENTE,
                                FECHA_INICIO = loReservaEdicion.Reserva.FECHA_INICIO,
                                FECHA_FIN = loReservaEdicion.Reserva.FECHA_FIN,
                                ESTADO = loReservaEdicion.Estado.NOMBRE,
                                COD_EDICION = loReservaEdicion.COD_PROD_EDICION.ToString(),
                                EDICION = loReservaEdicion.ProductoEdicion.EDICION,
                                NOMBRE_EDICION = loReservaEdicion.ProductoEdicion.NOMBRE,
                                DESC_EDICION = loReservaEdicion.ProductoEdicion.DESCRIPCION,
                                PRECIO_EDICION = loReservaEdicion.ProductoEdicion.PRECIO.ToString()
                            };
                            //ProductoEdicion oProductoEdicion = new BLL.ProductoEdicionBLL().ObtenerEdicion(Convert.ToInt32(oReservaListado.COD_EDICION));
                            oReservaListado.NOMBRE_PRODUCTO = loReservaEdicion.ProductoEdicion.Producto.NOMBRE;
                            if (loReservaEdicion.Reserva.ENVIO_DOMICILIO == null)
                                oReservaListado.FORMA_ENTREGA = "Retira en Local";
                            else
                                oReservaListado.FORMA_ENTREGA = "Envío a Domicilio";
                            lstReservaListado.Add(oReservaListado);
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

        /// <summary>
        /// Obtener Reservas Edicion confirmadas y con envío a domicilio
        /// </summary>
        /// <returns></returns>
        public List<ReservaEdicion> ObtenerReservaEdicionConEnvioDomicilio()
        {
            List<ReservaEdicion> lstReservaEdicion = null;

            try
            {
                using (var loRepReservaEdicion = new Repository<ReservaEdicion>())
                {
                    lstReservaEdicion = loRepReservaEdicion.Search(p => p.COD_ESTADO == 15 && p.Reserva.ENVIO_DOMICILIO == "X");
                }
            }
            catch (Exception)
            {
                throw;
            }

            return lstReservaEdicion;
        }

        public int CantidadReservaEdicionPorProductoEdicion(long codProductoEdicion)
        {
            // Obtener cantidad de productos edición reservados, es decir, con Reserva Edición: Confirmada.

            int loCantidadProductoEdicion = 0;

            try
            {
                using (var loRepReservaEdicion = new Repository<ReservaEdicion>())
                {
                    loCantidadProductoEdicion = loRepReservaEdicion.Search(p => p.COD_PROD_EDICION == codProductoEdicion && p.COD_ESTADO == 15).Count();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return loCantidadProductoEdicion;
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

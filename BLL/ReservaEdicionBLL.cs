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

        public ReservaEdicion ObtenerReservaEdicionDeReservaUnica(long idReserva)
        {
            ReservaEdicion oReservaEdicion = null;

            try
            {
                using (var rep = new Repository<ReservaEdicion>())
                {
                    oReservaEdicion = rep.Find(p => p.COD_RESERVA == idReserva);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return oReservaEdicion;
        }

        public List<ReservaEdicionListado> ObtenerReservasEdicionPorReserva(long idReserva)
        {
            List<ReservaEdicionListado> lstReservaEdicionListado = new List<ReservaEdicionListado>();
            ReservaEdicionListado oReservaEdicionListado = null;

            try
            {
                using (var repReservaEdicion = new Repository<ReservaEdicion>())
                {
                    var lstReservaEdicion = repReservaEdicion.Search(p => p.COD_RESERVA == idReserva).OrderByDescending(p => p.ID_RESERVA_EDICION).ToList();

                    if (lstReservaEdicion.Count > 0)
                    {
                        foreach (var item in lstReservaEdicion)
                        {
                            oReservaEdicionListado = new ReservaEdicionListado()
                            {
                                ID_RESERVA_EDICION = item.ID_RESERVA_EDICION,
                                EDICION = item.ProductoEdicion.EDICION,
                                NOMBRE_EDICION = item.ProductoEdicion.NOMBRE,
                                ESTADO = item.Estado.NOMBRE,
                                DESC_EDICION = item.ProductoEdicion.DESCRIPCION,
                                PRECIO_EDICION = "$ " + item.ProductoEdicion.PRECIO.ToString()
                            };

                            lstReservaEdicionListado.Add(oReservaEdicionListado);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return lstReservaEdicionListado;
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
                                            String dia = loReservaEdicion.FECHA.DayOfWeek.ToString();
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
                                                PRECIO_EDICION = loReservaEdicion.ProductoEdicion.PRECIO.ToString(),
                                                NOMBRE_PRODUCTO = loReservaEdicion.ProductoEdicion.Producto.NOMBRE
                                            };

                                            if (loReservaEdicion.ProductoEdicion.Producto.COD_TIPO_PRODUCTO == 1)
                                                oReservaListado.NOMBRE_PRODUCTO = loReservaEdicion.ProductoEdicion.Producto.NOMBRE + " - " + loReservaEdicion.ProductoEdicion.Producto.DESCRIPCION;

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
        public List<ReservaEdicionReparto> ObtenerReservaEdicionConEnvioDomicilio(ReservaFiltro oReservaFiltro)
        {
            List<ReservaEdicionReparto> lstReservaEdicionReparto = null;

            try
            {
                using (var loRepReservaEdicion = new Repository<ReservaEdicion>())
                {
                    var lstReservaEdicion = loRepReservaEdicion.Search(p => p.COD_ESTADO == 15 && p.Reserva.ENVIO_DOMICILIO == "X");

                    if (lstReservaEdicion.Count > 0 && !String.IsNullOrEmpty(oReservaFiltro.NOMBRE_PRODUCTO))
                        lstReservaEdicion = lstReservaEdicion.FindAll(p => p.Reserva.Producto.NOMBRE.ToUpper().Contains(oReservaFiltro.NOMBRE_PRODUCTO.ToUpper()));

                    if (lstReservaEdicion.Count > 0 && !String.IsNullOrEmpty(oReservaFiltro.EDICION))
                        lstReservaEdicion = lstReservaEdicion.FindAll(p => p.ProductoEdicion.EDICION.ToUpper().Contains(oReservaFiltro.EDICION.ToUpper()));

                    if (lstReservaEdicion.Count > 0 && !String.IsNullOrEmpty(oReservaFiltro.NOMBRE_EDICION))
                        lstReservaEdicion = lstReservaEdicion.FindAll(p => p.ProductoEdicion.NOMBRE.ToUpper().Contains(oReservaFiltro.NOMBRE_EDICION.ToUpper()));

                    if (lstReservaEdicion.Count > 0)
                    {
                        lstReservaEdicionReparto = new List<ReservaEdicionReparto>();

                        foreach (var item in lstReservaEdicion)
                        {
                            ReservaEdicionReparto oReservaEdicionReparto = new ReservaEdicionReparto
                            {
                                ID_RESERVA_EDICION = item.ID_RESERVA_EDICION,
                                CLIENTE = item.Reserva.Cliente.ID_CLIENTE.ToString() + " - " + item.Reserva.Cliente.NOMBRE + " " + item.Reserva.Cliente.APELLIDO,
                                CODIGO_CLIENTE = item.Reserva.Cliente.ID_CLIENTE.ToString(),
                                CLIENTE_NOMBRE = item.Reserva.Cliente.NOMBRE + " " + item.Reserva.Cliente.APELLIDO,
                                DIRECCION_MAPS = item.Reserva.Cliente.DIRECCION_MAPS,
                                PRODUCTO = item.Reserva.Producto.NOMBRE,
                                CODIGO_EDICION = item.COD_PROD_EDICION,
                                TIPO_PRODUCTO = item.ProductoEdicion.Producto.TipoProducto.DESCRIPCION
                            };

                            if (item.Reserva.Producto.COD_TIPO_PRODUCTO == 1)
                                oReservaEdicionReparto.PRODUCTO = item.Reserva.Producto.NOMBRE + " - " + item.Reserva.Producto.DESCRIPCION;

                            if (item.ProductoEdicion.NOMBRE == null)
                                oReservaEdicionReparto.EDICION = item.ProductoEdicion.EDICION;
                            else
                                oReservaEdicionReparto.EDICION = item.ProductoEdicion.EDICION + " - " + item.ProductoEdicion.NOMBRE;

                            lstReservaEdicionReparto.Add(oReservaEdicionReparto);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return lstReservaEdicionReparto;
        }

        /// <summary>
        /// Obtener Reservas Edicion En Reparto
        /// </summary>
        /// <returns></returns>
        public List<ReservaEdicionReparto> ObtenerReservaEdicionEnReparto(ReservaFiltro oReservaFiltro)
        {
            List<ReservaEdicionReparto> lstReservaEdicionReparto = null;

            try
            {
                using (var loRepReservaEdicion = new Repository<ReservaEdicion>())
                {
                    var lstReservaEdicion = loRepReservaEdicion.Search(p => p.COD_ESTADO == 17);

                    if (lstReservaEdicion.Count > 0 && !String.IsNullOrEmpty(oReservaFiltro.NOMBRE_PRODUCTO))
                        lstReservaEdicion = lstReservaEdicion.FindAll(p => p.Reserva.Producto.NOMBRE.ToUpper().Contains(oReservaFiltro.NOMBRE_PRODUCTO.ToUpper()));

                    if (lstReservaEdicion.Count > 0 && !String.IsNullOrEmpty(oReservaFiltro.EDICION))
                        lstReservaEdicion = lstReservaEdicion.FindAll(p => p.ProductoEdicion.EDICION.ToUpper().Contains(oReservaFiltro.EDICION.ToUpper()));

                    if (lstReservaEdicion.Count > 0 && !String.IsNullOrEmpty(oReservaFiltro.NOMBRE_EDICION))
                        lstReservaEdicion = lstReservaEdicion.FindAll(p => p.ProductoEdicion.NOMBRE.ToUpper().Contains(oReservaFiltro.NOMBRE_EDICION.ToUpper()));

                    if (lstReservaEdicion.Count > 0)
                    {
                        lstReservaEdicionReparto = new List<ReservaEdicionReparto>();

                        foreach (var item in lstReservaEdicion)
                        {
                            ReservaEdicionReparto oReservaEdicionReparto = new ReservaEdicionReparto
                            {
                                ID_RESERVA_EDICION = item.ID_RESERVA_EDICION,
                                CLIENTE = item.Reserva.Cliente.ID_CLIENTE.ToString() + " - " + item.Reserva.Cliente.NOMBRE + " " + item.Reserva.Cliente.APELLIDO,
                                CODIGO_CLIENTE = item.Reserva.Cliente.ID_CLIENTE.ToString(),
                                CLIENTE_NOMBRE = item.Reserva.Cliente.NOMBRE + " " + item.Reserva.Cliente.APELLIDO,
                                DIRECCION_MAPS = item.Reserva.Cliente.DIRECCION_MAPS,
                                PRODUCTO = item.Reserva.Producto.NOMBRE,
                                CODIGO_EDICION = item.COD_PROD_EDICION,
                                TIPO_PRODUCTO = item.ProductoEdicion.Producto.TipoProducto.DESCRIPCION
                            };

                            if (item.Reserva.Producto.COD_TIPO_PRODUCTO == 1)
                                oReservaEdicionReparto.PRODUCTO = item.Reserva.Producto.NOMBRE + " - " + item.Reserva.Producto.DESCRIPCION;

                            if (item.ProductoEdicion.NOMBRE == null)
                                oReservaEdicionReparto.EDICION = item.ProductoEdicion.EDICION;
                            else
                                oReservaEdicionReparto.EDICION = item.ProductoEdicion.EDICION + " - " + item.ProductoEdicion.NOMBRE;

                            lstReservaEdicionReparto.Add(oReservaEdicionReparto);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return lstReservaEdicionReparto;
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

        public ReservaEdicion ObtenerReservaEdicionEstadoSinStockORegistrada(long codreserva, long codProductoEdicion)
        {
            ReservaEdicion oReservaEdicion = new ReservaEdicion();

            try
            {
                using (var loRepReservaEdicion = new Repository<ReservaEdicion>())
                {
                    oReservaEdicion = loRepReservaEdicion.Find(p => p.COD_RESERVA == codreserva && p.COD_PROD_EDICION == codProductoEdicion && (p.COD_ESTADO == 18 || p.COD_ESTADO == 10));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return oReservaEdicion;
        }

        //public List<ReservaEdicionListado> ObtenerReservaEdicionParaAnular()
        //{
        //    List<ReservaEdicionListado> lstReservaListado = null;
        //    List<ReservaEdicion> lstReservaEdicion = null;

        //    try
        //    {
        //        using (var loRepReservaEdicion = new Repository<ReservaEdicion>())
        //        {
        //            lstReservaEdicion = loRepReservaEdicion.Search(p => p.COD_ESTADO == 10 || p.COD_ESTADO == 15 || p.COD_ESTADO == 18).ToList();

        //            if (lstReservaEdicion.Count > 0)
        //            {
        //                foreach (var loReservaEdicion in lstReservaEdicion)
        //                {
        //                    ReservaEdicionListado oReservaListado = new ReservaEdicionListado
        //                    {
        //                        ID_RESERVA_EDICION = loReservaEdicion.ID_RESERVA_EDICION,
        //                        COD_RESERVA = loReservaEdicion.COD_RESERVA,
        //                        //FECHA = loReservaEdicion.FECHA,
        //                        NOMBRE_CLIENTE = loReservaEdicion.Reserva.COD_CLIENTE + " - " + loReservaEdicion.Reserva.Cliente.APELLIDO + ", " + loReservaEdicion.Reserva.Cliente.NOMBRE,
        //                        //COD_CLIENTE = loReservaEdicion.Reserva.COD_CLIENTE,
        //                        //FECHA_INICIO = loReservaEdicion.Reserva.FECHA_INICIO,
        //                        //FECHA_FIN = loReservaEdicion.Reserva.FECHA_FIN,
        //                        ESTADO = loReservaEdicion.Estado.NOMBRE,
        //                        COD_EDICION = loReservaEdicion.COD_PROD_EDICION.ToString(),
        //                        EDICION = loReservaEdicion.ProductoEdicion.EDICION,
        //                        //NOMBRE_EDICION = loReservaEdicion.ProductoEdicion.NOMBRE,
        //                        //DESC_EDICION = loReservaEdicion.ProductoEdicion.DESCRIPCION
        //                    };

        //                    oReservaListado.NOMBRE_PRODUCTO = loReservaEdicion.ProductoEdicion.Producto.NOMBRE;
        //                    lstReservaListado.Add(oReservaListado);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return lstReservaListado;
        //}

        public List<ReservaEdicionListado> ObtenerReservaEdicion(ReservaFiltro oReservaFiltro)
        {
            List<ReservaEdicionListado> lstListReservaEdicionListado = null;

            try
            {
                using (var loRepReservaEdicion = new Repository<ReservaEdicion>())
                {
                    var lstReservaEdicion = loRepReservaEdicion.Search(p => p.COD_ESTADO == oReservaFiltro.COD_ESTADO).ToList();

                    if (lstReservaEdicion.Count > 0 && !String.IsNullOrEmpty(oReservaFiltro.EDICION))
                        lstReservaEdicion = lstReservaEdicion.FindAll(p => p.ProductoEdicion.EDICION.ToUpper().Contains(oReservaFiltro.EDICION.ToUpper()));

                    if (lstReservaEdicion.Count > 0 && oReservaFiltro.COD_TIPO_PRODUCTO > 0)
                        lstReservaEdicion = lstReservaEdicion.FindAll(p => p.Reserva.Producto.COD_TIPO_PRODUCTO == oReservaFiltro.COD_TIPO_PRODUCTO);

                    if (lstReservaEdicion.Count > 0 && !String.IsNullOrEmpty(oReservaFiltro.NOMBRE_PRODUCTO))
                        lstReservaEdicion = lstReservaEdicion.FindAll(p => p.Reserva.Producto.NOMBRE.ToUpper().Contains(oReservaFiltro.NOMBRE_PRODUCTO.ToUpper()));

                    if (lstReservaEdicion.Count > 0 && oReservaFiltro.TIPO_DOCUMENTO != 0 && oReservaFiltro.NRO_DOCUMENTO != 0)
                        lstReservaEdicion = lstReservaEdicion.FindAll(p => p.Reserva.Cliente.TIPO_DOCUMENTO == oReservaFiltro.TIPO_DOCUMENTO && p.Reserva.Cliente.NRO_DOCUMENTO == oReservaFiltro.NRO_DOCUMENTO);

                    if (lstReservaEdicion.Count > 0 && !String.IsNullOrEmpty(oReservaFiltro.NOMBRE))
                        lstReservaEdicion = lstReservaEdicion.FindAll(p => p.Reserva.Cliente.NOMBRE.ToUpper().Contains(oReservaFiltro.NOMBRE.ToUpper()));

                    if (lstReservaEdicion.Count > 0 && !String.IsNullOrEmpty(oReservaFiltro.APELLIDO))
                        lstReservaEdicion = lstReservaEdicion.FindAll(p => p.Reserva.Cliente.APELLIDO.ToUpper().Contains(oReservaFiltro.APELLIDO.ToUpper()));

                    if (lstReservaEdicion.Count > 0 && !String.IsNullOrEmpty(oReservaFiltro.ALIAS))
                        lstReservaEdicion = lstReservaEdicion.FindAll(p => p.Reserva.Cliente.ALIAS != null && p.Reserva.Cliente.ALIAS.ToUpper().Contains(oReservaFiltro.ALIAS.ToUpper()));

                    if (lstReservaEdicion.Count > 0)
                    {
                        lstListReservaEdicionListado = new List<ReservaEdicionListado>();

                        foreach (var oReservaEdicion in lstReservaEdicion)
                        {
                            ReservaEdicionListado oReservaEdicionListado = new ReservaEdicionListado()
                            {
                                EDICION = oReservaEdicion.ProductoEdicion.EDICION,
                                NOMBRE_CLIENTE = oReservaEdicion.Reserva.COD_CLIENTE + " - " + oReservaEdicion.Reserva.Cliente.APELLIDO + ", " + oReservaEdicion.Reserva.Cliente.NOMBRE,
                                NOMBRE_PRODUCTO = oReservaEdicion.ProductoEdicion.Producto.NOMBRE,
                                ESTADO = oReservaEdicion.Estado.NOMBRE,
                                ID_RESERVA_EDICION = oReservaEdicion.ID_RESERVA_EDICION,
                                COD_CLIENTE = oReservaEdicion.Reserva.COD_CLIENTE
                            };

                            if (oReservaEdicion.ProductoEdicion.Producto.COD_TIPO_PRODUCTO == 1)
                                oReservaEdicionListado.NOMBRE_PRODUCTO = oReservaEdicion.ProductoEdicion.Producto.NOMBRE + " - " + oReservaEdicion.ProductoEdicion.Producto.DESCRIPCION;

                            lstListReservaEdicionListado.Add(oReservaEdicionListado);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstListReservaEdicionListado;
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

    public class ReservaEdicionReparto
    {
        public string CODIGO_CLIENTE { get; set; }
        public string CLIENTE { get; set; } // Código + Nombre 
        public string CLIENTE_NOMBRE { get; set; } // Solo Nombre
        public string DIRECCION_MAPS { get; set; } // Dirección del Cliente
        public string DIRECCION_MAPS_ORIGEN { get; set; }
        public int ID_RESERVA_EDICION { get; set; }
        public int CODIGO_EDICION { get; set; }
        public string EDICION { get; set; }
        public string PRODUCTO { get; set; }
        public string TIPO_PRODUCTO { get; set; }
        public int CANTIDAD { get; set; }
    }

    #endregion
}
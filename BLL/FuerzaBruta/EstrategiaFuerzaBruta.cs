﻿using System;
using System.Collections.Generic;
using System.Linq;
using GoogleMapsAPI.NET.API.Client;
using GoogleMapsAPI.NET.API.Common.Components.Locations.Interfaces.Combined;
using GoogleMapsAPI.NET.API.Common.Components.Locations;
using GoogleMapsAPI.NET.API.Directions.Enums;

namespace BLL.FuerzaBruta
{
    public class EstrategiaFuerzaBruta
    {
        private NodoFB PRIMERNODO;
        private NodoFB ULTIMONODO;
        private string MODO_TRANSPORTE;

        public EstrategiaFuerzaBruta(string modoTransporte)
        {
            // Aquí obtenemos la dirección del Kiosco
            var oNegocio = new NegocioBLL().ObtenerNegocio();
            this.PRIMERNODO = new NodoFB();
            PRIMERNODO.CLIENTE.DIRECCION_MAPS = oNegocio.DIRECCION_MAPS;
            this.ULTIMONODO = new NodoFB();
            ULTIMONODO.CLIENTE.DIRECCION_MAPS = oNegocio.DIRECCION_MAPS;
            this.MODO_TRANSPORTE = modoTransporte;
        }

        #region Métodos Públicos

        public void Ejecutar(List<DAL.Cliente> lstCliente)
        {
            List<Delivery> lstDelivery = new List<Delivery>();

            foreach (var itemCliente in lstCliente)
            {
                Delivery oDelivery = new Delivery();
                oDelivery.CLIENTE = itemCliente;
                lstDelivery.Add(oDelivery);
            }

            //Delivery oDelivery1 = new Delivery();
            //oDelivery1.CLIENTE.ID_CLIENTE = 10;
            //oDelivery1.CLIENTE.DIRECCION_MAPS = "Río Negro 1200, Ciudad de Córdoba, Provincia de Córdoba, Argentina";
            //oDelivery1.COD_EDICION = 20;
            //lstDelivery.Add(oDelivery1);

            //Delivery oDelivery2 = new Delivery();
            //oDelivery2.CLIENTE.ID_CLIENTE = 20;
            //oDelivery2.CLIENTE.DIRECCION_MAPS = "La Rioja 300, Ciudad de Córdoba, Provincia de Córdoba, Argentina";
            //oDelivery2.COD_EDICION = 20;
            //lstDelivery.Add(oDelivery2);

            //Delivery oDelivery3 = new Delivery();
            //oDelivery3.CLIENTE.ID_CLIENTE = 30;
            //oDelivery3.CLIENTE.DIRECCION_MAPS = "Santiago del Estero 200, Ciudad de Córdoba, Provincia de Córdoba, Argentina";
            //oDelivery3.COD_EDICION = 20;
            //lstDelivery.Add(oDelivery3);

            List<NodoFB> nodosDelivery = obtenerNodosDelivery(lstDelivery);
            MatrizFB matrizFuerzaBruta = new MatrizFB();
            matrizFuerzaBruta = obtenerMatrizConCombinatorias(nodosDelivery);
            matrizFuerzaBruta = agregarInicioYFinCombinaciones(matrizFuerzaBruta);
            matrizFuerzaBruta = calcularCostosCombinaciones(matrizFuerzaBruta);
            FilaMatrizFB mejorCombinacion = buscarMejorCombinacion(matrizFuerzaBruta);

        }

        #endregion

        #region Métodos Privados

        private List<NodoFB> obtenerNodosDelivery(List<Delivery> operacionesPlanificar)
        {
            List<NodoFB> nodosDelivery = new List<NodoFB>();
            foreach (var operacion in operacionesPlanificar)
            {
                NodoFB nodo = new NodoFB();
                nodo.CLIENTE = operacion.CLIENTE;
                nodosDelivery.Add(nodo);
            }
            return nodosDelivery;
        }

        private MatrizFB obtenerMatrizConCombinatorias(List<NodoFB> nodosDelivery)
        {
            MatrizFB matrizFuerzaBruta = new MatrizFB();
            if (nodosDelivery.Count() > 0)
            {
                object[] pool = nodosDelivery.ToArray();
                List<List<NodoFB>> matrizCombinaciones = GetKCombinacionesManual(pool, pool.Count());

                foreach (var unaCombinacion in matrizCombinaciones)
                {
                    FilaMatrizFB unaFila = new FilaMatrizFB();
                    foreach (var unValor in unaCombinacion)
                    {
                        unaFila.NODOS.Add(unValor);
                    }
                    matrizFuerzaBruta.COMBINACIONES_POSIBLES.Add(unaFila);
                }
            }
            return matrizFuerzaBruta;
        }

        private MatrizFB agregarInicioYFinCombinaciones(MatrizFB matrizFuerzaBruta)
        {
            foreach (var unaCombinacion in matrizFuerzaBruta.COMBINACIONES_POSIBLES)
            {
                List<NodoFB> nodosConInicioYFin = new List<NodoFB>();
                nodosConInicioYFin.Add(PRIMERNODO);
                nodosConInicioYFin.AddRange(unaCombinacion.NODOS);
                nodosConInicioYFin.Add(ULTIMONODO);

                unaCombinacion.NODOS = nodosConInicioYFin;
            }
            return matrizFuerzaBruta;
        }

        private List<NodoDistanciaFB> generarNodosDistanciaList(List<NodoFB> nodos)
        {
            List<NodoDistanciaFB> nodosDistanciaList = new List<NodoDistanciaFB>();

            int anterior = 0;
            for (int actual = 0; actual < nodos.Count; actual++)
            {
                NodoDistanciaFB nuevoNodoDistancia = new NodoDistanciaFB();
                if (actual > 0)
                {
                    nuevoNodoDistancia.ORIGEN = nodos[anterior];
                    nuevoNodoDistancia.DESTINO = nodos[actual];

                    nodosDistanciaList.Add(nuevoNodoDistancia);
                }

                anterior = actual;
            }

            return nodosDistanciaList;
        }

        private MatrizFB calcularCostosCombinaciones(MatrizFB matrizFuerzaBruta)
        {
            GoogleMapsAPI.NET.API.DistanceMatrix.Responses.GetDistanceMatrixResponse matrix = new GoogleMapsAPI.NET.API.DistanceMatrix.Responses.GetDistanceMatrixResponse();

            var client = new MapsAPIClient("AIzaSyDx16zfH9UGcTIwpXN9JS3q8u0hx-2D9XI");

            foreach (var unaCombinacion in matrizFuerzaBruta.COMBINACIONES_POSIBLES)
                unaCombinacion.NODOS_DISTANCIA = generarNodosDistanciaList(unaCombinacion.NODOS);

            // obtener datos desde api google 
            foreach (var unaCombinacion in matrizFuerzaBruta.COMBINACIONES_POSIBLES)
            {
                foreach (var unNodoDistancia in unaCombinacion.NODOS_DISTANCIA)
                {
                    switch (MODO_TRANSPORTE)
                    {
                        case "Driving":

                            matrix = client.DistanceMatrix.GetDistanceMatrix(
                            new List<IAddressOrGeoCoordinatesLocation>
                            {
                                                    new AddressLocation(unNodoDistancia.ORIGEN.CLIENTE.DIRECCION_MAPS)
                            },
                            new List<IAddressOrGeoCoordinatesLocation>
                            {
                                                    new AddressLocation(unNodoDistancia.DESTINO.CLIENTE.DIRECCION_MAPS)
                            },
                            TransportationModeEnum.Driving, // Driving: en coche - Walking: a pie -  Bicycling: en bicicleta
                            language: "es-ES");

                            break;

                        case "Bicycling":

                            matrix = client.DistanceMatrix.GetDistanceMatrix(
                            new List<IAddressOrGeoCoordinatesLocation>
                            {
                                                    new AddressLocation(unNodoDistancia.ORIGEN.CLIENTE.DIRECCION_MAPS)
                            },
                            new List<IAddressOrGeoCoordinatesLocation>
                            {
                                                    new AddressLocation(unNodoDistancia.DESTINO.CLIENTE.DIRECCION_MAPS)
                            },
                            TransportationModeEnum.Bicycling, // Driving: en coche - Walking: a pie -  Bicycling: en bicicleta
                            language: "es-ES");

                            break;

                        case "Walking":

                            matrix = client.DistanceMatrix.GetDistanceMatrix(
                            new List<IAddressOrGeoCoordinatesLocation>
                            {
                                                    new AddressLocation(unNodoDistancia.ORIGEN.CLIENTE.DIRECCION_MAPS)
                            },
                            new List<IAddressOrGeoCoordinatesLocation>
                            {
                                                    new AddressLocation(unNodoDistancia.DESTINO.CLIENTE.DIRECCION_MAPS)
                            },
                            TransportationModeEnum.Walking, // Driving: en coche - Walking: a pie -  Bicycling: en bicicleta
                            language: "es-ES");

                            break;
                    }

                    if (matrix.Status == "OK")
                        unNodoDistancia.DISTANCIA = matrix.Rows[0].Elements[0].Distance.Value;
                }
            }

            foreach (var unaCombinacion in matrizFuerzaBruta.COMBINACIONES_POSIBLES)
                unaCombinacion.calcularCostosTotales();

            return matrizFuerzaBruta;
        }

        private FilaMatrizFB buscarMejorCombinacion(MatrizFB matrizFuerzaBruta)
        {
            FilaMatrizFB mejorCombinacion = new FilaMatrizFB();

            mejorCombinacion = matrizFuerzaBruta.COMBINACIONES_POSIBLES.ElementAt(0);

            foreach (var unaCombinacion in matrizFuerzaBruta.COMBINACIONES_POSIBLES)
            {
                // encuentro el menor por distancia
                if (unaCombinacion.COSTO_TOTAL_DISTANCIA < mejorCombinacion.COSTO_TOTAL_DISTANCIA)
                {
                    mejorCombinacion = unaCombinacion;
                }
            }
            return mejorCombinacion;
        }

        private static List<List<NodoFB>> kCombinacionesEliminarRepetidos(List<List<NodoFB>> matrizCombinaciones)
        {
            List<List<NodoFB>> matrizCombinacionesReturn = new List<List<NodoFB>>();
            //elimino filas combinaciones con nodos repetidos en la fila
            foreach (var unaCombinacion in matrizCombinaciones)
            {
                NodoFB[] unaCombinacionArray = unaCombinacion.ToArray();
                Boolean incluir = true;
                int cantRepetidosTodos = 0;

                //analizo una fila
                for (int i = 0; i < unaCombinacionArray.Count(); i++)
                {
                    NodoFB nodoAnalizar = unaCombinacionArray[i];
                    //int repeticionesPorNumero = 0;

                    //si en nodos anteriores ya encontre duplicado corto
                    if (cantRepetidosTodos > 0)
                    {
                        incluir = false;
                        break;
                    }

                    //analizo un caracter especifico contra todos los de la fila
                    for (int j = i + 1; j < unaCombinacionArray.Count(); j++)
                    {
                        if (unaCombinacionArray[j].CLIENTE.ID_CLIENTE == nodoAnalizar.CLIENTE.ID_CLIENTE)
                        {
                            //if (repeticionesPorNumero > 0)
                            //{
                            //    repeticionesPorNumero = 0;
                            //}
                            //repeticionesPorNumero++; 
                            cantRepetidosTodos++;
                            break;
                        }
                    }
                }
                //fin analisis de la fila

                if (incluir)
                {
                    matrizCombinacionesReturn.Add(unaCombinacion);
                }
            }
            // Fin analisis de la combinacion

            return matrizCombinacionesReturn;
        }

        private static List<List<NodoFB>> GetKCombinacionesManual(object[] root, int length)
        {
            List<List<NodoFB>> kCombinacionesReturn = new List<List<NodoFB>>();

            // allocate an array to hold our counts:
            int[] pos = new int[length];
            object[] combo = new object[length];
            for (int i = 0; i < length; i++)
            {
                combo[i] = root[0];
            }

            while (true)
            {
                // genero una fila combinacion y la agrego a la matriz
                List<NodoFB> unaKCombinacion = new List<NodoFB>();
                for (int i = 0; i < combo.Count(); i++)
                {
                    NodoFB unNodo = (NodoFB)combo[i];
                    unaKCombinacion.Add(unNodo);
                }
                kCombinacionesReturn.Add(unaKCombinacion);

                // move on to the next combination:
                int place = length - 1;
                while (place >= 0)
                {
                    if (++pos[place] == root.Count())
                    {
                        pos[place] = 0;
                        combo[place] = root[0];
                        place--;
                    }
                    else
                    {
                        combo[place] = root[pos[place]];
                        break;
                    }
                }
                if (place < 0)
                    break;
            }

            //TODO:
            //cuando el mismo elemento esta repetido mas de una vez, no incluir ese elemento
            //sacar duplicados espejo
            return kCombinacionesEliminarRepetidos(kCombinacionesReturn);
        }

        #endregion
    }
}

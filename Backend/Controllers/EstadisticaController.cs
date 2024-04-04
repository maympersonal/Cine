using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.ServiceLayer;
using Backend.Models;
using Backend.Data.DTOs;
using System.Runtime.CompilerServices;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadisticaController : ControllerBase
    {
        private readonly ServiceCompra _servicecompra;
        private readonly ServicePelicula _servicepelicula;
        private readonly ServiceCliente _servicecliente;
        private readonly ServicePago _servicepago;

        public EstadisticaController(ServiceCompra servicecompra,ServicePelicula servicepelicula,ServiceCliente servicecliente,ServicePago servicepago)
        {
            _servicecompra = servicecompra;
            _servicepelicula=servicepelicula;
            _servicecliente=servicecliente;
            _servicepago = servicepago;
        }

        [HttpGet("GetByFecha/{FechaInicio}/{FechaFinal}")]
        public async Task<ActionResult<EstadisticaByFechaDtoOut>> GetByFecha(DateTime FechaInicio,DateTime FechaFinal)
        {
            var compra = await _servicecompra.GetComprasByFecha(FechaInicio,FechaFinal);
            EstadisticaByFechaDtoOut result = new EstadisticaByFechaDtoOut{RegistroCompra = compra};
            foreach(Compra a in compra)
            {
                result.TotalButacas += a.IdBs.Count;
                var pago = await _servicepago.GetPago(a.IdPg);
                if(pago is null) return BadRequest();
                if(a.Tipo=="Tarjeta") 
                {
                    result.ButacasTransferencia += a.IdBs.Count;
                    if(pago.Web is not null ) 
                    {
                        result.TotalTransferencia += pago.Web.Cantidad;
                        result.TotalDinero += pago.Web.Cantidad;
                    }
                }
                else if(a.Tipo=="Puntos")
                { 
                    result.ButacasPuntos += a.IdBs.Count;
                    if(pago.Punto is not null )result.TotalPuntos += pago.Punto.Gastados;
                }
                else 
                {
                    result.ButacasEfectivo +=a.IdBs.Count;
                    if(pago.Efectivo is not null)
                    {
                        result.TotalEfectivo += pago.Efectivo.CantidadE;
                        result.TotalDinero += pago.Efectivo.CantidadE;
                    }
                }
            }
            return Ok(result);
        }

        [HttpGet("GetByTipo/{tipo}")]
        public async Task<ActionResult<IEnumerable<Compra>>> GetByTipo(string tipo)
        {
            var compra = await _servicecompra.GetComprasByTipo(tipo);

            if (compra == null)
            {
                return NotFound();
            }

            return Ok(compra);
        }

        [HttpGet("GetByPeliculaDinero")]
        public async Task<ActionResult<IEnumerable<EstadisticaByPeliculaDtoOut>>> GetByPeliculaDinero()
        {
            var pelicula=await _servicepelicula.GetPeliculas();
            List<EstadisticaByPeliculaDtoOut> resultlist = new List<EstadisticaByPeliculaDtoOut>();
            foreach(Pelicula peli in pelicula)
            {
                var compras = await _servicecompra.GetComprasByPelicula(peli.IdP);
                EstadisticaByPeliculaDtoOut result = new EstadisticaByPeliculaDtoOut{pelicula = peli};
                foreach(Compra a in compras)
                {
                    result.TotalButacas += a.IdBs.Count;
                    var pago = await _servicepago.GetPago(a.IdPg);
                    if(pago is null) return BadRequest();
                    if(a.Tipo=="Tarjeta") 
                    {
                        result.ButacasTransferencia += a.IdBs.Count;
                        if(pago.Web is not null ) 
                        {
                            result.TotalTransferencia += pago.Web.Cantidad;
                            result.TotalDinero += pago.Web.Cantidad;
                        }
                    }
                    else if(a.Tipo=="Puntos")
                    { 
                        result.ButacasPuntos += a.IdBs.Count;
                        if(pago.Punto is not null )result.TotalPuntos += pago.Punto.Gastados;
                    }
                    else 
                    {
                        result.ButacasEfectivo +=a.IdBs.Count;
                        if(pago.Efectivo is not null)
                        {
                            result.TotalEfectivo += pago.Efectivo.CantidadE;
                            result.TotalDinero += pago.Efectivo.CantidadE;
                        }
                    }
                }
                resultlist.Add(result);
            }

            return Ok(resultlist.OrderByDescending(x=>x.TotalDinero));
        }
     
        [HttpGet("GetByPeliculaButacas")]
        public async Task<ActionResult<IEnumerable<EstadisticaByPeliculaDtoOut>>> GetByPeliculaButacas()
        {
            var pelicula=await _servicepelicula.GetPeliculas();
            List<EstadisticaByPeliculaDtoOut> resultlist = new List<EstadisticaByPeliculaDtoOut>();
            foreach(Pelicula peli in pelicula)
            {
                var compras = await _servicecompra.GetComprasByPelicula(peli.IdP);
                EstadisticaByPeliculaDtoOut result = new EstadisticaByPeliculaDtoOut{pelicula = peli};
                foreach(Compra a in compras)
                {
                    result.TotalButacas += a.IdBs.Count;
                    var pago = await _servicepago.GetPago(a.IdPg);
                    if(pago is null) return BadRequest();
                    if(a.Tipo=="Tarjeta") 
                    {
                        result.ButacasTransferencia += a.IdBs.Count;
                        if(pago.Web is not null ) 
                        {
                            result.TotalTransferencia += pago.Web.Cantidad;
                            result.TotalDinero += pago.Web.Cantidad;
                        }
                    }
                    else if(a.Tipo=="Puntos")
                    { 
                        result.ButacasPuntos += a.IdBs.Count;
                        if(pago.Punto is not null )result.TotalPuntos += pago.Punto.Gastados;
                    }
                    else 
                    {
                        result.ButacasEfectivo +=a.IdBs.Count;
                        if(pago.Efectivo is not null)
                        {
                            result.TotalEfectivo += pago.Efectivo.CantidadE;
                            result.TotalDinero += pago.Efectivo.CantidadE;
                        }
                    }
                }
                resultlist.Add(result);
            }

            return Ok(resultlist.OrderByDescending(x=>x.TotalButacas));
        }

        [HttpGet("GetByPeliculaEfectivo")]
        public async Task<ActionResult<IEnumerable<EstadisticaByPeliculaDtoOut>>> GetByPeliculaEfectivo()
        {
            var pelicula=await _servicepelicula.GetPeliculas();
            List<EstadisticaByPeliculaDtoOut> resultlist = new List<EstadisticaByPeliculaDtoOut>();
            foreach(Pelicula peli in pelicula)
            {
                var compras = await _servicecompra.GetComprasByPelicula(peli.IdP);
                EstadisticaByPeliculaDtoOut result = new EstadisticaByPeliculaDtoOut{pelicula = peli};
                foreach(Compra a in compras)
                {
                    result.TotalButacas += a.IdBs.Count;
                    var pago = await _servicepago.GetPago(a.IdPg);
                    if(pago is null) return BadRequest();
                    if(a.Tipo=="Tarjeta") 
                    {
                        result.ButacasTransferencia += a.IdBs.Count;
                        if(pago.Web is not null ) 
                        {
                            result.TotalTransferencia += pago.Web.Cantidad;
                            result.TotalDinero += pago.Web.Cantidad;
                        }
                    }
                    else if(a.Tipo=="Puntos")
                    { 
                        result.ButacasPuntos += a.IdBs.Count;
                        if(pago.Punto is not null )result.TotalPuntos += pago.Punto.Gastados;
                    }
                    else 
                    {
                        result.ButacasEfectivo +=a.IdBs.Count;
                        if(pago.Efectivo is not null)
                        {
                            result.TotalEfectivo += pago.Efectivo.CantidadE;
                            result.TotalDinero += pago.Efectivo.CantidadE;
                        }
                    }
                }
                resultlist.Add(result);
            }

            return Ok(resultlist.OrderByDescending(x=>x.TotalEfectivo));
        }
        [HttpGet("GetByPeliculaTransferencia")]
        public async Task<ActionResult<IEnumerable<EstadisticaByPeliculaDtoOut>>> GetByPeliculaTransferencia()
        {
            var pelicula=await _servicepelicula.GetPeliculas();
            List<EstadisticaByPeliculaDtoOut> resultlist = new List<EstadisticaByPeliculaDtoOut>();
            foreach(Pelicula peli in pelicula)
            {
                var compras = await _servicecompra.GetComprasByPelicula(peli.IdP);
                EstadisticaByPeliculaDtoOut result = new EstadisticaByPeliculaDtoOut{pelicula = peli};
                foreach(Compra a in compras)
                {
                    result.TotalButacas += a.IdBs.Count;
                    var pago = await _servicepago.GetPago(a.IdPg);
                    if(pago is null) return BadRequest();
                    if(a.Tipo=="Tarjeta") 
                    {
                        result.ButacasTransferencia += a.IdBs.Count;
                        if(pago.Web is not null ) 
                        {
                            result.TotalTransferencia += pago.Web.Cantidad;
                            result.TotalDinero += pago.Web.Cantidad;
                        }
                    }
                    else if(a.Tipo=="Puntos")
                    { 
                        result.ButacasPuntos += a.IdBs.Count;
                        if(pago.Punto is not null )result.TotalPuntos += pago.Punto.Gastados;
                    }
                    else 
                    {
                        result.ButacasEfectivo +=a.IdBs.Count;
                        if(pago.Efectivo is not null)
                        {
                            result.TotalEfectivo += pago.Efectivo.CantidadE;
                            result.TotalDinero += pago.Efectivo.CantidadE;
                        }
                    }
                }
                resultlist.Add(result);
            }

            return Ok(resultlist.OrderByDescending(x=>x.TotalTransferencia));
        }
        [HttpGet("GetByPeliculaPuntos")]
        public async Task<ActionResult<IEnumerable<EstadisticaByPeliculaDtoOut>>> GetByPeliculaPuntos()
        {
            var pelicula=await _servicepelicula.GetPeliculas();
            List<EstadisticaByPeliculaDtoOut> resultlist = new List<EstadisticaByPeliculaDtoOut>();
            foreach(Pelicula peli in pelicula)
            {
                var compras = await _servicecompra.GetComprasByPelicula(peli.IdP);
                EstadisticaByPeliculaDtoOut result = new EstadisticaByPeliculaDtoOut{pelicula = peli};
                foreach(Compra a in compras)
                {
                    result.TotalButacas += a.IdBs.Count;
                    var pago = await _servicepago.GetPago(a.IdPg);
                    if(pago is null) return BadRequest();
                    if(a.Tipo=="Tarjeta") 
                    {
                        result.ButacasTransferencia += a.IdBs.Count;
                        if(pago.Web is not null ) 
                        {
                            result.TotalTransferencia += pago.Web.Cantidad;
                            result.TotalDinero += pago.Web.Cantidad;
                        }
                    }
                    else if(a.Tipo=="Puntos")
                    { 
                        result.ButacasPuntos += a.IdBs.Count;
                        if(pago.Punto is not null )result.TotalPuntos += pago.Punto.Gastados;
                    }
                    else 
                    {
                        result.ButacasEfectivo +=a.IdBs.Count;
                        if(pago.Efectivo is not null)
                        {
                            result.TotalEfectivo += pago.Efectivo.CantidadE;
                            result.TotalDinero += pago.Efectivo.CantidadE;
                        }
                    }
                }
                resultlist.Add(result);
            }

            return Ok(resultlist.OrderByDescending(x=>x.TotalPuntos));
        }

        [HttpGet("GetByPeliculaButacasEfectivo")]
        public async Task<ActionResult<IEnumerable<EstadisticaByPeliculaDtoOut>>> GetByPeliculaButacasEfectivo()
        {
            var pelicula=await _servicepelicula.GetPeliculas();
            List<EstadisticaByPeliculaDtoOut> resultlist = new List<EstadisticaByPeliculaDtoOut>();
            foreach(Pelicula peli in pelicula)
            {
                var compras = await _servicecompra.GetComprasByPelicula(peli.IdP);
                EstadisticaByPeliculaDtoOut result = new EstadisticaByPeliculaDtoOut{pelicula = peli};
                foreach(Compra a in compras)
                {
                    result.TotalButacas += a.IdBs.Count;
                    var pago = await _servicepago.GetPago(a.IdPg);
                    if(pago is null) return BadRequest();
                    if(a.Tipo=="Tarjeta") 
                    {
                        result.ButacasTransferencia += a.IdBs.Count;
                        if(pago.Web is not null ) 
                        {
                            result.TotalTransferencia += pago.Web.Cantidad;
                            result.TotalDinero += pago.Web.Cantidad;
                        }
                    }
                    else if(a.Tipo=="Puntos")
                    { 
                        result.ButacasPuntos += a.IdBs.Count;
                        if(pago.Punto is not null )result.TotalPuntos += pago.Punto.Gastados;
                    }
                    else 
                    {
                        result.ButacasEfectivo +=a.IdBs.Count;
                        if(pago.Efectivo is not null)
                        {
                            result.TotalEfectivo += pago.Efectivo.CantidadE;
                            result.TotalDinero += pago.Efectivo.CantidadE;
                        }
                    }
                }
                resultlist.Add(result);
            }

            return Ok(resultlist.OrderByDescending(x=>x.ButacasEfectivo));
        }

        [HttpGet("GetByPeliculaButacasTransferencia")]
        public async Task<ActionResult<IEnumerable<EstadisticaByPeliculaDtoOut>>> GetByPeliculaButacasTransferencia()
        {
            var pelicula=await _servicepelicula.GetPeliculas();
            List<EstadisticaByPeliculaDtoOut> resultlist = new List<EstadisticaByPeliculaDtoOut>();
            foreach(Pelicula peli in pelicula)
            {
                var compras = await _servicecompra.GetComprasByPelicula(peli.IdP);
                EstadisticaByPeliculaDtoOut result = new EstadisticaByPeliculaDtoOut{pelicula = peli};
                foreach(Compra a in compras)
                {
                    result.TotalButacas += a.IdBs.Count;
                    var pago = await _servicepago.GetPago(a.IdPg);
                    if(pago is null) return BadRequest();
                    if(a.Tipo=="Tarjeta") 
                    {
                        result.ButacasTransferencia += a.IdBs.Count;
                        if(pago.Web is not null ) 
                        {
                            result.TotalTransferencia += pago.Web.Cantidad;
                            result.TotalDinero += pago.Web.Cantidad;
                        }
                    }
                    else if(a.Tipo=="Puntos")
                    { 
                        result.ButacasPuntos += a.IdBs.Count;
                        if(pago.Punto is not null )result.TotalPuntos += pago.Punto.Gastados;
                    }
                    else 
                    {
                        result.ButacasEfectivo +=a.IdBs.Count;
                        if(pago.Efectivo is not null)
                        {
                            result.TotalEfectivo += pago.Efectivo.CantidadE;
                            result.TotalDinero += pago.Efectivo.CantidadE;
                        }
                    }
                }
                resultlist.Add(result);
            }

            return Ok(resultlist.OrderByDescending(x=>x.ButacasTransferencia));
        }

        [HttpGet("GetByPeliculaButacasPuntos")]
        public async Task<ActionResult<IEnumerable<EstadisticaByPeliculaDtoOut>>> GetByPeliculaButacasPuntos()
        {
            var pelicula=await _servicepelicula.GetPeliculas();
            List<EstadisticaByPeliculaDtoOut> resultlist = new List<EstadisticaByPeliculaDtoOut>();
            foreach(Pelicula peli in pelicula)
            {
                var compras = await _servicecompra.GetComprasByPelicula(peli.IdP);
                EstadisticaByPeliculaDtoOut result = new EstadisticaByPeliculaDtoOut{pelicula = peli};
                foreach(Compra a in compras)
                {
                    result.TotalButacas += a.IdBs.Count;
                    var pago = await _servicepago.GetPago(a.IdPg);
                    if(pago is null) return BadRequest();
                    if(a.Tipo=="Tarjeta") 
                    {
                        result.ButacasTransferencia += a.IdBs.Count;
                        if(pago.Web is not null ) 
                        {
                            result.TotalTransferencia += pago.Web.Cantidad;
                            result.TotalDinero += pago.Web.Cantidad;
                        }
                    }
                    else if(a.Tipo=="Puntos")
                    { 
                        result.ButacasPuntos += a.IdBs.Count;
                        if(pago.Punto is not null )result.TotalPuntos += pago.Punto.Gastados;
                    }
                    else 
                    {
                        result.ButacasEfectivo +=a.IdBs.Count;
                        if(pago.Efectivo is not null)
                        {
                            result.TotalEfectivo += pago.Efectivo.CantidadE;
                            result.TotalDinero += pago.Efectivo.CantidadE;
                        }
                    }
                }
                resultlist.Add(result);
            }

            return Ok(resultlist.OrderByDescending(x=>x.ButacasPuntos));
        }

        [HttpGet("GetByClienteDinero")]
        public async Task<ActionResult<IEnumerable<EstadisticaByClienteDtoOut>>> GetByClienteDinero()
        {
            var cliente=await _servicecliente.GetClientes();
            List<EstadisticaByClienteDtoOut> resultlist = new List<EstadisticaByClienteDtoOut>();
            foreach(Cliente cli in cliente)
            {
                var compras = await _servicecompra.GetComprasByCliente(cli.Ci);
                EstadisticaByClienteDtoOut result = new EstadisticaByClienteDtoOut{cliente = cli};
                foreach(Compra a in compras)
                {
                    result.TotalButacas += a.IdBs.Count;
                    var pago = await _servicepago.GetPago(a.IdPg);
                    if(pago is null) return BadRequest();
                    if(a.Tipo=="Tarjeta") 
                    {
                        result.ButacasTransferencia += a.IdBs.Count;
                        if(pago.Web is not null ) 
                        {
                            result.TotalTransferencia += pago.Web.Cantidad;
                            result.TotalDinero += pago.Web.Cantidad;
                        }
                    }
                    else if(a.Tipo=="Puntos")
                    { 
                        result.ButacasPuntos += a.IdBs.Count;
                        if(pago.Punto is not null )result.TotalPuntos += pago.Punto.Gastados;
                    }
                    else 
                    {
                        result.ButacasEfectivo +=a.IdBs.Count;
                        if(pago.Efectivo is not null)
                        {
                            result.TotalEfectivo += pago.Efectivo.CantidadE;
                            result.TotalDinero += pago.Efectivo.CantidadE;
                        }
                    }
                }
                resultlist.Add(result);
            }

            return Ok(resultlist.OrderByDescending(x=>x.TotalDinero));
        }

        [HttpGet("GetByClienteEfectivo")]
        public async Task<ActionResult<IEnumerable<EstadisticaByClienteDtoOut>>> GetByClienteEfectivo()
        {
            var cliente=await _servicecliente.GetClientes();
            List<EstadisticaByClienteDtoOut> resultlist = new List<EstadisticaByClienteDtoOut>();
            foreach(Cliente cli in cliente)
            {
                var compras = await _servicecompra.GetComprasByCliente(cli.Ci);
                EstadisticaByClienteDtoOut result = new EstadisticaByClienteDtoOut{cliente = cli};
                foreach(Compra a in compras)
                {
                    result.TotalButacas += a.IdBs.Count;
                    var pago = await _servicepago.GetPago(a.IdPg);
                    if(pago is null) return BadRequest();
                    if(a.Tipo=="Tarjeta") 
                    {
                        result.ButacasTransferencia += a.IdBs.Count;
                        if(pago.Web is not null ) 
                        {
                            result.TotalTransferencia += pago.Web.Cantidad;
                            result.TotalDinero += pago.Web.Cantidad;
                        }
                    }
                    else if(a.Tipo=="Puntos")
                    { 
                        result.ButacasPuntos += a.IdBs.Count;
                        if(pago.Punto is not null )result.TotalPuntos += pago.Punto.Gastados;
                    }
                    else 
                    {
                        result.ButacasEfectivo +=a.IdBs.Count;
                        if(pago.Efectivo is not null)
                        {
                            result.TotalEfectivo += pago.Efectivo.CantidadE;
                            result.TotalDinero += pago.Efectivo.CantidadE;
                        }
                    }
                }
                resultlist.Add(result);
            }

            return Ok(resultlist.OrderByDescending(x=>x.TotalEfectivo));
        }

        [HttpGet("GetByClienteTransferencia")]
        public async Task<ActionResult<IEnumerable<EstadisticaByClienteDtoOut>>> GetByClienteTransferencia()
        {
            var cliente=await _servicecliente.GetClientes();
            List<EstadisticaByClienteDtoOut> resultlist = new List<EstadisticaByClienteDtoOut>();
            foreach(Cliente cli in cliente)
            {
                var compras = await _servicecompra.GetComprasByCliente(cli.Ci);
                EstadisticaByClienteDtoOut result = new EstadisticaByClienteDtoOut{cliente = cli};
                foreach(Compra a in compras)
                {
                    result.TotalButacas += a.IdBs.Count;
                    var pago = await _servicepago.GetPago(a.IdPg);
                    if(pago is null) return BadRequest();
                    if(a.Tipo=="Tarjeta") 
                    {
                        result.ButacasTransferencia += a.IdBs.Count;
                        if(pago.Web is not null ) 
                        {
                            result.TotalTransferencia += pago.Web.Cantidad;
                            result.TotalDinero += pago.Web.Cantidad;
                        }
                    }
                    else if(a.Tipo=="Puntos")
                    { 
                        result.ButacasPuntos += a.IdBs.Count;
                        if(pago.Punto is not null )result.TotalPuntos += pago.Punto.Gastados;
                    }
                    else 
                    {
                        result.ButacasEfectivo +=a.IdBs.Count;
                        if(pago.Efectivo is not null)
                        {
                            result.TotalEfectivo += pago.Efectivo.CantidadE;
                            result.TotalDinero += pago.Efectivo.CantidadE;
                        }
                    }
                }
                resultlist.Add(result);
            }

            return Ok(resultlist.OrderByDescending(x=>x.TotalTransferencia));
        }

        [HttpGet("GetByClientePuntos")]
        public async Task<ActionResult<IEnumerable<EstadisticaByClienteDtoOut>>> GetByClientePuntos()
        {
            var cliente=await _servicecliente.GetClientes();
            List<EstadisticaByClienteDtoOut> resultlist = new List<EstadisticaByClienteDtoOut>();
            foreach(Cliente cli in cliente)
            {
                var compras = await _servicecompra.GetComprasByCliente(cli.Ci);
                EstadisticaByClienteDtoOut result = new EstadisticaByClienteDtoOut{cliente = cli};
                foreach(Compra a in compras)
                {
                    result.TotalButacas += a.IdBs.Count;
                    var pago = await _servicepago.GetPago(a.IdPg);
                    if(pago is null) return BadRequest();
                    if(a.Tipo=="Tarjeta") 
                    {
                        result.ButacasTransferencia += a.IdBs.Count;
                        if(pago.Web is not null ) 
                        {
                            result.TotalTransferencia += pago.Web.Cantidad;
                            result.TotalDinero += pago.Web.Cantidad;
                        }
                    }
                    else if(a.Tipo=="Puntos")
                    { 
                        result.ButacasPuntos += a.IdBs.Count;
                        if(pago.Punto is not null )result.TotalPuntos += pago.Punto.Gastados;
                    }
                    else 
                    {
                        result.ButacasEfectivo +=a.IdBs.Count;
                        if(pago.Efectivo is not null)
                        {
                            result.TotalEfectivo += pago.Efectivo.CantidadE;
                            result.TotalDinero += pago.Efectivo.CantidadE;
                        }
                    }
                }
                resultlist.Add(result);
            }

            return Ok(resultlist.OrderByDescending(x=>x.TotalPuntos));
        }

        [HttpGet("GetByClienteButacas")]
        public async Task<ActionResult<IEnumerable<EstadisticaByClienteDtoOut>>> GetByClienteButacas()
        {
            var cliente=await _servicecliente.GetClientes();
            List<EstadisticaByClienteDtoOut> resultlist = new List<EstadisticaByClienteDtoOut>();
            foreach(Cliente cli in cliente)
            {
                var compras = await _servicecompra.GetComprasByCliente(cli.Ci);
                EstadisticaByClienteDtoOut result = new EstadisticaByClienteDtoOut{cliente = cli};
                foreach(Compra a in compras)
                {
                    result.TotalButacas += a.IdBs.Count;
                    var pago = await _servicepago.GetPago(a.IdPg);
                    if(pago is null) return BadRequest();
                    if(a.Tipo=="Tarjeta") 
                    {
                        result.ButacasTransferencia += a.IdBs.Count;
                        if(pago.Web is not null ) 
                        {
                            result.TotalTransferencia += pago.Web.Cantidad;
                            result.TotalDinero += pago.Web.Cantidad;
                        }
                    }
                    else if(a.Tipo=="Puntos")
                    { 
                        result.ButacasPuntos += a.IdBs.Count;
                        if(pago.Punto is not null )result.TotalPuntos += pago.Punto.Gastados;
                    }
                    else 
                    {
                        result.ButacasEfectivo +=a.IdBs.Count;
                        if(pago.Efectivo is not null)
                        {
                            result.TotalEfectivo += pago.Efectivo.CantidadE;
                            result.TotalDinero += pago.Efectivo.CantidadE;
                        }
                    }
                }
                resultlist.Add(result);
            }

            return Ok(resultlist.OrderByDescending(x=>x.TotalButacas));
        }

        [HttpGet("GetByClienteButacasEfectivo")]
        public async Task<ActionResult<IEnumerable<EstadisticaByClienteDtoOut>>> GetByClienteButacasEfectivo()
        {
            var cliente=await _servicecliente.GetClientes();
            List<EstadisticaByClienteDtoOut> resultlist = new List<EstadisticaByClienteDtoOut>();
            foreach(Cliente cli in cliente)
            {
                var compras = await _servicecompra.GetComprasByCliente(cli.Ci);
                EstadisticaByClienteDtoOut result = new EstadisticaByClienteDtoOut{cliente = cli};
                foreach(Compra a in compras)
                {
                    result.TotalButacas += a.IdBs.Count;
                    var pago = await _servicepago.GetPago(a.IdPg);
                    if(pago is null) return BadRequest();
                    if(a.Tipo=="Tarjeta") 
                    {
                        result.ButacasTransferencia += a.IdBs.Count;
                        if(pago.Web is not null ) 
                        {
                            result.TotalTransferencia += pago.Web.Cantidad;
                            result.TotalDinero += pago.Web.Cantidad;
                        }
                    }
                    else if(a.Tipo=="Puntos")
                    { 
                        result.ButacasPuntos += a.IdBs.Count;
                        if(pago.Punto is not null )result.TotalPuntos += pago.Punto.Gastados;
                    }
                    else 
                    {
                        result.ButacasEfectivo +=a.IdBs.Count;
                        if(pago.Efectivo is not null)
                        {
                            result.TotalEfectivo += pago.Efectivo.CantidadE;
                            result.TotalDinero += pago.Efectivo.CantidadE;
                        }
                    }
                }
                resultlist.Add(result);
            }

            return Ok(resultlist.OrderByDescending(x=>x.ButacasEfectivo));
        }

        [HttpGet("GetByClienteButacasTransferencia")]
        public async Task<ActionResult<IEnumerable<EstadisticaByClienteDtoOut>>> GetByClienteButacasTransferencia()
        {
            var cliente=await _servicecliente.GetClientes();
            List<EstadisticaByClienteDtoOut> resultlist = new List<EstadisticaByClienteDtoOut>();
            foreach(Cliente cli in cliente)
            {
                var compras = await _servicecompra.GetComprasByCliente(cli.Ci);
                EstadisticaByClienteDtoOut result = new EstadisticaByClienteDtoOut{cliente = cli};
                foreach(Compra a in compras)
                {
                    result.TotalButacas += a.IdBs.Count;
                    var pago = await _servicepago.GetPago(a.IdPg);
                    if(pago is null) return BadRequest();
                    if(a.Tipo=="Tarjeta") 
                    {
                        result.ButacasTransferencia += a.IdBs.Count;
                        if(pago.Web is not null ) 
                        {
                            result.TotalTransferencia += pago.Web.Cantidad;
                            result.TotalDinero += pago.Web.Cantidad;
                        }
                    }
                    else if(a.Tipo=="Puntos")
                    { 
                        result.ButacasPuntos += a.IdBs.Count;
                        if(pago.Punto is not null )result.TotalPuntos += pago.Punto.Gastados;
                    }
                    else 
                    {
                        result.ButacasEfectivo +=a.IdBs.Count;
                        if(pago.Efectivo is not null)
                        {
                            result.TotalEfectivo += pago.Efectivo.CantidadE;
                            result.TotalDinero += pago.Efectivo.CantidadE;
                        }
                    }
                }
                resultlist.Add(result);
            }

            return Ok(resultlist.OrderByDescending(x=>x.ButacasTransferencia));
        }

        [HttpGet("GetByClienteButacasPuntos")]
        public async Task<ActionResult<IEnumerable<EstadisticaByClienteDtoOut>>> GetByClienteButacasPuntos()
        {
            var cliente=await _servicecliente.GetClientes();
            List<EstadisticaByClienteDtoOut> resultlist = new List<EstadisticaByClienteDtoOut>();
            foreach(Cliente cli in cliente)
            {
                var compras = await _servicecompra.GetComprasByCliente(cli.Ci);
                EstadisticaByClienteDtoOut result = new EstadisticaByClienteDtoOut{cliente = cli};
                foreach(Compra a in compras)
                {
                    result.TotalButacas += a.IdBs.Count;
                    var pago = await _servicepago.GetPago(a.IdPg);
                    if(pago is null) return BadRequest();
                    if(a.Tipo=="Tarjeta") 
                    {
                        result.ButacasTransferencia += a.IdBs.Count;
                        if(pago.Web is not null ) 
                        {
                            result.TotalTransferencia += pago.Web.Cantidad;
                            result.TotalDinero += pago.Web.Cantidad;
                        }
                    }
                    else if(a.Tipo=="Puntos")
                    { 
                        result.ButacasPuntos += a.IdBs.Count;
                        if(pago.Punto is not null )result.TotalPuntos += pago.Punto.Gastados;
                    }
                    else 
                    {
                        result.ButacasEfectivo +=a.IdBs.Count;
                        if(pago.Efectivo is not null)
                        {
                            result.TotalEfectivo += pago.Efectivo.CantidadE;
                            result.TotalDinero += pago.Efectivo.CantidadE;
                        }
                    }
                }
                resultlist.Add(result);
            }

            return Ok(resultlist.OrderByDescending(x=>x.ButacasPuntos));
        }
    } 
}
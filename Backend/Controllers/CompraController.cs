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

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraController : ControllerBase
    {
        private readonly ServiceCompra _servicecompra;
        private readonly ServiceCliente _servicecliente;
        private readonly ServiceSesion _servicesesion;
        private readonly ServiceButaca _servicebutaca;
        private readonly ServiceDescuento _servicedescuento;
        private readonly ServiceTarjetum _servicetarjetum;

        public CompraController(ServiceCompra servicecompra,ServiceCliente servicecliente,ServiceSesion servicesesion,ServiceButaca servicebutaca,ServiceDescuento servicedescuento,ServiceTarjetum _servicetarjetum)
        {
            _servicecompra = servicecompra;
            _servicecliente=servicecliente;
            _servicesesion=servicesesion;  
            _servicebutaca=servicebutaca;
            _servicedescuento=servicedescuento;
            _servicetarjetum= servicetarjetum;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Compra>>> GetCompras()
        {
            return Ok(await _servicecompra.GetCompras());
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Compra>> GetCompra(int id)
        {
            var compra = await _servicecompra.GetCompra(id);

            if (compra == null)
            {
                return NotFound();
            }

            return compra;
        }


        [HttpPut("Update/{id}")]
        public async Task<IActionResult> PutCompra(int id, Compra compra)
        {
            if (id != compra.IdP)
            {
                return BadRequest();
            }

            try
            {
                await _servicecompra.PutCompra(compra);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompraExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Compra>> CompraByUserTarjeta(CompraByUserTarjetaDtoIn compra)
        {   
            var tarjetaExis = await _servicetarjetum.GetTarjetum(compra.CodigoT);
            if(tarjetaExis==null)
            {
                var tarjeta = new Tarjetum
                {
                    CodigoT=compra.CodigoT,
                    Ci=compra.Ci
                };
                await _servicetarjetum.PostTarjetum(tarjeta);
            }
            
            var pago = new Pago();
            pago.Web=new Web{ IdPg = pago.IdPg, CodigoT=compra.CodigoT,Cantidad=compra.Cantidad};
            var cliente = await _servicecliente.GetCliente(compra.Ci);
            var sesion = await _servicesesion.GetSesionIdPIdSF(compra.IdP,compra.IdS,compra.Fecha);
            List<Butaca> butaca=new List<Butaca>();

            foreach(int a in compra.IdB)
            {
                butaca.Add(await _servicebutaca.GetButaca(a));
            }
            List<Descuento> descuentos=new List<Descuento>();

            foreach(int a in compra.IdD)
            {
                descuentos.Add(await _servicedescuento.GetDescuento(a));
            }

            if(cliente is null || sesion is null) return NotFound();

            var ticket = new Compra
            {
                IdP=compra.IdP,
                IdS=compra.IdS,
                Fecha=compra.Fecha,
                Ci=compra.Ci,
                IdPg=pago.IdPg,
                Tipo="",
                FechaDeCompra=compra.FechaDeCompra,
                MedioAd="Web",
                IdPgNavigation=pago,
                CiNavigation = cliente,
                Sesion=sesion,
                IdBs=butaca,
                IdDs=descuentos
            };
            try
            {
                await _servicecompra.PostCompra(ticket);
            }
            catch (DbUpdateException)
            {
                if (CompraExists(compra.IdP))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCompra", new { id = ticket.IdP }, ticket);
        }

        [HttpDelete("Delete/{IdP}/{IdS}/{Fecha}/{Ci}/{IdPg}")]
        public async Task<IActionResult> DeleteCompra(int IdP,int IdS,DateTime Fecha,int Ci,int IdPg)
        {
            var compra = await _servicecompra.GetCompraByAll(IdP,IdS,Fecha,Ci,IdPg);
            if (compra == null)
            {
                return NotFound();
            }
            await _servicecompra.DeleteCompra(compra);
            return NoContent();
        }

        private bool CompraExists(int id)
        {
            return _servicecompra.GetCompra(id)!=null;
        }
    } 
}
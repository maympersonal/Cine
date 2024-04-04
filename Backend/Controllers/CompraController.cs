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
    public class CompraController : ControllerBase
    {
        private readonly ServiceCompra _servicecompra;
        private readonly ServiceCliente _servicecliente;
        private readonly ServiceSesion _servicesesion;
        private readonly ServiceButaca _servicebutaca;
        private readonly ServiceDescuento _servicedescuento;
        private readonly ServiceTarjetum _servicetarjetum;
        private readonly ServiceUsuario _serviceusuario;
        private readonly ServicePago _servicepago;

        public CompraController(ServiceCompra servicecompra,ServiceCliente servicecliente,ServiceSesion servicesesion,ServiceButaca servicebutaca,ServiceDescuento servicedescuento,ServiceTarjetum servicetarjetum,ServiceUsuario serviceusuario,ServicePago servicepago)
        {
            _servicecompra = servicecompra;
            _servicecliente=servicecliente;
            _servicesesion=servicesesion;  
            _servicebutaca=servicebutaca;
            _servicedescuento=servicedescuento;
            _servicetarjetum= servicetarjetum;
            _serviceusuario = serviceusuario;
            _servicepago = servicepago;
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

        [HttpPost("CompraByUserTarjeta")]
        public async Task<ActionResult<Compra>> CompraByUserTarjeta(CompraByUserTarjetaDtoIn compra)
        {   
            var usuario = await _serviceusuario.GetUsuario(compra.Ci);

            if(usuario is null) return NotFound("El usuario no existe.");

            if(usuario.CiNavigation.Confiabilidad is false && compra.IdD.Count>0) return BadRequest("El usuario no puede asignarse descuentos.");

            List<Butaca> butaca=new List<Butaca>();

            if(compra.IdB.Count == 0) return BadRequest("No estas seleccionando ninguna butaca.");

            foreach(int a in compra.IdB)
            {
                var but = await _servicebutaca.GetButaca(a);
                if(but is null) return BadRequest("La butaca con Id "+a+" no existe.");
                butaca.Add(but);
            }

            var sesion = await _servicesesion.GetSesionIdPIdSF(compra.IdP,compra.IdS,compra.Fecha);

            List<Descuento> descuentos=new List<Descuento>();

            foreach(int a in compra.IdD)
            {
                var des = await _servicedescuento.GetDescuento(a);
                if(des is null) return BadRequest("El descuento con Id "+a+" no existe.");
                descuentos.Add(des);
            }

            if(sesion is null) return NotFound("La sesion no existe.");

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

            var ticket = new Compra
            {
                IdP=compra.IdP,
                IdS=compra.IdS,
                Fecha=compra.Fecha,
                Ci=compra.Ci,
                IdPg=pago.IdPg,
                Tipo="Tarjeta",
                FechaDeCompra=compra.FechaDeCompra,
                MedioAd="Web",
                IdPgNavigation=pago,
                CiNavigation = usuario.CiNavigation,
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


            
            if(usuario.Rol != "Cliente")
            {
                usuario.Puntos= usuario.Puntos + 5* compra.IdB.Count;
                await _serviceusuario.PutUsuario(usuario);
            }
            else
            {
                if(usuario.CiNavigation.Compras.Count >= 10)
                {
                    usuario.Rol="Socio";
                    usuario.Puntos= usuario.Puntos + 5* compra.IdB.Count;
                    await _serviceusuario.PutUsuario(usuario);
                }
            }
        

            return CreatedAtAction("GetCompra", new { id = ticket.IdP }, ticket);
        }

        [HttpPost("CompraByTaquillaEfectivo")]
        public async Task<ActionResult<Compra>> CompraByTaquillaEfectivo(CompraByUserTaquillaDtoIn compra)
        {   
            var taquillero = await _serviceusuario.GetUsuario(compra.CiTaquillero);
            if(taquillero is null || taquillero.Rol =="Cliente" || taquillero.Rol == "Socio") return BadRequest("El usuario que efectua la venta no existe o no es taquillero.");

            if(compra.IdB.Count == 0) return BadRequest("No estas seleccionando ninguna butaca.");

            List<Butaca> butaca=new List<Butaca>();

            foreach(int a in compra.IdB)
            {
                var but = await _servicebutaca.GetButaca(a);
                if(but is null) return BadRequest("La butaca con Id "+a+" no existe.");
                butaca.Add(but);
            }

            List<Descuento> descuentos=new List<Descuento>();

            foreach(int a in compra.IdD)
            {
                var des = await _servicedescuento.GetDescuento(a);
                if(des is null) return BadRequest("El descuento con Id "+a+" no existe.");
                descuentos.Add(des);
            }

            var sesion = await _servicesesion.GetSesionIdPIdSF(compra.IdP,compra.IdS,compra.Fecha);            

            if(sesion is null) return NotFound("La sesion no existe.");

            var cliente = await _servicecliente.GetCliente(compra.Ci);
            if(cliente is null) 
            {
                cliente = new Cliente
                {
                    Ci=compra.Ci,
                    Correo=compra.Correo,
                    Confiabilidad=true
                };
                await _servicecliente.PostCliente(cliente);
            }
            if(cliente.Confiabilidad is false && compra.IdD.Count>0) return BadRequest("El cliente no puede asignarse desceuntos.");
            var pago = new Pago();
            pago.Efectivo=new Efectivo{IdPg=pago.IdPg,CantidadE=compra.Cantidad};

            var ticket = new Compra
            {
                IdP=compra.IdP,
                IdS=compra.IdS,
                Fecha=compra.Fecha,
                Ci=compra.Ci,
                IdPg=pago.IdPg,
                Tipo="Efectivo",
                FechaDeCompra=compra.FechaDeCompra,
                MedioAd=compra.CiTaquillero,
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
            var usuario = await _serviceusuario.GetUsuario(compra.Ci);
            if(usuario is not null)
            {
                if(usuario.Rol != "Cliente")
                {
                    usuario.Puntos= usuario.Puntos + 5* compra.IdB.Count;
                    await _serviceusuario.PutUsuario(usuario);
                }
                else
                {
                    if(usuario.CiNavigation.Compras.Count >= 10)
                    {
                        usuario.Rol="Socio";
                        usuario.Puntos= usuario.Puntos + 5* compra.IdB.Count;
                        await _serviceusuario.PutUsuario(usuario);
                    }
                }
            }
           
            return CreatedAtAction("GetCompra", new { id = ticket.IdP }, ticket);
        }

        [HttpPost("CompraByUserPuntos")]
        public async Task<ActionResult<Compra>> CompraByUserPuntos(CompraByUserPuntoDtoIn compra)
        {   
            var usuario = await _serviceusuario.GetUsuario(compra.Ci);
            if(usuario is null) return NotFound("El usuario no existe.");

            if(usuario.Puntos < compra.Cantidad) return BadRequest("El usuario no tiene suficientes puntos.");

            var sesion = await _servicesesion.GetSesionIdPIdSF(compra.IdP,compra.IdS,compra.Fecha);
            List<Butaca> butaca=new List<Butaca>();

            if(compra.IdB.Count == 0) return BadRequest("No estas seleccionando ninguna butaca.");

            var pago = new Pago();

            pago.Punto=new Punto{ IdPg = pago.IdPg,Gastados=compra.Cantidad};

            foreach(int a in compra.IdB)
            {
                var but = await _servicebutaca.GetButaca(a);
                if(but is null) return NotFound("La butaca con Id "+a+" no existe.");
                butaca.Add(but);
            }

            if(sesion is null) return NotFound("La sesion no existe.");

            var ticket = new Compra
            {
                IdP=compra.IdP,
                IdS=compra.IdS,
                Fecha=compra.Fecha,
                Ci=compra.Ci,
                IdPg=pago.IdPg,
                Tipo="Puntos",
                FechaDeCompra=compra.FechaDeCompra,
                IdPgNavigation=pago,
                MedioAd="Web",
                CiNavigation = usuario.CiNavigation,
                Sesion=sesion,
                IdBs=butaca
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

            usuario.Puntos = usuario.Puntos-compra.Cantidad;
            await _serviceusuario.PutUsuario(usuario);

            return CreatedAtAction("GetCompra", new { id = ticket.IdP }, ticket);
        }

        [HttpDelete("Delete/{IdP}/{IdS}/{Fecha}/{Ci}/{IdPg}")]
        public async Task<IActionResult> DeleteCompra(int IdP,int IdS,DateTime Fecha,string Ci,int IdPg)
        {
            var compra = await _servicecompra.GetCompraByAll(IdP,IdS,Fecha,Ci,IdPg);
            if (compra == null)
            {
                return NotFound("No se encuentra este ticket.");
            }

            var usuario = await _serviceusuario.GetUsuario(compra.Ci);
            if(compra.Tipo=="Puntos")
            {
                var pago = await _servicepago.GetPago(compra.IdPg);
                if(usuario is not null && pago is not null && pago.Punto is not null) usuario.Puntos = usuario.Puntos + pago.Punto.Gastados;
                else return BadRequest("El pago no fue hecho.");
                await _serviceusuario.PutUsuario(usuario);
            }
            else if(usuario is not null && usuario.Rol !="Cliente")
            {
                if(usuario.Puntos - compra.IdBs.Count*5 < 0) return BadRequest("El usuario ha gastado todos los puntos asignados por esta compra.");
                usuario.Puntos = usuario.Puntos - compra.IdBs.Count*5;
                await _serviceusuario.PutUsuario(usuario);
            }

            await _servicecompra.DeleteCompra(IdP,IdS,Fecha,Ci,IdPg);
            return NoContent();
        }

        private bool CompraExists(int id)
        {
            return _servicecompra.GetCompra(id)!=null;
        }
    } 
}
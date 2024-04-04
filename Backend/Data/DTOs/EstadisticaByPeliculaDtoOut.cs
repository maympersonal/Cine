using Backend.Models;

namespace Backend.Data.DTOs;

public class EstadisticaByPeliculaDtoOut
{
    public decimal? TotalDinero{get;set;}

    public decimal? TotalEfectivo {get;set;}

    public decimal? TotalTransferencia {get;set;}

    public int? TotalPuntos {get;set;}

    public int TotalButacas {get;set;}

    public int ButacasEfectivo {get;set;}

    public int ButacasTransferencia{get;set;}

    public int ButacasPuntos{get;set;}

    public Pelicula pelicula {get;set;} =null!; 
}
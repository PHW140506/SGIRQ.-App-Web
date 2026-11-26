using EntidadPersonal = global::Sgirq.Domain.Entities.Personal;
using Sgirq.Application.Abstractions;
using Sgirq.Application.Reactivos;
using Sgirq.Application.Personal;
using Sgirq.Domain.Entities;
using Sgirq.Application.Reactivos.Crear;
using Sgirq.Application.Reactivos.Editar;
namespace Sgirq.Application.Common;
public sealed class EntityMapper : IMapper
{
    public ReactivoDto ReactivoDto(Reactivo r) => new(r.IdReactivo, r.NombreComercial,
        r.NombreQuimico, r.NumeroCAS, r.EstadoFisico, r.CantidadReactivo,
        r.UnidadMedida, r.StockMinimo, r.ColorAnaquel, r.IdPersonal);
    public Reactivo CrearReactivo(CrearReactivoCommand x) => new()
    {
        NombreComercial = x.NombreComercial.Trim(), NombreQuimico = x.NombreQuimico.Trim(),
        NumeroCAS = x.NumeroCAS.Trim(), EstadoFisico = x.EstadoFisico,
        CantidadReactivo = x.CantidadReactivo, UnidadMedida = x.UnidadMedida,
        StockMinimo = x.StockMinimo, ColorAnaquel = x.ColorAnaquel, IdPersonal = x.IdPersonal
    };
    public void ActualizarReactivo(EditarReactivoDatos x, Reactivo r)
    {
        r.NombreComercial = x.NombreComercial.Trim();
        r.NombreQuimico = x.NombreQuimico.Trim();
        r.NumeroCAS = x.NumeroCAS.Trim();
        r.EstadoFisico = x.EstadoFisico;
        r.UnidadMedida = x.UnidadMedida;
        r.StockMinimo = x.StockMinimo;
        r.ColorAnaquel = x.ColorAnaquel;
        r.IdPersonal = x.IdPersonal;
    }
    public EntidadPersonal CrearPersonal(Sgirq.Application.Personal.Crear.CrearPersonalCommand x, string hash) => new()
    {
        NombrePersonal = x.NombrePersonal.Trim(), ApellidosPersonal = x.ApellidosPersonal.Trim(),
        UsuarioPersonalLogin = x.UsuarioPersonalLogin.Trim(), Contrasenia = hash,
        TipoPersonal = x.TipoPersonal
    };
    public void ActualizarPersonal(Sgirq.Application.Personal.Editar.EditarPersonalDatos x, EntidadPersonal p)
    {
        p.NombrePersonal = x.NombrePersonal.Trim();
        p.ApellidosPersonal = x.ApellidosPersonal.Trim();
        p.TipoPersonal = x.TipoPersonal;
    }
    public PersonalDto PersonalDto(EntidadPersonal p) => new(p.IdPersonal, p.NombrePersonal,
        p.ApellidosPersonal, p.UsuarioPersonalLogin, p.TipoPersonal);
}

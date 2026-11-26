using EntidadPersonal = global::Sgirq.Domain.Entities.Personal;
using Sgirq.Domain.Entities;
using Sgirq.Application.Reactivos.Crear;
using Sgirq.Application.Reactivos.Editar;
using Sgirq.Application.Reactivos;
using Sgirq.Application.Personal;
namespace Sgirq.Application.Abstractions;
public interface IMapper
{
    ReactivoDto ReactivoDto(Reactivo reactivo);
    Reactivo CrearReactivo(CrearReactivoCommand command);
    void ActualizarReactivo(EditarReactivoDatos datos, Reactivo reactivo);
    PersonalDto PersonalDto(EntidadPersonal personal);
    EntidadPersonal CrearPersonal(Sgirq.Application.Personal.Crear.CrearPersonalCommand comando, string hash);
    void ActualizarPersonal(Sgirq.Application.Personal.Editar.EditarPersonalDatos datos, EntidadPersonal personal);
}

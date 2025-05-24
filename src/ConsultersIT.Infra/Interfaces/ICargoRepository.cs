using ConsultersIT.Common.ViewModels;

namespace ConsultersIT.Infra.Interfaces;

public interface ICargoRepository
{
    List<CargoViewModel> GetAllCargos();
}

using AutoMapper;
using BtcDemo.Core.UnitOfWorks;

namespace BtcDemo.Service.Services;

public class ServiceBase
{
	protected IUnitOfWork UnitOfWork { get; }
	protected IMapper Mapper { get; }

	public ServiceBase(IUnitOfWork unitOfWork, IMapper mapper)
	{
		UnitOfWork = unitOfWork;
		Mapper = mapper;
	}
}

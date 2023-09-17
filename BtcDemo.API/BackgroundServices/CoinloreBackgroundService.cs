using BtcDemo.API.Service;

namespace BtcDemo.API.BackgroundServices;

// bunun yerine RabbitMQ kullanmak sanırım daha mantıklı.
// fakat sonradan aklıma geldi RabbitMQ :)
public class CoinloreBackgroundService : BackgroundService
{
	public IServiceProvider Services { get; }

	public CoinloreBackgroundService(IServiceProvider services)
	{
		Services = services;
	}
	//public override Task StartAsync(CancellationToken cancellationToken)
	//{
	//	return base.StartAsync(cancellationToken);
	//}

	//public override Task StopAsync(CancellationToken cancellationToken)
	//{
	//	return base.StopAsync(cancellationToken);
	//}

	protected async override Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (!stoppingToken.IsCancellationRequested) {
			await DoWork(stoppingToken);
			await Task.Delay(60000, stoppingToken);
		}
	}

	private async Task DoWork(CancellationToken stoppingToken)
	{
		using (var scope = Services.CreateScope())
		{
			var coinloreService =
				scope.ServiceProvider
					.GetRequiredService<ICoinloreService>();

			//var unitOfWork = scope.ServiceProvider
			//		.GetRequiredService<IUnitOfWork>();

			//var coinService = scope.ServiceProvider
			//		.GetRequiredService<ICoinService>();

			//var mapper = scope.ServiceProvider
			//		.GetRequiredService<IMapper>();

			var coin = await coinloreService.GetBitcoinAsync(stoppingToken);
		}

    }
}

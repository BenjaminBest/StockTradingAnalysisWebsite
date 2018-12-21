using System;
using System.Linq;
using AutoMapper;
using StockTradingAnalysis.Core.Common;
using StockTradingAnalysis.Interfaces.Common;

namespace StockTradingAnalysis.Web.BootModules
{
	public class AutoMapperBootModule : IBootModule
	{
		public int Priority => 0;

		public void Boot()
		{
			Mapper.Initialize(configuration =>
			{
				var profiles = TypeHelper.FindNonAbstractTypes<Profile>("StockTradingAnalysis.");

				profiles
					.Where(p => p != typeof(Profile))
					// Profile "base" class is not abstract, so it is found, but should be excluded
					.ToList()
					.ForEach(p => configuration.AddProfile(Activator.CreateInstance(p) as Profile));
			});

			Mapper.AssertConfigurationIsValid();
		}
	}
}
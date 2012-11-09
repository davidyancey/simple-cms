using System.Web.Mvc;
using MvcMembership;

[assembly: WebActivator.PreApplicationStartMethod(typeof(AgileDevDays.Web.App_Start.MvcMembership), "Start")]

namespace AgileDevDays.Web.App_Start
{
	public static class MvcMembership
	{
		public static void Start()
		{
			GlobalFilters.Filters.Add(new TouchUserOnEachVisitFilter());
		}
	}
}

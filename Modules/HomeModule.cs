using Nancy;
using System.Collections.Generic;
using Review.Objects;

namespace Stylist
{
	public class HomeModule : NancyModule
	{
		public HomeModule()
		{
			Get["/"] = _ =>
			{
				return View["index.cshtml"];
			};
			Get["/add-new-stylist"] = _ => {
				return View["add-new-stylist.cshtml"];
			};
			Get["add-new-stylist"] = _ =>
			{
				return View["index.cshtml"];
			};
		}
	}
}

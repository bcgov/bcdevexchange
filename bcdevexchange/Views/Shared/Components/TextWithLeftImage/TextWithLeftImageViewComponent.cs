using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace bcdevexchange.Components.TextWithLeftImage 
{
  public class TextWithLeftImageViewComponent : ViewComponent
  {
    public TextWithLeftImageViewComponent()
    {

    }

    // public IViewComponentResult Invoke(string textWithLeftImageControlType) 
    // {
    //   return View("Default", textWithLeftImageControlType);
    // }

    public IViewComponentResult Invoke(string title, string text) 
    {
      return View("Default", textWithLeftImageControlType);
    }
  }

}

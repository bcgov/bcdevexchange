using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using bcdevexchange.Models;



namespace bcdevexchange.Components.TextWithLeftImage 
{
  public class TextWithLeftImageModel 
  {
    public bool TextOnLeft {get; set;}
    public string Title {get; set;}
    public string SubTitle {get; set;}
    public string ImageFilePath {get; set;}
    public string ButtonUrl {get; set;}

  }

  public class TextWithLeftImageViewComponent : ViewComponent
  {
    public TextWithLeftImageViewComponent()
    {

    }

    // public IViewComponentResult Invoke(string textWithLeftImageControlType) 
    // {
    //   return View("Default", textWithLeftImageControlType);
    // }

    public IViewComponentResult Invoke(bool textOnLeft, string title, string subTitle, string buttonUrl, string imageFilePath) 
    {

      
      TextWithLeftImageModel textWithLeftImageModel = new TextWithLeftImageModel
      {
        TextOnLeft = textOnLeft,
        Title = title, 
        SubTitle = subTitle, 
        ButtonUrl = buttonUrl, 
        ImageFilePath = imageFilePath
      };

      return View("Default", textWithLeftImageModel);
    }
  }

}

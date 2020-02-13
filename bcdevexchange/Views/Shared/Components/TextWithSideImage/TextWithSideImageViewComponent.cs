using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using bcdevexchange.Models;



namespace bcdevexchange.Components.TextWithSideImage 
{
  public class TextWithSideImageModel 
  {
    public bool TextOnLeft {get; set;}
    public string Title {get; set;}
    public string SubTitle {get; set;}
    public string ImageFilePath {get; set;}
    public string ButtonUrl {get; set;}

  }

  public class TextWithSideImageViewComponent : ViewComponent
  {
    public TextWithSideImageViewComponent()
    {

    }

    public IViewComponentResult Invoke(string subTitle, 
                                       string buttonUrl, 
                                       string imageFilePath="",
                                       bool textOnLeft=false, 
                                       string title="") 
    {

      
      TextWithSideImageModel textWithSideImageModel = new TextWithSideImageModel
      {
        TextOnLeft = textOnLeft,
        Title = title, 
        SubTitle = subTitle, 
        ButtonUrl = buttonUrl, 
        ImageFilePath = imageFilePath
      };

      return View("Default", textWithSideImageModel);
    }
  }

}

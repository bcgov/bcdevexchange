using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using bcdevexchange.Models;



namespace bcdevexchange.Components.PersonnelCard 
{
  public class PersonnelCardModel 
  {
    // Mandatory fields
    public string Name {get; set;}
    public string Role {get; set;}
    public string ImageFileName {get; set;}
    // Optional Fields
    public string Email {get; set;}
    public string TwitterLink {get; set;}
    public string LinkedIn {get; set;}
  }

  public class PersonnelCardViewComponent : ViewComponent
  {
    public PersonnelCardViewComponent()
    {

    }

    public IViewComponentResult Invoke(string name,
                                       string role, 
                                       string imageFileName,
                                       string twitterLink="",
                                       string linkedIn="",
                                       string email="") 
    {

      
      PersonnelCardModel personnelCardModel = new PersonnelCardModel
      {
        Name = name,
        Role = role,
        ImageFileName = imageFileName,
        TwitterLink= twitterLink,
        LinkedIn= linkedIn,
        Email=email
      };

      return View("Default", personnelCardModel);
    }
  }

}

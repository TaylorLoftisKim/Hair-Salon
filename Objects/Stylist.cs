using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Salon.Objects
{
  public class Stylist
  {
    private int _id;
    public string _name;
    public string _details;
    public int _clientId;
  }

  public Stylist(string stylistName, string stylistDetails, int stylistClientId, int id = 0)
  {
    _name = stylistName;
    _details = stylistDetails;
    _clientId = stylistClientId;
    _id = id;
  }

  public override bool Equals(System.Object otherStylist)
  {
    if(!(otherStylist is Stylist))
    {
      return false;
    }
    else
    {
      Stylist newStylist = (Stylist) otherStylist;
    }
  }
}

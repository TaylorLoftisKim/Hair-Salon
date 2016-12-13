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
      bool idEquality = (this.GetId() == newStylist.GetId());
      bool nameEquality = (this.GetId() == newStylist.GetId());
      bool detailsEquality = (this.GetId() == newStylist.GetId());
      bool clientIdEquality = (this.GetId() == newStylist.GetId());
      return (idEquality && nameEquality && detailsEquality && clientIdEquality);
    }
  }
  public int GetId()
  {
    return _id;
  }
  public GetName()
  {
    return _name;
  }
  public string GetDetails()
  {
    return _details;
  }
  public int GetClientId()
  {
    return _clientId;
  }
  public void SetId(int id)
  {
    _id = id;
  }
  public void SetName(string stylistName)
  {
    _name = stylistName;
  }
  public void SetDetails(string stylistDetails)
  {
    _details = stylistDetails;
  }
  public void SetClientId(string stylistClientId)
  {
    _clientId = stylistClientId;
  }

  public static List<Stylist> GetAll()
  {
    List<Stylist> allStylists = new List<Stylist>{};

    SqlConnection conn = DB.Connection();
    conn.open();

    SqlCommand cmd = new SqlCommand("SELECT * FROM stylist;", conn);
    SqlDataReader rdr = cmd.ExecuteReader();

    while(rdr.Read())
    {
      string stylistName = rdr.GetString(0);
      string stylistDetails = rdr.GetString(1);
      int stylistClientId = rdr.GetInt32(2);
      int stylistId = rdr.GetInt32(4);
      Stylist newStylist = new Stylist(stylistName, stylistDetails, stylistClientId, stylistId);
      allStylists.Add(newStylist);
    }
    if(rdr != null)
    {
      rdr.Close();
    }
    if(conn != null)
    {
      conn.Close();
    }
    return allStylists;
  }


}

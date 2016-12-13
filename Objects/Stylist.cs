using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Salon.Objects
{
  public class Stylist
  {
    private int _id;
    private string _name;
    private string _details;
    private int _clientId;

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
      int stylistId = rdr.GetInt32(1);
      string stylistDetails = rdr.GetString(2);
      int stylistClientId = rdr.GetInt32(3);
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
  public void Save()
		{
			SqlConnection conn = DB.Connection();
			conn.Open();

			SqlCommand cmd = new SqlCommand("INSERT INTO stylist (name, details, stylistId) OUTPUT INSERTED.id VALUES (@stylistName, @stylistDetails, @stylistClientId);", conn);

			SqlParameter nameParameter = new SqlParameter();
			nameParameter.ParameterName = "@stylistName";
			nameParameter.Value = this.GetName();

			SqlParameter detailsParameter = new SqlParameter();
			detailsParameter.ParameterName = "@stylistDetails";
			detailsParameter.Value = this.GetDetails();

			SqlParameter clientIdParameter = new SqlParameter();
			clientIdParameter.ParameterName = "@stylistClientId";
			clientIdParameter.Value = this.GetStylistClientId();

			cmd.Parameters.Add(nameParameter);
			cmd.Parameters.Add(detailsParameter);
			cmd.Parameters.Add(clientIdParameter);

			SqlDataReader rdr = cmd.ExecuteReader();

			while(rdr.Read())
			{
				this._id = rdr.GetInt32(0);
			}
			if (rdr != null)
			{
				rdr.Close();
			}
			if (conn != null)
			{
				conn.Close();
			}
		}
    public static Stylist Find(int id)
		{
			SqlConnection conn = DB.Connection();
			conn.Open();

			SqlCommand cmd = new SqlCommand("SELECT * FROM stylist WHERE id = @StylistId;", conn);
			SqlParameter stylistIdParameter = new SqlParameter();
			stylistIdParameter.ParameterName = "@StylistId";
			stylistIdParameter.Value = id.ToString();
			cmd.Parameters.Add(stylistIdParameter);
			SqlDataReader rdr = cmd.ExecuteReader();

			int foundStylistId = 0;
			string foundStylistName = null;
			string foundStylistDetails = null;
			int foundStylistClientId = 0;
			while(rdr.Read())
			{
				foundStylistId = rdr.GetInt32(0);
				foundStylistName = rdr.GetString(1);
				foundStylistDetails = rdr.GetString(2);
				foundStylistClientId = rdr.GetInt32(3);
			}
			Stylist foundStylist = new Stylist(foundStylistId, foundStylistName, foundStylistDetails, foundStylistClientId);

			if(rdr != null)
			{
				rdr.Close();
			}
			if(conn != null)
			{
				conn.Close();
			}
			return foundStylist;
		}

		public static void DeleteAll()
		{
			SqlConnection conn = DB.Connection();
			conn.Open();
			SqlCommand cmd = new SqlCommand("DELETE FROM stylist;", conn);
			cmd.ExecuteNonQuery();
			conn.Close();
		}
  }
}

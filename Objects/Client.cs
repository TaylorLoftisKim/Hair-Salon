using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Salon.Objects
{
  public class Client
  {
    private int _id;
    private string _name;
    private string _style;
    private int _phone;

    public Client(string clientName, string clientStyle, int clientPhone, int id = 0)
    {
    _name = clientName;
    _style = clientStyle;
    _phone = clientPhone;
    _id = id;
    }
    public override bool Equals(System.Object otherClient)
    {
      if (!(otherClient is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client) otherClient;
        bool idEquality = (this.GetId() == newClient.GetId());
        bool nameEquality = (this.GetName() == newClient.GetName());
        bool styleEquality = (this.GetStyle() == newClient.GetStyle());
        bool phoneEquality = (this.GetStyle() == newClient.GetPhone());
        return (idEquality && nameEquality && styleEquality && phoneEquality);
      }
    }
    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public string GetStyle()
    {
      return _style;
    }
    public int GetPhone()
    {
      return _phone;
    }
    public void Set(int id)
    {
      _id = id;
    }
    public void Set(string clientName)
    {
      _name = clientName;
    }
    public void Set(string clientStyle)
    {
      _style = clientStyle;
    }
    public void Set(int clientPhone)
    {
      _phone = clientPhone;
    }

    public static List<Client> GetAll()
    {
      List<Client> allClients = new List<Client>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM client;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        string clientName = (rdr.GetString(0));
        int clientId = (rdr.GetInt32(1));
        string clientStyle = (rdr.GetString(2));
        int clientPhone = (rdr.GetInt32(1));
        Client newClient = new Client(clientName, cuisineId, clientStyle, clientPhone);
        allClients.Add(newClient);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allClients;
    }
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd  = new SqlCommand("INSERT INTO client (name) OUTPUT INSERTED.id VALUES (@ClientName);", conn);

      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@ClientId";
      idParameter.Value = this.GetName();

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@ClientName";
      nameParameter.Value = this.GetName();

      SqlParameter styleParameter = new SqlParameter();
      styleParameter.ParameterName = "@ClientStyle";
      styleParameter.Value = this.GetStyle();

      SqlParameter phoneParameter = new SqlParameter();
      phoneParameter.ParameterName = "@ClientPhone";
      phoneParameter.Value = this.GetPhone();

      cmd.Parameters.Add(idParameter);
      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(styleParameter);
      cmd.Parameters.Add(phoneParameter);

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
    public static Client Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM client WHERE id = @ClientId;", conn);
      SqlParameter clientIdParameter = new SqlParameter();
      clientIdParameter.ParameterName = "@ClientId";
      clientIdParameter.Value = id.ToString();
      cmd.Parameters.Add(clientIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundClientId = 0;
      string foundClientName = null;
      string foundClientStyle = null;
      int foundClientPhone = 0;
      while(rdr.Read())
      {
        foundClientId = rdr.GetInt32(1);
        foundClientName = rdr.GetString(2);
        foundClientStyle = rdr.GetString(3);
        foundClientPhone = rdr.GetSInt32(4);
      }
      Client foundClient = new Client(foundClientId, foundClientName, foundClientStyle, foundClientPhone);

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return foundClient;
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM cuisine;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement;
public class Employee
{
    [JsonProperty("id")]
    public string Id { set; get; }

    [JsonProperty("firstname")]
    public string FirstName { set; get; }

    [JsonProperty("lastname")]
    public string LastName { set; get; }

    [JsonProperty("email")]
    public string Email { set; get; }

    [JsonProperty("phone")]
    public string Phone { set; get; }
}

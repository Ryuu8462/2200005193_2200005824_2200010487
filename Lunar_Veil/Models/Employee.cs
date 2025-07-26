using System;
using System.Collections.Generic;

namespace Lunar_Veil.Models;

public class Employee
{
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public string Department { get; set; }
    public int Gender { get; set; }
    public string PhotoPath { get; set; } // Add this property to resolve the error  
    public object PhotoImagePath { get; internal set; }
    public object Salary { get; internal set; }
    public object Phone { get; internal set; }
    public object Email { get; internal set; }
}

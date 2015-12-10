using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for clsSTUDENT
/// </summary>
public class clsSTUDENT
{
    public static Student[] listAllStudent()
    {
        dbDataContext db = new dbDataContext();
        Student [] student = (from x in db.Students  select x).ToArray();
        return student;
    }

    public static Student getStudentNumberByUserId(int inputUserID)
    {
        dbDataContext db = new dbDataContext();
        Student student = (from x in db.Students where x.UserID == inputUserID  select x).SingleOrDefault();
        return student;
    }
    public static int getUserIdByStudentNumber(string inputStudentNumber)
    {
        dbDataContext db = new dbDataContext();
        int id = Convert.ToInt32((from x in db.Students where x.StudentNumber == inputStudentNumber select x.UserID).SingleOrDefault());
        return id;
    }
    public static Student [] getAlumniByStudentNumber()
    {
        dbDataContext db = new dbDataContext();
        Student[] student = (from x in db.Students where x.Mezun == true select x).ToArray();
        return student;
    }
}
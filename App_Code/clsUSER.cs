using System;
using System.Linq;


/// <summary>
/// Summary description for clsUSER
/// </summary>
public class clsUSER
{

    public static Message userSave(USER u)
    {
        Message m = new Message();
        try
        {

            dbDataContext db = new dbDataContext();
            db.USERs.InsertOnSubmit(u);
            db.SubmitChanges();



            m.message = "Basariyla kaydedildi";
            m.durum = true;
            //m.id = u.UserID;

        }
        catch (Exception ex)
        {
            m.message = ex.Message;
            m.durum = false;

        }
        return m;
    }

    public static Message userUpdate(USER u)
    {
        Message m = new Message();
        try
        {

            dbDataContext db = new dbDataContext();
            USER u2 = (from x in db.USERs where x.UserID == u.UserID select x).SingleOrDefault();

            u2.UserTypeID = u.UserTypeID;
            u2.DeptID = u.DeptID;
            u2.Durum = u.Durum;
            u2.Email = u.Email;
            u2.Instructors = u.Instructors;
            u2.Name = u.Name;
            u2.Password = u.Password;
            u2.Surname = u.Surname;

            db.SubmitChanges();


            m.message = "Basariyla degistirildi";
            m.durum = true;


        }
        catch (Exception ex)
        {
            m.message = ex.Message;
            m.durum = false;

        }
        return m;
    }

    public static USER[] listAllUser()
    {
        dbDataContext db = new dbDataContext();
        USER[] users = (from x in db.USERs where x.Durum == true select x).ToArray();
        return users;

    }
    public static USER[] getUserById(int id)
    {
        dbDataContext db = new dbDataContext();
        USER[] users = (from x in db.USERs where x.Durum == true && x.UserID == id select x).ToArray();
        return users;
    }
    public static string getNamebyId(int id)
    {
        dbDataContext db = new dbDataContext();
        string name = (from x in db.USERs where x.Durum == true && x.UserID == id select x.Name).ToString();
        return name;
    }

    public static string getSurnamebyId(int id)
    {
        dbDataContext db = new dbDataContext();
        string surname = (from x in db.USERs where x.Durum == true && x.UserID == id select x.Surname).ToString();
        return surname;
    }
    public static string getEmailbyId(int id)
    {
        dbDataContext db = new dbDataContext();
        string mail = (from x in db.USERs where x.Durum == true && x.UserID == id select x.Email).ToString();
        return mail;
    }
    public static int getDeptIdbyUserId(int id)
    {
        dbDataContext db = new dbDataContext();
        int deptID = Convert.ToInt32((from x in db.USERs where x.Durum == true && x.UserID == id select x.DeptID));//burada int donmuyor
        return deptID;
    }
    public static string getUserbyEmail(string email)
    {
        dbDataContext db = new dbDataContext();
        string password = (from x in db.USERs where x.Durum == true && x.Email == email select x.Password).SingleOrDefault().ToString();
        return password;
    }
}




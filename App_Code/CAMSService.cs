using System;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using Newtonsoft.Json;

/// <summary>
/// Summary description for CAMSService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class CAMSService : System.Web.Services.WebService
{

    public CAMSService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void getAllUSer()
    {
        USER [] u = clsUSER.listAllUser();
        ModelUser[] users = new ModelUser[u.Length];
        int i = 0;
        for (; i < u.Length; i++)
        {
            users[i] = new ModelUser();
            users[i].Name = u[i].Name;
            users[i].UserID = u[i].UserID;
            users[i].Surname = u[i].Surname;
        }
        string output = JsonConvert.SerializeObject(users, Formatting.Indented);
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(output);
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void viewDeadline()
    {
        Announcement[] ann = clsANNOUNCEMENT.getAnnouncementsbyFinishDate(DateTime.UtcNow);
        ModelAnnouncement[] retAnn = new ModelAnnouncement[ann.Length];
        int i = 0;
        for (; i < ann.Length; i++)
        {
            retAnn[i] = new ModelAnnouncement();
            retAnn[i].ID = ann[i].ID;
            retAnn[i].Ann_Header = ann[i].Ann_Header;
            retAnn[i].TextFull = ann[i].TextFull;
            retAnn[i].StartDate = ann[i].StartDate;
            retAnn[i].FinishDate = ann[i].FinishDate;
            retAnn[i].Location = ann[i].Location; 
            retAnn[i].CourseID = Convert.ToInt32(ann[i].CourseID);
            retAnn[i].DeptID = Convert.ToInt32(ann[i].DeptID);
            retAnn[i].UserID = ann[i].UserID;
            retAnn[i].CategoryType = ann[i].CategoryType;
            retAnn[i].IsActive = ann[i].IsActive; 
        }
        string output = JsonConvert.SerializeObject(retAnn, Formatting.Indented);
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(output);
    }
    public string Login(string email, string inputpassword)//email and password deserialize edilmedi
    {
        string answer;
        answer = clsUSER.getUserbyEmail(email);
        if (answer != null)
            if (answer == inputpassword)
                answer = "true";
            else
                answer = "false";
        else
            answer = "false";
        var jss = new JavaScriptSerializer();
        string json = jss.Serialize(answer);
        return json;
    }
   
    public void listAllAnnouncements()
    {
        
        Announcement[] ann = clsANNOUNCEMENT.getAllActiveAnnouncements();
        ModelAnnouncement[] retAnn = new ModelAnnouncement[ann.Length];
        int i = 0;
        for (; i < ann.Length; i++)
        {
            retAnn[i] = new ModelAnnouncement();
            retAnn[i].ID = ann[i].ID;
            retAnn[i].Ann_Header = ann[i].Ann_Header;
            retAnn[i].TextFull = ann[i].TextFull;
            retAnn[i].StartDate = ann[i].StartDate;
            retAnn[i].FinishDate = ann[i].FinishDate;
            retAnn[i].Location = ann[i].Location;
            retAnn[i].CourseID = Convert.ToInt32(ann[i].CourseID);
            retAnn[i].DeptID = Convert.ToInt32(ann[i].DeptID);
            retAnn[i].UserID = ann[i].UserID;
            retAnn[i].CategoryType = ann[i].CategoryType;
            retAnn[i].IsActive = ann[i].IsActive;
        }
        string output = JsonConvert.SerializeObject(retAnn, Formatting.Indented);
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(output);
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void publishAnnouncement(string json)
    {
        Announcement ann = JsonConvert.DeserializeObject<Announcement>(json);
        Message mes = clsANNOUNCEMENT.addAnnouncement(ann);
        string message = JsonConvert.SerializeObject(mes); //serialize etme
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(message);
    }
    

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void deleteAnnouncement(Announcement ann)
    {

        Message m = new Message();

        dbDataContext db = new dbDataContext();
        Announcement a = (from x in db.Announcements where x.ID == ann.ID select x).SingleOrDefault();

        a.CategoryType = ann.CategoryType;
        a.DeptID = ann.DeptID;
        a.Location = ann.Location;
        a.UserID = ann.UserID;
        a.CourseID = ann.CourseID;
        a.FinishDate = ann.FinishDate;
        a.StartDate = ann.StartDate;
        a.TextFull = ann.TextFull;
        a.Ann_Header = ann.Ann_Header;

        a.IsActive = true;

        db.SubmitChanges();


        m.message = "Duyuru başarıyla silindi.";
        m.durum = true;
        string output = JsonConvert.SerializeObject(m, Formatting.Indented);
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(output);

    }


    public void addAnnouncementToCalendar(string jsonID)
    {
        int [] ID = JsonConvert.DeserializeObject<int[]>(jsonID);
        dbDataContext db = new dbDataContext();
        //YARIM KALMIŞ
    }

    public void deleteAnnouncementFromCalendar(string jsonID)
    {
        int[] ID = JsonConvert.DeserializeObject<int[]>(jsonID);
        dbDataContext db = new dbDataContext();
        //YARIM KALMIŞ
    }

    //public static Message editAnnouncement(string ann, string jsonfield, string jsonfieldvalue)
    //{
    //    Message m = new Message();
    //    Announcement a = JsonConvert.DeserializeObject<Announcement>(ann);
    //    dbDataContext db = new dbDataContext();
    //    Announcement b = (from x in db.Announcements where x.ID == a.ID select x).SingleOrDefault();
    //    string field = JsonConvert.DeserializeObject<string>(jsonfield);
    //    string fieldvalue = JsonConvert.DeserializeObject<string>(jsonfieldvalue);

    //    switch (field)
    //    {
    //        case "Header": b.Ann_Header = fieldvalue;
    //            break;
    //        case "FullText": b.TextFull = fieldvalue;
    //            break;
    //        case "StartDate": b.StartDate = fieldvalue;
    //            break;
    //        case "FinishDate": b.FinishDate = fieldvalue;
    //            break;
    //        case "Location": b.Location = fieldvalue;
    //            break;
    //        case "CourseID": b.CourseID = fieldvalue;
    //            break;
    //        case "DeptID": b.DeptID = fieldvalue;
    //            break;
    //        case "CategoryType": b.CategoryType = fieldvalue;
    //            break;
    //        default:
    //            break;
    //    }

    //    db.SubmitChanges();


    //    m.message = "Basariyla degistirildi";
    //    m.durum = true;
    //}

    //public string respondAnnouncement(int announcementID)
    //{ }
    //public string searchThread(string name, string creator, string category, string keyword)
    //{ }
    //public string sendMessage(int senderID, int receiverID)
    //{ }
}
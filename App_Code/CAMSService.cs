using System;
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
public class CAMSService : System.Web.Services.WebService {

    public CAMSService () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true,ResponseFormat = ResponseFormat.Json)] 
    public void getAllUSer() {
        USER[] u = clsUSER.listAllUser();
        Message [] msg = new Message[u.Length];
        int i = 0;
        for(;i<u.Length;i++)
        {
            msg[i] = new Message();
            msg[i].username = u[i].Name;
            msg[i].userID = u[i].UserID;
            msg[i].surname = u[i].Surname;
        }
        string output = JsonConvert.SerializeObject(msg, Formatting.Indented);
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(output);
    }          

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string viewDeadlinebyCategory()
    {
        Announcement [] ann =clsANNOUNCEMENT.getAnnouncementsbyFinishDate(DateTime.UtcNow);
        var jss = new JavaScriptSerializer();
        string json = jss.Serialize(ann);
        return json;

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
    public string searchAnnouncementByCategoryType(string categorytype)
    {
        int type = JsonConvert.DeserializeObject<int>(categorytype);
        Announcement[] a = clsANNOUNCEMENT.getAnnouncementbyCategoryType(type);
        var jss = new JavaScriptSerializer();
        string json = jss.Serialize(a);
        return json;
    }
    public string searchAnnouncementbyCreatorID(int creatorid)
    {
        Announcement[] a = clsANNOUNCEMENT.getAnnouncementsbyUserID(creatorid);
        var jss = new JavaScriptSerializer();
        string json = jss.Serialize(a);
        return json;

    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string publishAnnouncemennt(string json)
    {
        Announcement ann = JsonConvert.DeserializeObject < Announcement > (json);
        Message mes=clsANNOUNCEMENT.addAnnouncement(ann);
        string message = JsonConvert.SerializeObject(mes);
        return message;
    }
    //public string respondAnnouncement(int announcementID)
    //{ }
    //public string searchThread(string name, string creator, string category, string keyword)
    //{ }
    //public string sendMessage(int senderID, int receiverID)
    //{ }


}

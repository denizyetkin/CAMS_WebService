using System;
using System.Linq;


public class clsANNOUNCEMENT
{
    public static Announcement [] getAllActiveAnnouncements()
    {
        dbDataContext db = new dbDataContext();
        Announcement[] ann = (from x in db.Announcements where x.IsActive == true select x).ToArray();
        return ann;
    }

    public static Announcement[] getAnnouncementbyCategoryType(int category)
    {
        dbDataContext db = new dbDataContext();
        Announcement[] ann = (from x in db.Announcements where x.IsActive == true && x.CategoryType == category select x).ToArray();
        return ann;
    }

    public static Announcement getAnnouncementbyAnnouncementID(int AnnID)
    {
        dbDataContext db = new dbDataContext();
        Announcement ann = (from x in db.Announcements where x.IsActive == true && x.ID == AnnID select x).SingleOrDefault();
        return ann;
    }

    public static Announcement[] getAnnouncementbyCourseID(int inputCourseID)
    {
        dbDataContext db = new dbDataContext();
        Announcement[] ann = (from x in db.Announcements where x.IsActive == true && x.CourseID == inputCourseID select x).ToArray();
        return ann;
    }
    public static Announcement[] getAnnouncementsbyStartDate(DateTime inputStartDate)
    {
        dbDataContext db = new dbDataContext();
        Announcement[] ann = (from x in db.Announcements where x.IsActive == true && x.StartDate == inputStartDate select x).ToArray();
        return ann;
    }
    public static Announcement[] getAnnouncementsbyFinishDate(DateTime inputFinishDate)
    {
        dbDataContext db = new dbDataContext();
        Announcement[] ann = (from x in db.Announcements where x.IsActive == true && x.FinishDate == inputFinishDate select x).ToArray();
        return ann;
    }

    public static Announcement[] getAnnouncementsbyDateSubtraction(DateTime inputStartDate, DateTime inputFinishDate)
    {
        dbDataContext db = new dbDataContext();
        Announcement[] ann = (from x in db.Announcements where x.IsActive == true && x.FinishDate <= inputFinishDate && x.StartDate >= inputStartDate select x).ToArray();
        return ann;
    }

    public static Announcement[] getAnnouncementsbyDepartmentID(int inputCourseID)
    {
        dbDataContext db = new dbDataContext();
        Announcement[] ann = (from x in db.Announcements where x.IsActive == true && x.CourseID == inputCourseID select x).ToArray();
        return ann;
    }
    public static Announcement[] getAnnouncementsbyUserID(int inputUserID)
    {
        dbDataContext db = new dbDataContext();
        Announcement[] ann = (from x in db.Announcements where x.IsActive == true && x.UserID == inputUserID select x).ToArray();
        return ann;
    }
    public static Announcement[] getAnnouncementsbyLocation(string inputLocation)
    {
        dbDataContext db = new dbDataContext();
        Announcement[] ann = (from x in db.Announcements where x.IsActive == true && x.Location == inputLocation select x).ToArray();
        return ann;
    }
    public static Message addAnnouncement(Announcement ann)
    {
        Message m = new Message();
        try
        {

            dbDataContext db = new dbDataContext();
            db.Announcements.InsertOnSubmit(ann);
            db.SubmitChanges();

            m.message = "Basariyla kaydedildi";
            m.durum = true;
            //m.id = ann.ID;
        }
        catch (Exception ex)
        {
            m.message = ex.Message;
            m.durum = false;

        }
        return m;
    }
}
using System.Security.Claims;

namespace Agape316.Data;

public partial class AgapeDocument
{        
    public AgapeDocument()
    {}    
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string? Notes { get; set; }
    public string Author { get; set; }
    public string FileType { get; set; }
    public DateTime Created { get; set; }
    public string UserName
    {
        get
        {
            string userName = string.Empty;
            if (Agape316.Helpers.AppContext.Current != null)
            {
                userName = Agape316.Helpers.AppContext.Current.Session.GetString("UserName");
            }            
            return userName;
        }
    }   
}

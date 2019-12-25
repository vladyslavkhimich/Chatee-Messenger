using System.Collections.ObjectModel;
using System.Runtime.Serialization;
// Use a data contract as illustrated in the sample below to add composite types to service operations.
[DataContract]
public class UserContract
{
    #region Public Properties
    [DataMember]
    public int UserID { get; set; }
    [DataMember]
    public string Username { get; set; }
    [DataMember]
    public string Email { get; set; }
    [DataMember]
    public string Name { get; set; }
    [DataMember]
    public string Bio { get; set; }
    [DataMember]
    public string Initials { get; set; }
    [DataMember]
    public string ProfilePictureRGB { get; set; }
    [DataMember]
    public string LastMessage { get; set; }
    [DataMember]
    public ObservableCollection<ChatContract> Chats { get; set; }   
}


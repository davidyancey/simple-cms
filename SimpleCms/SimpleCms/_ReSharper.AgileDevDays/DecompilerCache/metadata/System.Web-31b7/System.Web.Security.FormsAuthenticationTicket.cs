// Type: System.Web.Security.FormsAuthenticationTicket
// Assembly: System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\System.Web.dll

using System;
using System.Runtime;

namespace System.Web.Security
{
    [Serializable]
    public sealed class FormsAuthenticationTicket
    {
        public FormsAuthenticationTicket(int version, string name, DateTime issueDate, DateTime expiration,
                                         bool isPersistent, string userData);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public FormsAuthenticationTicket(int version, string name, DateTime issueDate, DateTime expiration,
                                         bool isPersistent, string userData, string cookiePath);

        public FormsAuthenticationTicket(string name, bool isPersistent, int timeout);

        public int Version { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public string Name { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public DateTime Expiration { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public DateTime IssueDate { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public bool IsPersistent { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public bool Expired { get; }

        public string UserData { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public string CookiePath { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }
    }
}

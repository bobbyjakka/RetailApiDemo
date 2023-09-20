using System;
using System.ComponentModel;

namespace DotnetCoding.Core.Constants
{
    public enum ProductStatus
    {
        [Description("Active")] Active,
        [Description("Inactive")] Inactive,
        [Description("PendingApproval")] PendingApproval
    }

    public enum RequestType
    {
        [Description("Create")] Create,
        [Description("Update")] Update,
        [Description("Delete")] Delete
    }
}


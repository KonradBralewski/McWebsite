using Microsoft.EntityFrameworkCore;

namespace McWebsite.Infrastructure.Exceptions
{
    public static class ExceptionsList
    {
        public static McWebsiteException ThrowCreationException => 
             new McWebsiteException(new DbUpdateException(), ErrorOr.Error.Unexpected("Unit creation failed."));

    }
}

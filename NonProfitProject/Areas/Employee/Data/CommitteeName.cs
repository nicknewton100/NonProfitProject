using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NonProfitProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitProject.Areas.Employee.Data
{
    public class CommitteeName
    {
        public static string Get(NonProfitContext context, string id, HttpContext httpContext)
        {
            var committeeMember = context.CommitteeMembers.Include(cm => cm.committee).Where(cm => cm.employee.UserID == id).FirstOrDefault();
            if(committeeMember == null)
            {
                return "";
            }
            else
            {
                return committeeMember.committee.CommitteeName;
            }
        }
    }
}

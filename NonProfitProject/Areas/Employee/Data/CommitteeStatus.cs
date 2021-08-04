using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NonProfitProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitProject.Areas.Employee.Data
{
    public class CommitteeStatus
    {
        //gets the name of the committee fo the logged in employee
        public static string GetName(NonProfitContext context, string id)
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
        //gets the psotion f the currently logged in employee
        public static string GetPosition(NonProfitContext context, string id)
        {
            var member = context.CommitteeMembers.Where(cm => cm.employee.UserID == id).FirstOrDefault();
            if(member == null)
            {
                return "";
            }
            else
            {
                return member.CommitteePosition;
            }
        }
    }
}

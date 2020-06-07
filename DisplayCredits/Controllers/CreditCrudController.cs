using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Management;
using DisplayCredits.Models;

namespace DisplayCredits.Controllers
{
    public class CreditCrudController : ApiController
    {
        loansEntities le = new loansEntities();
        public IHttpActionResult getCreditCrud()
        {
            IList<branch> branchList = le.branches.ToList();
            IList<client> clientList = le.clients.ToList();
            IList<credit> creditList = le.credits.ToList();
            IList<diary> diaryList = le.diaries.ToList();
            IList<parameter> parameterList = le.parameters.ToList();

            IEnumerable<JoinClass> results = null;

            results = from crd in creditList
                      join clnt in clientList on crd.client_no equals clnt.client_no into table1
                      from clnt in table1.DefaultIfEmpty()
                      join brnch in branchList on crd.branch_code equals brnch.branch_code into table2
                      from brnch in table2.DefaultIfEmpty()
                      join prmtr in parameterList on crd.status equals prmtr.value into table3
                      from prmtr in table3.DefaultIfEmpty()
                      select new JoinClass { GetCredit = crd, GetClient = clnt, GetBranch = brnch, GetParameter = prmtr };

            return Ok(results);
        }

        [HttpPost]
        public IHttpActionResult creditInsert(credit creditInsert)
        {
            le.credits.Add(creditInsert);
            le.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult Put(credit ct)
        {
            var updateCrdt = le.credits.Where(x => x.contract_no == ct.contract_no).FirstOrDefault<credit>();
            if (updateCrdt != null)
            {
                updateCrdt.contract_no = ct.contract_no;
                updateCrdt.client_no = ct.client_no;
                updateCrdt.branch_code = ct.branch_code;
                updateCrdt.opening_amount = ct.opening_amount;
                updateCrdt.currency = ct.currency;
                updateCrdt.status = ct.status;
                updateCrdt.start_date = ct.start_date;
                updateCrdt.maturity_date = ct.maturity_date;
                le.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            var creditDel = le.credits.Where(x => x.contract_no == id).FirstOrDefault();
            le.Entry(creditDel).State = System.Data.Entity.EntityState.Deleted;
            le.SaveChanges();
            return Ok();
        }

        ////public IHttpActionResult GetCrdId(int id)
        ////{
        ////    diary diaryDetails = null;
        ////    diaryDetails = le.diaries.Where(x => x.contract_no == id).Select(x => new diary()
        ////    {
        ////        contract_no = x.contract_no,
        ////        interest_rate = x.interest_rate,
        ////        interest_amount = x.interest_amount,
        ////        principal_amount = x.principal_amount,
        ////        installment_date = x.installment_date
        ////    }).FirstOrDefault<diary>();

        ////    if (diaryDetails==null)
        ////    {
        ////        return NotFound();
        ////    }
        ////    return Ok(diaryDetails);

        ////}

    }
}

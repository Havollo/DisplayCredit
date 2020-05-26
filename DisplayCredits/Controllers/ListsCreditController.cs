using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DisplayCredits.Models;

namespace DisplayCredits.Controllers
{
    public class ListsCreditController : ApiController
    {
        public IHttpActionResult getCredit(int clntNo, int authId)
        {
            loansEntities nd = new loansEntities();

            IList<branch> branchList = nd.branches.ToList();
            IList<client> clientList = nd.clients.ToList();
            IList<credit> creditList = nd.credits.ToList();
            IList<diary> diaryList = nd.diaries.ToList();
            IList<parameter> parameterList = nd.parameters.ToList();

            IEnumerable<JoinClass> query = null;

            if (authId == 2)
            {
                query = from crd in creditList
                        join clnt in clientList on crd.client_no equals clnt.client_no into table1
                        from clnt in table1.DefaultIfEmpty()
                        join brnch in branchList on crd.branch_code equals brnch.branch_code into table2
                        from brnch in table2.DefaultIfEmpty()
                        join prmtr in parameterList on crd.status equals prmtr.value into table3
                        from prmtr in table3.DefaultIfEmpty()
                        select new JoinClass { GetCredit = crd, GetClient = clnt, GetBranch = brnch, GetParameter = prmtr };
            }
            else
            {
                query = from crd in creditList
                        where crd.client_no == clntNo
                        join clnt in clientList on crd.client_no equals clnt.client_no into table1
                        from clnt in table1.DefaultIfEmpty()
                        join brnch in branchList on crd.branch_code equals brnch.branch_code into table2
                        from brnch in table2.DefaultIfEmpty()
                        join prmtr in parameterList on crd.status equals prmtr.value into table3
                        from prmtr in table3.DefaultIfEmpty()
                        select new JoinClass { GetCredit = crd, GetClient = clnt, GetBranch = brnch, GetParameter = prmtr };
            }


            return Ok(query);


                        //IList<Credit> crdObj = nd.credits.Include("credit").Select(x => new Credit()
                        //{
                        //    ContractNo = x.contract_no,
                        //    ClientNo = x.client_no,
                        //    BranchCode = x.branch_code,
                        //    OpeningAmount = x.opening_amount,
                        //    Currency = x.currency,
                        //    Status = x.status,
                        //    StartDate = x.start_date,
                        //    MaturityDate = x.maturity_date
                        //}).ToList<Credit>();
                        //return Ok(crdObj);
        }
    }
}

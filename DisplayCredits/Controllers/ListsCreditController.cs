using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using DisplayCredits.Models;

namespace DisplayCredits.Controllers
{
    public class ListsCreditController : ApiController
    {
        loansEntities le = new loansEntities();

        public IHttpActionResult getClients()
        {
            IList<branch> branchList = le.branches.ToList();
            IList<SelectListItem> clientList = le.clients.Where(x => x.authoriyId == 1)
                .Select(x => new SelectListItem
                {
                    Value = x.client_no.ToString(),
                    Text = x.name + " " + x.surname
                }).ToList();

            CreditViewModel creditModel = new CreditViewModel
            {
                Clients = clientList,
                Branches = branchList,
                Currencies = new List<string>
                {
                    "TL",
                    "USD",
                    "EUR"
                }
            };

            return Ok(creditModel);
        }

        public IHttpActionResult getCredit(int clntNo, int authId)
        {


            IList<branch> branchList = le.branches.ToList();
            IList<client> clientList = le.clients.ToList();
            IList<credit> creditList = le.credits.ToList();
            IList<diary> diaryList = le.diaries.ToList();
            IList<parameter> parameterList = le.parameters.ToList();

            IEnumerable<JoinClass> query = null;

            if (authId == 2 || authId == 3)
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

        }

        [System.Web.Mvc.HttpPost]
        public IHttpActionResult creditInsert(credit creditInsert)
        {
            le.credits.Add(creditInsert);
            le.SaveChanges();
            return Ok();
        }

        public IHttpActionResult Put(credit ct)
        {
            var updateCrdt = le.credits.Where(x => x.contract_no == ct.contract_no).FirstOrDefault<credit>();
            if (updateCrdt != null)
            {
                //updateCrdt.contract_no = ct.ContractNo;
                //updateCrdt.client_no = ct.ClientNo;
                //updateCrdt.branch_code = ct.BranchCode;
                //updateCrdt.opening_amount = ct.OpeningAmount;
                //updateCrdt.currency = ct.Currency;
                //updateCrdt.status = ct.Status;
                //updateCrdt.start_date = ct.StartDate;
                //updateCrdt.maturity_date = ct.MaturityDate;
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
        public IHttpActionResult getSelectedCredit(int creditId)
        {
            IList<branch> branchList = le.branches.ToList();
            IList<client> clientList = le.clients.ToList();
            IList<credit> creditList = le.credits.ToList();
            IList<parameter> parameterList = le.parameters.ToList();

            credit credit = creditList.Where(x => x.contract_no == creditId)
                .FirstOrDefault();

            client client = clientList.Where(x => x.client_no == credit.client_no)
                .FirstOrDefault();

            CreditViewModel creditModel = new CreditViewModel
            {
                Branches = branchList,
                Parameters = parameterList,
                Credit = credit,
                Client = client,
                Currencies = new List<string>
                {
                    "TL",
                    "USD",
                    "EUR"
                }
            };


            return Ok(creditModel);
        }
    }
}

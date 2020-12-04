using refactor_this.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace refactor_this.Controllers
{
    public class AccountController : ApiController
    {
        [HttpGet, Route("api/Accounts/{id}")]
        public IHttpActionResult GetById(Guid id)
        {
            using (var connection = Helpers.NewConnection())
            {
                var account = new Account();
                return Ok(account.Get(id));
            }
        }

        [HttpGet, Route("api/Accounts")]
        public IHttpActionResult Get()
        {
            var accountList = new Account();
            return Ok(accountList.GetAccounts());
        }


        [HttpPost, Route("api/Accounts")]
        public IHttpActionResult Add(Account account)
        {
            account.Save();
            return Ok();
        }

        [HttpPut, Route("api/Accounts/{id}")]
        public IHttpActionResult Update(Guid id, Account account)
        {
            var existing = account.Get(id);
            existing.Name = account.Name;
            existing.Save();
            return Ok();
        }

        [HttpDelete, Route("api/Accounts/{id}")]
        public IHttpActionResult Delete(Guid id)
        {
            var account = new Account();
            account.Delete(id);
            return Ok();
        }
    }
}
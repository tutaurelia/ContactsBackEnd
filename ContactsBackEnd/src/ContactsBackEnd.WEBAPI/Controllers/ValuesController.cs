using System.Collections.Generic;
using ContactsBackEnd.DATA.Entities;
using ContactsBackEnd.DATA.Repositories;
using Microsoft.AspNet.Mvc;

namespace ContactsBackEnd.WEBAPI.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private IContactsRepository _repo;

        public ValuesController(IContactsRepository repo)
        {
            _repo = repo;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Contact> Get()
        {
            return _repo.GetAllContacts("bart", 0, 10);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
